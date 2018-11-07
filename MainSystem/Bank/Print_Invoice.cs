using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_Invoice : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Invoice()
        {
            InitializeComponent();
        }

        public void InitData(string client_Name, string delegate_Name, DateTime billDate, string pay_Type, int bill_Number, string branch_Name, double TotalBillPriceBD, double TotalBillPriceAD, double TotalDiscount, List<Bill_Items> Bill_Items)
        {
            Client_Name.Value = client_Name;
            Delegate_Name.Value = delegate_Name;
            PayType.Value = pay_Type;
            Bill_Number.Value = bill_Number;
            Branch_Name.Value = branch_Name;
            Bill_Date.Value = billDate.Date;
            TotaBillCost.Value = TotalBillPriceBD;
            FinalBillCost.Value = TotalBillPriceAD;
            FinalDiscount.Value = TotalDiscount;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}
