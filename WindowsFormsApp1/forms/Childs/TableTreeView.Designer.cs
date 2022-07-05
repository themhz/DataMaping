
namespace WindowsFormsApp1.forms.Childs {
    partial class TableTreeView {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.DatabaseTreeView = new System.Windows.Forms.TreeView();
            this.dgvTableRows = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableRows)).BeginInit();
            this.SuspendLayout();
            // 
            // DatabaseTreeView
            // 
            this.DatabaseTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DatabaseTreeView.Location = new System.Drawing.Point(2, 1);
            this.DatabaseTreeView.Name = "DatabaseTreeView";
            this.DatabaseTreeView.Size = new System.Drawing.Size(243, 599);
            this.DatabaseTreeView.TabIndex = 0;
            this.DatabaseTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DatabaseTreeView_AfterSelect);
            // 
            // dgvTableRows
            // 
            this.dgvTableRows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTableRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableRows.Location = new System.Drawing.Point(251, 120);
            this.dgvTableRows.Name = "dgvTableRows";
            this.dgvTableRows.Size = new System.Drawing.Size(749, 480);
            this.dgvTableRows.TabIndex = 1;
            // 
            // TableTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 601);
            this.Controls.Add(this.dgvTableRows);
            this.Controls.Add(this.DatabaseTreeView);
            this.Name = "TableTreeView";
            this.Text = "TableTreeView";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableRows)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView DatabaseTreeView;
        private System.Windows.Forms.DataGridView dgvTableRows;
    }
}