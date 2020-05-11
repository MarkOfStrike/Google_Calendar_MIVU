namespace Google_Calendar_Desktop_App
{
    partial class InfoEvent
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
            this.canelMod = new System.Windows.Forms.Button();
            this.modEvent = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.delEvent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CalendarsForEvents
            // 
            this.CalendarsForEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CalendarsForEvents.FormattingEnabled = true;
            this.CalendarsForEvents.Location = new System.Drawing.Point(193, 437);
            this.CalendarsForEvents.Name = "CalendarsForEvents";
            this.CalendarsForEvents.Size = new System.Drawing.Size(207, 21);
            this.CalendarsForEvents.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "——";
            // 
            // eventEnd
            // 
            this.eventEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.eventEnd.Location = new System.Drawing.Point(317, 100);
            this.eventEnd.Name = "eventEnd";
            this.eventEnd.Size = new System.Drawing.Size(91, 20);
            this.eventEnd.TabIndex = 31;
            // 
            // eventStart
            // 
            this.eventStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.eventStart.Location = new System.Drawing.Point(192, 100);
            this.eventStart.Name = "eventStart";
            this.eventStart.Size = new System.Drawing.Size(94, 20);
            this.eventStart.TabIndex = 30;
            // 
            // eventSummary
            // 
            this.eventSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventSummary.Location = new System.Drawing.Point(192, 65);
            this.eventSummary.Name = "eventSummary";
            this.eventSummary.Size = new System.Drawing.Size(306, 20);
            this.eventSummary.TabIndex = 29;
            // 
            // eventAttendees
            // 
            this.eventAttendees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventAttendees.Location = new System.Drawing.Point(192, 283);
            this.eventAttendees.Name = "eventAttendees";
            this.eventAttendees.Size = new System.Drawing.Size(305, 105);
            this.eventAttendees.TabIndex = 28;
            this.eventAttendees.Text = "";
            // 
            // eventLocation
            // 
            this.eventLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventLocation.Location = new System.Drawing.Point(193, 406);
            this.eventLocation.Name = "eventLocation";
            this.eventLocation.Size = new System.Drawing.Size(207, 20);
            this.eventLocation.TabIndex = 27;
            // 
            // eventDescription
            // 
            this.eventDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventDescription.Location = new System.Drawing.Point(192, 146);
            this.eventDescription.Multiline = true;
            this.eventDescription.Name = "eventDescription";
            this.eventDescription.Size = new System.Drawing.Size(305, 120);
            this.eventDescription.TabIndex = 26;
            // 
            // canelMod
            // 
            this.canelMod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.canelMod.BackColor = System.Drawing.SystemColors.ControlLight;
            this.canelMod.Location = new System.Drawing.Point(407, 464);
            this.canelMod.Name = "canelMod";
            this.canelMod.Size = new System.Drawing.Size(91, 23);
            this.canelMod.TabIndex = 25;
            this.canelMod.Text = "Отмена";
            this.canelMod.UseVisualStyleBackColor = false;
            this.canelMod.Click += new System.EventHandler(this.canelMod_Click);
            // 
            // modEvent
            // 
            this.modEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modEvent.Location = new System.Drawing.Point(407, 435);
            this.modEvent.Name = "modEvent";
            this.modEvent.Size = new System.Drawing.Size(91, 23);
            this.modEvent.TabIndex = 24;
            this.modEvent.Text = "Изменить";
            this.modEvent.UseVisualStyleBackColor = true;
            this.modEvent.Click += new System.EventHandler(this.modEvent_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(122, 437);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Календарь:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(126, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Описание:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(145, 406);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Место:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Гости:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Дата:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Название:";
            // 
            // delEvent
            // 
            this.delEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.delEvent.Location = new System.Drawing.Point(407, 406);
            this.delEvent.Name = "delEvent";
            this.delEvent.Size = new System.Drawing.Size(91, 23);
            this.delEvent.TabIndex = 17;
            this.delEvent.Text = "Удалить";
            this.delEvent.UseVisualStyleBackColor = true;
            this.delEvent.Click += new System.EventHandler(this.delEvent_Click);
            // 
            // InfoEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 539);
            this.Controls.Add(this.CalendarsForEvents);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eventEnd);
            this.Controls.Add(this.eventStart);
            this.Controls.Add(this.eventSummary);
            this.Controls.Add(this.eventAttendees);
            this.Controls.Add(this.eventLocation);
            this.Controls.Add(this.eventDescription);
            this.Controls.Add(this.canelMod);
            this.Controls.Add(this.modEvent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.delEvent);
            this.Name = "InfoEvent";
            this.Text = "InfoEvent";
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
        private System.Windows.Forms.Button canelMod;
        private System.Windows.Forms.Button modEvent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button delEvent;
    }
}