using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;

namespace BLogicPOS7SupportTool.Core
{
    // Reads worksheet rows out of an .xlsx without any third-party library. An .xlsx is
    // a zip of XML parts, and only a small slice is needed here: the sheet list, the
    // shared string table and the cell values of one sheet.
    //
    // Cached values are read, not formulas - Excel stores the last calculated result of
    // every formula cell, which is exactly what the operator saw when they saved.
    static class XlsxReader
    {
        private static readonly XNamespace Main =
            "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        private static readonly XNamespace Rel =
            "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        private static readonly XNamespace PackageRel =
            "http://schemas.openxmlformats.org/package/2006/relationships";

        // Row 1 is the header; every later row comes back keyed by header name. Rows
        // whose keyColumn is blank are skipped - templates carry hundreds of buffer
        // rows whose formulas evaluate to "" and those are not data.
        public static List<Dictionary<string, string>> ReadSheet(string path, string sheetName, string keyColumn)
        {
            var rows = new List<Dictionary<string, string>>();

            using (var zip = ZipFile.OpenRead(path))
            {
                string sheetPart = FindSheetPart(zip, sheetName);
                if (sheetPart == null)
                    throw new InvalidDataException($"The workbook has no sheet named \"{sheetName}\".");

                var shared = ReadSharedStrings(zip);
                var sheet = LoadXml(zip, sheetPart);

                var header = new Dictionary<string, string>();   // column letters -> header text
                foreach (var row in sheet.Descendants(Main + "row"))
                {
                    bool isHeader = header.Count == 0;
                    var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                    foreach (var cell in row.Elements(Main + "c"))
                    {
                        string column = ColumnOf((string)cell.Attribute("r"));
                        string text = CellValue(cell, shared);

                        if (isHeader)
                        {
                            if (!string.IsNullOrWhiteSpace(text)) header[column] = text.Trim();
                        }
                        else
                        {
                            string name;
                            if (header.TryGetValue(column, out name)) values[name] = text;
                        }
                    }

                    if (isHeader) continue;

                    string key;
                    if (!values.TryGetValue(keyColumn, out key) || string.IsNullOrWhiteSpace(key)) continue;
                    rows.Add(values);
                }
            }

            return rows;
        }

        // Sheet names live in workbook.xml, the part they point at in its .rels file.
        private static string FindSheetPart(ZipArchive zip, string sheetName)
        {
            var workbook = LoadXml(zip, "xl/workbook.xml");
            if (workbook == null) return null;

            var sheet = workbook.Descendants(Main + "sheet").FirstOrDefault(s =>
                string.Equals((string)s.Attribute("name"), sheetName, StringComparison.OrdinalIgnoreCase));
            if (sheet == null) return null;

            string id = (string)sheet.Attribute(Rel + "id");
            var rels = LoadXml(zip, "xl/_rels/workbook.xml.rels");
            if (rels == null) return null;

            var target = rels.Descendants(PackageRel + "Relationship")
                .FirstOrDefault(r => (string)r.Attribute("Id") == id);
            if (target == null) return null;

            string value = ((string)target.Attribute("Target") ?? "").TrimStart('/');
            return value.StartsWith("xl/", StringComparison.OrdinalIgnoreCase) ? value : "xl/" + value;
        }

        public static List<string> SheetNames(string path)
        {
            using (var zip = ZipFile.OpenRead(path))
            {
                var workbook = LoadXml(zip, "xl/workbook.xml");
                if (workbook == null) return new List<string>();
                return workbook.Descendants(Main + "sheet")
                    .Select(s => (string)s.Attribute("name")).ToList();
            }
        }

        private static List<string> ReadSharedStrings(ZipArchive zip)
        {
            var list = new List<string>();
            var doc = LoadXml(zip, "xl/sharedStrings.xml");
            if (doc == null) return list;

            // A shared string is either one <t>, or several runs each with their own <t>.
            foreach (var si in doc.Descendants(Main + "si"))
                list.Add(string.Concat(si.Descendants(Main + "t").Select(t => t.Value)));
            return list;
        }

        private static string CellValue(XElement cell, List<string> shared)
        {
            string type = (string)cell.Attribute("t");

            if (type == "inlineStr")
                return string.Concat(cell.Descendants(Main + "t").Select(t => t.Value));

            var v = cell.Element(Main + "v");
            if (v == null) return "";

            if (type == "s")
            {
                int index;
                if (int.TryParse(v.Value, out index) && index >= 0 && index < shared.Count)
                    return shared[index];
                return "";
            }

            return v.Value;   // number, date serial, boolean or a formula's string result
        }

        // "BC12" -> "BC"
        private static string ColumnOf(string reference)
        {
            if (string.IsNullOrEmpty(reference)) return "";
            int i = 0;
            while (i < reference.Length && !char.IsDigit(reference[i])) i++;
            return reference.Substring(0, i);
        }

        private static XDocument LoadXml(ZipArchive zip, string partName)
        {
            var entry = zip.Entries.FirstOrDefault(e =>
                string.Equals(e.FullName, partName, StringComparison.OrdinalIgnoreCase));
            if (entry == null) return null;

            using (var stream = entry.Open())
                return XDocument.Load(stream);
        }
    }
}
