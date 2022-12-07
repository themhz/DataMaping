namespace WindowsFormsApp1.forms.Childs
{
    partial class SelectXmlXsdPopup
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
            this.txtXsd = new System.Windows.Forms.TextBox();
            this.txtXml = new System.Windows.Forms.TextBox();
            this.btnSelectXsd = new System.Windows.Forms.Button();
            this.btnSelectXml = new System.Windows.Forms.Button();
            this.numOfTables = new System.Windows.Forms.Label();
            this.txtNumbOfTables = new System.Windows.Forms.Label();
            this.btnReadxml = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtXsd
            // 
            this.txtXsd.Location = new System.Drawing.Point(139, 88);
            this.txtXsd.Margin = new System.Windows.Forms.Padding(4);
            this.txtXsd.Name = "txtXsd";
            this.txtXsd.Size = new System.Drawing.Size(380, 22);
            this.txtXsd.TabIndex = 31;
            // 
            // txtXml
            // 
            this.txtXml.Location = new System.Drawing.Point(139, 52);
            this.txtXml.Margin = new System.Windows.Forms.Padding(4);
            this.txtXml.Name = "txtXml";
            this.txtXml.Size = new System.Drawing.Size(380, 22);
            this.txtXml.TabIndex = 30;
            // 
            // btnSelectXsd
            // 
            this.btnSelectXsd.Location = new System.Drawing.Point(27, 84);
            this.btnSelectXsd.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectXsd.Name = "btnSelectXsd";
            this.btnSelectXsd.Size = new System.Drawing.Size(100, 28);
            this.btnSelectXsd.TabIndex = 29;
            this.btnSelectXsd.Text = "Select Xsd";
            this.btnSelectXsd.UseVisualStyleBackColor = true;
            this.btnSelectXsd.Click += new System.EventHandler(this.btnSelectXsd_Click);
            // 
            // btnSelectXml
            // 
            this.btnSelectXml.Location = new System.Drawing.Point(27, 48);
            this.btnSelectXml.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectXml.Name = "btnSelectXml";
            this.btnSelectXml.Size = new System.Drawing.Size(100, 28);
            this.btnSelectXml.TabIndex = 28;
            this.btnSelectXml.Text = "Select Xml";
            this.btnSelectXml.UseVisualStyleBackColor = true;
            this.btnSelectXml.Click += new System.EventHandler(this.btnSelectXml_Click);
            // 
            // numOfTables
            // 
            this.numOfTables.AutoSize = true;
            this.numOfTables.Location = new System.Drawing.Point(246, 19);
            this.numOfTables.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numOfTables.Name = "numOfTables";
            this.numOfTables.Size = new System.Drawing.Size(14, 16);
            this.numOfTables.TabIndex = 27;
            this.numOfTables.Text = "0";
            // 
            // txtNumbOfTables
            // 
            this.txtNumbOfTables.AutoSize = true;
            this.txtNumbOfTables.Location = new System.Drawing.Point(135, 19);
            this.txtNumbOfTables.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtNumbOfTables.Name = "txtNumbOfTables";
            this.txtNumbOfTables.Size = new System.Drawing.Size(120, 16);
            this.txtNumbOfTables.TabIndex = 26;
            this.txtNumbOfTables.Text = "Number Of Tables:";
            // 
            // btnReadxml
            // 
            this.btnReadxml.Location = new System.Drawing.Point(27, 13);
            this.btnReadxml.Margin = new System.Windows.Forms.Padding(4);
            this.btnReadxml.Name = "btnReadxml";
            this.btnReadxml.Size = new System.Drawing.Size(100, 28);
            this.btnReadxml.TabIndex = 25;
            this.btnReadxml.Text = "ReadXml";
            this.btnReadxml.UseVisualStyleBackColor = true;
            this.btnReadxml.Click += new System.EventHandler(this.btnReadxml_Click);
            // 
            // SelectXmlXsdPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 253);
            this.Controls.Add(this.txtXsd);
            this.Controls.Add(this.txtXml);
            this.Controls.Add(this.btnSelectXsd);
            this.Controls.Add(this.btnSelectXml);
            this.Controls.Add(this.numOfTables);
            this.Controls.Add(this.txtNumbOfTables);
            this.Controls.Add(this.btnReadxml);
            this.Name = "SelectXmlXsdPopup";
            this.Text = "SelectXmlXsdPopup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtXsd;
        private System.Windows.Forms.TextBox txtXml;
        private System.Windows.Forms.Button btnSelectXsd;
        private System.Windows.Forms.Button btnSelectXml;
        private System.Windows.Forms.Label numOfTables;
        private System.Windows.Forms.Label txtNumbOfTables;
        private System.Windows.Forms.Button btnReadxml;
    }
}