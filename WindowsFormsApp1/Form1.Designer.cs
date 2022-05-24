
namespace WindowsFormsApp1
{
    partial class Form1
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.relationsList = new System.Windows.Forms.ListBox();
            this.fieldList = new System.Windows.Forms.ListBox();
            this.numOfTables = new System.Windows.Forms.Label();
            this.txtNumbOfTables = new System.Windows.Forms.Label();
            this.tableList = new System.Windows.Forms.ListBox();
            this.btnReadxml = new System.Windows.Forms.Button();
            this.dataGridViewRelations = new System.Windows.Forms.DataGridView();
            this.fieldRelationList = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRelations)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(430, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(905, 311);
            this.dataGridView.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.fieldRelationList);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.relationsList);
            this.panel1.Controls.Add(this.fieldList);
            this.panel1.Controls.Add(this.numOfTables);
            this.panel1.Controls.Add(this.txtNumbOfTables);
            this.panel1.Controls.Add(this.tableList);
            this.panel1.Controls.Add(this.btnReadxml);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 675);
            this.panel1.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Table Relations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Fields - Types";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Tables";
            // 
            // relationsList
            // 
            this.relationsList.FormattingEnabled = true;
            this.relationsList.Location = new System.Drawing.Point(10, 304);
            this.relationsList.Name = "relationsList";
            this.relationsList.Size = new System.Drawing.Size(398, 147);
            this.relationsList.TabIndex = 15;
            // 
            // fieldList
            // 
            this.fieldList.FormattingEnabled = true;
            this.fieldList.Location = new System.Drawing.Point(219, 66);
            this.fieldList.Name = "fieldList";
            this.fieldList.Size = new System.Drawing.Size(189, 212);
            this.fieldList.TabIndex = 14;
            // 
            // numOfTables
            // 
            this.numOfTables.AutoSize = true;
            this.numOfTables.Location = new System.Drawing.Point(174, 21);
            this.numOfTables.Name = "numOfTables";
            this.numOfTables.Size = new System.Drawing.Size(13, 13);
            this.numOfTables.TabIndex = 13;
            this.numOfTables.Text = "0";
            // 
            // txtNumbOfTables
            // 
            this.txtNumbOfTables.AutoSize = true;
            this.txtNumbOfTables.Location = new System.Drawing.Point(91, 21);
            this.txtNumbOfTables.Name = "txtNumbOfTables";
            this.txtNumbOfTables.Size = new System.Drawing.Size(96, 13);
            this.txtNumbOfTables.TabIndex = 12;
            this.txtNumbOfTables.Text = "Number Of Tables:";
            // 
            // tableList
            // 
            this.tableList.FormattingEnabled = true;
            this.tableList.Location = new System.Drawing.Point(10, 66);
            this.tableList.Name = "tableList";
            this.tableList.Size = new System.Drawing.Size(203, 212);
            this.tableList.TabIndex = 11;
            // 
            // btnReadxml
            // 
            this.btnReadxml.Location = new System.Drawing.Point(10, 16);
            this.btnReadxml.Name = "btnReadxml";
            this.btnReadxml.Size = new System.Drawing.Size(75, 23);
            this.btnReadxml.TabIndex = 10;
            this.btnReadxml.Text = "ReadXml";
            this.btnReadxml.UseVisualStyleBackColor = true;
            this.btnReadxml.Click += new System.EventHandler(this.btnReadxml_Click);
            // 
            // dataGridViewRelations
            // 
            this.dataGridViewRelations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewRelations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRelations.Location = new System.Drawing.Point(430, 319);
            this.dataGridViewRelations.Name = "dataGridViewRelations";
            this.dataGridViewRelations.Size = new System.Drawing.Size(905, 358);
            this.dataGridViewRelations.TabIndex = 13;
            // 
            // fieldRelationList
            // 
            this.fieldRelationList.FormattingEnabled = true;
            this.fieldRelationList.Location = new System.Drawing.Point(13, 479);
            this.fieldRelationList.Name = "fieldRelationList";
            this.fieldRelationList.Size = new System.Drawing.Size(395, 186);
            this.fieldRelationList.TabIndex = 19;            
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 463);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Field - Relations";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 679);
            this.Controls.Add(this.dataGridViewRelations);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView);
            this.Name = "Form1";
            this.Text = "DataBase Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRelations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox relationsList;
        private System.Windows.Forms.ListBox fieldList;
        private System.Windows.Forms.Label numOfTables;
        private System.Windows.Forms.Label txtNumbOfTables;
        private System.Windows.Forms.ListBox tableList;
        private System.Windows.Forms.Button btnReadxml;
        private System.Windows.Forms.DataGridView dataGridViewRelations;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox fieldRelationList;
    }
}

