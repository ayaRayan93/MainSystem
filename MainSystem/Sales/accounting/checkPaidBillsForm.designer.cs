namespace MainSystem
{
    partial class checkPaidBillsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labRecivedMoney = new System.Windows.Forms.Label();
            this.btnDone = new System.Windows.Forms.Button();
            this.comClientName = new System.Windows.Forms.ComboBox();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panHeader = new System.Windows.Forms.Panel();
            this.panFooter = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panHeader.SuspendLayout();
            this.panFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeight = 30;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(50, 80);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(50, 0, 50, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.Size = new System.Drawing.Size(734, 447);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // labRecivedMoney
            // 
            this.labRecivedMoney.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labRecivedMoney.AutoSize = true;
            this.labRecivedMoney.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labRecivedMoney.Location = new System.Drawing.Point(506, 34);
            this.labRecivedMoney.Name = "labRecivedMoney";
            this.labRecivedMoney.Size = new System.Drawing.Size(0, 17);
            this.labRecivedMoney.TabIndex = 1;
            // 
            // btnDone
            // 
            this.btnDone.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDone.FlatAppearance.BorderSize = 0;
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDone.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDone.Location = new System.Drawing.Point(225, 20);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(85, 31);
            this.btnDone.TabIndex = 2;
            this.btnDone.Text = "تم";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // comClientName
            // 
            this.comClientName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comClientName.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comClientName.FormattingEnabled = true;
            this.comClientName.Location = new System.Drawing.Point(354, 27);
            this.comClientName.Name = "comClientName";
            this.comClientName.Size = new System.Drawing.Size(157, 24);
            this.comClientName.TabIndex = 127;
            this.comClientName.SelectedValueChanged += new System.EventHandler(this.comClientName_SelectedValueChanged);
            // 
            // txtClientID
            // 
            this.txtClientID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtClientID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtClientID.Location = new System.Drawing.Point(295, 27);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(53, 24);
            this.txtClientID.TabIndex = 128;
            this.txtClientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClientID_KeyDown);
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
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(834, 608);
            this.tableLayoutPanel1.TabIndex = 129;
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panHeader.Controls.Add(this.comClientName);
            this.panHeader.Controls.Add(this.txtClientID);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(834, 80);
            this.panHeader.TabIndex = 1;
            // 
            // panFooter
            // 
            this.panFooter.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panFooter.Controls.Add(this.label1);
            this.panFooter.Controls.Add(this.btnDone);
            this.panFooter.Controls.Add(this.labRecivedMoney);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panFooter.Location = new System.Drawing.Point(0, 527);
            this.panFooter.Margin = new System.Windows.Forms.Padding(0);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(834, 81);
            this.panFooter.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(587, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "الاجمالي";
            // 
            // checkPaidBillsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(834, 608);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "checkPaidBillsForm";
            this.Text = "checkPaidBillsForm";
            this.Load += new System.EventHandler(this.checkPaidBillsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panFooter.ResumeLayout(false);
            this.panFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labRecivedMoney;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.ComboBox comClientName;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.Label label1;
    }
}