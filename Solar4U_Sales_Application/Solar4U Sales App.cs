/* 
 * Name: Soham Tambde
 * Date: 18/11/2024
 * Description: The Application allows the Business User to first take in the order details and then take Customer Data.
 *             After confirming Customers Data and their order details, User is given a proper view of the loan options available.
 *             User can choose of the three available financing option or go with no loan needed and then confirm order. The Order 
 *             Details are saved in a text file which can be then looked up in the Search functionality of the application. Search 
 *             can be perfomed either by Transaction number or Transaction Date.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Solar4U_Sales_Application
{
    public partial class Solar4USalesAppForm : Form
    {
        // Fields to track panel and battery costs and selection
        private int NumberOfPanels;
        private decimal SolarPanelSelectedCost, CellSelectedAdjustmentRate, BatteryCost;

        // Static properties to share selected details across forms
        public static string SolarPanelSelectedName { get; private set; }
        public static string BatterySize { get; private set; }
        public static int CellSelectedSize { get; private set; }
        public static decimal InverterCost { get; private set; }
        public static bool BatteryPurchased { get; private set; } = false;
        public static bool SpecialDiscountApplied { get; private set; } = false;
        public static bool IsExpediteInstallation { get; private set; } = false;

        // Constants for solar panel pricing and naming
        private const decimal PANEL0_COST = 129.50m, PANEL1_COST = 135.00m, PANEL2_COST = 112.79m, PANEL3_COST = 149.00m, PANEL4_COST = 131.00m, PANEL5_COST = 119.00m;
        private const string PANEL0_NAME = "LONGi Solar", PANEL1_NAME = "Jinko Solar", PANEL2_NAME = "Trina Solar", PANEL3_NAME = "Canadian Solar", PANEL4_NAME = "Q-Cells Solar", PANEL5_NAME = "First Solar";

        // Constants for solar cell sizes and adjustment rates
        private const int CELL0_SIZE = 30, CELL1_SIZE = 48, CELL2_SIZE = 60, CELL3_SIZE = 72, CELL4_SIZE = 84, CELL5_SIZE = 96;
        private const decimal CELL0_ADJUSTMENT_RATE = -.25m, CELL1_ADJUSTMENT_RATE = -.15m, CELL2_ADJUSTMENT_RATE = 0.0m, CELL3_ADJUSTMENT_RATE = .15m, CELL4_ADJUSTMENT_RATE = 0.25m, CELL5_ADJUSTMENT_RATE = .40m;

        // Constants for battery options and pricing
        private const string BATTERY0_SIZE = "5 KWh", BATTERY1_SIZE = "10 KWh", BATTERY2_SIZE = "20 KWh";
        private const decimal BATTERY0_COST = 4500.00m, BATTERY1_COST = 7500.00m, BATTERY2_COST = 9500.00m;

        // Installation costs based on preference
        private const decimal STANDARD_INSTALLATION_COST = 499.00m, EXPEDITE_INSTALLATION_COST = 299.00m;

        // Special discount rate offered by Solar4U
        private const decimal S4U_SPECIAL_DISCOUNT = 0.03m;

        // Constants for inverter cost decisions
        private const int INVERTERCOSTCELLSDECIDERQUANTITY = 700; // Threshold for inverter cost
        private const decimal INVERTER_COST_NO_BATTERY_STANDARD = 650m;
        private const decimal INVERTER_COST_NO_BATTERY_LARGE = 950m;
        private const decimal INVERTER_COST_WITH_BATTERY_STANDARD = 1150m;
        private const decimal INVERTER_COST_WITH_BATTERY_LARGE = 1350m;

        // Constants and field for form size adjustments
        const int INCREMENT = 4, FORMSTARTWIDTH = 1108, FORMSTARTHEIGHT = 654, FORMEXPANDWIDTH = 1675;
        Boolean FormWidthExpanded = false;

        // Static properties for customer and order information
        public static decimal TotalCostOfOrder { get; private set; }
        public static string CustomerFirstName { get; private set; }
        public static string CustomerLastName { get; private set; }
        public static string CustomerEIRCode { get; private set; }
        public static string CustomerPhoneNumber { get; private set; }
        public static string CustomerEmailID { get; private set; }
        public static string TransactionNumber { get; private set; }
        public static string TodaysDate { get; private set; }
        public static int NumberOfSolarPanels { get; private set; }
        public static decimal DiscountValue { get; private set; } = 0m; // Holds the discount value if applied

        public Solar4USalesAppForm()
        {
            InitializeComponent();
        }           



        // Event handler for the 'Quote' button to calculate the quote based on user selections
        private void QuoteButton_Click(object sender, EventArgs e)
        {

            int SolarPanelSelectedIndex, SolarPanelSizeSelectedIndex;


            //initilizing flags
            SpecialDiscountApplied = false;
            BatteryPurchased = false;
            IsExpediteInstallation = false;


            // Ensure that both a solar panel and size are selected before proceeding
            if (SolarPanelAndPriceListBox.SelectedIndex != -1)
            {
                if (SolarPanelSizesListBox.SelectedIndex != -1)
                {
                    // Ensure the number of panels is a valid integer greater than zero
                    if (int.TryParse(NumberOfPanelsInputTextBox.Text, out NumberOfPanels))
                    {
                        if (NumberOfPanels > 0)
                        {
                            //Updating Title
                            Text = "Solar4U Sales App - Quote Details";

                            // Assign selected solar panel details (name and cost)
                            SolarPanelSelectedIndex = SolarPanelAndPriceListBox.SelectedIndex;
                            if (SolarPanelSelectedIndex == 0)
                            {
                                SolarPanelSelectedName = PANEL0_NAME;
                                SolarPanelSelectedCost = PANEL0_COST;
                            }
                            else if (SolarPanelSelectedIndex == 1)
                            {
                                SolarPanelSelectedName = PANEL1_NAME;
                                SolarPanelSelectedCost = PANEL1_COST;
                            }
                            else if (SolarPanelSelectedIndex == 2)
                            {
                                SolarPanelSelectedName = PANEL2_NAME;
                                SolarPanelSelectedCost = PANEL2_COST;
                            }
                            else if (SolarPanelSelectedIndex == 3)
                            {
                                SolarPanelSelectedName = PANEL3_NAME;
                                SolarPanelSelectedCost = PANEL3_COST;
                            }
                            else if (SolarPanelSelectedIndex == 4)
                            {
                                SolarPanelSelectedName = PANEL4_NAME;
                                SolarPanelSelectedCost = PANEL4_COST;
                            }
                            else if (SolarPanelSelectedIndex == 5)
                            {
                                SolarPanelSelectedName = PANEL5_NAME;
                                SolarPanelSelectedCost = PANEL5_COST;
                            }

                            // Assign selected cell size and its adjustment rate
                            SolarPanelSizeSelectedIndex = SolarPanelSizesListBox.SelectedIndex;
                            if (SolarPanelSizeSelectedIndex == 0)
                            {
                                CellSelectedSize = CELL0_SIZE;
                                CellSelectedAdjustmentRate = CELL0_ADJUSTMENT_RATE;
                            }
                            else if (SolarPanelSizeSelectedIndex == 1)
                            {
                                CellSelectedSize = CELL1_SIZE;
                                CellSelectedAdjustmentRate = CELL1_ADJUSTMENT_RATE;
                            }
                            else if (SolarPanelSizeSelectedIndex == 2)
                            {
                                CellSelectedSize = CELL2_SIZE;
                                CellSelectedAdjustmentRate = CELL2_ADJUSTMENT_RATE;
                            }
                            else if (SolarPanelSizeSelectedIndex == 3)
                            {
                                CellSelectedSize = CELL3_SIZE;
                                CellSelectedAdjustmentRate = CELL3_ADJUSTMENT_RATE;
                            }
                            else if (SolarPanelSizeSelectedIndex == 4)
                            {
                                CellSelectedSize = CELL4_SIZE;
                                CellSelectedAdjustmentRate = CELL4_ADJUSTMENT_RATE;
                            }
                            else if (SolarPanelSizeSelectedIndex == 5)
                            {
                                CellSelectedSize = CELL5_SIZE;
                                CellSelectedAdjustmentRate = CELL5_ADJUSTMENT_RATE;
                            }

                            // Calculate the total cost of the order based on number of panels and panel adjustment rate
                            TotalCostOfOrder = (NumberOfPanels * ((SolarPanelSelectedCost * CellSelectedAdjustmentRate) + SolarPanelSelectedCost)) + STANDARD_INSTALLATION_COST;

                            // Add battery cost if a battery is purchased
                            if (Battery5kwhRadioButton.Checked)
                            {
                                BatterySize = BATTERY0_SIZE;
                                BatteryCost = BATTERY0_COST;
                                BatteryPurchased = true;
                            }
                            else if (Battery10kwhRadioButton.Checked)
                            {
                                BatterySize = BATTERY1_SIZE;
                                BatteryCost = BATTERY1_COST;
                                BatteryPurchased = true;
                            }
                            else if (Battery20kwhRadioButton.Checked)
                            {
                                BatterySize = BATTERY2_SIZE;
                                BatteryCost = BATTERY2_COST;
                                BatteryPurchased = true;
                            }
                          

                            // Inverter cost adjustment based on number of cells and whether a battery is purchased
                            if (BatteryPurchased)
                            {
                                TotalCostOfOrder += BatteryCost;
                                if ((NumberOfPanels * CellSelectedSize) > INVERTERCOSTCELLSDECIDERQUANTITY)
                                {
                                    InverterCost = INVERTER_COST_WITH_BATTERY_LARGE;
                                }
                                else
                                {
                                    InverterCost = INVERTER_COST_WITH_BATTERY_STANDARD;
                                }
                            }
                            else
                            {
                                if ((NumberOfPanels * CellSelectedSize) > INVERTERCOSTCELLSDECIDERQUANTITY)
                                {
                                    InverterCost = INVERTER_COST_NO_BATTERY_LARGE;
                                }
                                else
                                {
                                    InverterCost = INVERTER_COST_NO_BATTERY_STANDARD;
                                }
                            }

                            // Add inverter cost to the total
                            TotalCostOfOrder += InverterCost;

                            // Add expedited installation cost if selected
                            if (ExpediteInstallCheckBox.Checked)
                            {
                                TotalCostOfOrder += EXPEDITE_INSTALLATION_COST;
                            }

                            // Apply special discount if conditions are met
                            if (((NumberOfPanels * CellSelectedSize) > INVERTERCOSTCELLSDECIDERQUANTITY) && (Battery10kwhRadioButton.Checked || Battery20kwhRadioButton.Checked))
                            {
                                DiscountValue = TotalCostOfOrder * S4U_SPECIAL_DISCOUNT;
                                TotalCostOfOrder -= DiscountValue;
                                SpecialDiscountApplied = true;
                            }

                            // Update the user interface with the calculated values
                            QuoteDetailsGroupBox.Visible = true;
                            SolarPanelTypeQuoteOutputLabel.Text = SolarPanelSelectedName.ToString();
                            NumberOfPanelsQuoteOutputLabel.Text = NumberOfPanels.ToString();
                            PanelSizeQuoteOutputLabel.Text = (CellSelectedSize + " - Cell").ToString();
                            if (BatteryPurchased)
                            {
                                BatterySizeQuoteOutputLabel.Text = BatterySize;
                            }
                            else
                            {
                                BatterySizeQuoteOutputLabel.Text = "Battery Not Purchased";
                            }

                            if (ExpediteInstallCheckBox.Checked)
                            {
                                ExpediteInstallationQuoteOutputLabel.Text = "Yes";
                                IsExpediteInstallation = true;
                            }
                            else
                            {
                                ExpediteInstallationQuoteOutputLabel.Text = "No";
                            }

                            NumberOfCellsQuoteOutputLabel.Text = (NumberOfPanels * CellSelectedSize).ToString();
                            InverterCostQuoteOutputLabel.Text = InverterCost.ToString("C2");
                            if (SpecialDiscountApplied)
                            {
                                DiscountReceivedQuoteOutputLabel.Text = DiscountValue.ToString("C2");
                            }
                            else
                            {
                                DiscountReceivedQuoteOutputLabel.Text = "No Discount Received";
                            }

                            TotalCostQuoteOutputLabel.Text = TotalCostOfOrder.ToString("C2");

                            // Enable the 'Order' button after generating the quote
                            OrderButton.Enabled = true;
                        }
                        else
                        {
                            // Show error message if the number of panels is zero or less
                            NumberOfPanelsInputTextBox.Focus();
                            NumberOfPanelsInputTextBox.SelectAll();
                            MessageBox.Show("Please Enter Value that is Greater Than Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Show error message if the input for number of panels is not a valid number
                        NumberOfPanelsInputTextBox.Focus();
                        NumberOfPanelsInputTextBox.SelectAll();
                        MessageBox.Show("Please Enter a Whole Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Show error message if no solar panel size is selected
                    MessageBox.Show("Please Select a Solar Panel Size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Show error message if no solar panel type is selected
                MessageBox.Show("Please Select a Solar Panel Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Event handler for automatic recalculation when order details change
        private void OrderDetails_Changed(object sender, EventArgs e)
        {
            if (Battery20kwhRadioButton.Checked || Battery10kwhRadioButton.Checked || Battery5kwhRadioButton.Checked)
            {
                NoBatteryOptionRadioButton.Visible = true;
            }
            if (OrderButton.Enabled)
            {
                QuoteButton_Click(sender, e);
            }
        }

        // Event handler to validate panel input as numbers are typed
        private void NumberOfPanelsInputTextBox_TextChanged(object sender, EventArgs e)
        {
            int temp;

            if (int.TryParse(NumberOfPanelsInputTextBox.Text, out temp))
            {
                OrderDetails_Changed(sender, e);
            }
        }

        
        private void OrderButton_Click(object sender, EventArgs e)
        {
            
            //configuring visibility
            CustomerDetailsPanel.Visible = true;

                        
            // helps in expanding the form for client details entry
            if (!FormWidthExpanded)
            {
                for (int i = FORMSTARTWIDTH; i < FORMEXPANDWIDTH; i += INCREMENT)
                {
                    this.Size = new Size(i, FORMSTARTHEIGHT);
                    this.Update();
                    System.Threading.Thread.Sleep(1);
                }
                FormWidthExpanded = true;
            }

            //focus to firstname field
            FirstNameInputTextBox.Focus();

            //generate the transaction number using Random class
            Random RandomTransactionNumber = new Random();
            TransactionNumber = RandomTransactionNumber.Next(10000, 999999).ToString();
            TransactionNumberOutputLabel.Text = TransactionNumber;
            
            //populate todays date
            DateTime CurrentTime  = DateTime.Now;
            TodaysDate = CurrentTime.ToShortDateString();
            TodaysDateOutputLabel.Text = TodaysDate;    


        }

        // Event handler for the Transaction History button click
        private void TransactionHistoryButton_Click(object sender, EventArgs e)
        {
            // Opens the Solar4URecordSearchForm to allow searching through existing records
            Solar4URecordSearchForm recordSearchForm = new Solar4URecordSearchForm();
            recordSearchForm.ShowDialog(); // Displays the form as a modal dialog
        }
        

        // Event handler for the Customer Details Submit button click
        private void CustomerDetailsSubmitButton_Click(object sender, EventArgs e)
        {
            // Checks if user-provided data is valid
            if (ValidUserData())
            {
                // Transfers user data from input fields to corresponding variables
                TransferUserData();

                // Opens the OrderSummaryForm to display the order summary
                OrderSummaryForm Solar4UOrderSummaryForm = new OrderSummaryForm();

                // If the user confirms the order, clear the input fields
                if (Solar4UOrderSummaryForm.ShowDialog() == DialogResult.Yes)
                {
                    ClearButton_Click(sender, e); 
                }
            }
        }

        // Transfers user data from the input fields to global variables
        private void TransferUserData()
        {
            CustomerFirstName = FirstNameInputTextBox.Text; 
            CustomerLastName = LastNameInputTextBox.Text; 
            CustomerEIRCode = EIRCodeInputTextBox.Text;
            CustomerEmailID = EmailIDInputTextBox.Text; 
            CustomerPhoneNumber = PhoneNumberInputTextBox.Text;
            NumberOfSolarPanels = NumberOfPanels; 
        }

        // Validates user data entered in the input fields
        private bool ValidUserData()
        {
            // Check if First Name is entered
            if (FirstNameInputTextBox.Text != string.Empty)
            {
                // Check if Last Name is entered
                if (LastNameInputTextBox.Text != string.Empty)
                {
                    // Check if EIR Code is entered
                    if (EIRCodeInputTextBox.Text != string.Empty)
                    {
                        // Check if Phone Number is entered
                        if (PhoneNumberInputTextBox.Text != string.Empty)
                        {
                            // Check if Email ID is entered
                            if (EmailIDInputTextBox.Text != string.Empty)
                            {
                                // Check if Email ID is in a valid format
                                if (EmailIDInputTextBox.Text.Contains("@") && EmailIDInputTextBox.Text.Contains("."))
                                {
                                    return true;
                                }
                                else
                                {
                                    // Highlight Email ID field and show error message for invalid format
                                    EmailIDInputTextBox.Focus();
                                    EmailIDInputTextBox.SelectAll();
                                    MessageBox.Show("Please Enter valid Email ID of the Customer in 'abc@xyz.com' format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }
                            else
                            {
                                // Highlight Email ID field and show error message for missing email
                                EmailIDInputTextBox.Focus();
                                EmailIDInputTextBox.SelectAll();
                                MessageBox.Show("Please Enter Email ID of the Customer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        else
                        {
                            // Highlight Phone Number field and show error message for missing phone number
                            PhoneNumberInputTextBox.Focus();
                            PhoneNumberInputTextBox.SelectAll();
                            MessageBox.Show("Please Enter the Phone Number of the Customer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        // Highlight EIR Code field and show error message for missing EIR Code
                        EIRCodeInputTextBox.Focus();
                        EIRCodeInputTextBox.SelectAll();
                        MessageBox.Show("Please Enter the EIR Code of the Customer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    // Highlight Last Name field and show error message for missing last name
                    LastNameInputTextBox.Focus();
                    LastNameInputTextBox.SelectAll();
                    MessageBox.Show("Please Enter the Last Name of the Customer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                // Highlight First Name field and show error message for missing first name
                FirstNameInputTextBox.Focus();
                FirstNameInputTextBox.SelectAll();
                MessageBox.Show("Please Enter the First Name of the Customer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Event handler for 'Clear' button to reset the form fields for a new quote
        private void ClearButton_Click(object sender, EventArgs e)
        {
            //Title Reset
            Text = "Solar4U Sales App";

            // form minimizes to the original width and height
            this.Size = new Size(FORMSTARTWIDTH, FORMSTARTHEIGHT);
            FormWidthExpanded = false;

            // Reset form for new order, enable user inputs and buttons
            OrderButton.Enabled = false;
            UserInputPanel.Enabled = true;
            QuoteButton.Enabled = true;
            QuoteDetailsGroupBox.Visible = false;

            // Clear output labels and reset input fields
            SolarPanelTypeQuoteOutputLabel.Text = string.Empty;
            NumberOfPanelsQuoteOutputLabel.Text = string.Empty;
            PanelSizeQuoteOutputLabel.Text = string.Empty;
            BatterySizeQuoteOutputLabel.Text = string.Empty;
            ExpediteInstallationQuoteOutputLabel.Text = string.Empty;
            NumberOfCellsQuoteOutputLabel.Text = string.Empty;
            InverterCostQuoteOutputLabel.Text = string.Empty;
            DiscountReceivedQuoteOutputLabel.Text = string.Empty;
            TotalCostQuoteOutputLabel.Text = string.Empty;

            // Clear selections and reset all form fields to default
            SolarPanelAndPriceListBox.ClearSelected();
            SolarPanelSizesListBox.ClearSelected();
            NumberOfPanelsInputTextBox.Text = "0";
            Battery5kwhRadioButton.Checked = false;
            Battery10kwhRadioButton.Checked = false;
            Battery20kwhRadioButton.Checked = false;
            NoBatteryOptionRadioButton.Visible = false;
            ExpediteInstallCheckBox.Checked = false;

            // Hide customer details panel and clear its content
            CustomerDetailsPanel.Visible = false;
            TransactionNumberOutputLabel.Text = string.Empty;
            TodaysDateOutputLabel.Text = string.Empty;

            // Clear customer input fields
            FirstNameInputTextBox.Text = string.Empty;
            LastNameInputTextBox.Text = string.Empty;
            EIRCodeInputTextBox.Text = string.Empty;
            PhoneNumberInputTextBox.Text = string.Empty;
            EmailIDInputTextBox.Text = string.Empty;
            
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
