using FastMember;
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
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataGridView.Rows.Add(dataRow.ItemArray);
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



        public void connectToSqlServerTest()
        {
            string connetionString;
            SqlConnection con;
            connetionString = @"Data Source=DEV-02;Initial Catalog=CivilDb;User ID=sa;Password=ethe526996!@#$";
            con = new SqlConnection(connetionString);
            con.Open();

            using (SqlCommand command = new SqlCommand("CREATE TABLE Customer(First_Name char(50),Last_Name char(50),Address char(50),City char(50),Country char(25),Birth_Date datetime);", con))
                command.ExecuteNonQuery();

            MessageBox.Show("Connection Open  !");
            con.Close();
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
            if(dataSetPath!=null || dataSetPath != "")
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

        private void btnClearDataGridViewRelations_Click(object sender, EventArgs e)
        {
            dataGridViewRelations.Rows.Clear();
        }

        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {

            //Queries queries = new Queries();
            //var result = queries.Anex1(xml);
            //dataGridViewRelations.DataSource = result;           


        }

        public void select_V5()
        {
            string[] query = "PageA.PageADetails".Split('.');
            //string[] query = "Projects.PageCBuildings.ThermalBridgeCategories".Split('.');
            //DataTable table = xml.DataSet.Tables[query[0]];

            DataTable table = xml.DataSet.Tables[query[0]].Clone();
            xml.DataSet.Tables[query[0]].AsEnumerable()
           .Where(s => s.Field<Guid>("ID") == Guid.Parse("5458936f-902e-47b5-8c63-7e4e1b3bdbf5"))
           .CopyToDataTable(table, LoadOption.Upsert);

            //DataRelation dr = xml.DataSet.Relations.Add(table.TableName+"_copy", xml.DataSet.Relations, table.ChildRelations, false);

            //table.ChildRelations.Add(dr);

            string name = "";
            for (int i = 1; i < query.Length; i++)
            {
                name = query[i - 1] + "_" + query[i];
                table = table.ChildRelations[name].ChildTable;
            }
            dataGridViewRelations.DataSource = table;
        }

        public DataTable select(DataTable dt)
        {

            //DataTable PageA = xml.DataSet.Tables[query];
            //return PageA;
            //DataTable result = PageA.ChildRelations["PageA_PageADetails"].ChildTable;
            //dataGridViewRelations.DataSource = result.Select("Density = 1800 and λ = 0.87").CopyToDataTable();

            return dt;
        }

        public void select_V4()
        {
            DataTable PageA = xml.DataSet.Tables["PageA"];
            DataTable result = PageA.ChildRelations["PageA_PageADetails"].ChildTable;
            dataGridViewRelations.DataSource = result.Select("Density = 1800 and λ = 0.87").CopyToDataTable();
        }
        public void select_V3()
        {
            DataTable PageA = xml.DataSet.Tables["PageA"];
            DataTable PageADetails = xml.DataSet.Tables["PageADetails"];

            DataTable test = new DataTable();

            var joins = from pageA in PageA.AsEnumerable()
                        join pageADetails in PageADetails.AsEnumerable()
         on pageA.Field<Guid>("ID") equals pageADetails.Field<Guid>("PageADetailID")
                        select new { pageA, pageADetails };

            foreach (var row in joins)
            {
                var t1 = row.pageA;
                var t2 = row.pageADetails;
            }
        }
        public void select_V2()
        {
            DataTable PageA = xml.DataSet.Tables["PageA"];

            DataRow[] dr = PageA.Select("Thickness =0.35 and RefID1 = '1c79b36c-bc75-4f9f-a02f-d0917c9dfa20'");
        }
        public void select_V1()
        {
            DataTable PageA = xml.DataSet.Tables["PageA"];
            DataTable PageADetails = xml.DataSet.Tables["PageADetails"];

            var result = from pageA in PageA.AsEnumerable()
                         join pageADetails in PageADetails.AsEnumerable()
                         on pageA.Field<Guid>("ID") equals pageADetails.Field<Guid>("PageADetailID") into ALLCOLUMNS
                         select ALLCOLUMNS.CopyToDataTable();
        }

        public DataTable GetValueByLinq(DataSet dataSet)
        {
            //https://www.c-sharpcorner.com/blogs/inner-join-and-outer-join-in-datatable-using-linq
            //Inner join
            DataTable PageA = dataSet.Tables["PageA"];
            DataTable PageADetails = dataSet.Tables["PageADetails"];


            var JoinResult = (from pageA in PageA.AsEnumerable()
                              join pageADetails in PageADetails.AsEnumerable()
                              on pageA.Field<object>("ID") equals pageADetails.Field<object>("PageADetailID")
                              select new
                              {
                                  id = pageA.Field<object>("ID"),
                                  pageAName = pageA.Field<object>("Name"),
                                  pageATypeName = pageA.Field<object>("TypeName"),
                                  pageADetailsName = pageADetails.Field<object>("Name")
                              }).ToList();




            return JoinResult.CopyToDataTable2();

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

        

       
    }
}
