using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Solar4U_Sales_Application
{
    public partial class OrderSummaryForm : Form
    {
        // Constants defining loan cut-off amounts for tiers
        private const int TIER1LOANCUTOFFAMOUNT = 15000, TIER2LOANCUTOFFAMOUNT = 30000;

        // Constants defining APR values for different tiers and loan terms
        private const decimal TIER1APR5YEARVALUE = 7.50m, TIER1APR3YEARVALUE = 8.00m, TIER1APR1YEARVALUE = 8.50m;
        private const decimal TIER2APR5YEARVALUE = 6.85m, TIER2APR3YEARVALUE = 7.55m, TIER2APR1YEARVALUE = 8.15m;
        private const decimal TIER3APR5YEARVALUE = 6.15m, TIER3APR3YEARVALUE = 7.25m, TIER3APR1YEARVALUE = 7.85m;

        // Boolean to track if any I/O problem occurs
        private bool IOProblem;

        public OrderSummaryForm()
        {
            InitializeComponent();
        }

        // Event handler for form load
        private void OrderSummaryForm_Load(object sender, EventArgs e)
        {
            // Output customer details on the form
            OutputCustomerDetails();

            // Output solar panel details on the form
            OutputCustomerSolarPanelDetails();

            // Output loan options based on tier
            OutputLoanOptions();
        }

        // Event handler for Proceed button click
        private void ProceedButton_Click(object sender, EventArgs e)
        {
            string UserSelectedAPR;
            string UserSelectedInterestAmount;
            string UserSelectedEMIAmount;
            string UserSelectedTotalAmountPayable;

            // Reset I/O problem flag
            IOProblem = false;

            // Confirm the user's intent to proceed with the order
            if (MessageBox.Show("Are you sure you want to confirm the order?", "Solar4U - Confirm Order",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Retrieve values based on selected loan term
                if (Term5YearRadioButton.Checked)
                {
                    UserSelectedAPR = APR5YearOutputLabel.Text;
                    UserSelectedInterestAmount = InterestAmount5YearOutputLabel.Text;
                    UserSelectedEMIAmount = EMI5YearOutputLabel.Text;
                    UserSelectedTotalAmountPayable = TotalAmountPayable5YearOutputLabel.Text;
                }
                else if (Term3YearRadioButton.Checked)
                {
                    UserSelectedAPR = APR3YearOutputLabel.Text;
                    UserSelectedInterestAmount = InterestAmount3YearOutputLabel.Text;
                    UserSelectedEMIAmount = EMI3YearOutputLabel.Text;
                    UserSelectedTotalAmountPayable = TotalAmountPayable3YearOutputLabel.Text;
                }
                else if (Term1YearRadioButton.Checked)
                {
                    UserSelectedAPR = APR1YearOutputLabel.Text;
                    UserSelectedInterestAmount = InterestAmount1YearOutputLabel.Text;
                    UserSelectedEMIAmount = EMI1YearOutputLabel.Text;
                    UserSelectedTotalAmountPayable = TotalAmountPayable1YearOutputLabel.Text;
                }
                else
                {
                    // Default values when no loan term is selected
                    UserSelectedAPR = "NA";
                    UserSelectedInterestAmount = "NA";
                    UserSelectedEMIAmount = "NA";
                    UserSelectedTotalAmountPayable = "NA";
                }

                // Save order details to a file
                SaveDataIntoFile(UserSelectedAPR, UserSelectedInterestAmount, UserSelectedEMIAmount, UserSelectedTotalAmountPayable);

                // If no I/O problem occurred, set dialog result to Yes
                if (!IOProblem)
                {
                    DialogResult = DialogResult.Yes;
                }
            }
        }

        // Outputs customer details on the form
        private void OutputCustomerDetails()
        {
            FullNameOutputLabel.Text = Solar4USalesAppForm.CustomerFirstName.ToString() + " " + Solar4USalesAppForm.CustomerLastName.ToString();
            EIRCodeOutputLabel.Text = Solar4USalesAppForm.CustomerEIRCode.ToString();
            EmailIDOutputLabel.Text = Solar4USalesAppForm.CustomerEmailID.ToString();
            TransactionNumberOutputLabel.Text = Solar4USalesAppForm.TransactionNumber;
            TodaysDateOutputLabel.Text = Solar4USalesAppForm.TodaysDate;
        }

        // Outputs solar panel order details on the form
        private void OutputCustomerSolarPanelDetails()
        {
            SolarPanelTypeOutputLabel.Text = Solar4USalesAppForm.SolarPanelSelectedName;
            PanelSizeOutputLabel.Text = (Solar4USalesAppForm.CellSelectedSize + " - Cell").ToString();
            NumberOfPanelsOutputLabel.Text = Solar4USalesAppForm.NumberOfSolarPanels.ToString();

            // Display battery details if purchased, else indicate no battery
            if (Solar4USalesAppForm.BatteryPurchased)
            {
                BatterySizeOutputLabel.Text = Solar4USalesAppForm.BatterySize;
            }
            else
            {
                BatterySizeOutputLabel.Text = "Battery Not Purchased";
            }

            // Output additional order details
            InverterCostOutputLabel.Text = Solar4USalesAppForm.InverterCost.ToString("C2");
            ExpediteInstallationOutputLabel.Text = Solar4USalesAppForm.IsExpediteInstallation ? "Yes" : "No";

            // Display discount received if applicable
            if (Solar4USalesAppForm.SpecialDiscountApplied)
            {
                DiscountReceivedOutputLabel.Text = Solar4USalesAppForm.DiscountValue.ToString("C2");
            }
            else
            {
                DiscountReceivedOutputLabel.Text = "No Discount Received";
            }

            // Output total cost of the order
            TotalCostOfOrderOutputLabel.Text = Solar4USalesAppForm.TotalCostOfOrder.ToString("C2");
        }

        // Outputs loan options based on tier determination
        private void OutputLoanOptions()
        {
            int TIME5YEARSINMONTHS = 60;
            int TIME3YEARSINMONTHS = 36;
            int TIME1YEARSINMONTHS = 12;

            // Determine loan tier and populate details
            switch (TierDecider())
            {
                case 1:
                    // Populate loan details for Tier 1
                    APR5YearOutputLabel.Text = TIER1APR5YEARVALUE.ToString() + "%";
                    APR3YearOutputLabel.Text = TIER1APR3YEARVALUE.ToString() + "%";
                    APR1YearOutputLabel.Text = TIER1APR1YEARVALUE.ToString() + "%";

                    InterestAmount5YearOutputLabel.Text = InterestAmountCalculation(TIER1APR5YEARVALUE, TIME5YEARSINMONTHS).ToString("C2");
                    InterestAmount3YearOutputLabel.Text = InterestAmountCalculation(TIER1APR3YEARVALUE, TIME3YEARSINMONTHS).ToString("C2");
                    InterestAmount1YearOutputLabel.Text = InterestAmountCalculation(TIER1APR1YEARVALUE, TIME1YEARSINMONTHS).ToString("C2");

                    EMI5YearOutputLabel.Text = (InterestAmountCalculation(TIER1APR5YEARVALUE, TIME5YEARSINMONTHS) / TIME5YEARSINMONTHS).ToString("C2");
                    EMI3YearOutputLabel.Text = (InterestAmountCalculation(TIER1APR3YEARVALUE, TIME3YEARSINMONTHS) / TIME3YEARSINMONTHS).ToString("C2");
                    EMI1YearOutputLabel.Text = (InterestAmountCalculation(TIER1APR1YEARVALUE, TIME1YEARSINMONTHS) / TIME1YEARSINMONTHS).ToString("C2");

                    TotalAmountPayable5YearOutputLabel.Text = (InterestAmountCalculation(TIER1APR5YEARVALUE, TIME5YEARSINMONTHS) + Solar4USalesAppForm.TotalCostOfOrder).ToString("C2");
                    TotalAmountPayable3YearOutputLabel.Text = (InterestAmountCalculation(TIER1APR3YEARVALUE, TIME3YEARSINMONTHS) + Solar4USalesAppForm.TotalCostOfOrder).ToString("C2");
                    TotalAmountPayable1YearOutputLabel.Text = (InterestAmountCalculation(TIER1APR1YEARVALUE, TIME1YEARSINMONTHS) + Solar4USalesAppForm.TotalCostOfOrder).ToString("C2");
                    break;

                case 2:
                    // Populate loan details for Tier 2
                    APR5YearOutputLabel.Text = TIER2APR5YEARVALUE.ToString() + "%";
                    APR3YearOutputLabel.Text = TIER2APR3YEARVALUE.ToString() + "%";
                    APR1YearOutputLabel.Text = TIER2APR1YEARVALUE.ToString() + "%";

                    InterestAmount5YearOutputLabel.Text = InterestAmountCalculation(TIER2APR5YEARVALUE, TIME5YEARSINMONTHS).ToString("C2");
                    InterestAmount3YearOutputLabel.Text = InterestAmountCalculation(TIER2APR3YEARVALUE, TIME3YEARSINMONTHS).ToString("C2");
                    InterestAmount1YearOutputLabel.Text = InterestAmountCalculation(TIER2APR1YEARVALUE, TIME1YEARSINMONTHS).ToString("C2");

                    EMI5YearOutputLabel.Text = (InterestAmountCalculation(TIER2APR5YEARVALUE, TIME5YEARSINMONTHS) / TIME5YEARSINMONTHS).ToString("C2");
                    EMI3YearOutputLabel.Text = (InterestAmountCalculation(TIER2APR3YEARVALUE, TIME3YEARSINMONTHS) / TIME3YEARSINMONTHS).ToString("C2");
                    EMI1YearOutputLabel.Text = (InterestAmountCalculation(TIER2APR1YEARVALUE, TIME1YEARSINMONTHS) / TIME1YEARSINMONTHS).ToString("C2");

                    TotalAmountPayable5YearOutputLabel.Text = (InterestAmountCalculation(TIER2APR5YEARVALUE, TIME5YEARSINMONTHS) + Solar4USalesAppForm.TotalCostOfOrder).ToString("C2");
                    TotalAmountPayable3YearOutputLabel.Text = (InterestAmountCalculation(TIER2APR3YEARVALUE, TIME3YEARSINMONTHS) + Solar4USalesAppForm.TotalCostOfOrder).ToString("C2");
                    TotalAmountPayable1YearOutputLabel.Text = (InterestAmountCalculation(TIER2APR1YEARVALUE, TIME1YEARSINMONTHS) + Solar4USalesAppForm.TotalCostOfOrder).ToString("C2");
                    break;

                case 3:
                    // Populate loan details for Tier 3
                    APR5YearOutputLabel.Text = TIER3APR5YEARVALUE.ToString() + "%";
                    APR3YearOutputLabel.Text = TIER3APR3YEARVALUE.ToString() + "%";
                    APR1YearOutputLabel.Text = TIER3APR1YEARVALUE.ToString() + "%";

                    InterestAmount5YearOutputLabel.Text = InterestAmountCalculation(TIER3APR5YEARVALUE, TIME5YEARSINMONTHS).ToString("C2");
                    InterestAmount3YearOutputLabel.Text = InterestAmountCalculation(TIER3APR3YEARVALUE, TIME3YEARSINMONTHS).ToString("C2");
                    InterestAmount1YearOutputLabel.Text = InterestAmountCalculation(TIER3APR1YEARVALUE, TIME1YEARSINMONTHS).ToString("C2");

                    EMI5YearOutputLabel.Text = (InterestAmountCalculation(TIER3APR5YEARVALUE, TIME5YEARSINMONTHS) / TIME5YEARSINMONTHS).ToString("C2");
                    EMI3YearOutputLabel.Text = (InterestAmountCalculation(TIER3APR3YEARVALUE, TIME3YEARSINMONTHS) / TIME3YEARSINMONTHS).ToString("C2");
                    EMI1YearOutputLabel.Text = (InterestAmountCalculation(TIER3APR1YEARVALUE, TIME1YEARSINMONTHS) / TIME1YEARSINMONTHS).ToString("C2");

                    TotalAmountPayable5YearOutputLabel.Text = (InterestAmountCalculation(TIER3APR5YEARVALUE, TIME5YEARSINMONTHS) + Solar4USalesAppForm.TotalCostOfOrder).ToString("C2");
                    TotalAmountPayable3YearOutputLabel.Text = (InterestAmountCalculation(TIER3APR3YEARVALUE, TIME3YEARSINMONTHS) + Solar4USalesAppForm.TotalCostOfOrder).ToString("C2");
                    TotalAmountPayable1YearOutputLabel.Text = (InterestAmountCalculation(TIER3APR1YEARVALUE, TIME1YEARSINMONTHS) + Solar4USalesAppForm.TotalCostOfOrder).ToString("C2");
                    break;
            }
        }

        // Determines the loan tier based on the total cost of the order
        private int TierDecider()
        {
            if (Solar4USalesAppForm.TotalCostOfOrder <= TIER1LOANCUTOFFAMOUNT)
            {
                return 1;
            }
            else if (Solar4USalesAppForm.TotalCostOfOrder > TIER1LOANCUTOFFAMOUNT && Solar4USalesAppForm.TotalCostOfOrder <= TIER2LOANCUTOFFAMOUNT)
            {
                return 2;
            }
            return 3;
        }

        // Calculates the interest amount for the given APR and loan term
        private decimal InterestAmountCalculation(decimal AnnualPercentageRate, int TotalMonths)
        {
            decimal MonthlyRate = 0;
            decimal CompoundFactor = 1;
            decimal FinalAmount = 0;

            const int COMPOUNDINGPERIODSPERYEAR = 12;

            // Calculate monthly interest rate
            MonthlyRate = (AnnualPercentageRate / 100) / COMPOUNDINGPERIODSPERYEAR;

            // Calculate compound factor over the loan term
            for (int month = 0; month < TotalMonths; month++)
            {
                CompoundFactor *= (1 + MonthlyRate);
            }

            // Calculate final amount and derive interest amount
            FinalAmount = Solar4USalesAppForm.TotalCostOfOrder * CompoundFactor;
            return (FinalAmount - Solar4USalesAppForm.TotalCostOfOrder);
        }

        // Saves the order data into a file
        private void SaveDataIntoFile(string APR, string InterestAmount, string EMIAmount, string TotalAmountPayable)
        {
            try
            {
                StreamWriter OutputFile;
                OutputFile = File.AppendText("Solar4U_Sales_Record.txt");

                // Save transaction details
                OutputFile.WriteLine(Solar4USalesAppForm.TransactionNumber);
                OutputFile.WriteLine(Solar4USalesAppForm.TodaysDate);
                OutputFile.WriteLine(Solar4USalesAppForm.CustomerFirstName + " " + Solar4USalesAppForm.CustomerLastName);
                OutputFile.WriteLine(Solar4USalesAppForm.CustomerEIRCode);
                OutputFile.WriteLine(Solar4USalesAppForm.CustomerEmailID);
                OutputFile.WriteLine(Solar4USalesAppForm.SolarPanelSelectedName);
                OutputFile.WriteLine(Solar4USalesAppForm.CellSelectedSize + " - Cell");
                OutputFile.WriteLine(Solar4USalesAppForm.NumberOfSolarPanels);
                if (Solar4USalesAppForm.BatteryPurchased)
                {
                    OutputFile.WriteLine(Solar4USalesAppForm.BatterySize);
                }
                else
                {
                    OutputFile.WriteLine("Battery Not Purchased");
                }
                OutputFile.WriteLine(Solar4USalesAppForm.InverterCost.ToString("C2"));
                if (Solar4USalesAppForm.IsExpediteInstallation)
                {
                    OutputFile.WriteLine("Expedite Install");
                }
                else
                {
                    OutputFile.WriteLine("Standard Install");
                }
                if (Solar4USalesAppForm.SpecialDiscountApplied)
                {
                    OutputFile.WriteLine(Solar4USalesAppForm.DiscountValue.ToString("C2"));
                }
                else
                {
                    OutputFile.WriteLine("No Discount Received");
                }
                OutputFile.WriteLine(Solar4USalesAppForm.TotalCostOfOrder.ToString("C2"));
                if (NoLoanRequiredRadioButton.Checked)
                {
                    OutputFile.WriteLine("No Loan Availed");
                }
                else
                {
                    OutputFile.WriteLine("Loan Availed");
                }
                OutputFile.WriteLine(APR);
                OutputFile.WriteLine(EMIAmount);
                OutputFile.WriteLine(InterestAmount);
                OutputFile.WriteLine(TotalAmountPayable);

                OutputFile.Close();

                // Confirm successful data save
                MessageBox.Show("Order Data Saved Successfully!. Thank you!", "Order Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any exceptions during file I/O
                MessageBox.Show(ex.Message);
                IOProblem = true;
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}

