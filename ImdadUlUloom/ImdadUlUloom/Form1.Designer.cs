namespace ImdadUlUloom
{
    partial class adminLoginPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminLoginPage));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.login = new System.Windows.Forms.Label();
            this.adminName = new System.Windows.Forms.Label();
            this.passwordAdmin = new System.Windows.Forms.Label();
            this.adminNameTB = new System.Windows.Forms.TextBox();
            this.adminPasswordTB = new System.Windows.Forms.TextBox();
            this.adminLoginBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(22, -36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(358, 309);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(172, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(73, 78);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // login
            // 
            this.login.AutoSize = true;
            this.login.Font = new System.Drawing.Font("Alvi Nastaleeq", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login.Location = new System.Drawing.Point(157, 228);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(98, 42);
            this.login.TabIndex = 2;
            this.login.Text = "لاگ ان کیجئے";
            // 
            // adminName
            // 
            this.adminName.AutoSize = true;
            this.adminName.Font = new System.Drawing.Font("Alvi Nastaleeq", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminName.Location = new System.Drawing.Point(286, 282);
            this.adminName.Name = "adminName";
            this.adminName.Size = new System.Drawing.Size(59, 25);
            this.adminName.TabIndex = 3;
            this.adminName.Text = "ایڈمن کا نام";
            // 
            // passwordAdmin
            // 
            this.passwordAdmin.AutoSize = true;
            this.passwordAdmin.Font = new System.Drawing.Font("Alvi Nastaleeq", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordAdmin.Location = new System.Drawing.Point(307, 319);
            this.passwordAdmin.Name = "passwordAdmin";
            this.passwordAdmin.Size = new System.Drawing.Size(38, 25);
            this.passwordAdmin.TabIndex = 4;
            this.passwordAdmin.Text = "پاسورڈ";
            // 
            // adminNameTB
            // 
            this.adminNameTB.Location = new System.Drawing.Point(124, 287);
            this.adminNameTB.MaxLength = 15;
            this.adminNameTB.Name = "adminNameTB";
            this.adminNameTB.Size = new System.Drawing.Size(131, 20);
            this.adminNameTB.TabIndex = 0;
            // 
            // adminPasswordTB
            // 
            this.adminPasswordTB.Location = new System.Drawing.Point(124, 324);
            this.adminPasswordTB.MaxLength = 15;
            this.adminPasswordTB.Name = "adminPasswordTB";
            this.adminPasswordTB.PasswordChar = '*';
            this.adminPasswordTB.Size = new System.Drawing.Size(131, 20);
            this.adminPasswordTB.TabIndex = 1;
            // 
            // adminLoginBtn
            // 
            this.adminLoginBtn.Font = new System.Drawing.Font("Alvi Nastaleeq", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminLoginBtn.Location = new System.Drawing.Point(164, 365);
            this.adminLoginBtn.Name = "adminLoginBtn";
            this.adminLoginBtn.Size = new System.Drawing.Size(82, 25);
            this.adminLoginBtn.TabIndex = 2;
            this.adminLoginBtn.Text = "لاگ ان";
            this.adminLoginBtn.UseVisualStyleBackColor = true;
            this.adminLoginBtn.Click += new System.EventHandler(this.adminLoginBtn_Click);
            // 
            // adminLoginPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(409, 416);
            this.Controls.Add(this.adminLoginBtn);
            this.Controls.Add(this.adminPasswordTB);
            this.Controls.Add(this.adminNameTB);
            this.Controls.Add(this.passwordAdmin);
            this.Controls.Add(this.adminName);
            this.Controls.Add(this.login);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Name = "adminLoginPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " جامعہ اسلامیہ امداد العلوم";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.adminLoginPage_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label login;
        private System.Windows.Forms.Label adminName;
        private System.Windows.Forms.Label passwordAdmin;
        private System.Windows.Forms.TextBox adminNameTB;
        private System.Windows.Forms.TextBox adminPasswordTB;
        private System.Windows.Forms.Button adminLoginBtn;
    }
}

