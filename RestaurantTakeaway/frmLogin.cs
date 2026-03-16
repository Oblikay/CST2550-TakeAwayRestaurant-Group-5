using System;
using System.Windows.Forms;

namespace RestaurantTakeaway
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = "admin";
            string password = "1234";

            if (txtUsername.Text == username && txtPassword.Text == password)
            {
                MessageBox.Show("Login Successful");

                frmMenu menu = new frmMenu();
                menu.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }
    }
}