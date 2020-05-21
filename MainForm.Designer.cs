using System;

namespace Google_Calendar_Desktop_App
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.updEvent = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.connectStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.printCal = new System.Windows.Forms.Button();
            this.switchingConBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.upcomingEvents = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createEvent = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.monthOfYear = new System.Windows.Forms.Label();
            this.calendarPage = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nextMonth = new System.Windows.Forms.Button();
            this.prevMonth = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.eventSelectDay = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calendarsItem = new System.Windows.Forms.CheckedListBox();
            this.miniCalendar = new System.Windows.Forms.MonthCalendar();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upcomingEvents)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calendarPage)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventSelectDay)).BeginInit();
            this.SuspendLayout();
            // 
            // updEvent
            // 
            this.updEvent.Enabled = true;
            this.updEvent.Interval = 3000;
            this.updEvent.Tick += new System.EventHandler(this.updEvent_Tick);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.FillWeight = 45.68528F;
            this.dataGridViewTextBoxColumn6.HeaderText = "№";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 37;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.FillWeight = 127.1574F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Заголовок";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 104;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.FillWeight = 127.1574F;
            this.dataGridViewTextBoxColumn8.HeaderText = "Дата";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 103;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "IdCal";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "IdEv";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 705);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1033, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // connectStatus
            // 
            this.connectStatus.Image = ((System.Drawing.Image)(resources.GetObject("connectStatus.Image")));
            this.connectStatus.Name = "connectStatus";
            this.connectStatus.Size = new System.Drawing.Size(134, 17);
            this.connectStatus.Text = "toolStripStatusLabel1";
            // 
            // printCal
            // 
            this.printCal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.printCal.Location = new System.Drawing.Point(681, 661);
            this.printCal.Name = "printCal";
            this.printCal.Size = new System.Drawing.Size(148, 34);
            this.printCal.TabIndex = 24;
            this.printCal.Text = "Печать календаря";
            this.printCal.UseVisualStyleBackColor = true;
            this.printCal.Click += new System.EventHandler(this.printCal_Click);
            // 
            // switchingConBtn
            // 
            this.switchingConBtn.Location = new System.Drawing.Point(835, 7);
            this.switchingConBtn.Name = "switchingConBtn";
            this.switchingConBtn.Size = new System.Drawing.Size(187, 23);
            this.switchingConBtn.TabIndex = 23;
            this.switchingConBtn.Text = "Переключение соединения";
            this.switchingConBtn.UseVisualStyleBackColor = true;
            this.switchingConBtn.Click += new System.EventHandler(this.switchingConBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(678, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Кол-во событий:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(774, 10);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(45, 20);
            this.numericUpDown1.TabIndex = 22;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.upcomingEvents);
            this.groupBox2.Location = new System.Drawing.Point(678, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(347, 619);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Предстоящие события";
            // 
            // upcomingEvents
            // 
            this.upcomingEvents.AllowUserToAddRows = false;
            this.upcomingEvents.AllowUserToDeleteRows = false;
            this.upcomingEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.upcomingEvents.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.upcomingEvents.BackgroundColor = System.Drawing.SystemColors.Control;
            this.upcomingEvents.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.upcomingEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.upcomingEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Column4,
            this.Column5});
            this.upcomingEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.upcomingEvents.Location = new System.Drawing.Point(3, 16);
            this.upcomingEvents.MultiSelect = false;
            this.upcomingEvents.Name = "upcomingEvents";
            this.upcomingEvents.ReadOnly = true;
            this.upcomingEvents.RowHeadersVisible = false;
            this.upcomingEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.upcomingEvents.Size = new System.Drawing.Size(341, 600);
            this.upcomingEvents.TabIndex = 0;
            this.upcomingEvents.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.upcomingEvents_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 45.68528F;
            this.dataGridViewTextBoxColumn1.HeaderText = "№";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 127.1574F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Заголовок";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 127.1574F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Дата";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "IdCal";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "IdEv";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Visible = false;
            // 
            // createEvent
            // 
            this.createEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.createEvent.Location = new System.Drawing.Point(835, 661);
            this.createEvent.Name = "createEvent";
            this.createEvent.Size = new System.Drawing.Size(190, 34);
            this.createEvent.TabIndex = 17;
            this.createEvent.Text = "Создать новое событие";
            this.createEvent.UseVisualStyleBackColor = true;
            this.createEvent.Click += new System.EventHandler(this.createEvent_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.monthOfYear);
            this.groupBox4.Controls.Add(this.calendarPage);
            this.groupBox4.Controls.Add(this.nextMonth);
            this.groupBox4.Controls.Add(this.prevMonth);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(660, 499);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Календарь";
            // 
            // monthOfYear
            // 
            this.monthOfYear.AutoSize = true;
            this.monthOfYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthOfYear.Location = new System.Drawing.Point(249, 23);
            this.monthOfYear.Name = "monthOfYear";
            this.monthOfYear.Size = new System.Drawing.Size(58, 24);
            this.monthOfYear.TabIndex = 3;
            this.monthOfYear.Text = "name";
            this.monthOfYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // calendarPage
            // 
            this.calendarPage.AllowUserToAddRows = false;
            this.calendarPage.AllowUserToDeleteRows = false;
            this.calendarPage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.calendarPage.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.calendarPage.BackgroundColor = System.Drawing.SystemColors.Control;
            this.calendarPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.calendarPage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.calendarPage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.calendarPage.DefaultCellStyle = dataGridViewCellStyle1;
            this.calendarPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.calendarPage.Location = new System.Drawing.Point(3, 65);
            this.calendarPage.MultiSelect = false;
            this.calendarPage.Name = "calendarPage";
            this.calendarPage.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.calendarPage.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.calendarPage.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.calendarPage.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.calendarPage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.calendarPage.Size = new System.Drawing.Size(654, 431);
            this.calendarPage.TabIndex = 0;
            this.calendarPage.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.calendarPage_CellClick);
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Понедельник";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Вторник";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Среда";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Четверг";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Пятница";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Суббота";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Воскресенье";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // nextMonth
            // 
            this.nextMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nextMonth.Location = new System.Drawing.Point(398, 25);
            this.nextMonth.Name = "nextMonth";
            this.nextMonth.Size = new System.Drawing.Size(64, 23);
            this.nextMonth.TabIndex = 2;
            this.nextMonth.Text = ">";
            this.nextMonth.UseVisualStyleBackColor = true;
            this.nextMonth.Click += new System.EventHandler(this.nextMonth_Click);
            // 
            // prevMonth
            // 
            this.prevMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prevMonth.Location = new System.Drawing.Point(179, 25);
            this.prevMonth.Name = "prevMonth";
            this.prevMonth.Size = new System.Drawing.Size(64, 23);
            this.prevMonth.TabIndex = 1;
            this.prevMonth.Text = "<";
            this.prevMonth.UseVisualStyleBackColor = true;
            this.prevMonth.Click += new System.EventHandler(this.prevMonth_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.eventSelectDay);
            this.groupBox1.Location = new System.Drawing.Point(390, 517);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 181);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "События выбранного дня";
            // 
            // eventSelectDay
            // 
            this.eventSelectDay.AllowUserToAddRows = false;
            this.eventSelectDay.AllowUserToDeleteRows = false;
            this.eventSelectDay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.eventSelectDay.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.eventSelectDay.BackgroundColor = System.Drawing.SystemColors.Control;
            this.eventSelectDay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.eventSelectDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventSelectDay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.eventSelectDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventSelectDay.Location = new System.Drawing.Point(3, 16);
            this.eventSelectDay.MultiSelect = false;
            this.eventSelectDay.Name = "eventSelectDay";
            this.eventSelectDay.ReadOnly = true;
            this.eventSelectDay.RowHeadersVisible = false;
            this.eventSelectDay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.eventSelectDay.Size = new System.Drawing.Size(276, 162);
            this.eventSelectDay.TabIndex = 0;
            this.eventSelectDay.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.eventSelectDay_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 45.68528F;
            this.Column1.HeaderText = "№";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 127.1574F;
            this.Column2.HeaderText = "Заголовок";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 127.1574F;
            this.Column3.HeaderText = "Дата";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "IdCal";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "IdEv";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // calendarsItem
            // 
            this.calendarsItem.FormattingEnabled = true;
            this.calendarsItem.Location = new System.Drawing.Point(206, 514);
            this.calendarsItem.Name = "calendarsItem";
            this.calendarsItem.Size = new System.Drawing.Size(178, 184);
            this.calendarsItem.TabIndex = 18;
            this.calendarsItem.SelectedValueChanged += new System.EventHandler(this.calendarsItem_SelectedValueChanged);
            // 
            // miniCalendar
            // 
            this.miniCalendar.Location = new System.Drawing.Point(18, 523);
            this.miniCalendar.MaxSelectionCount = 1;
            this.miniCalendar.Name = "miniCalendar";
            this.miniCalendar.TabIndex = 16;
            this.miniCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.miniCalendar_DateSelected);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 727);
            this.Controls.Add(this.printCal);
            this.Controls.Add(this.switchingConBtn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.createEvent);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.calendarsItem);
            this.Controls.Add(this.miniCalendar);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Google calendar ";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upcomingEvents)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calendarPage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventSelectDay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.Timer updEvent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel connectStatus;
        private System.Windows.Forms.Button printCal;
        private System.Windows.Forms.Button switchingConBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView upcomingEvents;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button createEvent;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label monthOfYear;
        private System.Windows.Forms.DataGridView calendarPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.Button nextMonth;
        private System.Windows.Forms.Button prevMonth;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView eventSelectDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.CheckedListBox calendarsItem;
        private System.Windows.Forms.MonthCalendar miniCalendar;
    }
}

