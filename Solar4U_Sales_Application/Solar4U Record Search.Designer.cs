namespace Solar4U_Sales_Application
{
    partial class Solar4URecordSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Solar4URecordSearchForm));
            this.UserSearchChoiceLabel = new System.Windows.Forms.Label();
            this.SearchByOptionsListBox = new System.Windows.Forms.ListBox();
            this.SearchByOptionsPanel = new System.Windows.Forms.Panel();
            this.SearchByOptionEnterButton = new System.Windows.Forms.Button();
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.SearchByDatePanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchCriteriaDateYearTextBox = new System.Windows.Forms.TextBox();
            this.SearchCriteriaDateMonthTextBox = new System.Windows.Forms.TextBox();
            this.SearchCriteriaDateDayTextBox = new System.Windows.Forms.TextBox();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SearchResultsListBox = new System.Windows.Forms.ListBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchByTransactionNumberTextBox = new System.Windows.Forms.TextBox();
            this.BackButton = new System.Windows.Forms.Button();
            this.SearchByOptionsPanel.SuspendLayout();
            this.SearchPanel.SuspendLayout();
            this.SearchByDatePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserSearchChoiceLabel
            // 
            this.UserSearchChoiceLabel.AutoSize = true;
            this.UserSearchChoiceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserSearchChoiceLabel.Location = new System.Drawing.Point(3, 8);
            this.UserSearchChoiceLabel.Name = "UserSearchChoiceLabel";
            this.UserSearchChoiceLabel.Size = new System.Drawing.Size(332, 18);
            this.UserSearchChoiceLabel.TabIndex = 0;
            this.UserSearchChoiceLabel.Text = "How Do You Want to Search Records By? ";
            // 
            // SearchByOptionsListBox
            // 
            this.SearchByOptionsListBox.FormattingEnabled = true;
            this.SearchByOptionsListBox.ItemHeight = 16;
            this.SearchByOptionsListBox.Items.AddRange(new object[] {
            "Search By Date",
            "Search By Transaction Number"});
            this.SearchByOptionsListBox.Location = new System.Drawing.Point(6, 29);
            this.SearchByOptionsListBox.Name = "SearchByOptionsListBox";
            this.SearchByOptionsListBox.Size = new System.Drawing.Size(284, 36);
            this.SearchByOptionsListBox.TabIndex = 1;
            // 
            // SearchByOptionsPanel
            // 
            this.SearchByOptionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchByOptionsPanel.Controls.Add(this.SearchByOptionEnterButton);
            this.SearchByOptionsPanel.Controls.Add(this.UserSearchChoiceLabel);
            this.SearchByOptionsPanel.Controls.Add(this.SearchByOptionsListBox);
            this.SearchByOptionsPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchByOptionsPanel.Location = new System.Drawing.Point(223, 12);
            this.SearchByOptionsPanel.Name = "SearchByOptionsPanel";
            this.SearchByOptionsPanel.Size = new System.Drawing.Size(402, 73);
            this.SearchByOptionsPanel.TabIndex = 2;
            // 
            // SearchByOptionEnterButton
            // 
            this.SearchByOptionEnterButton.Location = new System.Drawing.Point(307, 29);
            this.SearchByOptionEnterButton.Name = "SearchByOptionEnterButton";
            this.SearchByOptionEnterButton.Size = new System.Drawing.Size(89, 36);
            this.SearchByOptionEnterButton.TabIndex = 2;
            this.SearchByOptionEnterButton.Text = "Enter";
            this.SearchByOptionEnterButton.UseVisualStyleBackColor = true;
            this.SearchByOptionEnterButton.Click += new System.EventHandler(this.SearchByOptionEnterButton_Click);
            // 
            // SearchPanel
            // 
            this.SearchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchPanel.Controls.Add(this.SearchByDatePanel);
            this.SearchPanel.Controls.Add(this.ClearButton);
            this.SearchPanel.Controls.Add(this.SearchResultsListBox);
            this.SearchPanel.Controls.Add(this.SearchButton);
            this.SearchPanel.Controls.Add(this.SearchByTransactionNumberTextBox);
            this.SearchPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchPanel.Location = new System.Drawing.Point(13, 96);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(863, 459);
            this.SearchPanel.TabIndex = 3;
            this.SearchPanel.Visible = false;
            // 
            // SearchByDatePanel
            // 
            this.SearchByDatePanel.Controls.Add(this.label2);
            this.SearchByDatePanel.Controls.Add(this.label1);
            this.SearchByDatePanel.Controls.Add(this.SearchCriteriaDateYearTextBox);
            this.SearchByDatePanel.Controls.Add(this.SearchCriteriaDateMonthTextBox);
            this.SearchByDatePanel.Controls.Add(this.SearchCriteriaDateDayTextBox);
            this.SearchByDatePanel.Location = new System.Drawing.Point(17, 23);
            this.SearchByDatePanel.Name = "SearchByDatePanel";
            this.SearchByDatePanel.Size = new System.Drawing.Size(194, 30);
            this.SearchByDatePanel.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(107, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "-";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SearchCriteriaDateYearTextBox
            // 
            this.SearchCriteriaDateYearTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchCriteriaDateYearTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchCriteriaDateYearTextBox.Location = new System.Drawing.Point(127, 0);
            this.SearchCriteriaDateYearTextBox.Name = "SearchCriteriaDateYearTextBox";
            this.SearchCriteriaDateYearTextBox.Size = new System.Drawing.Size(59, 26);
            this.SearchCriteriaDateYearTextBox.TabIndex = 6;
            // 
            // SearchCriteriaDateMonthTextBox
            // 
            this.SearchCriteriaDateMonthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchCriteriaDateMonthTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchCriteriaDateMonthTextBox.Location = new System.Drawing.Point(62, 0);
            this.SearchCriteriaDateMonthTextBox.Name = "SearchCriteriaDateMonthTextBox";
            this.SearchCriteriaDateMonthTextBox.Size = new System.Drawing.Size(39, 26);
            this.SearchCriteriaDateMonthTextBox.TabIndex = 5;
            // 
            // SearchCriteriaDateDayTextBox
            // 
            this.SearchCriteriaDateDayTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchCriteriaDateDayTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchCriteriaDateDayTextBox.Location = new System.Drawing.Point(0, 0);
            this.SearchCriteriaDateDayTextBox.Name = "SearchCriteriaDateDayTextBox";
            this.SearchCriteriaDateDayTextBox.Size = new System.Drawing.Size(39, 26);
            this.SearchCriteriaDateDayTextBox.TabIndex = 4;
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(406, 394);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(94, 39);
            this.ClearButton.TabIndex = 3;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // SearchResultsListBox
            // 
            this.SearchResultsListBox.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchResultsListBox.FormattingEnabled = true;
            this.SearchResultsListBox.ItemHeight = 18;
            this.SearchResultsListBox.Location = new System.Drawing.Point(17, 60);
            this.SearchResultsListBox.Name = "SearchResultsListBox";
            this.SearchResultsListBox.Size = new System.Drawing.Size(832, 328);
            this.SearchResultsListBox.TabIndex = 2;
            this.SearchResultsListBox.Visible = false;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(237, 16);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(94, 41);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchByTransactionNumberTextBox
            // 
            this.SearchByTransactionNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchByTransactionNumberTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchByTransactionNumberTextBox.Location = new System.Drawing.Point(17, 23);
            this.SearchByTransactionNumberTextBox.Name = "SearchByTransactionNumberTextBox";
            this.SearchByTransactionNumberTextBox.Size = new System.Drawing.Size(194, 26);
            this.SearchByTransactionNumberTextBox.TabIndex = 0;
            this.SearchByTransactionNumberTextBox.Visible = false;
            // 
            // BackButton
            // 
            this.BackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackButton.Location = new System.Drawing.Point(128, 49);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(89, 36);
            this.BackButton.TabIndex = 4;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // Solar4URecordSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(887, 567);
            this.Controls.Add(this.SearchPanel);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.SearchByOptionsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Solar4URecordSearchForm";
            this.Text = "Solar4U Record Search";
            this.SearchByOptionsPanel.ResumeLayout(false);
            this.SearchByOptionsPanel.PerformLayout();
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            this.SearchByDatePanel.ResumeLayout(false);
            this.SearchByDatePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label UserSearchChoiceLabel;
        private System.Windows.Forms.ListBox SearchByOptionsListBox;
        private System.Windows.Forms.Panel SearchByOptionsPanel;
        private System.Windows.Forms.Panel SearchPanel;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox SearchByTransactionNumberTextBox;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.ListBox SearchResultsListBox;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.TextBox SearchCriteriaDateDayTextBox;
        private System.Windows.Forms.TextBox SearchCriteriaDateMonthTextBox;
        private System.Windows.Forms.TextBox SearchCriteriaDateYearTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SearchByOptionEnterButton;
        private System.Windows.Forms.Panel SearchByDatePanel;
    }
}