namespace elle
{
    partial class ImmovableViewer
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
            TableViewer = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)TableViewer).BeginInit();
            SuspendLayout();
            // 
            // TableViewer
            // 
            TableViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TableViewer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TableViewer.Location = new Point(12, 12);
            TableViewer.Name = "TableViewer";
            TableViewer.Size = new Size(776, 426);
            TableViewer.TabIndex = 0;
            // 
            // ImmovableViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TableViewer);
            Name = "ImmovableViewer";
            Text = "ImmovableViewer";
            ((System.ComponentModel.ISupportInitialize)TableViewer).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView TableViewer;
    }
}