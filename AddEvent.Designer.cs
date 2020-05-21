namespace Google_Calendar_Desktop_App
{
    partial class AddEvent
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
            this.CalendarsForEvents = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.eventEnd = new System.Windows.Forms.DateTimePicker();
            this.eventStart = new System.Windows.Forms.DateTimePicker();
            this.eventSummary = new System.Windows.Forms.TextBox();
            this.eventAttendees = new System.Windows.Forms.RichTextBox();
            this.eventLocation = new System.Windows.Forms.TextBox();
            this.eventDescription = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.createEvent = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CalendarsForEvents
            // 
            this.CalendarsForEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CalendarsForEvents.FormattingEnabled = true;
            this.CalendarsForEvents.Location = new System.Drawing.Point(79, 378);
            this.CalendarsForEvents.Name = "CalendarsForEvents";
            this.CalendarsForEvents.Size = new System.Drawing.Size(276, 21);
            this.CalendarsForEvents.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "——";
            // 
            // eventEnd
            // 
            this.eventEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.eventEnd.Location = new System.Drawing.Point(204, 41);
            this.eventEnd.Name = "eventEnd";
            this.eventEnd.Size = new System.Drawing.Size(91, 20);
            this.eventEnd.TabIndex = 48;
            this.eventEnd.ValueChanged += new System.EventHandler(this.eventEnd_ValueChanged);
            // 
            // eventStart
            // 
            this.eventStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.eventStart.Location = new System.Drawing.Point(79, 41);
            this.eventStart.Name = "eventStart";
            this.eventStart.Size = new System.Drawing.Size(94, 20);
            this.eventStart.TabIndex = 47;
            this.eventStart.ValueChanged += new System.EventHandler(this.eventStart_ValueChanged);
            // 
            // eventSummary
            // 
            this.eventSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventSummary.Location = new System.Drawing.Point(79, 6);
            this.eventSummary.Name = "eventSummary";
            this.eventSummary.Size = new System.Drawing.Size(276, 20);
            this.eventSummary.TabIndex = 46;
            // 
            // eventAttendees
            // 
            this.eventAttendees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventAttendees.Location = new System.Drawing.Point(79, 224);
            this.eventAttendees.Name = "eventAttendees";
            this.eventAttendees.Size = new System.Drawing.Size(276, 105);
            this.eventAttendees.TabIndex = 45;
            this.eventAttendees.Text = "";
            // 
            // eventLocation
            // 
            this.eventLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventLocation.Location = new System.Drawing.Point(79, 347);
            this.eventLocation.Name = "eventLocation";
            this.eventLocation.Size = new System.Drawing.Size(276, 20);
            this.eventLocation.TabIndex = 44;
            // 
            // eventDescription
            // 
            this.eventDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventDescription.Location = new System.Drawing.Point(79, 87);
            this.eventDescription.Multiline = true;
            this.eventDescription.Name = "eventDescription";
            this.eventDescription.Size = new System.Drawing.Size(276, 120);
            this.eventDescription.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 378);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Календарь:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Описание:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 347);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Место:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Гости:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Дата:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Название:";
            // 
            // createEvent
            // 
            this.createEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.createEvent.Location = new System.Drawing.Point(11, 422);
            this.createEvent.Name = "createEvent";
            this.createEvent.Size = new System.Drawing.Size(150, 35);
            this.createEvent.TabIndex = 51;
            this.createEvent.Text = "Создать";
            this.createEvent.UseVisualStyleBackColor = true;
            this.createEvent.Click += new System.EventHandler(this.createEvent_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.closeBtn.Location = new System.Drawing.Point(168, 422);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(187, 35);
            this.closeBtn.TabIndex = 52;
            this.closeBtn.Text = "Отмена";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // AddEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 469);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.createEvent);
            this.Controls.Add(this.CalendarsForEvents);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eventEnd);
            this.Controls.Add(this.eventStart);
            this.Controls.Add(this.eventSummary);
            this.Controls.Add(this.eventAttendees);
            this.Controls.Add(this.eventLocation);
            this.Controls.Add(this.eventDescription);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление события";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CalendarsForEvents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker eventEnd;
        private System.Windows.Forms.DateTimePicker eventStart;
        private System.Windows.Forms.TextBox eventSummary;
        private System.Windows.Forms.RichTextBox eventAttendees;
        private System.Windows.Forms.TextBox eventLocation;
        private System.Windows.Forms.TextBox eventDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button createEvent;
        private System.Windows.Forms.Button closeBtn;
    }
}