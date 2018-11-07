namespace MainSystem
{
    partial class customerBills
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.labTotalBillCost = new System.Windows.Forms.Label();
            this.labTotalReturnCost = new System.Windows.Forms.Label();
            this.labRest = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panHeader = new System.Windows.Forms.Panel();
            this.panFooter = new System.Windows.Forms.Panel();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.radDealer = new System.Windows.Forms.RadioButton();
            this.comEngCon = new System.Windows.Forms.ComboBox();
            this.comClient = new System.Windows.Forms.ComboBox();
            this.labelClient = new System.Windows.Forms.Label();
            this.radCon = new System.Windows.Forms.RadioButton();
            this.radEng = new System.Windows.Forms.RadioButton();
            this.radClient = new System.Windows.Forms.RadioButton();
            this.labelEng = new System.Windows.Forms.Label();
            this.Bill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnBill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panHeader.SuspendLayout();
            this.panFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Bill,
            this.ReturnBill,
            this.Customer,
            this.Client});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 160);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.RowHeadersWidth = 80;
            this.dataGridView1.Size = new System.Drawing.Size(942, 335);
            this.dataGridView1.TabIndex = 161;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(227, 63);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 37);
            this.btnSearch.TabIndex = 162;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labTotalBillCost
            // 
            this.labTotalBillCost.AutoSize = true;
            this.labTotalBillCost.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalBillCost.Location = new System.Drawing.Point(796, 463);
            this.labTotalBillCost.Name = "labTotalBillCost";
            this.labTotalBillCost.Size = new System.Drawing.Size(0, 17);
            this.labTotalBillCost.TabIndex = 163;
            // 
            // labTotalReturnCost
            // 
            this.labTotalReturnCost.AutoSize = true;
            this.labTotalReturnCost.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalReturnCost.Location = new System.Drawing.Point(704, 463);
            this.labTotalReturnCost.Name = "labTotalReturnCost";
            this.labTotalReturnCost.Size = new System.Drawing.Size(0, 17);
            this.labTotalReturnCost.TabIndex = 164;
            // 
            // labRest
            // 
            this.labRest.AutoSize = true;
            this.labRest.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labRest.Location = new System.Drawing.Point(205, 14);
            this.labRest.Name = "labRest";
            this.labRest.Size = new System.Drawing.Size(0, 17);
            this.labRest.TabIndex = 165;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(295, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 16);
            this.label6.TabIndex = 166;
            this.label6.Text = "الباقي";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panFooter, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(962, 565);
            this.tableLayoutPanel1.TabIndex = 167;
            // 
            // panHeader
            // 
            this.panHeader.AutoScroll = true;
            this.panHeader.Controls.Add(this.txtClientID);
            this.panHeader.Controls.Add(this.txtCustomerID);
            this.panHeader.Controls.Add(this.radDealer);
            this.panHeader.Controls.Add(this.comEngCon);
            this.panHeader.Controls.Add(this.comClient);
            this.panHeader.Controls.Add(this.labelClient);
            this.panHeader.Controls.Add(this.radCon);
            this.panHeader.Controls.Add(this.radEng);
            this.panHeader.Controls.Add(this.radClient);
            this.panHeader.Controls.Add(this.labelEng);
            this.panHeader.Controls.Add(this.btnSearch);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panHeader.Location = new System.Drawing.Point(3, 3);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(956, 144);
            this.panHeader.TabIndex = 162;
            // 
            // panFooter
            // 
            this.panFooter.Controls.Add(this.label6);
            this.panFooter.Controls.Add(this.labRest);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panFooter.Location = new System.Drawing.Point(3, 508);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(956, 54);
            this.panFooter.TabIndex = 163;
            // 
            // txtClientID
            // 
            this.txtClientID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtClientID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtClientID.Location = new System.Drawing.Point(445, 90);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(48, 24);
            this.txtClientID.TabIndex = 182;
            this.txtClientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCustomerID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCustomerID.Location = new System.Drawing.Point(445, 59);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Size = new System.Drawing.Size(48, 24);
            this.txtCustomerID.TabIndex = 181;
            this.txtCustomerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // radDealer
            // 
            this.radDealer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radDealer.AutoSize = true;
            this.radDealer.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDealer.Location = new System.Drawing.Point(479, 33);
            this.radDealer.Name = "radDealer";
            this.radDealer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radDealer.Size = new System.Drawing.Size(46, 20);
            this.radDealer.TabIndex = 180;
            this.radDealer.TabStop = true;
            this.radDealer.Text = "تاجر";
            this.radDealer.UseVisualStyleBackColor = true;
            this.radDealer.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // comEngCon
            // 
            this.comEngCon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comEngCon.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comEngCon.FormattingEnabled = true;
            this.comEngCon.Location = new System.Drawing.Point(500, 59);
            this.comEngCon.Name = "comEngCon";
            this.comEngCon.Size = new System.Drawing.Size(173, 24);
            this.comEngCon.TabIndex = 178;
            this.comEngCon.Visible = false;
            this.comEngCon.SelectedValueChanged += new System.EventHandler(this.comEngCon_SelectedValueChanged);
            // 
            // comClient
            // 
            this.comClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comClient.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comClient.FormattingEnabled = true;
            this.comClient.Location = new System.Drawing.Point(500, 90);
            this.comClient.Name = "comClient";
            this.comClient.Size = new System.Drawing.Size(173, 24);
            this.comClient.TabIndex = 173;
            this.comClient.Visible = false;
            this.comClient.SelectedValueChanged += new System.EventHandler(this.comClient_SelectedValueChanged);
            // 
            // labelClient
            // 
            this.labelClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClient.Location = new System.Drawing.Point(679, 94);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(35, 16);
            this.labelClient.TabIndex = 174;
            this.labelClient.Text = "عميل";
            this.labelClient.Visible = false;
            // 
            // radCon
            // 
            this.radCon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radCon.AutoSize = true;
            this.radCon.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCon.Location = new System.Drawing.Point(534, 33);
            this.radCon.Name = "radCon";
            this.radCon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radCon.Size = new System.Drawing.Size(59, 20);
            this.radCon.TabIndex = 175;
            this.radCon.TabStop = true;
            this.radCon.Text = "مقاول";
            this.radCon.UseVisualStyleBackColor = true;
            this.radCon.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // radEng
            // 
            this.radEng.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radEng.AutoSize = true;
            this.radEng.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEng.Location = new System.Drawing.Point(599, 33);
            this.radEng.Name = "radEng";
            this.radEng.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radEng.Size = new System.Drawing.Size(65, 20);
            this.radEng.TabIndex = 176;
            this.radEng.TabStop = true;
            this.radEng.Text = "مهندس";
            this.radEng.UseVisualStyleBackColor = true;
            this.radEng.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // radClient
            // 
            this.radClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radClient.AutoSize = true;
            this.radClient.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radClient.Location = new System.Drawing.Point(676, 33);
            this.radClient.Name = "radClient";
            this.radClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radClient.Size = new System.Drawing.Size(53, 20);
            this.radClient.TabIndex = 177;
            this.radClient.TabStop = true;
            this.radClient.Text = "عميل";
            this.radClient.UseVisualStyleBackColor = true;
            this.radClient.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // labelEng
            // 
            this.labelEng.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelEng.AutoSize = true;
            this.labelEng.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEng.Location = new System.Drawing.Point(677, 63);
            this.labelEng.Name = "labelEng";
            this.labelEng.Size = new System.Drawing.Size(110, 16);
            this.labelEng.TabIndex = 179;
            this.labelEng.Text = "مهندس/مقاول/تاجر";
            this.labelEng.Visible = false;
            // 
            // Bill
            // 
            this.Bill.HeaderText = "فواتير";
            this.Bill.Name = "Bill";
            // 
            // ReturnBill
            // 
            this.ReturnBill.HeaderText = "المرتجعات";
            this.ReturnBill.Name = "ReturnBill";
            // 
            // Customer
            // 
            this.Customer.HeaderText = "مهندس/مقاول/تاجر";
            this.Customer.Name = "Customer";
            // 
            // Client
            // 
            this.Client.HeaderText = "عميل";
            this.Client.Name = "Client";
            // 
            // customerBills
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(962, 565);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labTotalReturnCost);
            this.Controls.Add(this.labTotalBillCost);
            this.MaximizeBox = false;
            this.Name = "customerBills";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "customerBills";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panFooter.ResumeLayout(false);
            this.panFooter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labTotalBillCost;
        private System.Windows.Forms.Label labTotalReturnCost;
        private System.Windows.Forms.Label labRest;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.TextBox txtCustomerID;
        private System.Windows.Forms.RadioButton radDealer;
        private System.Windows.Forms.ComboBox comEngCon;
        private System.Windows.Forms.ComboBox comClient;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.RadioButton radCon;
        private System.Windows.Forms.RadioButton radEng;
        private System.Windows.Forms.RadioButton radClient;
        private System.Windows.Forms.Label labelEng;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bill;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReturnBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client;
    }
}