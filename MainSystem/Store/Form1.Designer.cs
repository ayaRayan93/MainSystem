namespace MainSystem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.Store_ID = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_Store_ID = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.Store_Phone = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_Store_Phone = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.Store_Address = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_Store_Address = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.Store_Name = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_Store_Name = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewCard2 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Store_ID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Store_Phone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Store_Address)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Store_Name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(43, 12);
            this.gridControl1.MainView = this.layoutView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(656, 402);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
            // 
            // layoutView1
            // 
            this.layoutView1.Appearance.FocusedCardCaption.Font = new System.Drawing.Font("Tahoma", 1F);
            this.layoutView1.Appearance.FocusedCardCaption.Options.UseFont = true;
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.Store_ID,
            this.Store_Phone,
            this.Store_Address,
            this.Store_Name});
            this.layoutView1.GridControl = this.gridControl1;
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.TemplateCard = this.layoutViewCard2;
            // 
            // Store_ID
            // 
            this.Store_ID.Caption = "كود";
            this.Store_ID.LayoutViewField = this.layoutViewField_Store_ID;
            this.Store_ID.Name = "Store_ID";
            // 
            // layoutViewField_Store_ID
            // 
            this.layoutViewField_Store_ID.EditorPreferredWidth = 159;
            this.layoutViewField_Store_ID.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_Store_ID.Name = "layoutViewField_Store_ID";
            this.layoutViewField_Store_ID.Size = new System.Drawing.Size(202, 24);
            this.layoutViewField_Store_ID.TextSize = new System.Drawing.Size(36, 13);
            // 
            // Store_Phone
            // 
            this.Store_Phone.Caption = "التلفون";
            this.Store_Phone.LayoutViewField = this.layoutViewField_Store_Phone;
            this.Store_Phone.Name = "Store_Phone";
            // 
            // layoutViewField_Store_Phone
            // 
            this.layoutViewField_Store_Phone.EditorPreferredWidth = 159;
            this.layoutViewField_Store_Phone.Location = new System.Drawing.Point(0, 24);
            this.layoutViewField_Store_Phone.Name = "layoutViewField_Store_Phone";
            this.layoutViewField_Store_Phone.Size = new System.Drawing.Size(202, 24);
            this.layoutViewField_Store_Phone.TextSize = new System.Drawing.Size(36, 13);
            // 
            // Store_Address
            // 
            this.Store_Address.Caption = "العنوان";
            this.Store_Address.LayoutViewField = this.layoutViewField_Store_Address;
            this.Store_Address.Name = "Store_Address";
            // 
            // layoutViewField_Store_Address
            // 
            this.layoutViewField_Store_Address.EditorPreferredWidth = 159;
            this.layoutViewField_Store_Address.Location = new System.Drawing.Point(0, 48);
            this.layoutViewField_Store_Address.Name = "layoutViewField_Store_Address";
            this.layoutViewField_Store_Address.Size = new System.Drawing.Size(202, 24);
            this.layoutViewField_Store_Address.TextSize = new System.Drawing.Size(36, 13);
            // 
            // Store_Name
            // 
            this.Store_Name.Caption = "المخزن";
            this.Store_Name.LayoutViewField = this.layoutViewField_Store_Name;
            this.Store_Name.Name = "Store_Name";
            // 
            // layoutViewField_Store_Name
            // 
            this.layoutViewField_Store_Name.EditorPreferredWidth = 159;
            this.layoutViewField_Store_Name.Location = new System.Drawing.Point(0, 72);
            this.layoutViewField_Store_Name.Name = "layoutViewField_Store_Name";
            this.layoutViewField_Store_Name.Size = new System.Drawing.Size(202, 24);
            this.layoutViewField_Store_Name.TextSize = new System.Drawing.Size(36, 13);
            // 
            // layoutViewCard2
            // 
            this.layoutViewCard2.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_Store_ID,
            this.layoutViewField_Store_Phone,
            this.layoutViewField_Store_Address,
            this.layoutViewField_Store_Name});
            this.layoutViewCard2.Name = "layoutViewTemplateCard";
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Name = "layoutViewCard1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "كود";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 498);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Store_ID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Store_Phone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Store_Address)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Store_Name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Views.Layout.LayoutView layoutView1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn Store_ID;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_Store_ID;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn Store_Phone;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_Store_Phone;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn Store_Address;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_Store_Address;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn Store_Name;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_Store_Name;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard2;
    }
}