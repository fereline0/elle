namespace elle
{
    partial class AdminPanel
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
            HandleDelete = new Button();
            RefreshTable = new Button();
            TableSelector = new ComboBox();
            HandleAdd = new Button();
            DynamicFieldsPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)TableViewer).BeginInit();
            SuspendLayout();
            // 
            // TableViewer
            // 
            TableViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TableViewer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TableViewer.Location = new Point(12, 12);
            TableViewer.Name = "TableViewer";
            TableViewer.Size = new Size(624, 477);
            TableViewer.TabIndex = 0;
            TableViewer.CellEndEdit += TableViewer_CellEndEdit;
            // 
            // HandleDelete
            // 
            HandleDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            HandleDelete.Location = new Point(642, 437);
            HandleDelete.Name = "HandleDelete";
            HandleDelete.Size = new Size(156, 23);
            HandleDelete.TabIndex = 1;
            HandleDelete.Text = "Delete";
            HandleDelete.Click += HandleDelete_Click;
            // 
            // RefreshTable
            // 
            RefreshTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            RefreshTable.Location = new Point(642, 466);
            RefreshTable.Name = "RefreshTable";
            RefreshTable.Size = new Size(156, 23);
            RefreshTable.TabIndex = 2;
            RefreshTable.Text = "Refresh table";
            RefreshTable.UseVisualStyleBackColor = true;
            RefreshTable.Click += RefreshTable_Click;
            // 
            // TableSelector
            // 
            TableSelector.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TableSelector.FormattingEnabled = true;
            TableSelector.Location = new Point(642, 12);
            TableSelector.Name = "TableSelector";
            TableSelector.Size = new Size(156, 23);
            TableSelector.TabIndex = 3;
            TableSelector.SelectedIndexChanged += TableSelector_SelectedIndexChanged;
            // 
            // HandleAdd
            // 
            HandleAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            HandleAdd.BackgroundImageLayout = ImageLayout.Center;
            HandleAdd.Location = new Point(642, 408);
            HandleAdd.Name = "HandleAdd";
            HandleAdd.Size = new Size(156, 23);
            HandleAdd.TabIndex = 4;
            HandleAdd.Text = "Add";
            HandleAdd.UseVisualStyleBackColor = true;
            HandleAdd.Click += HandleAdd_Click;
            // 
            // DynamicFieldsPanel
            // 
            DynamicFieldsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DynamicFieldsPanel.Location = new Point(642, 41);
            DynamicFieldsPanel.Name = "DynamicFieldsPanel";
            DynamicFieldsPanel.Size = new Size(156, 361);
            DynamicFieldsPanel.TabIndex = 5;
            // 
            // AdminPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 501);
            Controls.Add(DynamicFieldsPanel);
            Controls.Add(HandleAdd);
            Controls.Add(TableSelector);
            Controls.Add(RefreshTable);
            Controls.Add(HandleDelete);
            Controls.Add(TableViewer);
            Name = "AdminPanel";
            Text = "AdminPanel";
            ((System.ComponentModel.ISupportInitialize)TableViewer).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView TableViewer;
        private Button HandleDelete;
        private Button RefreshTable;
        private ComboBox TableSelector;
        private Button HandleAdd;
        private Panel DynamicFieldsPanel;
    }
}