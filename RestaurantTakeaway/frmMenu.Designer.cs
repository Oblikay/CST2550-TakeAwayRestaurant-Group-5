namespace RestaurantTakeaway
{
    partial class frmMenu
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnBurger;
        private System.Windows.Forms.Button btnPizza;
        private System.Windows.Forms.Button btnFries;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.ListBox lstOrder;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtPayment;
        private System.Windows.Forms.Label lblPayment;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label lblQuantity;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnBurger = new Button();
            btnPizza = new Button();
            btnFries = new Button();
            btnCheckout = new Button();
            lstOrder = new ListBox();
            lblTotal = new Label();
            txtPayment = new TextBox();
            lblPayment = new Label();
            numQuantity = new NumericUpDown();
            lblQuantity = new Label();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            SuspendLayout();
            // 
            // btnBurger
            // 
            btnBurger.Location = new Point(40, 80);
            btnBurger.Name = "btnBurger";
            btnBurger.Size = new Size(75, 29);
            btnBurger.TabIndex = 0;
            btnBurger.Text = "Burger ($5)";
            btnBurger.Click += btnBurger_Click;
            // 
            // btnPizza
            // 
            btnPizza.Location = new Point(40, 140);
            btnPizza.Name = "btnPizza";
            btnPizza.Size = new Size(75, 29);
            btnPizza.TabIndex = 1;
            btnPizza.Text = "Pizza ($8)";
            btnPizza.Click += btnPizza_Click;
            // 
            // btnFries
            // 
            btnFries.Location = new Point(40, 200);
            btnFries.Name = "btnFries";
            btnFries.Size = new Size(75, 34);
            btnFries.TabIndex = 2;
            btnFries.Text = "Fries ($3)";
            btnFries.Click += btnFries_Click;
            // 
            // btnCheckout
            // 
            btnCheckout.Location = new Point(200, 282);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(120, 40);
            btnCheckout.TabIndex = 7;
            btnCheckout.Text = "Checkout";
            btnCheckout.Click += btnCheckout_Click;
            // 
            // lstOrder
            // 
            lstOrder.Location = new Point(200, 80);
            lstOrder.Name = "lstOrder";
            lstOrder.Size = new Size(185, 84);
            lstOrder.TabIndex = 3;
            // 
            // lblTotal
            // 
            lblTotal.Location = new Point(200, 181);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(100, 23);
            lblTotal.TabIndex = 4;
            lblTotal.Text = "Total: $0";
            // 
            // txtPayment
            // 
            txtPayment.Location = new Point(200, 249);
            txtPayment.Name = "txtPayment";
            txtPayment.Size = new Size(100, 27);
            txtPayment.TabIndex = 6;
            // 
            // lblPayment
            // 
            lblPayment.Location = new Point(200, 223);
            lblPayment.Name = "lblPayment";
            lblPayment.Size = new Size(100, 23);
            lblPayment.TabIndex = 5;
            lblPayment.Text = "Cash Payment:";
            // 
            // numQuantity
            // 
            numQuantity.Location = new Point(110, 38);
            numQuantity.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(120, 27);
            numQuantity.TabIndex = 8;
            numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblQuantity
            // 
            lblQuantity.Location = new Point(40, 40);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(64, 23);
            lblQuantity.TabIndex = 9;
            lblQuantity.Text = "Quantity:";
            // 
            // frmMenu
            // 
            ClientSize = new Size(450, 353);
            Controls.Add(btnBurger);
            Controls.Add(btnPizza);
            Controls.Add(btnFries);
            Controls.Add(lstOrder);
            Controls.Add(lblTotal);
            Controls.Add(lblPayment);
            Controls.Add(txtPayment);
            Controls.Add(btnCheckout);
            Controls.Add(numQuantity);
            Controls.Add(lblQuantity);
            Name = "frmMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Restaurant Menu";
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}