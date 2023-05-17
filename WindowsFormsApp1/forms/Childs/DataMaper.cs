﻿using FastMember;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Linq.Expressions;
using System.Data.Entity;
using System.Collections;

using System.Linq.Dynamic;
using WindowsFormsApp1.interfaces;
using System.Configuration;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WindowsFormsApp1.classes;
using Formatting = Newtonsoft.Json.Formatting;
using System.Runtime.Remoting.Contexts;
using DevExpress.XtraEditors.Controls;
using static DevExpress.Utils.Svg.CommonSvgImages;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DevExpress.XtraEditors.SyntaxEditor;
using DevExpress.XtraEditors.TextEditController.Utils;

namespace WindowsFormsApp1
{
    public partial class DataMaper : Form, IForm
    {

        public Xml xml { get; set; }

        string dataSetPath = "";
        string dataSetPathSchema = "";
        public DataMaper()
        {
            InitializeComponent();
            tableList.SelectedValueChanged += new EventHandler(tableList_SelectedValueChanged);
            relationsList.SelectedValueChanged += new EventHandler(relationsList_SelectedValueChanged);
            dataGridView.DataError += dataGridView_DataError;


            //txtXml.Text = dataSetPath = @"C:\Users\themis\Desktop\test\dataHeatInsulation.xml";
            //txtXsd.Text = dataSetPathSchema = @"C:\Users\themis\Desktop\test\dsBuildingHeatInsulation.xsd";

            txtXml.Text = ConfigurationManager.AppSettings["XmlPath"];
            txtXsd.Text = ConfigurationManager.AppSettings["XsdPath"];
            dataSetPath = txtXml.Text;
            dataSetPathSchema = txtXsd.Text;
            readXml();


        }

        public void tableList_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox listbox = (ListBox)sender;

            DataTable dataTable = xml.GetDataSet().Tables[listbox.SelectedItem.ToString()];
            this.Text = this.Text.Substring(0, this.Text.IndexOf(".xsd")+4)+ "-" + listbox.SelectedItem.ToString();
            fieldList.Items.Clear();
            dataGridView.Columns.Clear();
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                fieldList.Items.Add(dataColumn.ColumnName + " (" + dataColumn.DataType + ")");
                dataGridView.Columns.Add(dataColumn.ColumnName, dataColumn.ColumnName);
            }

            populateDataGridView(dataTable);
            populateRelationsList(dataTable);

        }

        public void relationsList_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox listbox = (ListBox)sender;

            var table = listbox.SelectedItem.ToString().Split('_');

            DataTable dataTable = xml.GetDataSet().Tables[table[1]];
            dataGridViewRelations.Columns.Clear();
            if (dataTable != null)
            {

                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    dataGridViewRelations.Columns.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                }
                populateRelationDataGridView(dataTable);
                populateFieldRelationList(dataTable);
            }
        }

        private void populateFieldRelationList(DataTable dataTable)
        {
            fieldRelationList.Items.Clear();
            for (int i = 0; i < dataTable.ParentRelations.Count; i++)
            {
                foreach (var column in dataTable.ParentRelations[i].ChildColumns)
                {
                    for (int j = 0; j < dataTable.ParentRelations[i].ParentColumns.Count(); j++)
                    {
                        fieldRelationList.Items.Add(dataTable.ParentRelations[i].ParentTable.TableName + "->" + dataTable.ParentRelations[i].ParentColumns[j].ToString() + " - " + column.ColumnName);
                    }
                }
            }
        }
        private void populateRelationsList(DataTable dataTable)
        {

            relationsList.Items.Clear();
            foreach (var relations in dataTable.ChildRelations)
            {
                relationsList.Items.Add(relations.ToString());
            }
        }

        private void populateDataGridView(DataTable dataTable)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear(); // clear existing columns
            dataGridView.AutoGenerateColumns = false;
            bool hasImage = false;

            List<int> imageColumns = new List<int>();

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                if (dataTable.Columns[i].DataType == typeof(byte[]))
                {
                    hasImage = true;
                    imageColumns.Add(i);

                    // Create a new image column
                    DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                    imageCol.HeaderText = dataTable.Columns[i].ColumnName;
                    imageCol.Name = dataTable.Columns[i].ColumnName;
                    imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom; // set as needed
                    

                    // Add the image column to the dataGridView
                    dataGridView.Columns.Add(imageCol);
                }
                else
                {
                    DataGridViewTextBoxColumn textCol = new DataGridViewTextBoxColumn();
                    textCol.HeaderText = dataTable.Columns[i].ColumnName;
                    textCol.Name = dataTable.Columns[i].ColumnName;
                    textCol.DataPropertyName = dataTable.Columns[i].ColumnName;

                    // Add the text column to the dataGridView
                    dataGridView.Columns.Add(textCol);
                }
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                DataGridViewRow row = new DataGridViewRow();

                if (hasImage)
                {
                    row.Height = 150; // Set this value to what you need
                }
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (imageColumns.Contains(i) && dataRow[i] != DBNull.Value)
                    {
                        byte[] imageByteArray = dataRow[i] as byte[];
                        DataGridViewImageCell imageCell = new DataGridViewImageCell();
                        imageCell.Value = ByteArrayToImage(imageByteArray);
                        row.Cells.Add(imageCell);
                    }
                    else if (imageColumns.Contains(i) && dataRow[i] == DBNull.Value)
                    {
                        //if null image
                        DataGridViewTextBoxCell textCell = new DataGridViewTextBoxCell();
                        byte[] defaultImageBytes = new byte[]
                            {
                                137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 1, 0, 0, 0, 1, 8, 6, 0, 0, 0, 31, 21, 196, 137, 0, 0, 0, 11, 73, 68, 65, 84, 120, 156, 99, 248, 255, 255, 63, 0, 5, 0, 1, 2, 130, 147, 160, 189, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130
                            };
                      
                        textCell.Value = ByteArrayToImage(defaultImageBytes);
                        row.Cells.Add(textCell);
                    }
                    else
                    {
                        DataGridViewTextBoxCell textCell = new DataGridViewTextBoxCell();                        
                        textCell.Value = dataRow[i];                        
                        row.Cells.Add(textCell);
                    }
                }

                dataGridView.Rows.Add(row);
            }
        }

        private void populateRelationDataGridView(DataTable dataTable)
        {
            dataGridViewRelations.Rows.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataGridViewRelations.Rows.Add(dataRow.ItemArray);
            }
        }
        

        private void btnReadxml_Click(object sender, EventArgs e)
        {
            readXml();

        }

        public void readXml()
        {
            if (dataSetPath == "" || dataSetPathSchema == "")
            {
                MessageBox.Show("Please select an xml and an xsd file");
                numOfTables.Text = "0";
            }
            else
            {
                xml = new Xml(dataSetPath, dataSetPathSchema);
                tableList.Items.Clear();

                List<string> tables = new List<string>();


                foreach (var table in xml.GetDataSet().Tables)
                {
                    tables.Add(table.ToString());
                    //tableList.Items.Add(table.ToString());
                }
                tables.Sort();


                foreach (string table in tables)
                {
                    tableList.Items.Add(table);
                }


                numOfTables.Text = xml.GetDataSet().Tables.Count.ToString();
            }
            this.Text = txtXsd.Text.Split('\\').Last();
        }

        private void btnSelectXml_Click(object sender, EventArgs e)
        {
            dataSetPath = openSelectFileBox("xml");
            if(dataSetPath!=null && dataSetPath != "")
            {
                txtXml.Text = dataSetPath;
                dataSetPathSchema = Path.GetDirectoryName(dataSetPath) + "\\" + GetXsdString(dataSetPath);
                txtXsd.Text = dataSetPathSchema;

                this.Text = dataSetPathSchema.Split('\\').Last();
            }
            
        }

        public string GetXsdString(string filePath)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;

            // Create an XmlReader for the XML file
            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
                // Read the XML document and its schema(s)
                string xsdName = "";
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.NamespaceURI != "")
                    {
                        xsdName = reader.NamespaceURI.Split('/').Last();
                        break;
                    }
                }
                return xsdName;
            }
        }


        private void btnSelectXsd_Click(object sender, EventArgs e)
        {
            dataSetPathSchema = openSelectFileBox("xsd");
            txtXsd.Text = dataSetPathSchema;
            this.Text = dataSetPathSchema.Split('\\').Last();
        }
        private string openSelectFileBox(string ext = "")
        {

            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"C:\Users\themis\Desktop\test";
                if (ext != "")
                {
                    openFileDialog.Filter = ext + " files (*." + ext + ")|*." + ext + "|All files (*.*)|*.*";
                }
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }

            return filePath;
        }             

        string inputJsonFilePath = "d:\\ProjNet2022\\applications\\Building.Project\\Building.UI\\data.el\\reports\\Building.Accessible\\AccessibleReport\\fields.txt";
        string outputJsonFilePath = "d:\\ProjNet2022\\applications\\Building.Project\\Building.UI\\data.el\\reports\\Building.Accessible\\AccessibleReport\\fields_old.txt";
        string newinputJsonFilePath = "d:\\ProjNet2022\\applications\\Building.Project\\Building.UI\\data.el\\reports\\Building.Accessible\\AccessibleReport\\fields_new.txt";
        string xsdDirectoryPath = "c:\\Users\\themis\\Documents\\xmlOutput\\accessible\\";
        private async void btnSynncFields_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Running";
            lblStatus.ForeColor = Color.Green;
            await Task.Run(() => Sync());

            lblStatus.Text = "Done!";
            lblStatus.ForeColor = Color.Blue;
        }

        public async void Sync()
        {
            //SyncFields();

            //Check if the fields.txt exist
            JsonHandler.CreateFieldsFileIfNotExists(inputJsonFilePath);

            //Backup the json file
            JObject jsonObject = JsonHandler.ReadJsonFile(inputJsonFilePath);
            JsonHandler.CopyJsonFile(inputJsonFilePath, outputJsonFilePath);

            SyncSchemas(jsonObject);
            SyncTables(jsonObject);
            SyncFields(jsonObject);
        }
        public void SyncSchemas(JObject jsonObject)
        {
            
            List<string> xsdFiles = XsdHandler.ListXSDFilesInDirectory(xsdDirectoryPath);

            //Loop through the xsdFiles
            foreach (var xsdFile in xsdFiles)
            {                
                //Check if the xsd SCHEMA exist in the fields.txt json file
                string xsdSchemaName = xsdFile.ToString().Split('\\').Last().Split('.')[0];
                this.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Text = "Syncing schemas";
                    lblStatusText.Text = $"Working on {xsdSchemaName}";
                });
                if (!JsonHandler.CheckIfSchemaExistInFile(jsonObject, xsdSchemaName))
                {
                    //Add the schema                   
                    JObject newSchema = new JObject
                                {
                                    { "Name", xsdSchemaName },
                                    { "Tables", new JArray() },
                                };

                    //Add schema in the fields.txt json file and save it
                    JObject newJsonObject = JsonHandler.AddSchemaInFile(jsonObject, newSchema);
                    JsonHandler.WriteFile(newinputJsonFilePath, newJsonObject);
                }
            }
        }
        public void SyncTables(JObject jsonObject)
        {

            List<string> xsdFiles = XsdHandler.ListXSDFilesInDirectory(xsdDirectoryPath);

            //Loop through the xsdFiles
            foreach (var xsdFile in xsdFiles)
            {                
                string xsdSchemaName = xsdFile.ToString().Split('\\').Last().Split('.')[0];
                List<TableInfo> xsdTables = XsdHandler.GetTablesFromXSD(xsdFile, xsdSchemaName);                
                //Loop through the xsdTables of the xsdFile
                foreach (var xsdTable in xsdTables)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblStatus.Text = "Syncing Tables";
                        lblStatusText.Text = $"Working on {xsdSchemaName} {xsdTable.TableName}";
                    });
                    //Check if the xsd TABLE exists in the fields.txt json file                    
                    if (!JsonHandler.CheckIfTableExistsInFile(jsonObject, xsdTable.TableName))
                    {
                        //Add the table
                        JObject newTable = new JObject
                                        {
                                            { "table", xsdTable.TableName },
                                            { "alias", xsdTable.TableName },
                                            { "Fields", new JArray() }
                                        };
                        //Add the table in the fields.txt json file and save it
                        JObject newJsonObject = JsonHandler.AddTableToSchemaInFile(jsonObject, xsdSchemaName, newTable);
                        JsonHandler.WriteFile(newinputJsonFilePath, newJsonObject);
                    }
                }
            }
        }       
        public void SyncFields(JObject jsonObject)
        {                        

            //Get the xsd files
            List<string> xsdFiles = XsdHandler.ListXSDFilesInDirectory(xsdDirectoryPath);

            //Loop through the xsdFiles
            foreach (var xsdFile in xsdFiles)
            {
                //Check if the xsd SCHEMA exist in the fields.txt json file
                string xsdSchemaName = xsdFile.ToString().Split('\\').Last().Split('.')[0];
                if (JsonHandler.CheckIfSchemaExistInFile(jsonObject, xsdSchemaName))
                {
                    List<TableInfo> xsdTables = XsdHandler.GetTablesFromXSD(xsdFile, xsdSchemaName);
                    //Loop through the xsdTables of the xsdFile
                    foreach (var xsdTable in xsdTables)
                    {
                        //Check if the xsd TABLE exists in the fields.txt json file                    
                        if (JsonHandler.CheckIfTableExistsInFile(jsonObject, xsdTable.TableName))
                        {
                            //Loop through the fields of the xsdTable
                            foreach (var xsdField in xsdTable.FieldNames)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    lblStatus.Text = "Syncing Fields";
                                    lblStatusText.Text = $"Working on {xsdSchemaName} {xsdTable.TableName} {xsdField}";
                                });
                                //CheckBox if the xsd FIELD NOT exists in the fields.txt json file
                                if (!JsonHandler.CheckIfFieldExistsInFile(jsonObject, xsdField, xsdTable.TableName))
                                {
                                    //If it doesn't then add it
                                    JObject newField = new JObject
                                        {
                                            { "name", xsdField },
                                            { "alias", "" },
                                            { "format", "" },
                                            { "formatNull", "" }
                                        };

                                    //Add the field in the fields.txt json file and save it
                                    JObject newJsonObject = JsonHandler.AddFieldToTableInFile(jsonObject, xsdTable.TableName, newField);
                                    JsonHandler.WriteFile(newinputJsonFilePath, newJsonObject);
                                }
                            }
                        }                        
                    }
                }                
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string directory = "d:\\ProjNet2022\\applications\\Building.Project\\Building.UI\\data.el\\reports\\Building.Accessible\\AccessibleReport\\";
            JsonHandler.formatJsonToReadAbleFormat(directory);

        }

        private void btnScann_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    //ScanDirectory(fbd.SelectedPath, out List<string> xmlFiles, out List<string> xsdFiles);

                    // Now xmlFiles and xsdFiles contain the paths to the XML and XSD files, respectively.
                    // Do whatever you need to do with these files.

                    var scanner = new FileScanner();
            scanner.ScanDirectory("d:\\ProjNet2022\\applications\\Building.EnergySavingFProject\\", out List<string> xmlFiles, out List<string> xsdFiles);

            lstXmls.Items.Clear();

            foreach(string xmlFile in xmlFiles)
            {
                lstXmls.Items.Add(xmlFile);
            }
                }
            }
            
            
            //lstXmls.Items.Add("test1");
            //lstXmls.Items.Add("test2");
            //lstXmls.Items.Add("test3");
            //lstXmls.Items.Add("test1");
            //lstXmls.Items.Add("test2");
            //lstXmls.Items.Add("test3");
            //lstXmls.Items.Add("test1");
            //lstXmls.Items.Add("test2");
            //lstXmls.Items.Add("test3");

        }

        private void fieldList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected item from the list box and convert it to string
            string selectedItem = fieldList.SelectedItem.ToString();

            // Parse the column name from the selected item
            // assuming the format is "ColumnName(DataType)"
            string columnName = selectedItem.Substring(0, selectedItem.IndexOf('(')).Trim();

            // Loop through all columns of the data grid view
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                // If the name of the column matches the parsed column name
                if (dataGridView.Columns[i].Name == columnName)
                {
                    // Clear the previous selection
                    dataGridView.ClearSelection();
                    // Color and select the matching column
                    ColorColumnAndSelect(i);
                    break;
                }
            }
        }

        int prevcolumnIndex = 0;
        private void ColorColumnAndSelect(int columnIndex)
        {            

            Color selectedColor = Color.Yellow; // Change to your preferred color
            foreach (DataGridViewRow row in dataGridView.Rows)
            {                
                if (!row.IsNewRow)
                {
                    row.Cells[prevcolumnIndex].Style.BackColor = Color.White;
                    row.Cells[prevcolumnIndex].Selected = false;

                    row.Cells[columnIndex].Style.BackColor = selectedColor;
                    row.Cells[columnIndex].Selected = true;
                    prevcolumnIndex = columnIndex;
                }
            }

            // Scroll to the first cell of the selected column
            dataGridView.FirstDisplayedScrollingColumnIndex = columnIndex;
        }

        private void tableList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyToClipboard(tableList);
            }
        }

        private void fieldList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyToClipboard(fieldList);
            }
        }

        private void relationsList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyToClipboard(relationsList);
            }
        }

        private void fieldRelationList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyToClipboard(fieldRelationList);
            }
        }

        private void CopyToClipboard(ListBox listBox)
        {
            if (listBox.SelectedItem != null)
            {
                Clipboard.SetText(listBox.SelectedItem.ToString());
            }
        }

        private void txttable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectItemInListBox(txttable.Text, tableList);
            }
        }

        private void txtfield_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectItemInListBox(txtfield.Text, fieldList);
            }
        }
        private void SelectItemInListBox(string itemText, ListBox listbox)
        {
            // Find the item in the list box
            int index = listbox.FindString(itemText);

            // If the item was found
            if (index != ListBox.NoMatches)
            {
                // Select the item
                listbox.SelectedIndex = index;
            }
            else
            {
                // Clear selection if no match was found
                listbox.ClearSelected();
            }
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

        private void SearchAndSelect(string keyword)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (chkExact.Checked)
                    {
                        if (cell.FormattedValue.ToString().Equals(keyword))
                        {
                            // Select the cell
                            cell.Selected = true;

                            // Scroll to the selected cell
                            dataGridView.FirstDisplayedScrollingRowIndex = cell.RowIndex;

                            // Stop the loop
                            return;
                        }
                    }
                    else
                    {
                        if (cell.FormattedValue.ToString().Contains(keyword))
                        {
                            // Select the cell
                            cell.Selected = true;

                            // Scroll to the selected cell
                            dataGridView.FirstDisplayedScrollingRowIndex = cell.RowIndex;

                            // Stop the loop
                            return;
                        }
                    }
                    
                }
            }

            MessageBox.Show(keyword + " not found");
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchAndSelect(txtSearch.Text.Trim());
            }
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Suppress the error message and continue with the program
            e.ThrowException = false;
            e.Cancel = true;
        }

    }
}
