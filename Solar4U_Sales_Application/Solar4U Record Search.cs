using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solar4U_Sales_Application
{
    public partial class Solar4URecordSearchForm : Form
    {

        public Solar4URecordSearchForm()
        {
            InitializeComponent();
        }

        // Event handler for the 'Search By Option' button click
        private void SearchByOptionEnterButton_Click(object sender, EventArgs e)
        {
            // Check if an option is selected in the ListBox
            if (SearchByOptionsListBox.SelectedIndex != -1)
            {
                SearchPanel.Visible = true; // Display the search panel

                // If the first option (e.g., search by date) is selected
                if (SearchByOptionsListBox.SelectedIndex == 0)
                {
                    // Prepare the UI for date-based search
                    SearchResultsListBox.Items.Clear();
                    SearchResultsListBox.Visible = false;
                    SearchCriteriaDateDayTextBox.Text = string.Empty;
                    SearchCriteriaDateMonthTextBox.Text = string.Empty;
                    SearchCriteriaDateYearTextBox.Text = string.Empty;
                    SearchByDatePanel.Visible = true; // Show date input panel
                    SearchByTransactionNumberTextBox.Visible = false; // Hide transaction number input
                    SearchCriteriaDateDayTextBox.Focus(); // Focus on the day input
                }
                else // For search by transaction number
                {
                    // Prepare the UI for transaction number-based search
                    SearchResultsListBox.Items.Clear();
                    SearchResultsListBox.Visible = false;
                    SearchByTransactionNumberTextBox.Text = string.Empty;
                    SearchByDatePanel.Visible = false; // Hide date input panel
                    SearchByTransactionNumberTextBox.Visible = true; // Show transaction number input
                    SearchByTransactionNumberTextBox.Focus(); // Focus on the transaction number input
                }
            }
            else
            {
                // Display an error message if no option is selected
                MessageBox.Show("Please Select a Search-By Option", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for the 'Search' button click
        private void SearchButton_Click(object sender, EventArgs e)
        {
            // Clear previous search results
            SearchResultsListBox.Items.Clear();

            try
            {
                // Variable to store each line read from the file
                String TransactionLine;

                // Open the sales record file for reading
                StreamReader InputFile = File.OpenText("Solar4U_Sales_Record.txt");

                // If the search is by transaction number
                if (SearchByOptionsListBox.SelectedIndex == 1)
                {
                    // Check if a transaction number is provided
                    if (SearchByTransactionNumberTextBox.Text != string.Empty)
                    {
                        bool RecordFound = false; // Flag to track if a match is found

                        // Read each line until the end of the file
                        while (!InputFile.EndOfStream)
                        {
                            // If the line matches the transaction number
                            if ((TransactionLine = InputFile.ReadLine()) == SearchByTransactionNumberTextBox.Text)
                            {
                                SearchResultsListBox.Visible = true;
                                RecordFound = true;

                                // Display the transaction details in the ListBox
                                SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Transaction Number", TransactionLine));
                                SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Date Of Purchase", InputFile.ReadLine()));
                                WriteRecordToOutput(InputFile);
                            }
                        }

                        // If no matching record is found
                        if (!RecordFound)
                        {
                            SearchResultsListBox.Items.Clear();
                            SearchResultsListBox.Visible = false;
                            SearchByTransactionNumberTextBox.Focus();
                            SearchByTransactionNumberTextBox.SelectAll();
                            MessageBox.Show($"No Record Found with '{SearchByTransactionNumberTextBox.Text}' Transaction Number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // Display an error message if no transaction number is provided
                        MessageBox.Show("Please Input a Transaction Number to Search", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (SearchByOptionsListBox.SelectedIndex == 0) // Search by date
                {
                    // Validate that the user has input a complete date
                    if (SearchCriteriaDateDayTextBox.Text != string.Empty &&
                        SearchCriteriaDateMonthTextBox.Text != string.Empty &&
                        SearchCriteriaDateYearTextBox.Text != string.Empty)
                    {
                        string SearchDate = $"{SearchCriteriaDateDayTextBox.Text}-{SearchCriteriaDateMonthTextBox.Text}-{SearchCriteriaDateYearTextBox.Text}";
                        bool RecordFound = false; // Flag to track if a match is found

                        // Read each record from the file
                        while (!InputFile.EndOfStream)
                        {
                            TransactionLine = InputFile.ReadLine(); // Read transaction number
                            string DateLine = InputFile.ReadLine(); // Read date of purchase

                            // Check if the date matches the search date
                            if (DateLine == SearchDate)
                            {
                                SearchResultsListBox.Visible = true;
                                RecordFound = true;

                                // Display the transaction details
                                SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Transaction Number", TransactionLine));
                                SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Date Of Purchase", DateLine));
                                WriteRecordToOutput(InputFile);
                                SearchResultsListBox.Items.Add("------------------------------------------------------------------"); // Separator
                            }
                            else
                            {
                                // Skip the remaining lines of this record
                                for (int i = 0; i < 16; i++)
                                {
                                    string temp = InputFile.ReadLine();
                                }
                            }
                        }

                        // If no matching record is found
                        if (!RecordFound)
                        {
                            SearchResultsListBox.Items.Clear();
                            SearchResultsListBox.Visible = false;
                            SearchCriteriaDateDayTextBox.Focus();
                            SearchCriteriaDateDayTextBox.SelectAll();
                            MessageBox.Show($"No Record Found for the date '{SearchDate}'", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // Display an error message if the date is incomplete
                        MessageBox.Show("Please Input a Valid Date to Search (DD-MM-YYYY)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Display any exception messages
                MessageBox.Show(ex.Message);
            }
        }

        // Method to write a record's details to the output ListBox
        private void WriteRecordToOutput(StreamReader InputFile)
        {
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Customer Full Name", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "EIR Code", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Email ID", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Solar Panel Purchased", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Panel Size", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Num. of Panels", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Battery Option", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Inverter Cost", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Type of Installation", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Discount Received", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Total Cost of System", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Financing", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "APR for Loan", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Monthly Installments", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Interest Amount Payable", InputFile.ReadLine()));
            SearchResultsListBox.Items.Add(string.Format("{0,-30}: {1}", "Total Repayment Amount Payable", InputFile.ReadLine()));
        }

        // Event handler for the 'Clear' button click
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // Clear all input fields and reset focus
            SearchByTransactionNumberTextBox.Text = string.Empty;
            SearchCriteriaDateDayTextBox.Text = string.Empty;
            SearchCriteriaDateMonthTextBox.Text = string.Empty;
            SearchCriteriaDateYearTextBox.Text = string.Empty;
            SearchResultsListBox.Items.Clear();
            SearchResultsListBox.Visible = false;
            SearchCriteriaDateDayTextBox.Focus();
            SearchByTransactionNumberTextBox.Focus();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
      
            this.Close();
        }
    }
}
