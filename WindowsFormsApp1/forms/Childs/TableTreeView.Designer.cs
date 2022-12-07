
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableRows)).BeginInit();
            this.SuspendLayout();
            // 
            // DatabaseTreeView
            // 
            this.DatabaseTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DatabaseTreeView.Location = new System.Drawing.Point(3, 1);
            this.DatabaseTreeView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DatabaseTreeView.Name = "DatabaseTreeView";
            this.DatabaseTreeView.Size = new System.Drawing.Size(323, 736);
            this.DatabaseTreeView.TabIndex = 0;
            this.DatabaseTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DatabaseTreeView_AfterSelect);
            // 
            // dgvTableRows
            // 
            this.dgvTableRows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTableRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableRows.Location = new System.Drawing.Point(335, 148);
            this.dgvTableRows.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvTableRows.Name = "dgvTableRows";
            this.dgvTableRows.RowHeadersWidth = 51;
            this.dgvTableRows.Size = new System.Drawing.Size(999, 591);
            this.dgvTableRows.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(331, 110);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Βάθος:";
            // 
            // lblPath
            // 
            this.lblPath.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblPath.Location = new System.Drawing.Point(393, 110);
            this.lblPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblPath.Name = "lblPath";
            this.lblPath.ReadOnly = true;
            this.lblPath.Size = new System.Drawing.Size(925, 15);
            this.lblPath.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(333, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "XML-XSL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TableTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 740);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTableRows);
            this.Controls.Add(this.DatabaseTreeView);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TableTreeView";
            this.Text = "TableTreeView";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableRows)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView DatabaseTreeView;
        private System.Windows.Forms.DataGridView dgvTableRows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lblPath;
        private System.Windows.Forms.Button button1;
    }
}