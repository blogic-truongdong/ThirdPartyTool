# BLogicPOS7SupportTool — Project Guide

## Project Overview

WinForms desktop support tool for BLogicPOS7. Used by support staff to view and edit invoice (receipt) data, manage employee information, and perform advanced POS operations.

- **Platform**: .NET Framework 4.7.2, C# 7.3, Windows Forms
- **Database**: SQL Server (via ADO.NET — `SqlCommand`, `SqlDataReader`)
- **Architecture**: Single-process WinForms, no MVVM — plain code-behind with service classes

---

## Tech Stack

| Concern | Library |
|---|---|
| Data access | ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataReader`) |
| JSON | Newtonsoft.Json 13.0.4 |
| UI controls | Default WinForms controls only (no third-party UI libs for the dashboard) |

> The existing `RJCodeAdvance.RJControls` package is used only in LoginForm. The dashboard and all subsequent forms use **standard WinForms controls styled manually** — no third-party UI libraries.

---

## Dashboard Design System

Reference screenshot: `Resources/Dashboard.png`

The design language is **minimal, modern, professional** — inspired by Material Design-like sidebar navigation.

### Layout

```
┌──────────────────────────────────────────────────────┐
│  BLogicPOS7 Support Tool                    [X EXIT] │  ← Title bar (height: 40px)
├─────────────────┬────────────────────────────────────┤
│  OPERATIONS     │                                    │
│  ─────────────  │                                    │
│  🏠 Home       │       Content Panel                │
│  🧾 Receipts   │       (white background)           │
│  📦 Items      │                                    │
│  👤 Employees  │                                    │
│  ⚙  Batch Mgr  │                                    │
│  💳 Transact.  │                                    │
│  🔧 Advanced   │                                    │
│                 │                                    │
└─────────────────┴────────────────────────────────────┘
    240px wide                fill remaining
```

### Color Palette

```csharp
// Define as static constants in a Theme.cs file
static class Theme
{
    // Colors
    public static readonly Color PrimaryBlue   = Color.FromArgb(11, 87, 208);   // #0B57D0 — active nav item
    public static readonly Color SidebarBg     = Color.FromArgb(240, 240, 240); // #F0F0F0 — sidebar background
    public static readonly Color ContentBg     = Color.White;                   // #FFFFFF — content area
    public static readonly Color TitleBarBg    = Color.White;
    public static readonly Color NavHover      = Color.FromArgb(220, 230, 248); // light blue tint on hover
    public static readonly Color NavText       = Color.FromArgb(50, 50, 50);
    public static readonly Color NavTextActive = Color.White;
    public static readonly Color SectionLabel  = Color.FromArgb(100, 100, 100); // "OPERATIONS" label
    public static readonly Color BorderLine    = Color.FromArgb(210, 210, 210);

    // Typography — Roboto loaded via PrivateFontCollection
    public static FontFamily FontFamily { get; private set; }

    static Theme()
    {
        // Load Roboto from embedded resource; fallback to Segoe UI if unavailable
        try
        {
            var pfc = new System.Drawing.Text.PrivateFontCollection();
            pfc.AddFontFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Fonts", "Roboto-Regular.ttf"));
            FontFamily = pfc.Families[0];
        }
        catch
        {
            FontFamily = new FontFamily("Segoe UI");
        }
    }

    public static Font Font(float size, FontStyle style = FontStyle.Regular)
        => new Font(FontFamily, size, style);
}
```

### Typography

- **Base font**: `Roboto, 12pt` — applied at the Form level so all child controls inherit it
- **Nav items**: `Roboto, 12pt, Regular`
- **Section header** ("OPERATIONS"): `Roboto, 9pt, Bold` — uppercase, muted color
- **Page titles**: `Roboto, 16pt, Regular`
- **Table/data text**: `Roboto, 12pt`

> Roboto is not bundled with Windows. Either embed the font via `PrivateFontCollection` in `Theme.cs`, or install it system-wide. Set it on the root Form: `this.Font = new Font(Theme.FontFamily, 12f);` so every child control inherits it automatically.

### Navigation Item States

| State | BackColor | ForeColor | Cursor |
|---|---|---|---|
| Normal | `SidebarBg` | `NavText` | Default |
| Hover | `NavHover` | `NavText` | Hand |
| Active | `PrimaryBlue` | `White` | Default |

Implement nav items as `Panel` controls hosting a `PictureBox` (icon) and a `Label` (text). Handle `MouseEnter`, `MouseLeave`, and `Click` events on the parent Panel.

---

## Project Structure

```
BLogicPOS7SupportTool/
├── CLAUDE.md
├── Program.cs                  # Entry point → LoginForm
├── App.config
│
├── Core/                       # Shared infrastructure
│   ├── Theme.cs                # Color/font constants (see above)
│   ├── Database.cs             # SqlConnection factory (reads conn string from App.config)
│   └── SessionContext.cs       # Logged-in user state (static singleton)
│
├── Login/
│   ├── LoginForm.cs
│   └── LoginForm.Designer.cs
│
├── Dashboard/
│   ├── MainForm.cs             # Shell: title bar + sidebar + content panel swap
│   ├── MainForm.Designer.cs
│   └── Pages/                  # UserControls loaded into the content panel
│       ├── HomePage.cs
│       ├── ReceiptsPage.cs
│       ├── ItemsPage.cs
│       ├── EmployeesPage.cs
│       ├── BatchManagerPage.cs
│       ├── TransactionsPage.cs
│       └── AdvancedFunctionsPage.cs
│
├── Services/                   # Data access — thin wrappers around Dapper queries
│   ├── ReceiptService.cs
│   ├── ItemService.cs
│   └── EmployeeService.cs
│
├── Models/                     # Plain C# model classes (no attributes needed for Dapper)
│   ├── Receipt.cs
│   ├── Item.cs
│   └── Employee.cs
│
└── Resources/
    ├── Dashboard.png
    └── Icons/                  # 24x24 PNG icons for nav items (grayscale, simple)
```

---

## MainForm Architecture

MainForm is the shell. It never shows its own content — it owns the sidebar and a single `Panel contentPanel` that fills the right side. Switching pages means clearing `contentPanel.Controls` and adding a new `UserControl`.

```csharp
// MainForm.cs — page navigation pattern
private void NavigateTo(UserControl page)
{
    contentPanel.Controls.Clear();
    page.Dock = DockStyle.Fill;
    contentPanel.Controls.Add(page);
}

// Called from nav item click handlers
private void btnHome_Click(object sender, EventArgs e)
{
    SetActiveNavItem(btnHome);
    NavigateTo(new HomePage());
}
```

Keep MainForm thin — no business logic, only layout and navigation wiring.

---

## Database Access Pattern

Use Dapper with a shared connection factory. Do **not** use Entity Framework.

```csharp
// Core/Database.cs
static class Database
{
    private static readonly string _connectionString =
        ConfigurationManager.ConnectionStrings["POS7DB"].ConnectionString;

    public static SqlConnection Open()
    {
        var conn = new SqlConnection(_connectionString);
        conn.Open();
        return conn;
    }
}

// App.config — add connection string
// <connectionStrings>
//   <add name="POS7DB" connectionString="Server=...;Database=...;Integrated Security=True;" />
// </connectionStrings>

// Services/ReceiptService.cs
class ReceiptService
{
    public IEnumerable<Receipt> GetAll()
    {
        using (var db = Database.Open())
            return db.Query<Receipt>("SELECT * FROM Receipts ORDER BY CreatedAt DESC");
    }

    public Receipt GetById(int id)
    {
        using (var db = Database.Open())
            return db.QuerySingleOrDefault<Receipt>("SELECT * FROM Receipts WHERE Id = @Id", new { Id = id });
    }
}
```

---

## WinForms Conventions for This Project

### UI Design Rule

All controls **must** be declared and laid out in the `.Designer.cs` file (and resources in `.resx`). Do **not** create or position controls programmatically in code-behind (`.cs`) — the Designer file is the single source of truth for the control tree, sizes, anchors, and initial properties. Code-behind only wires up event handlers and applies styling helpers (e.g. `StyleGrid`, `StyleButtonPrimary`).

When creating new UI, every control's `Font`, `ForeColor`, `BackColor`, `BorderStyle`, and other visual properties **must** be set in the Designer (`.Designer.cs`). Do **not** override or re-apply these properties in code-behind — style each control once, in the Designer, and leave it.

Every control added to `.Designer.cs` **must** have its `Text` property explicitly set — even if the initial value is an empty string (`""`). Never leave `Text` unset when declaring a control in the Designer.

### DataGridView styling (tables on Receipts/Items/Employees pages)

```csharp
// Apply this to all DataGridViews
static void StyleGrid(DataGridView grid)
{
    grid.BorderStyle = BorderStyle.None;
    grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
    grid.GridColor = Theme.BorderLine;
    grid.BackgroundColor = Color.White;
    grid.DefaultCellStyle.Font = Theme.Font(12f);
    grid.DefaultCellStyle.SelectionBackColor = Theme.NavHover;
    grid.DefaultCellStyle.SelectionForeColor = Theme.NavText;
    grid.ColumnHeadersDefaultCellStyle.BackColor = Theme.SidebarBg;
    grid.ColumnHeadersDefaultCellStyle.Font = Theme.Font(12f, FontStyle.Bold);
    grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.SidebarBg;
    grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Theme.NavText;
    grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
    grid.EnableHeadersVisualStyles = false;
    grid.RowHeadersVisible = false;
    grid.AllowUserToAddRows = false;
    grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    grid.ReadOnly = true;
    grid.RowTemplate.Height = 40;
    grid.RowTemplate.MinimumHeight = 40;
}
```

**DataGridView column rules:**
- All columns **must** be declared as `DataGridViewTextBoxColumn` (or appropriate type) local variables in `InitializeComponent()` and added via `Columns.AddRange` — never added in code-behind
- Always set `DataPropertyName` to bind to the model/DataTable column name
- Set `AutoGenerateColumns = false` on every DataGridView in the Designer
- Set `ColumnHeadersHeightSizeMode = DisableResizing` and `ColumnHeadersHeight = 40` in the Designer

### Default BackColor

All controls default to `Color.White`. Set it at the root Form level:
```csharp
this.BackColor = Color.White;
```
Panels, GroupBoxes, and UserControls must also explicitly set `BackColor = Color.White` in their constructors or Designer — WinForms does not always inherit automatically.

### Button styling

```csharp
// Primary button (e.g. Save, Search)
static void StyleButtonPrimary(Button btn)
{
    btn.FlatStyle = FlatStyle.Flat;
    btn.FlatAppearance.BorderSize = 1;
    btn.FlatAppearance.BorderColor = Theme.PrimaryBlue;
    btn.BackColor = Theme.PrimaryBlue;
    btn.ForeColor = Color.White;
    btn.Font = Theme.Font(12f);
}

// Secondary button (e.g. Cancel, Close)
static void StyleButtonSecondary(Button btn)
{
    btn.FlatStyle = FlatStyle.Flat;
    btn.FlatAppearance.BorderSize = 1;
    btn.FlatAppearance.BorderColor = Theme.BorderLine;
    btn.BackColor = Color.White;
    btn.ForeColor = Theme.NavText;
    btn.Font = Theme.Font(12f);
}
```

### TextBox styling

TextBoxes use `BorderStyle.FixedSingle` directly — no wrapper panel needed.

```csharp
// In the Designer: BorderStyle = FixedSingle, BackColor = White, Font = Roboto 12F
static void StyleTextBox(TextBox txt)
{
    txt.BorderStyle = BorderStyle.FixedSingle;
    txt.BackColor = Color.White;
    txt.Font = Theme.Font(12f);
}
```

Place the `TextBox` directly on the form or UserControl and size it there.

### Form sizing & Dialog style

- MainForm: `1024 x 768` minimum, resizable
- Modal edit dialogs: `480 x auto`

All dialogs (and MainForm) must set:
```csharp
this.FormBorderStyle = FormBorderStyle.None;
```

Because `FormBorderStyle.None` removes the OS title bar, every dialog must implement its own title bar as a `Panel` with a close button.

### Thread safety

Load data on a background thread (`Task.Run`) and update UI on the UI thread:
```csharp
// Inside a Page's Load event
Task.Run(() => _service.GetAll())
    .ContinueWith(t => {
        grid.DataSource = t.Result.ToList();
    }, TaskScheduler.FromCurrentSynchronizationContext());
```

---

## Database Schema — BLOGICPOS7

---

### Table: SaleReceipts *(main)*

Lưu thông tin Receipt (Ticket) của hệ thống POS.

#### Transaction Status (`TransactionStatusID` — `varchar(5)`)

| Value | Ý nghĩa |
|---|---|
| `OPER` | Ticket mới tạo, đang mở |
| `FIN` | Ticket đã Finish (thanh toán xong) |
| `VOID` | Ticket đã bị VOID |
| `REVOI` | Bản sao (ghost) của Ticket FIN — tạo khi VOID một FIN ticket |
| `CANR` | Ticket đã Cancel |
| `RECAN` | Bản sao (ghost) của Ticket OPER — tạo khi Cancel một OPER ticket |

#### Identity & Status Columns

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `SaleReceiptID` | uniqueidentifier | NO | Primary key |
| `AutoID` | bigint | NO | Auto-increment ID phụ |
| `SaleReceiptNumber` | varchar(50) | YES | Số hóa đơn hiển thị |
| `TransactionStatusID` | varchar(5) | NO | Trạng thái ticket (xem bảng trên) |
| `SaleReceiptType` | int | YES | Loại receipt |
| `TicketType` | int | YES | Loại ticket |
| `SaleTypeID` | int | NO | FK → SaleTypes |
| `SaleTypeName` | varchar(50) | YES | Tên sale type |
| `StoreID` | varchar(50) | YES | ID cửa hàng |
| `ShiftID` | varchar(50) | YES | ID ca làm việc |
| `WorkstationID` | varchar(50) | YES | ID máy POS |
| `WorkstationName` | nvarchar(100) | YES | Tên máy POS |
| `TableID` | uniqueidentifier | YES | FK → Tables |
| `GuestsNum` | smallint | YES | Số khách |
| `DateCreated` | datetime | YES | Ngày tạo ticket |
| `StartDate` | datetime | YES | Giờ bắt đầu |
| `LastModifyDateTime` | datetime | YES | Lần cuối chỉnh sửa |
| `IsModifyTicket` | bit | YES | Ticket đã bị sửa |
| `IsOpen` | bit | YES | Ticket đang mở |
| `IsHold` | bit | YES | Ticket đang hold |
| `IsOpenTab` | bit | YES | Ticket dạng open tab |
| `StatusSaleReceipt` | int | YES | Trạng thái nội bộ |
| `StatusProcess` | varchar(50) | YES | Trạng thái xử lý |
| `IsPrintKitchen` | bit | YES | Đã in kitchen chưa |

#### People Columns

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `CashierID` | int | NO | FK → Employees.EmployeeID — nhân viên tạo ticket |
| `EmployeePaidID` | int | YES | FK → Employees — nhân viên nhận thanh toán |
| `EmployeeIDDiscountTicket` | int | YES | Nhân viên thực hiện discount ticket |
| `AssociateSalePersonID` | int | YES | Nhân viên bán hàng phụ |
| `CustomerID` | varchar(50) | YES | FK → Customers |
| `CurrentCustomerName` | varchar(250) | YES | Tên khách tại thời điểm tạo |
| `CurrentCustomerPhone` | varchar(250) | YES | Phone khách |
| `CurrentCustomerAddress` | varchar(250) | YES | Địa chỉ khách |
| `CurrentCustomerEmail` | varchar(250) | YES | Email khách |

#### Reference Columns

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `ReasonCode` | int | YES | FK → ReasonCodes.ReasonCodeID — lý do VOID/CANR |
| `ReturnCode` | int | YES | FK → ReasonCodes.ReasonCodeID — lý do discount |
| `RefSaleReceiptID` | varchar(50) | YES | ID của ticket gốc (dùng cho REVOI / RECAN) |
| `RefNo` | varchar(50) | YES | Số tham chiếu ngoài |
| `InvoiceNum` | nvarchar(50) | YES | Số invoice |

#### Amount Columns

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `TotalAmount` | money | YES | Tổng ticket — **không** bao gồm discount, svc fee, cash discount, tax of svc fee, tax of cash discount |
| `TaxAmount` | money | YES | Tax của Item + tax của 5 loại service charge |
| `PaidAmount` | money | YES | Số tiền đã thanh toán; khi FIN = `TotalAmount` |
| `SoldQuantity` | int | YES | Tổng số lượng item đã bán |
| `TotalQuantity` | int | YES | Tổng số lượng item |
| `PriceLevel` | int | YES | Mức giá áp dụng |

#### Discount Columns

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `TotalDiscountAmount` | money | YES | Tổng discount của ticket |
| `GlobalDiscountAmount` | money | YES | Luôn bằng `TotalDiscountAmount` |
| `GlobalDiscountPercent` | float | YES | Phần trăm discount toàn ticket |
| `DiscountType` | int | YES | Loại discount |

#### Service Charge Columns

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `SalesOrderFee` | money | YES | Tổng 5 loại service charge |
| `PartyServiceFee` | decimal(18,2) | YES | Party service fee % |
| `PartyServiceFeeAmount` | money | YES | Party service fee amount |
| `AutoGratuityPercent` | money | YES | Auto gratuity % |
| `AutoGratuityAmount` | money | YES | Auto gratuity amount |
| `AreaFee` | money | YES | Area fee % |
| `AreaFeeAmount` | money | YES | Area service fee amount |
| `ServiceFeePercent` | decimal(18,5) | YES | Manual service fee % |
| `ServiceFee` | money | YES | Manual service fee amount |
| `ExtraFeeAmount` | money | YES | Extra fee amount |
| `DeliveryFee` | money | YES | Delivery fee |

#### Service Charge Tax Columns

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `ServiceChargeTaxAmount` | money | YES | Tổng tax của 5 loại service charge |
| `ServiceChargeTaxCodeID` | varchar(10) | YES | Tax code của service charge |
| `ServiceChargeTaxRate` | decimal(18,8) | YES | Tax rate của service charge |
| `PartyServiceFeeTaxAmount` | money | YES | Tax of party service fee |
| `AutoGratuityTaxAmount` | money | YES | Tax of auto gratuity |
| `AreaFeeTaxAmount` | money | YES | Tax of area fee |
| `ServiceFeeTaxAmount` | money | YES | Tax of manual service fee |
| `ExtraFeeTaxAmount` | money | YES | Tax of extra fee |
| `DeliveryFeeTaxAmount` | money | YES | Tax of delivery fee |
| `FinalGratuity` | smallmoney | YES | Gratuity cuối cùng sau phân bổ |

#### Custom Fee (SVC Fee) Columns

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `SVCFeePercent` | decimal(18,8) | YES | SVC fee % |
| `SVCFeeAmount` | money | YES | Base custom fee, **bao gồm** `SVCFeeTaxAmount` |
| `SVCFeeTaxAmount` | money | YES | Tax of custom fee |
| `SVCFeeTaxRate` | decimal(18,8) | YES | Tax rate of custom fee |
| `SVCFeeTaxCodeID` | varchar(10) | YES | Tax code of custom fee |
| `SVCFeePaidAmount` | money | YES | Custom fee đã thanh toán, **bao gồm** `SVCFeeTaxPaidAmount` |
| `SVCFeeTaxPaidAmount` | money | YES | Tax of custom fee đã thanh toán |

#### Cash Discount Columns

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `CashDiscountPercent` | decimal(18,8) | YES | Cash discount % |
| `CashDiscountAmount` | money | YES | Base cash discount, **bao gồm** `CashDiscountTaxAmount` |
| `CashDiscountTaxAmount` | money | YES | Tax of cash discount |
| `CashDiscountPaidAmount` | money | YES | Cash discount đã thanh toán, **bao gồm** `CashDiscountTaxPaidAmount` |
| `CashDiscountTaxPaidAmount` | money | YES | Tax of cash discount đã thanh toán |
| `CoinDiscountAmount` | money | YES | Số tiền giảm khi làm tròn xu khi thanh toán cash |

#### Deposit / Open Tab Columns

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `DepositAmount` | money | YES | Số tiền deposit yêu cầu |
| `DepositPaid` | money | YES | Số tiền deposit đã trả |
| `IsDeposit` | bit | YES | Ticket là deposit |
| `OpenTabAmount` | money | YES | Số tiền open tab |
| `OpenTabCreditCardTransactionID` | varchar(50) | YES | CC Transaction ID của open tab |
| `OpenTabCardNumber` | varchar(50) | YES | Card number của open tab |
| `OpenTabCustomer` | varchar(50) | YES | Tên khách open tab |

#### Loyalty / Points Columns

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `RedeemPoints` | decimal(18,0) | YES | Điểm đã đổi |
| `RewardPoints` | decimal(18,0) | YES | Điểm được thưởng |
| `CustomerPoints` | decimal(18,2) | YES | Điểm hiện tại của khách |
| `AccuPoint` | int | YES | Điểm tích lũy |
| `MoneyToGetPoint` | money | YES | Số tiền để tính điểm |

#### Calculation Logic

| Field | Công thức |
|---|---|
| `TotalAmount` | `SUM(SaleReceiptDetails.ExtendedPrice WHERE Status <> 2 AND IsCompItem <> 1)` + `SalesOrderFee` + `TaxAmount` |
| `TaxAmount` | `SUM(SaleReceiptDetails.TaxAmount WHERE Status <> 2 AND IsCompItem <> 1)` + `ServiceChargeTaxAmount` |
| `ServiceChargeTaxAmount` | `AreaFeeTaxAmount` + `AutoGratuityTaxAmount` + `PartyServiceFeeTaxAmount` + `ServiceFeeTaxAmount` + `ExtraFeeTaxAmount` |
| `ServiceFeeTaxAmount` | `SUM(SaleReceiptServiceChargeTaxDetails.ServiceFeeTaxAmount)` |
| `ExtraFeeTaxAmount` | `SUM(SaleReceiptServiceChargeTaxDetails.ExtraFeeTaxAmount)` |
| `AutoGratuityTaxAmount` | `SUM(SaleReceiptServiceChargeTaxDetails.AutoGratuityTaxAmount)` |
| `PartyServiceFeeTaxAmount` | `SUM(SaleReceiptServiceChargeTaxDetails.PartyServiceFeeTaxAmount)` |
| `AreaFeeTaxAmount` | `SUM(SaleReceiptServiceChargeTaxDetails.AreaFeeTaxAmount)` |
| `SalesOrderFee` | `AreaFeeAmount` + `AutoGratuityAmount` + `ServiceFee` + `ExtraFeeAmount` + `PartyServiceFeeAmount` |
| `GlobalDiscountAmount` = `TotalDiscountAmount` | `SUM(SaleReceiptDetails.ItemDiscountValue + TotalSaleDiscountAmount + StoreDiscountValue + DiscountAmount + TotalSaleDiscountAmountByCustomer + ItemDiscountByPriceLevelAmount WHERE Status <> 2 AND IsCompItem <> 1)` |
| `PaidAmount` | `SUM(SaleReceiptPaymentDetails.AmountTendered - SaleReceiptPaymentDetails.AmountReturn)` |
| `SVCFeePaidAmount` | `SUM(SaleReceiptPaymentDetails.SVCFeeAmount)` |
| `CashDiscountPaidAmount` | `SUM(SaleReceiptPaymentDetails.CashDiscountAmount)` |
| `SVCFeeTaxPaidAmount` | `SUM(SaleReceiptPaymentDetails.SVCFeeTaxAmount)` |
| `CashDiscountTaxAmount` | `SUM(SaleReceiptPaymentDetails.CashDiscountTaxAmount)` |
| `CoinDiscountAmount` | `SUM(SaleReceiptPaymentDetails.CoinDiscountAmount)` |
| `FinalGratuity` | `SUM(SaleReceiptPaymentDetails.Gratuity)` |

#### Final Total Formula

```
Final Total (exclude tip) = TotalAmount + SVCFeePaidAmount - CashDiscountPaidAmount - CoinDiscountAmount
```

---

### Table: SaleReceiptDetails *(detail)*

Lưu thông tin từng item trong ticket. FK: `SaleReceiptID → SaleReceipts.SaleReceiptID`.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `SaleReceiptDetailID` | uniqueidentifier | NO | Primary key |
| `SaleReceiptID` | uniqueidentifier | NO | FK → SaleReceipts |
| `ItemID` | uniqueidentifier | NO | FK → Items |
| `Description` | nvarchar(255) | YES | Tên item |
| `Description2` | nvarchar(1000) | YES | Mô tả phụ |
| `Description3` | nvarchar(1000) | YES | Mô tả phụ 3 |
| `ShortDescription` | nvarchar(1000) | YES | Tên ngắn |
| `DescriptionChinese` | nvarchar(1000) | YES | Tên tiếng Trung |
| `Lookup_Code` | nvarchar(255) | YES | Barcode / PLU |
| `Quantity` | float | YES | Số lượng bán |
| `FinalQuantity` | float | YES | Số lượng cuối sau refund/split |
| `QtyPerItem` | float | YES | Số lượng con trong 1 item |
| `QuantitySold` | float | YES | Số lượng thực bán |
| `Price` | money | YES | Đơn giá |
| `PriceInitial` | varchar(6) | YES | Giá gốc (flag) |
| `RegularPrice` | money | YES | Giá thường |
| `DualPrice` | money | YES | Dual price (cash/credit) |
| `PriceCash` | money | YES | Giá khi thanh toán cash |
| `PriceCC` | money | YES | Giá khi thanh toán thẻ |
| `OpenPriceValue` | money | YES | Giá nhập tay (open price) |
| `ExtendedPrice` | money | YES | Thành tiền (Price × Quantity) |
| `ItemUnitCost` | money | YES | Giá vốn |
| `MarkDown` | float | YES | % markdown |
| `Taxable` | bit | NO | Item có chịu thuế không |
| `TaxCodeID` | varchar(10) | YES | Mã thuế |
| `TaxRate` | decimal(18,8) | YES | Tax rate |
| `TaxMethod` | tinyint | YES | Phương pháp tính thuế |
| `TaxAmount` | money | YES | Tiền thuế của item |
| `TaxType` | int | YES | Loại thuế |
| `GroupID` | varchar(50) | YES | ID nhóm item |
| `ItemGroupID` | int | YES | FK → ItemGroups |
| `PartIndex` | int | YES | Index trong combo/modifier |
| `Range` | int | YES | Range của item |
| `SalePersonID` | int | YES | FK → Employees — nhân viên bán |
| `EmployeeID` | int | YES | FK → Employees |
| `EmployeeName` | nvarchar(100) | YES | Tên nhân viên |
| `ReturnCode` | int | YES | FK → ReasonCodes — lý do discount item |
| `ReasonComment` | nvarchar(50) | YES | Ghi chú lý do |
| `ReasonCodeDiscountStore` | varchar(50) | YES | Reason code discount store |
| `ReasonCodeDiscountCategory` | varchar(50) | YES | Reason code discount category |
| `ReasonCodeDiscountTicket` | varchar(50) | YES | Reason code discount ticket |
| `ReasonCodeDiscountItem` | varchar(50) | YES | Reason code discount item |
| `ReasonCodeDiscountCustomer` | varchar(50) | YES | Reason code discount customer |
| `ReasonCodeDiscountLevelCustomer` | varchar(50) | YES | Reason code discount price level |
| `DiscountPercent` | float | YES | % discount item |
| `DiscountAmount` | money | YES | Tiền discount item |
| `DiscountCode` | int | YES | Mã loại discount |
| `TotalSaleDiscountAmount` | money | YES | Tổng discount item (system + customer) |
| `TotalSaleDiscountPercent` | float | YES | % tổng discount |
| `TotalSaleDiscountAmountBySystem` | money | YES | Discount do hệ thống áp |
| `TotalSaleDiscountAmountByCustomer` | money | YES | Discount do cashier áp |
| `ItemDiscountType` | int | YES | Loại discount item |
| `ItemDiscountValue` | money | YES | Giá trị discount item |
| `ItemDiscountByPriceLevelAmount` | money | YES | Discount theo price level |
| `StoreDiscountRate` | float | YES | % discount store |
| `StoreDiscountValue` | money | YES | Tiền discount store |
| `CashDiscountPerItem` | money | YES | Cash discount phân bổ cho item |
| `CoinDiscountPerItem` | money | YES | Coin discount phân bổ cho item |
| `ServiceFeeAmount` | decimal(18,5) | YES | Service fee phân bổ cho item |
| `ServiceFeePercent` | decimal(18,5) | YES | % service fee của item |
| `CommissionRate` | float | YES | % hoa hồng |
| `CommissionValue` | money | YES | Tiền hoa hồng |
| `Status` | smallint | YES | Trạng thái item (0=normal) |
| `InventoryUpdatable` | bit | YES | Có cập nhật tồn kho |
| `IsToGo` | bit | YES | Item To-Go |
| `IsSplitItems` | bit | YES | Item được split |
| `IsCompItem` | bit | YES | Item được comp |
| `EmployeeIDComp` | int | YES | Nhân viên thực hiện comp |
| `EmployeeNameCOMP` | nvarchar(100) | YES | Tên nhân viên comp |
| `COMPDateTime` | datetime | YES | Thời điểm comp |
| `IsDeposit` | bit | NO | Item là deposit |
| `HoldDateTime` | datetime | YES | Thời điểm hold |
| `HoldMinutes` | int | YES | Số phút hold |
| `OrderDateTime` | datetime | YES | Thời điểm order |
| `StampDateTime` | datetime | YES | Thời điểm stamp |
| `LastModifyTime` | datetime | YES | Lần cuối chỉnh sửa |
| `LastModifyShiftID` | varchar(50) | YES | Shift ID lần cuối sửa |
| `RefSaleReceiptDetailID` | varchar(50) | YES | ID detail gốc (refund/void) |
| `IssuePoints` | decimal(18,0) | YES | Điểm phát hành |
| `RedeemPoints` | decimal(18,0) | YES | Điểm đổi |
| `RedeemStatus` | int | YES | Trạng thái đổi điểm |
| `WorkPoints` | decimal(18,4) | YES | Điểm làm việc |
| `SeatAssignment` | nvarchar(100) | YES | Chỗ ngồi |
| `SeatAssignmentIndex` | nvarchar(100) | YES | Index chỗ ngồi |
| `Comments` | nvarchar(512) | YES | Ghi chú item |
| `ModifierType` | nvarchar(255) | YES | Loại modifier |
| `IsModifierCombine` | bit | YES | Modifier kết hợp |
| `IsOverwritePrintModifier` | bit | YES | Ghi đè in modifier |
| `KDSIDDetail` | uniqueidentifier | YES | ID KDS detail |
| `QuantityKDS` | float | YES | Số lượng KDS |
| `KDSDoneDateTime` | datetime | YES | Thời điểm KDS done |
| `TPStatus` | smallint | NO | Third-party status |
| `TPID` | nvarchar(50) | YES | Third-party ID |
| `PriceBeforeTP` | decimal(18,2) | YES | Giá trước third-party |
| `TaxRateBeforeTP` | decimal(18,5) | YES | Tax rate trước third-party |
| `TPTime` | datetime | YES | Thời điểm third-party |
| `EBTType` | varchar(5) | YES | Loại EBT |
| `EBTPaid` | varchar(5) | YES | EBT đã thanh toán |
| `BulkID` | varchar(50) | YES | ID chương trình Bulk |
| `BulkGroupID` | varchar(50) | YES | Group ID Bulk |
| `PriceBeforeBulk` | decimal(18,2) | YES | Giá trước Bulk |
| `MMID` | varchar(50) | YES | Mix & Match ID |
| `MMGroupID` | varchar(50) | YES | Mix & Match Group ID |
| `PriceBeforeMM` | decimal(18,2) | YES | Giá trước Mix & Match |
| `MMIsApplyItem` | bit | YES | MM áp dụng cho item |
| `MMLeastExpensiveDiscountType` | varchar(10) | YES | Kiểu discount least expensive MM |
| `MMLeastExpensiveDiscountValue` | decimal(18,2) | YES | Giá trị discount least expensive MM |
| `PriceBeforeRedeem` | money | YES | Giá trước khi đổi điểm |
| `EmployeeIDDiscountTicket` | int | YES | Nhân viên discount ticket |
| `FinalDiscountAmount` | float | YES | Discount cuối |
| `FinalCashDiscount` | float | YES | Cash discount cuối |
| `FinalCoinDiscount` | float | YES | Coin discount cuối |
| `FinalTaxAmount` | float | YES | Tax cuối |
| `FinalTotalSales` | float | YES | Tổng sales cuối |
| `FinalGrossSales` | float | YES | Gross sales cuối |
| `FinalNetSales` | float | YES | Net sales cuối |
| `FinalNetSalesTax` | float | YES | Net sales tax cuối |

#### Calculation Logic

| Field | Công thức |
|---|---|
| `TaxAmount` | `SUM(SaleReceiptDetailTaxDetails.TaxAmount)` |
| `ExtendedPrice` | `(Quantity * Price) - (ItemDiscountValue + TotalSaleDiscountAmount + StoreDiscountValue + DiscountAmount + TotalSaleDiscountAmountByCustomer + ItemDiscountByPriceLevelAmount)` WHERE `Status <> 2 AND IsCompItem <> 1` |
| `FinalDiscountAmount` | `ItemDiscountValue + TotalSaleDiscountAmount + StoreDiscountValue + DiscountAmount + TotalSaleDiscountAmountByCustomer + ItemDiscountByPriceLevelAmount` |
| `FinalTaxAmount` | `TaxAmount` |
| `FinalTotalSales` | `ExtendedPrice + TaxAmount - CashDiscountPerItem - CoinDiscountPerItem` |
| `FinalGrossSales` | `ExtendedPrice + ItemDiscountValue + TotalSaleDiscountAmount + StoreDiscountValue + DiscountAmount + TotalSaleDiscountAmountByCustomer + ItemDiscountByPriceLevelAmount` |
| `FinalNetSales` | `ExtendedPrice - CASE WHEN (SaleReceipts.CashDiscountAmount > SaleReceipts.SVCFeeAmount) THEN CashDiscountPerItem ELSE 0 END - CoinDiscountPerItem` |
| `FinalNetSalesTax` | `ExtendedPrice + TaxAmount - CASE WHEN (SaleReceipts.CashDiscountAmount > SaleReceipts.SVCFeeAmount) THEN CashDiscountPerItem ELSE 0 END - CoinDiscountPerItem` |

---

### Table: SaleReceiptDetailTaxDetails *(detail của SaleReceiptDetails)*

Lưu chi tiết tax theo từng tax code của từng item trong ticket. FK kép: `SaleReceiptDetailID → SaleReceiptDetails.SaleReceiptDetailID` và `SaleReceiptID → SaleReceipts.SaleReceiptID`.

> Một item có thể chịu nhiều tax code khác nhau — mỗi tax code là một dòng riêng trong bảng này.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `SaleReceiptDetailTaxDetailID` | uniqueidentifier | NO | Primary key |
| `SaleReceiptDetailID` | uniqueidentifier | NO | FK → SaleReceiptDetails |
| `SaleReceiptID` | uniqueidentifier | NO | FK → SaleReceipts |
| `ItemID` | uniqueidentifier | NO | FK → Items |
| `TaxCodeID` | varchar(10) | YES | Mã tax code áp dụng |
| `TaxRate` | decimal(18,8) | YES | Tax rate của tax code này |
| `TaxAmount` | money | YES | Tiền thuế theo tax code này |
| `TaxType` | int | YES | Loại thuế |
| `Quantity` | float | YES | Số lượng item |
| `Price` | money | YES | Đơn giá item |
| `ExtendedPrice` | money | YES | Thành tiền (Price × Quantity) |
| `PriceInitial` | varchar(6) | YES | Flag giá gốc |
| `Description` | nvarchar(255) | YES | Tên item |
| `GroupID` | varchar(50) | YES | ID nhóm item |
| `ReturnCode` | int | YES | FK → ReasonCodes — lý do discount |
| `ReasonComment` | nvarchar(50) | YES | Ghi chú lý do |
| `Status` | smallint | YES | Trạng thái dòng (default: 0) |
| `Range` | int | YES | Range của item |
| `EmployeeID` | int | YES | FK → Employees |
| `EmployeeName` | nvarchar(100) | YES | Tên nhân viên |
| `IsCompItem` | bit | YES | Item được comp |
| `IsDeposit` | bit | YES | Item là deposit |
| `LastModifyTime` | datetime | YES | Lần cuối chỉnh sửa |
| `KDSIDDetail` | uniqueidentifier | YES | ID KDS detail |

---

### Table: SaleReceiptPaymentDetails *(detail)*

Lưu từng dòng thanh toán của ticket. FK: `SaleReceiptID → SaleReceipts.SaleReceiptID`.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `SaleReceiptPaymentDetailID` | bigint | NO | Primary key |
| `SaleReceiptID` | uniqueidentifier | NO | FK → SaleReceipts |
| `PaymentMethodID` | int | NO | FK → PaymentMethods |
| `PaymentType` | tinyint | YES | Loại thanh toán (0=normal) |
| `AmountTendered` | money | YES | Số tiền khách đưa |
| `AmountReturn` | money | YES | Số tiền trả lại |
| `CreatedDate` | datetime | YES | Ngày tạo |
| `Cashier` | int | YES | FK → Employees — cashier thực hiện |
| `EmployeePaidID` | int | YES | FK → Employees — nhân viên nhận |
| `ShiftID` | varchar(50) | YES | FK → Shifts |
| `PaymentReceiptID` | uniqueidentifier | YES | ID phiếu thanh toán |
| `IsDeposit` | bit | YES | Dòng thanh toán là deposit |
| `IsDepositPayment` | bit | NO | Là thanh toán deposit |
| `IsDecline` | bit | YES | Thanh toán bị từ chối |
| `GiftExtra` | money | YES | Số dư gift card dư |
| `CardBalance` | money | YES | Số dư thẻ |
| `GiftCardCode` | varchar(50) | YES | Mã gift card |
| `GiftCardTransactionID` | varchar(100) | YES | Transaction ID gift card |
| `CurrencyAmount` | money | YES | Số tiền theo ngoại tệ |
| `CurrencyID` | int | YES | FK → Currencies |
| `PayByAccuPoint` | int | YES | Số điểm dùng thanh toán |
| `HouseAccountNumber` | varchar(50) | YES | Số tài khoản house account |
| `HouseAccountTransactionID` | varchar(100) | YES | Transaction ID house account |
| `PaidHouseAccount` | money | YES | Số tiền thanh toán house account |
| `PaidServiceFee` | money | YES | Service fee đã thanh toán |
| `PaidPayout` | money | YES | Payout đã thanh toán |
| `Gratuity` | money | YES | Tip của dòng thanh toán này |
| `CreditCardTransactionID` | nvarchar(100) | YES | CC Transaction ID |
| `CreditCardTransactionNumber` | nvarchar(100) | YES | CC Transaction Number |
| `CreditCardTransactionCardNumber` | nvarchar(100) | YES | CC Card Number (masked) |
| `CreditCardTransactionType` | nvarchar(100) | YES | CC Transaction Type |
| `CreditCardTransactionMerchantID` | nvarchar(100) | YES | CC Merchant ID |
| `CreditCardRefNo` | nvarchar(100) | YES | CC Reference Number |
| `CreditCardAuthCode` | nvarchar(100) | YES | CC Auth Code |
| `CreditCardCardType` | nvarchar(50) | YES | Loại thẻ |
| `CardHolderName` | varchar(100) | YES | Tên chủ thẻ |
| `SVCFeeAmount` | money | YES | SVC fee phân bổ cho payment này |
| `SVCFeeTaxAmount` | money | YES | Tax of SVC fee |
| `SVCFeeForGratuity` | money | YES | SVC fee tính trên gratuity |
| `SVCFeeForServiceCharge` | money | YES | SVC fee tính trên service charge |
| `CashDiscountAmount` | money | YES | Cash discount phân bổ |
| `CashDiscountTaxAmount` | money | YES | Tax of cash discount |
| `CoinDiscountAmount` | money | YES | Coin discount phân bổ |
| `MerchantFee` | money | YES | Merchant fee tổng |
| `MerchantFeeForTip` | money | YES | Merchant fee trên tip |
| `PartyServiceFeeAmount` | money | YES | Party service fee phân bổ |
| `SVCFeeForPartyServiceFeeAmount` | money | YES | SVC fee trên party service fee |
| `MerchantFeeForPartyServiceFeeAmount` | money | YES | Merchant fee trên party service fee |
| `PartyServiceFeeTaxAmount` | money | YES | Tax of party service fee |
| `AutoGratuityAmount` | money | YES | Auto gratuity phân bổ |
| `SVCFeeForAutoGratuityAmount` | money | YES | SVC fee trên auto gratuity |
| `MerchantFeeForAutoGratuityAmount` | money | YES | Merchant fee trên auto gratuity |
| `AutoGratuityTaxAmount` | money | YES | Tax of auto gratuity |
| `AreaFeeAmount` | money | YES | Area fee phân bổ |
| `SVCFeeForAreaFeeAmount` | money | YES | SVC fee trên area fee |
| `MerchantFeeForAreaFeeAmount` | money | YES | Merchant fee trên area fee |
| `AreaFeeTaxAmount` | money | YES | Tax of area fee |
| `ServiceFee` | money | YES | Manual service fee phân bổ |
| `SVCFeeForServiceFee` | money | YES | SVC fee trên manual service fee |
| `MerchantFeeForServiceFee` | money | YES | Merchant fee trên manual service fee |
| `ServiceFeeTaxAmount` | money | YES | Tax of manual service fee |
| `TaxAmountByPaymentModeTaxCustomFee` | money | YES | Tax custom fee theo payment mode |
| `IsCalculateTaxCustomFee` | int | YES | Có tính tax custom fee không |

#### Calculation Logic

| Field | Công thức |
|---|---|
| `PaidServiceFee` | `AutoGratuityAmount + ServiceFee + PartyServiceFeeAmount + AreaFeeAmount` |

---

### Table: SaleReceiptServiceChargeTaxDetails *(detail)*

Lưu chi tiết tax theo từng tax code của service charges. FK: `SaleReceiptID → SaleReceipts.SaleReceiptID`.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `SaleReceiptServiceChargeTaxDetailID` | uniqueidentifier | NO | Primary key |
| `SaleReceiptID` | uniqueidentifier | NO | FK → SaleReceipts |
| `ServiceChargeTaxCodeID` | varchar(10) | YES | Mã tax code áp dụng |
| `ServiceChargeTaxRate` | decimal(18,8) | YES | Tax rate của tax code này |
| `ServiceFee` | money | YES | Manual service fee chịu tax code này |
| `ServiceFeeTaxAmount` | money | YES | Tax của manual service fee |
| `PartyServiceFeeAmount` | money | YES | Party service fee chịu tax code này |
| `PartyServiceFeeTaxAmount` | money | YES | Tax của party service fee |
| `AreaFeeAmount` | money | YES | Area fee chịu tax code này |
| `AreaFeeTaxAmount` | money | YES | Tax của area fee |
| `AutoGratuityAmount` | money | YES | Auto gratuity chịu tax code này |
| `AutoGratuityTaxAmount` | money | YES | Tax của auto gratuity |
| `ExtraFeeAmount` | money | YES | Extra fee chịu tax code này |
| `ExtraFeeTaxAmount` | money | YES | Tax của extra fee |
| `DeliveryFee` | money | YES | Delivery fee chịu tax code này |
| `DeliveryFeeTaxAmount` | money | YES | Tax của delivery fee |

#### Calculation Logic

| Field | Công thức |
|---|---|
| `ServiceFee` | `SaleReceipts.ServiceFee` |
| `ExtraFeeAmount` | `SaleReceipts.ExtraFeeAmount` |
| `AutoGratuityAmount` | `SaleReceipts.AutoGratuityAmount` |
| `PartyServiceFeeAmount` | `SaleReceipts.PartyServiceFeeAmount` |
| `AreaFeeAmount` | `SaleReceipts.AreaFeeAmount` |

---

### Table: SaleReceiptGratuityDetails *(detail)*

Lưu phân bổ gratuity (tip) cho từng nhân viên theo từng dòng payment. FK: `SaleReceiptID → SaleReceipts.SaleReceiptID`.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `SaleReceiptGratuityDetailID` | uniqueidentifier | NO | Primary key (default: newid()) |
| `SaleReceiptID` | uniqueidentifier | NO | FK → SaleReceipts |
| `SaleReceiptPaymentDetailID` | bigint | NO | FK → SaleReceiptPaymentDetails |
| `PaymentMethodID` | int | YES | FK → PaymentMethods |
| `DistributionMethod` | varchar(50) | NO | Phương thức phân bổ tip |
| `EmployeeID` | int | NO | FK → Employees — nhân viên nhận tip |
| `EmployeeIDCreate` | int | YES | FK → Employees — người tạo bản ghi |
| `Gratuity` | smallmoney | NO | Số tiền tip phân bổ |
| `SVCFeeForGratuity` | smallmoney | NO | SVC fee tính trên tip này |
| `MerchantFeeForGratuity` | smallmoney | NO | Merchant fee tính trên tip này |
| `DateCreated` | datetime | NO | Ngày tạo (default: getdate()) |

---

### Table: PaymentRoleDetails *(detail)*

Lưu phân bổ tip + service charge theo role của nhân viên trong từng dòng payment. FK: `SaleReceiptID → SaleReceipts.SaleReceiptID`.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `ID` | uniqueidentifier | NO | Primary key |
| `SaleReceiptID` | uniqueidentifier | NO | FK → SaleReceipts |
| `SaleReceiptPaymentDetailID` | bigint | NO | FK → SaleReceiptPaymentDetails |
| `PaymentMethodID` | smallint | NO | FK → PaymentMethods |
| `PaymentMethodType` | smallint | NO | Loại payment method |
| `EmployeeID` | smallint | NO | FK → Employees |
| `RolesID` | smallint | NO | FK → Roles — role của nhân viên |
| `DateCreated` | datetime | NO | Ngày tạo |
| `TipRate` | smallmoney | NO | Tỉ lệ tip regular |
| `TipRateOT` | smallmoney | NO | Tỉ lệ tip overtime |
| `Gratuity` | smallmoney | NO | Tip phân bổ cho nhân viên này |
| `SVCFeeForGratuity` | smallmoney | NO | SVC fee trên tip |
| `MerchantFeeForTip` | smallmoney | NO | Merchant fee trên tip |
| `PartyServiceFeeAmount` | smallmoney | NO | Party service fee phân bổ |
| `PartyServiceFeeTaxAmount` | smallmoney | NO | Tax of party service fee |
| `SVCFeeForPartyServiceFeeAmount` | smallmoney | NO | SVC fee trên party service fee |
| `MerchantFeeForPartyServiceFeeAmount` | smallmoney | NO | Merchant fee trên party service fee |
| `AutoGratuityAmount` | smallmoney | NO | Auto gratuity phân bổ |
| `AutoGratuityTaxAmount` | smallmoney | NO | Tax of auto gratuity |
| `SVCFeeForAutoGratuityAmount` | smallmoney | NO | SVC fee trên auto gratuity |
| `MerchantFeeForAutoGratuityAmount` | smallmoney | NO | Merchant fee trên auto gratuity |
| `AreaFeeAmount` | smallmoney | NO | Area fee phân bổ |
| `AreaFeeTaxAmount` | smallmoney | NO | Tax of area fee |
| `SVCFeeForAreaFeeAmount` | smallmoney | NO | SVC fee trên area fee |
| `MerchantFeeForAreaFeeAmount` | smallmoney | NO | Merchant fee trên area fee |
| `ServiceFee` | smallmoney | NO | Manual service fee phân bổ |
| `ServiceFeeTaxAmount` | smallmoney | NO | Tax of manual service fee |
| `SVCFeeForServiceFee` | smallmoney | NO | SVC fee trên manual service fee |
| `MerchantFeeForServiceFee` | smallmoney | NO | Merchant fee trên manual service fee |

---

### Table: Customers

Lưu thông tin khách hàng.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `CustomerID` | varchar(50) | NO | Primary key |
| `FirstName` | nvarchar(100) | YES | Tên |
| `LastName` | nvarchar(100) | YES | Họ |
| `FullName` | nvarchar(250) | YES | Họ và tên |
| `Phone` | varchar(50) | YES | Số điện thoại |
| `Email` | varchar(250) | YES | Email |
| `Address` | nvarchar(250) | YES | Địa chỉ |
| `DateCreated` | datetime | YES | Ngày tạo |
| `IsActive` | bit | YES | Đang hoạt động |
| `Points` | decimal(18,2) | YES | Điểm tích lũy hiện tại |
| `StoreID` | varchar(50) | YES | FK → Stores |

---

### Table: Departments

Lưu thông tin bộ phận (department) trong cửa hàng.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `DepartmentID` | int | NO | Primary key |
| `DepartmentName` | nvarchar(100) | YES | Tên bộ phận |
| `Description` | nvarchar(250) | YES | Mô tả |
| `IsActive` | bit | YES | Đang hoạt động |
| `StoreID` | varchar(50) | YES | FK → Stores |

---

### Table: DeptDepts

Lưu quan hệ phân cấp giữa các department (department cha — con).

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `ID` | int | NO | Primary key |
| `ParentDepartmentID` | int | NO | FK → Departments — department cha |
| `ChildDepartmentID` | int | NO | FK → Departments — department con |

---

### Table: Employees

Lưu thông tin nhân viên.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `EmployeeID` | int | NO | Primary key |
| `FirstName` | nvarchar(100) | YES | Tên |
| `LastName` | nvarchar(100) | YES | Họ |
| `FullName` | nvarchar(250) | YES | Họ và tên |
| `PIN` | varchar(50) | YES | Mã PIN đăng nhập |
| `DepartmentID` | int | YES | FK → Departments |
| `StoreID` | varchar(50) | YES | FK → Stores |
| `IsActive` | bit | YES | Đang hoạt động |
| `DateCreated` | datetime | YES | Ngày tạo |
| `DateModified` | datetime | YES | Ngày chỉnh sửa |

---

### Table: EmployeeRoles

Lưu quan hệ nhiều-nhiều giữa nhân viên và role.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `ID` | int | NO | Primary key |
| `EmployeeID` | int | NO | FK → Employees |
| `RolesID` | int | NO | FK → Roles |

---

### Table: Items

Lưu thông tin sản phẩm / menu item.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `ItemID` | uniqueidentifier | NO | Primary key |
| `Description` | nvarchar(255) | YES | Tên item |
| `ShortDescription` | nvarchar(100) | YES | Tên ngắn |
| `Lookup_Code` | nvarchar(255) | YES | Barcode / PLU |
| `Price` | money | YES | Giá bán |
| `ItemTypeID` | int | YES | FK → ItemTypes |
| `DepartmentID` | int | YES | FK → Departments |
| `TaxCodeID` | varchar(10) | YES | Mã thuế |
| `IsActive` | bit | YES | Đang hoạt động |
| `StoreID` | varchar(50) | YES | FK → Stores |

---

### Table: ItemTypes

Lưu loại item (ví dụ: Food, Beverage, Retail…).

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `ItemTypeID` | int | NO | Primary key |
| `ItemTypeName` | nvarchar(100) | YES | Tên loại item |
| `Description` | nvarchar(250) | YES | Mô tả |
| `IsActive` | bit | YES | Đang hoạt động |

---

### Table: Roles

Lưu các role / chức danh nhân viên trong hệ thống.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `RolesID` | int | NO | Primary key |
| `RoleName` | nvarchar(100) | YES | Tên role |
| `Description` | nvarchar(250) | YES | Mô tả |
| `IsActive` | bit | YES | Đang hoạt động |
| `StoreID` | varchar(50) | YES | FK → Stores |

---

### Table: Shifts

Lưu thông tin ca làm việc.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `ShiftID` | varchar(50) | NO | Primary key |
| `StoreID` | varchar(50) | YES | FK → Stores |
| `WorkstationID` | varchar(50) | YES | FK → Workstations |
| `EmployeeID` | int | YES | FK → Employees — người mở ca |
| `StartDate` | datetime | YES | Thời điểm bắt đầu ca |
| `EndDate` | datetime | YES | Thời điểm kết thúc ca |
| `IsOpen` | bit | YES | Ca đang mở |
| `DateCreated` | datetime | YES | Ngày tạo |

---

### Table: Tables

Lưu thông tin bàn trong nhà hàng.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `TableID` | uniqueidentifier | NO | Primary key |
| `TableName` | nvarchar(100) | YES | Tên bàn |
| `SectionID` | int | YES | FK → Sections — khu vực |
| `Capacity` | int | YES | Sức chứa (số khách) |
| `IsActive` | bit | YES | Đang hoạt động |
| `StoreID` | varchar(50) | YES | FK → Stores |

---

### Table: Workstations

Lưu thông tin máy POS.

| Column | Type | Nullable | Mô tả |
|---|---|---|---|
| `WorkstationID` | varchar(50) | NO | Primary key |
| `WorkstationName` | nvarchar(100) | YES | Tên máy POS |
| `StoreID` | varchar(50) | YES | FK → Stores |
| `IPAddress` | varchar(50) | YES | Địa chỉ IP |
| `IsActive` | bit | YES | Đang hoạt động |
| `DateCreated` | datetime | YES | Ngày tạo |

---

## Build

```
msbuild BLogicPOS7SupportTool.sln /p:Configuration=Debug
```

Output: `bin\Debug\BLogicPOS7SupportTool.exe`

---

## Key Constraints

- **No third-party UI libraries** for the dashboard — standard WinForms controls only, styled manually.
- **C# 7.3 syntax only** — no records, no `is not`, no `??=`, no range operators.
- **No async/await on event handlers** unless explicitly needed — use `Task.ContinueWith` pattern shown above.
- Target framework is `net472` — do not reference any `net5+` APIs.
- Credentials and connection strings go in `App.config`, never hardcoded.
