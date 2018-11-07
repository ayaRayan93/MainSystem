using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace MainSystem
{
    public partial class Print_Bill_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_Bill_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string ClientName, string DelegateName, DateTime billDate, string PayType, int BillNumber, string BranchName, double TotalBillPriceBD, double TotalBillPriceAD, double TotalDiscount, List<Bill_Items> BillItems)
        {
            Print_Invoice report = new Print_Invoice();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ClientName, DelegateName, billDate, PayType, BillNumber, BranchName, TotalBillPriceBD, TotalBillPriceAD, TotalDiscount, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}