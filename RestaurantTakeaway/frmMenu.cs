using System;
using System.Windows.Forms;

namespace RestaurantTakeaway
{
    public partial class frmMenu : Form
    {
        int total = 0;

        public frmMenu()
        {
            InitializeComponent();
        }

        private void btnBurger_Click(object sender, EventArgs e)
        {
            int qty = (int)numQuantity.Value;
            int price = 5 * qty;

            lstOrder.Items.Add("Burger x" + qty + " - $" + price);

            total += price;
            lblTotal.Text = "Total: $" + total;
        }

        private void btnPizza_Click(object sender, EventArgs e)
        {
            int qty = (int)numQuantity.Value;
            int price = 8 * qty;

            lstOrder.Items.Add("Pizza x" + qty + " - $" + price);

            total += price;
            lblTotal.Text = "Total: $" + total;
        }

        private void btnFries_Click(object sender, EventArgs e)
        {
            int qty = (int)numQuantity.Value;
            int price = 3 * qty;

            lstOrder.Items.Add("Fries x" + qty + " - $" + price);

            total += price;
            lblTotal.Text = "Total: $" + total;
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (total == 0)
            {
                MessageBox.Show("No items selected.");
                return;
            }

            DialogResult paymentMethod = MessageBox.Show(
                "Choose payment method\nYES = Cash\nNO = Card",
                "Payment Method",
                MessageBoxButtons.YesNoCancel);

            if (paymentMethod == DialogResult.Yes)
            {
                double payment;

                if (double.TryParse(txtPayment.Text, out payment))
                {
                    if (payment >= total)
                    {
                        double change = payment - total;

                        MessageBox.Show(
                            "Payment Successful\nChange: $" + change +
                            "\n\nYour delivery will arrive in 30 minutes.");

                        ResetOrder();
                    }
                    else
                    {
                        MessageBox.Show("Not enough cash.");
                    }
                }
                else
                {
                    MessageBox.Show("Enter payment amount.");
                }
            }

            else if (paymentMethod == DialogResult.No)
            {
                MessageBox.Show(
                    "Card payment successful\n\nYour delivery will arrive in 30 minutes.");

                ResetOrder();
            }
        }

        private void ResetOrder()
        {
            total = 0;
            lblTotal.Text = "Total: $0";
            txtPayment.Clear();
            lstOrder.Items.Clear();
            numQuantity.Value = 1;
        }
    }
}