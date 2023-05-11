
namespace WindowsFormsApp1
{
    partial class DataMaper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataMaper));
            this.openXmlFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnScann = new System.Windows.Forms.Button();
            this.lstXmls = new System.Windows.Forms.ListBox();
            this.lblStatusText = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSynncFields = new System.Windows.Forms.Button();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableList = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.fieldList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.relationsList = new System.Windows.Forms.ListBox();
            this.fieldRelationList = new System.Windows.Forms.ListBox();
            this.btnClearDataGridViewRelations = new System.Windows.Forms.Button();
            this.txtXsd = new System.Windows.Forms.TextBox();
            this.txtXml = new System.Windows.Forms.TextBox();
            this.btnSelectXsd = new System.Windows.Forms.Button();
            this.btnSelectXml = new System.Windows.Forms.Button();
            this.numOfTables = new System.Windows.Forms.Label();
            this.txtNumbOfTables = new System.Windows.Forms.Label();
            this.btnReadxml = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewRelations = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRelations)).BeginInit();
            this.SuspendLayout();
            // 
            // openXmlFileDialog
            // 
            this.openXmlFileDialog.FileName = "openXmlFileDialog";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(1, 1);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(2046, 1123);
            this.splitContainer1.SplitterDistance = 681;
            this.splitContainer1.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnScann);
            this.panel1.Controls.Add(this.lstXmls);
            this.panel1.Controls.Add(this.lblStatusText);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnSynncFields);
            this.panel1.Controls.Add(this.splitContainer4);
            this.panel1.Controls.Add(this.btnClearDataGridViewRelations);
            this.panel1.Controls.Add(this.txtXsd);
            this.panel1.Controls.Add(this.txtXml);
            this.panel1.Controls.Add(this.btnSelectXsd);
            this.panel1.Controls.Add(this.btnSelectXml);
            this.panel1.Controls.Add(this.numOfTables);
            this.panel1.Controls.Add(this.txtNumbOfTables);
            this.panel1.Controls.Add(this.btnReadxml);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(681, 1123);
            this.panel1.TabIndex = 12;
            // 
            // btnScann
            // 
            this.btnScann.Location = new System.Drawing.Point(13, 185);
            this.btnScann.Name = "btnScann";
            this.btnScann.Size = new System.Drawing.Size(100, 23);
            this.btnScann.TabIndex = 33;
            this.btnScann.Text = "Full Scan";
            this.btnScann.UseVisualStyleBackColor = true;
            this.btnScann.Click += new System.EventHandler(this.btnScann_Click);
            // 
            // lstXmls
            // 
            this.lstXmls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstXmls.FormattingEnabled = true;
            this.lstXmls.ItemHeight = 16;
            this.lstXmls.Location = new System.Drawing.Point(14, 217);
            this.lstXmls.Name = "lstXmls";
            this.lstXmls.Size = new System.Drawing.Size(614, 116);
            this.lstXmls.TabIndex = 32;
            // 
            // lblStatusText
            // 
            this.lblStatusText.AutoSize = true;
            this.lblStatusText.Location = new System.Drawing.Point(122, 159);
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(0, 16);
            this.lblStatusText.TabIndex = 31;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(122, 133);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 16);
            this.lblStatus.TabIndex = 30;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 24);
            this.button1.TabIndex = 29;
            this.button1.Text = "Format";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSynncFields
            // 
            this.btnSynncFields.Location = new System.Drawing.Point(14, 126);
            this.btnSynncFields.Name = "btnSynncFields";
            this.btnSynncFields.Size = new System.Drawing.Size(100, 23);
            this.btnSynncFields.TabIndex = 27;
            this.btnSynncFields.Text = "Sync Fields";
            this.btnSynncFields.UseVisualStyleBackColor = true;
            this.btnSynncFields.Click += new System.EventHandler(this.btnSynncFields_Click);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer4.Location = new System.Drawing.Point(14, 347);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer4.Size = new System.Drawing.Size(614, 736);
            this.splitContainer4.SplitterDistance = 233;
            this.splitContainer4.TabIndex = 26;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.panel3);
            this.splitContainer3.Size = new System.Drawing.Size(614, 233);
            this.splitContainer3.SplitterDistance = 306;
            this.splitContainer3.TabIndex = 26;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tableList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(306, 233);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "Tables";
            // 
            // tableList
            // 
            this.tableList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableList.FormattingEnabled = true;
            this.tableList.ItemHeight = 16;
            this.tableList.Location = new System.Drawing.Point(0, 37);
            this.tableList.Margin = new System.Windows.Forms.Padding(4);
            this.tableList.Name = "tableList";
            this.tableList.Size = new System.Drawing.Size(306, 196);
            this.tableList.TabIndex = 22;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.fieldList);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(304, 233);
            this.panel3.TabIndex = 22;
            // 
            // fieldList
            // 
            this.fieldList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fieldList.FormattingEnabled = true;
            this.fieldList.ItemHeight = 16;
            this.fieldList.Location = new System.Drawing.Point(0, 37);
            this.fieldList.Margin = new System.Windows.Forms.Padding(4);
            this.fieldList.Name = "fieldList";
            this.fieldList.Size = new System.Drawing.Size(304, 196);
            this.fieldList.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "Fields - Types";
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.relationsList);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.fieldRelationList);
            this.splitContainer5.Size = new System.Drawing.Size(614, 499);
            this.splitContainer5.SplitterDistance = 261;
            this.splitContainer5.TabIndex = 27;
            // 
            // relationsList
            // 
            this.relationsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relationsList.FormattingEnabled = true;
            this.relationsList.ItemHeight = 16;
            this.relationsList.Location = new System.Drawing.Point(0, 0);
            this.relationsList.Margin = new System.Windows.Forms.Padding(4);
            this.relationsList.Name = "relationsList";
            this.relationsList.Size = new System.Drawing.Size(614, 261);
            this.relationsList.TabIndex = 15;
            // 
            // fieldRelationList
            // 
            this.fieldRelationList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldRelationList.FormattingEnabled = true;
            this.fieldRelationList.ItemHeight = 16;
            this.fieldRelationList.Location = new System.Drawing.Point(0, 0);
            this.fieldRelationList.Margin = new System.Windows.Forms.Padding(4);
            this.fieldRelationList.Name = "fieldRelationList";
            this.fieldRelationList.Size = new System.Drawing.Size(614, 234);
            this.fieldRelationList.TabIndex = 19;
            // 
            // btnClearDataGridViewRelations
            // 
            this.btnClearDataGridViewRelations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearDataGridViewRelations.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClearDataGridViewRelations.Location = new System.Drawing.Point(14, 1090);
            this.btnClearDataGridViewRelations.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearDataGridViewRelations.Name = "btnClearDataGridViewRelations";
            this.btnClearDataGridViewRelations.Size = new System.Drawing.Size(100, 28);
            this.btnClearDataGridViewRelations.TabIndex = 17;
            this.btnClearDataGridViewRelations.Text = "Clear";
            this.btnClearDataGridViewRelations.UseVisualStyleBackColor = true;
            // 
            // txtXsd
            // 
            this.txtXsd.Location = new System.Drawing.Point(125, 95);
            this.txtXsd.Margin = new System.Windows.Forms.Padding(4);
            this.txtXsd.Name = "txtXsd";
            this.txtXsd.Size = new System.Drawing.Size(380, 22);
            this.txtXsd.TabIndex = 24;
            // 
            // txtXml
            // 
            this.txtXml.Location = new System.Drawing.Point(125, 59);
            this.txtXml.Margin = new System.Windows.Forms.Padding(4);
            this.txtXml.Name = "txtXml";
            this.txtXml.Size = new System.Drawing.Size(380, 22);
            this.txtXml.TabIndex = 23;
            // 
            // btnSelectXsd
            // 
            this.btnSelectXsd.Location = new System.Drawing.Point(13, 91);
            this.btnSelectXsd.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectXsd.Name = "btnSelectXsd";
            this.btnSelectXsd.Size = new System.Drawing.Size(100, 28);
            this.btnSelectXsd.TabIndex = 22;
            this.btnSelectXsd.Text = "Select Xsd";
            this.btnSelectXsd.UseVisualStyleBackColor = true;
            this.btnSelectXsd.Click += new System.EventHandler(this.btnSelectXsd_Click);
            // 
            // btnSelectXml
            // 
            this.btnSelectXml.Location = new System.Drawing.Point(13, 55);
            this.btnSelectXml.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectXml.Name = "btnSelectXml";
            this.btnSelectXml.Size = new System.Drawing.Size(100, 28);
            this.btnSelectXml.TabIndex = 21;
            this.btnSelectXml.Text = "Select Xml";
            this.btnSelectXml.UseVisualStyleBackColor = true;
            this.btnSelectXml.Click += new System.EventHandler(this.btnSelectXml_Click);
            // 
            // numOfTables
            // 
            this.numOfTables.AutoSize = true;
            this.numOfTables.Location = new System.Drawing.Point(232, 26);
            this.numOfTables.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numOfTables.Name = "numOfTables";
            this.numOfTables.Size = new System.Drawing.Size(14, 16);
            this.numOfTables.TabIndex = 13;
            this.numOfTables.Text = "0";
            // 
            // txtNumbOfTables
            // 
            this.txtNumbOfTables.AutoSize = true;
            this.txtNumbOfTables.Location = new System.Drawing.Point(121, 26);
            this.txtNumbOfTables.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtNumbOfTables.Name = "txtNumbOfTables";
            this.txtNumbOfTables.Size = new System.Drawing.Size(120, 16);
            this.txtNumbOfTables.TabIndex = 12;
            this.txtNumbOfTables.Text = "Number Of Tables:";
            // 
            // btnReadxml
            // 
            this.btnReadxml.Location = new System.Drawing.Point(13, 20);
            this.btnReadxml.Margin = new System.Windows.Forms.Padding(4);
            this.btnReadxml.Name = "btnReadxml";
            this.btnReadxml.Size = new System.Drawing.Size(100, 28);
            this.btnReadxml.TabIndex = 10;
            this.btnReadxml.Text = "ReadXml";
            this.btnReadxml.UseVisualStyleBackColor = true;
            this.btnReadxml.Click += new System.EventHandler(this.btnReadxml_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridViewRelations);
            this.splitContainer2.Size = new System.Drawing.Size(1361, 1123);
            this.splitContainer2.SplitterDistance = 537;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(1361, 537);
            this.dataGridView.TabIndex = 21;
            // 
            // dataGridViewRelations
            // 
            this.dataGridViewRelations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRelations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRelations.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewRelations.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewRelations.Name = "dataGridViewRelations";
            this.dataGridViewRelations.RowHeadersWidth = 51;
            this.dataGridViewRelations.Size = new System.Drawing.Size(1361, 582);
            this.dataGridViewRelations.TabIndex = 21;
            // 
            // DataMaper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2059, 1126);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DataMaper";
            this.Text = "DataBase Viewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRelations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openXmlFileDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtXsd;
        private System.Windows.Forms.TextBox txtXml;
        private System.Windows.Forms.Button btnSelectXsd;
        private System.Windows.Forms.Button btnSelectXml;
        private System.Windows.Forms.Label numOfTables;
        private System.Windows.Forms.Label txtNumbOfTables;
        private System.Windows.Forms.Button btnReadxml;
        private System.Windows.Forms.Button btnClearDataGridViewRelations;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridView dataGridViewRelations;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.ListBox relationsList;
        private System.Windows.Forms.ListBox fieldRelationList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox tableList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListBox fieldList;
        private System.Windows.Forms.Button btnSynncFields;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStatusText;
        private System.Windows.Forms.ListBox lstXmls;
        private System.Windows.Forms.Button btnScann;
    }
}

