namespace SocialMediaFormsApp
{
    partial class LoginForm
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
            label1 = new Label();
            label2 = new Label();
            UsernameTB = new TextBox();
            PasswordTB = new TextBox();
            LoginBT = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(84, 116);
            label1.Name = "label1";
            label1.Size = new Size(91, 25);
            label1.TabIndex = 0;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(84, 206);
            label2.Name = "label2";
            label2.Size = new Size(87, 25);
            label2.TabIndex = 1;
            label2.Text = "Password";
            // 
            // UsernameTB
            // 
            UsernameTB.Location = new Point(181, 116);
            UsernameTB.Name = "UsernameTB";
            UsernameTB.Size = new Size(189, 31);
            UsernameTB.TabIndex = 2;
            // 
            // PasswordTB
            // 
            PasswordTB.Location = new Point(181, 206);
            PasswordTB.Name = "PasswordTB";
            PasswordTB.Size = new Size(189, 31);
            PasswordTB.TabIndex = 3;
            // 
            // LoginBT
            // 
            LoginBT.Location = new Point(319, 326);
            LoginBT.Name = "LoginBT";
            LoginBT.Size = new Size(112, 34);
            LoginBT.TabIndex = 4;
            LoginBT.Text = "Login";
            LoginBT.UseVisualStyleBackColor = true;
            LoginBT.Click += LoginBT_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LoginBT);
            Controls.Add(PasswordTB);
            Controls.Add(UsernameTB);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "LoginForm";
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox UsernameTB;
        private TextBox PasswordTB;
        private Button LoginBT;
    }
}