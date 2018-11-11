namespace MainSystem
{
    partial class AccountStatement
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
            this.labTotalPaid = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.labTotalBillCost = new System.Windows.Forms.Label();
            this.labTotalReturnCost = new System.Windows.Forms.Label();
            this.labRest = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panHeader = new System.Windows.Forms.Panel();
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.dateTimeTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panFooter = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.btnTaswaya = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.labTotalReturn2 = new System.Windows.Forms.Label();
            this.labRest2 = new System.Windows.Forms.Label();
            this.labSafay = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnBill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type_Buy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panHeader.SuspendLayout();
            this.panFooter.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labTotalPaid
            // 
            this.labTotalPaid.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labTotalPaid.AutoSize = true;
            this.labTotalPaid.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalPaid.Location = new System.Drawing.Point(860, 13);
            this.labTotalPaid.Name = "labTotalPaid";
            this.labTotalPaid.Size = new System.Drawing.Size(0, 17);
            this.labTotalPaid.TabIndex = 168;
            // 
            // dataGridView2
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.ColumnHeadersHeight = 40;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Type,
            this.dataGridViewTextBoxColumn6});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 351);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView2.RowHeadersWidth = 80;
            this.dataGridView2.Size = new System.Drawing.Size(970, 191);
            this.dataGridView2.TabIndex = 167;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.labTotalBillCost);
            this.panel1.Controls.Add(this.labTotalReturnCost);
            this.panel1.Controls.Add(this.labRest);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 311);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(970, 40);
            this.panel1.TabIndex = 162;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(270, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 16);
            this.label6.TabIndex = 166;
            this.label6.Text = "الباقي";
            // 
            // labTotalBillCost
            // 
            this.labTotalBillCost.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labTotalBillCost.AutoSize = true;
            this.labTotalBillCost.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalBillCost.Location = new System.Drawing.Point(862, 9);
            this.labTotalBillCost.Name = "labTotalBillCost";
            this.labTotalBillCost.Size = new System.Drawing.Size(0, 17);
            this.labTotalBillCost.TabIndex = 163;
            // 
            // labTotalReturnCost
            // 
            this.labTotalReturnCost.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labTotalReturnCost.AutoSize = true;
            this.labTotalReturnCost.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalReturnCost.Location = new System.Drawing.Point(768, 9);
            this.labTotalReturnCost.Name = "labTotalReturnCost";
            this.labTotalReturnCost.Size = new System.Drawing.Size(0, 17);
            this.labTotalReturnCost.TabIndex = 164;
            // 
            // labRest
            // 
            this.labRest.AutoSize = true;
            this.labRest.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labRest.Location = new System.Drawing.Point(168, 10);
            this.labRest.Name = "labRest";
            this.labRest.Size = new System.Drawing.Size(0, 17);
            this.labRest.TabIndex = 165;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Bill,
            this.ReturnBill,
            this.BillNumber,
            this.Type_Buy,
            this.Date});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 120);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.RowHeadersWidth = 80;
            this.dataGridView1.Size = new System.Drawing.Size(970, 191);
            this.dataGridView1.TabIndex = 161;
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.SystemColors.ControlLightLight;
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
            this.panHeader.Controls.Add(this.dateTimeTo);
            this.panHeader.Controls.Add(this.dateTimeFrom);
            this.panHeader.Controls.Add(this.label2);
            this.panHeader.Controls.Add(this.label3);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(970, 120);
            this.panHeader.TabIndex = 0;
            // 
            // txtClientID
            // 
            this.txtClientID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtClientID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtClientID.Location = new System.Drawing.Point(583, 72);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(48, 24);
            this.txtClientID.TabIndex = 172;
            this.txtClientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCustomerID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCustomerID.Location = new System.Drawing.Point(583, 41);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Size = new System.Drawing.Size(48, 24);
            this.txtCustomerID.TabIndex = 171;
            this.txtCustomerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // radDealer
            // 
            this.radDealer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radDealer.AutoSize = true;
            this.radDealer.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDealer.Location = new System.Drawing.Point(617, 15);
            this.radDealer.Name = "radDealer";
            this.radDealer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radDealer.Size = new System.Drawing.Size(46, 20);
            this.radDealer.TabIndex = 170;
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
            this.comEngCon.Location = new System.Drawing.Point(638, 41);
            this.comEngCon.Name = "comEngCon";
            this.comEngCon.Size = new System.Drawing.Size(173, 24);
            this.comEngCon.TabIndex = 168;
            this.comEngCon.Visible = false;
            this.comEngCon.SelectedValueChanged += new System.EventHandler(this.comEngCon_SelectedValueChanged);
            // 
            // comClient
            // 
            this.comClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comClient.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comClient.FormattingEnabled = true;
            this.comClient.Location = new System.Drawing.Point(638, 72);
            this.comClient.Name = "comClient";
            this.comClient.Size = new System.Drawing.Size(173, 24);
            this.comClient.TabIndex = 163;
            this.comClient.Visible = false;
            this.comClient.SelectedIndexChanged += new System.EventHandler(this.comClient_SelectedValueChanged);
            // 
            // labelClient
            // 
            this.labelClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClient.Location = new System.Drawing.Point(817, 76);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(35, 16);
            this.labelClient.TabIndex = 164;
            this.labelClient.Text = "عميل";
            this.labelClient.Visible = false;
            // 
            // radCon
            // 
            this.radCon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radCon.AutoSize = true;
            this.radCon.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCon.Location = new System.Drawing.Point(672, 15);
            this.radCon.Name = "radCon";
            this.radCon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radCon.Size = new System.Drawing.Size(59, 20);
            this.radCon.TabIndex = 165;
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
            this.radEng.Location = new System.Drawing.Point(737, 15);
            this.radEng.Name = "radEng";
            this.radEng.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radEng.Size = new System.Drawing.Size(65, 20);
            this.radEng.TabIndex = 166;
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
            this.radClient.Location = new System.Drawing.Point(814, 15);
            this.radClient.Name = "radClient";
            this.radClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radClient.Size = new System.Drawing.Size(53, 20);
            this.radClient.TabIndex = 167;
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
            this.labelEng.Location = new System.Drawing.Point(815, 45);
            this.labelEng.Name = "labelEng";
            this.labelEng.Size = new System.Drawing.Size(110, 16);
            this.labelEng.TabIndex = 169;
            this.labelEng.Text = "مهندس/مقاول/تاجر";
            this.labelEng.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(49, 41);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(110, 42);
            this.btnSearch.TabIndex = 162;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dateTimeTo
            // 
            this.dateTimeTo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimeTo.Location = new System.Drawing.Point(211, 68);
            this.dateTimeTo.Name = "dateTimeTo";
            this.dateTimeTo.Size = new System.Drawing.Size(200, 20);
            this.dateTimeTo.TabIndex = 157;
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimeFrom.Location = new System.Drawing.Point(211, 32);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.Size = new System.Drawing.Size(200, 20);
            this.dateTimeFrom.TabIndex = 158;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(426, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 159;
            this.label2.Text = "من";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(425, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 16);
            this.label3.TabIndex = 160;
            this.label3.Text = "الي";
            // 
            // panFooter
            // 
            this.panFooter.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panFooter.Controls.Add(this.label9);
            this.panFooter.Controls.Add(this.labTotalPaid);
            this.panFooter.Controls.Add(this.btnTaswaya);
            this.panFooter.Controls.Add(this.label8);
            this.panFooter.Controls.Add(this.labTotalReturn2);
            this.panFooter.Controls.Add(this.labRest2);
            this.panFooter.Controls.Add(this.labSafay);
            this.panFooter.Controls.Add(this.label1);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panFooter.Location = new System.Drawing.Point(0, 542);
            this.panFooter.Margin = new System.Windows.Forms.Padding(0);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(970, 150);
            this.panFooter.TabIndex = 168;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(532, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 16);
            this.label9.TabIndex = 173;
            this.label9.Text = "الصافي";
            // 
            // btnTaswaya
            // 
            this.btnTaswaya.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnTaswaya.FlatAppearance.BorderSize = 0;
            this.btnTaswaya.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaswaya.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaswaya.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnTaswaya.Location = new System.Drawing.Point(24, 98);
            this.btnTaswaya.Name = "btnTaswaya";
            this.btnTaswaya.Size = new System.Drawing.Size(111, 40);
            this.btnTaswaya.TabIndex = 177;
            this.btnTaswaya.Text = "عمل تسوية";
            this.btnTaswaya.UseVisualStyleBackColor = false;
            this.btnTaswaya.Click += new System.EventHandler(this.btnTaswaya_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 20F);
            this.label8.Location = new System.Drawing.Point(221, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(630, 33);
            this.label8.TabIndex = 172;
            this.label8.Text = "_________________________________________";
            // 
            // labTotalReturn2
            // 
            this.labTotalReturn2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labTotalReturn2.AutoSize = true;
            this.labTotalReturn2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalReturn2.Location = new System.Drawing.Point(771, 13);
            this.labTotalReturn2.Name = "labTotalReturn2";
            this.labTotalReturn2.Size = new System.Drawing.Size(0, 17);
            this.labTotalReturn2.TabIndex = 169;
            // 
            // labRest2
            // 
            this.labRest2.AutoSize = true;
            this.labRest2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labRest2.Location = new System.Drawing.Point(162, 22);
            this.labRest2.Name = "labRest2";
            this.labRest2.Size = new System.Drawing.Size(0, 17);
            this.labRest2.TabIndex = 170;
            // 
            // labSafay
            // 
            this.labSafay.AutoSize = true;
            this.labSafay.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labSafay.Location = new System.Drawing.Point(439, 90);
            this.labSafay.Name = "labSafay";
            this.labSafay.Size = new System.Drawing.Size(0, 17);
            this.labSafay.TabIndex = 174;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(270, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 171;
            this.label1.Text = "الباقي";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panFooter, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(970, 692);
            this.tableLayoutPanel1.TabIndex = 178;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "السدادات";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "سداد مرتد";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "رقم فاتورة السداد";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Type.HeaderText = "النوع";
            this.Type.Name = "Type";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "تاريخ";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // Bill
            // 
            this.Bill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Bill.HeaderText = "فواتير";
            this.Bill.Name = "Bill";
            // 
            // ReturnBill
            // 
            this.ReturnBill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ReturnBill.HeaderText = "المرتجعات";
            this.ReturnBill.Name = "ReturnBill";
            // 
            // BillNumber
            // 
            this.BillNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BillNumber.HeaderText = "رقم الفاتورة";
            this.BillNumber.Name = "BillNumber";
            // 
            // Type_Buy
            // 
            this.Type_Buy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Type_Buy.HeaderText = "النوع";
            this.Type_Buy.Name = "Type_Buy";
            // 
            // Date
            // 
            this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Date.HeaderText = "تاريخ";
            this.Date.Name = "Date";
            // 
            // AccountStatement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 692);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "AccountStatement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "customerBills";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AccountStatement_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panFooter.ResumeLayout(false);
            this.panFooter.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labTotalPaid;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labTotalBillCost;
        private System.Windows.Forms.Label labTotalReturnCost;
        private System.Windows.Forms.Label labRest;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panHeader;
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
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dateTimeTo;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnTaswaya;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labTotalReturn2;
        private System.Windows.Forms.Label labRest2;
        private System.Windows.Forms.Label labSafay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bill;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReturnBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type_Buy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
    }
}