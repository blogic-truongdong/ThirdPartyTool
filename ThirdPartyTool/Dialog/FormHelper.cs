using BLogicSystems.XPos.Utilites;
using System.Windows.Forms;

namespace ThirdPartyTool.Dialog
{
    public class FormHelper
    {
        public static DialogResult ShowPopUpFormWithDIMBackground(Form childForm, Form parentForm)
        {
            BaseForm frmParent = new BaseForm(childForm);
            frmParent.ShowDialog(parentForm);
            return childForm.DialogResult;
        }
        public static DialogResult ShowPopUpFormWithDIMBackground(Form childForm)
        {
            BaseForm frmParent = new BaseForm(childForm);
            frmParent.ShowDialog();
            return childForm.DialogResult;
        }
    }
}
