namespace SocialMediaFormsApp
{
    partial class RegularUserForm
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
            tabControl1 = new TabControl();
            ProfileTab = new TabPage();
            LocationLB = new Label();
            label12 = new Label();
            GenderLB = new Label();
            label11 = new Label();
            label6 = new Label();
            ModForLiB = new ListBox();
            FriendsLiB = new ListBox();
            FollowingCommunitiesLiB = new ListBox();
            label5 = new Label();
            label4 = new Label();
            DateCreatedLB = new Label();
            EmailLB = new Label();
            UsernameLB = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            MessageTab = new TabPage();
            ViewMessageBT = new Button();
            ReceivedMessagesLiB = new ListBox();
            label10 = new Label();
            groupBox1 = new GroupBox();
            SendMessageBT = new Button();
            BodyRTB = new RichTextBox();
            label9 = new Label();
            SubjectTB = new TextBox();
            label8 = new Label();
            label7 = new Label();
            ToTB = new TextBox();
            tabControl1.SuspendLayout();
            ProfileTab.SuspendLayout();
            MessageTab.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(ProfileTab);
            tabControl1.Controls.Add(MessageTab);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1073, 580);
            tabControl1.TabIndex = 0;
            // 
            // ProfileTab
            // 
            ProfileTab.Controls.Add(LocationLB);
            ProfileTab.Controls.Add(label12);
            ProfileTab.Controls.Add(GenderLB);
            ProfileTab.Controls.Add(label11);
            ProfileTab.Controls.Add(label6);
            ProfileTab.Controls.Add(ModForLiB);
            ProfileTab.Controls.Add(FriendsLiB);
            ProfileTab.Controls.Add(FollowingCommunitiesLiB);
            ProfileTab.Controls.Add(label5);
            ProfileTab.Controls.Add(label4);
            ProfileTab.Controls.Add(DateCreatedLB);
            ProfileTab.Controls.Add(EmailLB);
            ProfileTab.Controls.Add(UsernameLB);
            ProfileTab.Controls.Add(label3);
            ProfileTab.Controls.Add(label2);
            ProfileTab.Controls.Add(label1);
            ProfileTab.Location = new Point(4, 34);
            ProfileTab.Name = "ProfileTab";
            ProfileTab.Padding = new Padding(3);
            ProfileTab.Size = new Size(1065, 542);
            ProfileTab.TabIndex = 0;
            ProfileTab.Text = "Profile";
            ProfileTab.UseVisualStyleBackColor = true;
            // 
            // LocationLB
            // 
            LocationLB.AutoSize = true;
            LocationLB.Location = new Point(6, 462);
            LocationLB.Name = "LocationLB";
            LocationLB.Size = new Size(43, 25);
            LocationLB.TabIndex = 15;
            LocationLB.Text = "Null";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 434);
            label12.Name = "label12";
            label12.Size = new Size(79, 25);
            label12.TabIndex = 14;
            label12.Text = "Location";
            // 
            // GenderLB
            // 
            GenderLB.AutoSize = true;
            GenderLB.Location = new Point(6, 378);
            GenderLB.Name = "GenderLB";
            GenderLB.Size = new Size(43, 25);
            GenderLB.TabIndex = 13;
            GenderLB.Text = "Null";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 353);
            label11.Name = "label11";
            label11.Size = new Size(69, 25);
            label11.TabIndex = 12;
            label11.Text = "Gender";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(289, 42);
            label6.Name = "label6";
            label6.Size = new Size(133, 25);
            label6.TabIndex = 11;
            label6.Text = "Moderating for";
            // 
            // ModForLiB
            // 
            ModForLiB.FormattingEnabled = true;
            ModForLiB.ItemHeight = 25;
            ModForLiB.Location = new Point(235, 83);
            ModForLiB.Name = "ModForLiB";
            ModForLiB.Size = new Size(280, 404);
            ModForLiB.TabIndex = 10;
            // 
            // FriendsLiB
            // 
            FriendsLiB.FormattingEnabled = true;
            FriendsLiB.ItemHeight = 25;
            FriendsLiB.Location = new Point(824, 83);
            FriendsLiB.Name = "FriendsLiB";
            FriendsLiB.Size = new Size(216, 404);
            FriendsLiB.TabIndex = 9;
            // 
            // FollowingCommunitiesLiB
            // 
            FollowingCommunitiesLiB.FormattingEnabled = true;
            FollowingCommunitiesLiB.ItemHeight = 25;
            FollowingCommunitiesLiB.Location = new Point(535, 83);
            FollowingCommunitiesLiB.Name = "FollowingCommunitiesLiB";
            FollowingCommunitiesLiB.Size = new Size(270, 404);
            FollowingCommunitiesLiB.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(895, 42);
            label5.Name = "label5";
            label5.Size = new Size(69, 25);
            label5.TabIndex = 7;
            label5.Text = "Friends";
            label5.Click += label5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(569, 42);
            label4.Name = "label4";
            label4.Size = new Size(199, 25);
            label4.TabIndex = 6;
            label4.Text = "Following Communities";
            // 
            // DateCreatedLB
            // 
            DateCreatedLB.AutoSize = true;
            DateCreatedLB.Location = new Point(6, 297);
            DateCreatedLB.Name = "DateCreatedLB";
            DateCreatedLB.Size = new Size(59, 25);
            DateCreatedLB.TabIndex = 5;
            DateCreatedLB.Text = "label4";
            // 
            // EmailLB
            // 
            EmailLB.AutoSize = true;
            EmailLB.Location = new Point(6, 199);
            EmailLB.Name = "EmailLB";
            EmailLB.Size = new Size(59, 25);
            EmailLB.TabIndex = 4;
            EmailLB.Text = "label4";
            // 
            // UsernameLB
            // 
            UsernameLB.AutoSize = true;
            UsernameLB.Location = new Point(6, 117);
            UsernameLB.Name = "UsernameLB";
            UsernameLB.Size = new Size(59, 25);
            UsernameLB.TabIndex = 3;
            UsernameLB.Text = "label4";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 272);
            label3.Name = "label3";
            label3.Size = new Size(112, 25);
            label3.TabIndex = 2;
            label3.Text = "Date created";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 174);
            label2.Name = "label2";
            label2.Size = new Size(54, 25);
            label2.TabIndex = 1;
            label2.Text = "Email";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 92);
            label1.Name = "label1";
            label1.Size = new Size(94, 25);
            label1.TabIndex = 0;
            label1.Text = "UserName";
            // 
            // MessageTab
            // 
            MessageTab.Controls.Add(ViewMessageBT);
            MessageTab.Controls.Add(ReceivedMessagesLiB);
            MessageTab.Controls.Add(label10);
            MessageTab.Controls.Add(groupBox1);
            MessageTab.Location = new Point(4, 34);
            MessageTab.Name = "MessageTab";
            MessageTab.Padding = new Padding(3);
            MessageTab.Size = new Size(1065, 542);
            MessageTab.TabIndex = 1;
            MessageTab.Text = "Message";
            MessageTab.UseVisualStyleBackColor = true;
            // 
            // ViewMessageBT
            // 
            ViewMessageBT.Location = new Point(781, 491);
            ViewMessageBT.Name = "ViewMessageBT";
            ViewMessageBT.Size = new Size(112, 34);
            ViewMessageBT.TabIndex = 3;
            ViewMessageBT.Text = "View";
            ViewMessageBT.UseVisualStyleBackColor = true;
            ViewMessageBT.Click += ViewMessageBT_Click;
            // 
            // ReceivedMessagesLiB
            // 
            ReceivedMessagesLiB.FormattingEnabled = true;
            ReceivedMessagesLiB.ItemHeight = 25;
            ReceivedMessagesLiB.Location = new Point(636, 31);
            ReceivedMessagesLiB.Name = "ReceivedMessagesLiB";
            ReceivedMessagesLiB.Size = new Size(411, 454);
            ReceivedMessagesLiB.TabIndex = 2;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(741, 3);
            label10.Name = "label10";
            label10.Size = new Size(164, 25);
            label10.TabIndex = 1;
            label10.Text = "Received Messages";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(SendMessageBT);
            groupBox1.Controls.Add(BodyRTB);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(SubjectTB);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(ToTB);
            groupBox1.Location = new Point(16, 19);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(599, 517);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Create Message";
            // 
            // SendMessageBT
            // 
            SendMessageBT.Location = new Point(202, 472);
            SendMessageBT.Name = "SendMessageBT";
            SendMessageBT.Size = new Size(112, 34);
            SendMessageBT.TabIndex = 6;
            SendMessageBT.Text = "Send";
            SendMessageBT.UseVisualStyleBackColor = true;
            SendMessageBT.Click += SendMessageBT_Click;
            // 
            // BodyRTB
            // 
            BodyRTB.Location = new Point(23, 237);
            BodyRTB.Name = "BodyRTB";
            BodyRTB.Size = new Size(533, 199);
            BodyRTB.TabIndex = 5;
            BodyRTB.Text = "";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(23, 209);
            label9.Name = "label9";
            label9.Size = new Size(53, 25);
            label9.TabIndex = 4;
            label9.Text = "Body";
            // 
            // SubjectTB
            // 
            SubjectTB.Location = new Point(23, 150);
            SubjectTB.Name = "SubjectTB";
            SubjectTB.Size = new Size(533, 31);
            SubjectTB.TabIndex = 3;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(23, 122);
            label8.Name = "label8";
            label8.Size = new Size(70, 25);
            label8.TabIndex = 2;
            label8.Text = "Subject";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(23, 38);
            label7.Name = "label7";
            label7.Size = new Size(122, 25);
            label7.TabIndex = 1;
            label7.Text = "To (username)";
            // 
            // ToTB
            // 
            ToTB.Location = new Point(23, 66);
            ToTB.Name = "ToTB";
            ToTB.Size = new Size(243, 31);
            ToTB.TabIndex = 0;
            // 
            // RegularUserForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1087, 593);
            Controls.Add(tabControl1);
            Name = "RegularUserForm";
            Text = "RegularUserForm";
            Load += RegularUserForm_Load;
            tabControl1.ResumeLayout(false);
            ProfileTab.ResumeLayout(false);
            ProfileTab.PerformLayout();
            MessageTab.ResumeLayout(false);
            MessageTab.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage ProfileTab;
        private TabPage MessageTab;
        private Label DateCreatedLB;
        private Label EmailLB;
        private Label UsernameLB;
        private Label label3;
        private Label label2;
        private Label label1;
        private ListBox FriendsLiB;
        private ListBox FollowingCommunitiesLiB;
        private Label label5;
        private Label label4;
        private Label label6;
        private ListBox ModForLiB;
        private GroupBox groupBox1;
        private Button SendMessageBT;
        private RichTextBox BodyRTB;
        private Label label9;
        private TextBox SubjectTB;
        private Label label8;
        private Label label7;
        private TextBox ToTB;
        private Button ViewMessageBT;
        private ListBox ReceivedMessagesLiB;
        private Label label10;
        private Label LocationLB;
        private Label label12;
        private Label GenderLB;
        private Label label11;
    }
}