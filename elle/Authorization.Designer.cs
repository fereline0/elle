namespace elle
{
    partial class Authorization
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
            FormLabel = new Label();
            Login = new TextBox();
            Password = new TextBox();
            HandleAuthorize = new Button();
            SuspendLayout();
            // 
            // FormLabel
            // 
            FormLabel.AutoSize = true;
            FormLabel.Location = new Point(12, 9);
            FormLabel.Name = "FormLabel";
            FormLabel.Size = new Size(79, 15);
            FormLabel.TabIndex = 0;
            FormLabel.Text = "Authorization";
            // 
            // Login
            // 
            Login.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Login.Location = new Point(12, 27);
            Login.Name = "Login";
            Login.Size = new Size(452, 23);
            Login.TabIndex = 1;
            // 
            // Password
            // 
            Password.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Password.Location = new Point(12, 56);
            Password.Name = "Password";
            Password.PasswordChar = '*';
            Password.Size = new Size(452, 23);
            Password.TabIndex = 2;
            // 
            // HandleAuthorize
            // 
            HandleAuthorize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            HandleAuthorize.Location = new Point(12, 85);
            HandleAuthorize.Name = "HandleAuthorize";
            HandleAuthorize.Size = new Size(452, 23);
            HandleAuthorize.TabIndex = 3;
            HandleAuthorize.Text = "Authorize";
            HandleAuthorize.UseVisualStyleBackColor = true;
            HandleAuthorize.Click += HandleAuthorize_Click;
            // 
            // Authorization
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(476, 232);
            Controls.Add(HandleAuthorize);
            Controls.Add(Password);
            Controls.Add(Login);
            Controls.Add(FormLabel);
            Name = "Authorization";
            Text = "Authorization";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label FormLabel;
        private TextBox Login;
        private TextBox Password;
        private Button HandleAuthorize;
    }
}
