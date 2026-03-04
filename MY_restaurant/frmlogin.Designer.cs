namespace MY_restaurant
{
    partial class frmlogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmlogin));
            panel1 = new Panel();
            textBox3 = new TextBox();
            label1 = new Label();
            txtUser = new TextBox();
            txtPassword = new TextBox();
            label2 = new Label();
            btnLogin = new Button();
            btnExit = new Button();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MediumSlateBlue;
            panel1.BackgroundImageLayout = ImageLayout.Center;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(textBox3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(329, 250);
            panel1.TabIndex = 4;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.MediumSlateBlue;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Location = new Point(563, 357);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(188, 18);
            textBox3.TabIndex = 0;
            textBox3.Text = "Please enter user information";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 253);
            label1.Name = "label1";
            label1.Size = new Size(67, 17);
            label1.TabIndex = 5;
            label1.Text = "Username";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(12, 273);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(199, 25);
            txtUser.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(9, 357);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(199, 25);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 337);
            label2.Name = "label2";
            label2.Size = new Size(64, 17);
            label2.TabIndex = 6;
            label2.Text = "Password";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(192, 192, 255);
            btnLogin.ForeColor = SystemColors.ActiveCaptionText;
            btnLogin.Location = new Point(9, 430);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(67, 30);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(128, 128, 255);
            btnExit.Location = new Point(92, 430);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(67, 30);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(92, 38);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(210, 193);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // frmlogin
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(329, 497);
            Controls.Add(btnExit);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(label2);
            Controls.Add(txtUser);
            Controls.Add(label1);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmlogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += frmlogin_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private TextBox txtUser;
        private TextBox txtPassword;
        private Label label2;
        private TextBox textBox3;
        private Button btnLogin;
        private Button btnExit;
        private PictureBox pictureBox1;
    }
}
