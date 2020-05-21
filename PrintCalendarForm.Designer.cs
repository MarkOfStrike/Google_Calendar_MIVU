namespace Google_Calendar_Desktop_App
{
    partial class PrintCalendarForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintCalendarForm));
            this.NextBtn = new System.Windows.Forms.Button();
            this.PrevBtn = new System.Windows.Forms.Button();
            this.countPage = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.selectPage = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.printBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.calendarImg = new System.Windows.Forms.PictureBox();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.setupPageForPrint = new System.Windows.Forms.PageSetupDialog();
            this.previewDocument = new System.Windows.Forms.PrintPreviewDialog();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calendarImg)).BeginInit();
            this.SuspendLayout();
            // 
            // NextBtn
            // 
            this.NextBtn.Location = new System.Drawing.Point(104, 214);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(75, 23);
            this.NextBtn.TabIndex = 19;
            this.NextBtn.Text = ">";
            this.NextBtn.UseVisualStyleBackColor = true;
            this.NextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // PrevBtn
            // 
            this.PrevBtn.Location = new System.Drawing.Point(12, 214);
            this.PrevBtn.Name = "PrevBtn";
            this.PrevBtn.Size = new System.Drawing.Size(75, 23);
            this.PrevBtn.TabIndex = 18;
            this.PrevBtn.Text = "<";
            this.PrevBtn.UseVisualStyleBackColor = true;
            this.PrevBtn.Click += new System.EventHandler(this.PrevBtn_Click);
            // 
            // countPage
            // 
            this.countPage.AutoSize = true;
            this.countPage.Location = new System.Drawing.Point(124, 187);
            this.countPage.Name = "countPage";
            this.countPage.Size = new System.Drawing.Size(13, 13);
            this.countPage.TabIndex = 17;
            this.countPage.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(106, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "/";
            // 
            // selectPage
            // 
            this.selectPage.AutoSize = true;
            this.selectPage.Location = new System.Drawing.Point(87, 187);
            this.selectPage.Name = "selectPage";
            this.selectPage.Size = new System.Drawing.Size(13, 13);
            this.selectPage.TabIndex = 15;
            this.selectPage.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Страница:";
            // 
            // printBtn
            // 
            this.printBtn.Location = new System.Drawing.Point(50, 128);
            this.printBtn.Name = "printBtn";
            this.printBtn.Size = new System.Drawing.Size(112, 23);
            this.printBtn.TabIndex = 13;
            this.printBtn.Text = "Печать";
            this.printBtn.UseVisualStyleBackColor = true;
            this.printBtn.Click += new System.EventHandler(this.printBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.endDate);
            this.groupBox2.Controls.Add(this.startDate);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(188, 97);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Диапозон дат";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "до";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "от";
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(36, 57);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(131, 20);
            this.endDate.TabIndex = 1;
            this.endDate.Value = new System.DateTime(2020, 5, 15, 0, 0, 0, 0);
            this.endDate.ValueChanged += new System.EventHandler(this.endDate_ValueChanged);
            // 
            // startDate
            // 
            this.startDate.Location = new System.Drawing.Point(36, 31);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(131, 20);
            this.startDate.TabIndex = 0;
            this.startDate.ValueChanged += new System.EventHandler(this.startDate_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.calendarImg);
            this.groupBox1.Location = new System.Drawing.Point(206, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(898, 658);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Предварительный просмотр";
            // 
            // calendarImg
            // 
            this.calendarImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendarImg.Location = new System.Drawing.Point(3, 16);
            this.calendarImg.Name = "calendarImg";
            this.calendarImg.Size = new System.Drawing.Size(892, 639);
            this.calendarImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.calendarImg.TabIndex = 0;
            this.calendarImg.TabStop = false;
            // 
            // setupPageForPrint
            // 
            this.setupPageForPrint.Document = this.printDoc;
            // 
            // previewDocument
            // 
            this.previewDocument.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.previewDocument.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.previewDocument.ClientSize = new System.Drawing.Size(400, 300);
            this.previewDocument.Document = this.printDoc;
            this.previewDocument.Enabled = true;
            this.previewDocument.Icon = ((System.Drawing.Icon)(resources.GetObject("previewDocument.Icon")));
            this.previewDocument.Name = "printPreviewDialog1";
            this.previewDocument.ShowIcon = false;
            this.previewDocument.Visible = false;
            // 
            // PrintCalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 711);
            this.Controls.Add(this.NextBtn);
            this.Controls.Add(this.PrevBtn);
            this.Controls.Add(this.countPage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.selectPage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.printBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PrintCalendarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrintCalendarForm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.calendarImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NextBtn;
        private System.Windows.Forms.Button PrevBtn;
        private System.Windows.Forms.Label countPage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label selectPage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button printBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox calendarImg;
        private System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.PageSetupDialog setupPageForPrint;
        private System.Windows.Forms.PrintPreviewDialog previewDocument;
    }
}