namespace Passificator
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.editStaffButton = new System.Windows.Forms.Button();
            this.senderPositionTextBox = new System.Windows.Forms.TextBox();
            this.addresseePositionTextBox = new System.Windows.Forms.TextBox();
            this.senderNameComboBox = new System.Windows.Forms.ComboBox();
            this.addresseeNameComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timeOfVisitTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.toWhomTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.escortTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.reasonTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.multipleDaysVisitRadioButton = new System.Windows.Forms.RadioButton();
            this.oneDayVisitRadioButton = new System.Windows.Forms.RadioButton();
            this.visitDateToPicker = new System.Windows.Forms.DateTimePicker();
            this.visitDateFromPicker = new System.Windows.Forms.DateTimePicker();
            this.visitDatePicker = new System.Windows.Forms.DateTimePicker();
            this.guestsDataGrid = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.addGuestButton = new System.Windows.Forms.Button();
            this.guestNameComboBox = new System.Windows.Forms.ComboBox();
            this.generateButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guestsDataGrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.editStaffButton);
            this.groupBox1.Controls.Add(this.senderPositionTextBox);
            this.groupBox1.Controls.Add(this.addresseePositionTextBox);
            this.groupBox1.Controls.Add(this.senderNameComboBox);
            this.groupBox1.Controls.Add(this.addresseeNameComboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(549, 109);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Note";
            // 
            // editStaffButton
            // 
            this.editStaffButton.Location = new System.Drawing.Point(10, 80);
            this.editStaffButton.Name = "editStaffButton";
            this.editStaffButton.Size = new System.Drawing.Size(33, 23);
            this.editStaffButton.TabIndex = 4;
            this.editStaffButton.Text = "+";
            this.editStaffButton.UseVisualStyleBackColor = true;
            this.editStaffButton.Click += new System.EventHandler(this.editStaffButton_Click);
            // 
            // senderPositionTextBox
            // 
            this.senderPositionTextBox.Location = new System.Drawing.Point(264, 51);
            this.senderPositionTextBox.Name = "senderPositionTextBox";
            this.senderPositionTextBox.Size = new System.Drawing.Size(279, 20);
            this.senderPositionTextBox.TabIndex = 3;
            // 
            // addresseePositionTextBox
            // 
            this.addresseePositionTextBox.Location = new System.Drawing.Point(264, 25);
            this.addresseePositionTextBox.Name = "addresseePositionTextBox";
            this.addresseePositionTextBox.Size = new System.Drawing.Size(279, 20);
            this.addresseePositionTextBox.TabIndex = 3;
            // 
            // senderNameComboBox
            // 
            this.senderNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.senderNameComboBox.FormattingEnabled = true;
            this.senderNameComboBox.Location = new System.Drawing.Point(66, 51);
            this.senderNameComboBox.Name = "senderNameComboBox";
            this.senderNameComboBox.Size = new System.Drawing.Size(121, 21);
            this.senderNameComboBox.TabIndex = 1;
            this.senderNameComboBox.SelectedIndexChanged += new System.EventHandler(this.senderNameComboBox_SelectedIndexChanged);
            // 
            // addresseeNameComboBox
            // 
            this.addresseeNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addresseeNameComboBox.FormattingEnabled = true;
            this.addresseeNameComboBox.Location = new System.Drawing.Point(66, 25);
            this.addresseeNameComboBox.Name = "addresseeNameComboBox";
            this.addresseeNameComboBox.Size = new System.Drawing.Size(121, 21);
            this.addresseeNameComboBox.TabIndex = 1;
            this.addresseeNameComboBox.SelectedIndexChanged += new System.EventHandler(this.addresseeNameComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Должность";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(193, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Должность";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "От";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Кому";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.timeOfVisitTextBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.toWhomTextBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.escortTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.reasonTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.multipleDaysVisitRadioButton);
            this.groupBox2.Controls.Add(this.oneDayVisitRadioButton);
            this.groupBox2.Controls.Add(this.visitDateToPicker);
            this.groupBox2.Controls.Add(this.visitDateFromPicker);
            this.groupBox2.Controls.Add(this.visitDatePicker);
            this.groupBox2.Location = new System.Drawing.Point(12, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(549, 126);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Visit";
            // 
            // timeOfVisitTextBox
            // 
            this.timeOfVisitTextBox.Location = new System.Drawing.Point(97, 99);
            this.timeOfVisitTextBox.Name = "timeOfVisitTextBox";
            this.timeOfVisitTextBox.Size = new System.Drawing.Size(100, 20);
            this.timeOfVisitTextBox.TabIndex = 10;
            this.timeOfVisitTextBox.Text = "09:00-18:00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Время";
            // 
            // toWhomTextBox
            // 
            this.toWhomTextBox.Location = new System.Drawing.Point(427, 33);
            this.toWhomTextBox.Name = "toWhomTextBox";
            this.toWhomTextBox.Size = new System.Drawing.Size(100, 20);
            this.toWhomTextBox.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(424, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "К кому";
            // 
            // escortTextBox
            // 
            this.escortTextBox.Location = new System.Drawing.Point(427, 73);
            this.escortTextBox.Name = "escortTextBox";
            this.escortTextBox.Size = new System.Drawing.Size(100, 20);
            this.escortTextBox.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(424, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Сопровождающий";
            // 
            // reasonTextBox
            // 
            this.reasonTextBox.Location = new System.Drawing.Point(97, 77);
            this.reasonTextBox.Name = "reasonTextBox";
            this.reasonTextBox.Size = new System.Drawing.Size(303, 20);
            this.reasonTextBox.TabIndex = 4;
            this.reasonTextBox.Text = "участия в совещании";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Цель: для...";
            // 
            // multipleDaysVisitRadioButton
            // 
            this.multipleDaysVisitRadioButton.AutoSize = true;
            this.multipleDaysVisitRadioButton.Location = new System.Drawing.Point(13, 54);
            this.multipleDaysVisitRadioButton.Name = "multipleDaysVisitRadioButton";
            this.multipleDaysVisitRadioButton.Size = new System.Drawing.Size(56, 17);
            this.multipleDaysVisitRadioButton.TabIndex = 2;
            this.multipleDaysVisitRadioButton.Text = "От До";
            this.multipleDaysVisitRadioButton.UseVisualStyleBackColor = true;
            // 
            // oneDayVisitRadioButton
            // 
            this.oneDayVisitRadioButton.AutoSize = true;
            this.oneDayVisitRadioButton.Checked = true;
            this.oneDayVisitRadioButton.Location = new System.Drawing.Point(13, 29);
            this.oneDayVisitRadioButton.Name = "oneDayVisitRadioButton";
            this.oneDayVisitRadioButton.Size = new System.Drawing.Size(78, 17);
            this.oneDayVisitRadioButton.TabIndex = 2;
            this.oneDayVisitRadioButton.TabStop = true;
            this.oneDayVisitRadioButton.Text = "Один день";
            this.oneDayVisitRadioButton.UseVisualStyleBackColor = true;
            this.oneDayVisitRadioButton.CheckedChanged += new System.EventHandler(this.oneDayVisitRadioButton_CheckedChanged);
            // 
            // visitDateToPicker
            // 
            this.visitDateToPicker.CustomFormat = "dd.MM.yyyy";
            this.visitDateToPicker.Enabled = false;
            this.visitDateToPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.visitDateToPicker.Location = new System.Drawing.Point(264, 51);
            this.visitDateToPicker.Name = "visitDateToPicker";
            this.visitDateToPicker.Size = new System.Drawing.Size(136, 20);
            this.visitDateToPicker.TabIndex = 0;
            // 
            // visitDateFromPicker
            // 
            this.visitDateFromPicker.CustomFormat = "dd.MM.yyyy";
            this.visitDateFromPicker.Enabled = false;
            this.visitDateFromPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.visitDateFromPicker.Location = new System.Drawing.Point(97, 51);
            this.visitDateFromPicker.Name = "visitDateFromPicker";
            this.visitDateFromPicker.Size = new System.Drawing.Size(136, 20);
            this.visitDateFromPicker.TabIndex = 0;
            // 
            // visitDatePicker
            // 
            this.visitDatePicker.CustomFormat = "dd.MM.yyyy";
            this.visitDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.visitDatePicker.Location = new System.Drawing.Point(97, 26);
            this.visitDatePicker.Name = "visitDatePicker";
            this.visitDatePicker.Size = new System.Drawing.Size(136, 20);
            this.visitDatePicker.TabIndex = 0;
            // 
            // guestsDataGrid
            // 
            this.guestsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.guestsDataGrid.Location = new System.Drawing.Point(6, 46);
            this.guestsDataGrid.Name = "guestsDataGrid";
            this.guestsDataGrid.Size = new System.Drawing.Size(539, 293);
            this.guestsDataGrid.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.addGuestButton);
            this.groupBox3.Controls.Add(this.guestsDataGrid);
            this.groupBox3.Controls.Add(this.guestNameComboBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 253);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(549, 334);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Гости";
            // 
            // addGuestButton
            // 
            this.addGuestButton.Location = new System.Drawing.Point(458, 19);
            this.addGuestButton.Name = "addGuestButton";
            this.addGuestButton.Size = new System.Drawing.Size(85, 23);
            this.addGuestButton.TabIndex = 4;
            this.addGuestButton.Text = "Добавить";
            this.addGuestButton.UseVisualStyleBackColor = true;
            this.addGuestButton.Click += new System.EventHandler(this.addGuestButton_Click);
            // 
            // guestNameComboBox
            // 
            this.guestNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.guestNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.guestNameComboBox.FormattingEnabled = true;
            this.guestNameComboBox.Location = new System.Drawing.Point(6, 19);
            this.guestNameComboBox.Name = "guestNameComboBox";
            this.guestNameComboBox.Size = new System.Drawing.Size(445, 21);
            this.guestNameComboBox.TabIndex = 1;
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(12, 588);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(549, 23);
            this.generateButton.TabIndex = 5;
            this.generateButton.Text = "Генерировать";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 623);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Passificator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guestsDataGrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox addresseeNameComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker visitDatePicker;
        private System.Windows.Forms.TextBox senderPositionTextBox;
        private System.Windows.Forms.TextBox addresseePositionTextBox;
        private System.Windows.Forms.ComboBox senderNameComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton multipleDaysVisitRadioButton;
        private System.Windows.Forms.RadioButton oneDayVisitRadioButton;
        private System.Windows.Forms.DateTimePicker visitDateToPicker;
        private System.Windows.Forms.DateTimePicker visitDateFromPicker;
        private System.Windows.Forms.DataGridView guestsDataGrid;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button addGuestButton;
        private System.Windows.Forms.ComboBox guestNameComboBox;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Button editStaffButton;
        private System.Windows.Forms.TextBox reasonTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox toWhomTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox escortTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox timeOfVisitTextBox;
        private System.Windows.Forms.Label label9;
    }
}

