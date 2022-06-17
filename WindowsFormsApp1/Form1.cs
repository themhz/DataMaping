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


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        Xml xml;
        string dataSetPath= "";
        string dataSetPathSchema ="";
        public Form1()
        {
            InitializeComponent();
            tableList.SelectedValueChanged += new EventHandler(tableList_SelectedValueChanged);
            relationsList.SelectedValueChanged += new EventHandler(relationsList_SelectedValueChanged);

            txtXml.Text = dataSetPath = @"C:\Users\themhz\Source\Repos\DataMaping\WindowsFormsApp1\data\testReport.xml";
            txtXsd.Text = dataSetPathSchema = @"C:\Users\themhz\Source\Repos\DataMaping\WindowsFormsApp1\data\dsBuildingHeatInsulation.xsd";
            readXml();
        

        }

        public void tableList_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox listbox = (ListBox)sender;
           
            DataTable dataTable = xml.getDataSet().Tables[listbox.SelectedItem.ToString()];
            fieldList.Items.Clear();
            dataGridView.Columns.Clear();
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                fieldList.Items.Add(dataColumn.ColumnName + " ("+ dataColumn.DataType + ")");
                dataGridView.Columns.Add(dataColumn.ColumnName, dataColumn.ColumnName);
            }

            populateDataGridView(dataTable);
            populateRelationsList(dataTable);
        }

        public void relationsList_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox listbox = (ListBox)sender;
           
            var table = listbox.SelectedItem.ToString().Split('_');

            DataTable dataTable = xml.getDataSet().Tables[table[1]];
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

            for(int i=0;i< dataTable.ParentRelations.Count; i++)
            {
                foreach(var column in dataTable.ParentRelations[i].ChildColumns)
                {
                    for(int j=0;j< dataTable.ParentRelations[i].ParentColumns.Count();j++)
                    {
                            fieldRelationList.Items.Add(dataTable.ParentRelations[i].ParentTable.TableName + "->"+ dataTable.ParentRelations[i].ParentColumns[j].ToString() + " - " + column.ColumnName);
                    }                                        
                }            
            }
        }
        private void populateRelationsList(DataTable dataTable)
        {
            relationsList.Items.Clear();
            foreach(var relations in dataTable.ChildRelations)
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

        public void readXml() {
            if (dataSetPath == "" || dataSetPathSchema == "") {
                MessageBox.Show("Please select an xml and an xsd file");
                numOfTables.Text = "0";
            } else {
                xml = new Xml(dataSetPath, dataSetPathSchema);
                tableList.Items.Clear();
                foreach (var table in xml.getDataSet().Tables) {
                    tableList.Items.Add(table.ToString());
                }
                numOfTables.Text = xml.getDataSet().Tables.Count.ToString();
            }
        }

        private void btnSelectXml_Click(object sender, EventArgs e)
        {
            dataSetPath = openSelectFileBox("xml");
            txtXml.Text = dataSetPath;
        }

        private void btnSelectXsd_Click(object sender, EventArgs e)
        {
            dataSetPathSchema = openSelectFileBox("xsd");
            txtXsd.Text = dataSetPathSchema;
        }

        private string openSelectFileBox(string ext = "")
        {
            
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"../../../data";
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

            select_V3();
           
        }
        public void select_V3() {
            DataTable PageA = xml.DataSet.Tables["PageA"];
            DataTable PageADetails = xml.DataSet.Tables["PageADetails"];

            
            var result = from pageA in PageA.AsEnumerable()
                         join pageADetails in PageADetails.AsEnumerable()
                         on pageA.Field<Guid>("ID") equals pageADetails.Field<Guid>("PageADetailID") into ALLCOLUMNS
                         select ALLCOLUMNS.CopyToDataTable();

            //DataRow[] dr = result.Select("Thickness =0.35 and RefID1 = '1c79b36c-bc75-4f9f-a02f-d0917c9dfa20'");

        }
        public void select_V2() {
            DataTable PageA = xml.DataSet.Tables["PageA"];

            DataRow[] dr = PageA.Select("Thickness =0.35 and RefID1 = '1c79b36c-bc75-4f9f-a02f-d0917c9dfa20'");
        }

        public void select_V1() {
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

            
            


            //var JoinResults = from results in JoinResult.AsEnumerable()
            //                  where results.Field<object>("d") == "0.02"
            //                  ;

            //var JoinResult = (from pageA in PageA.AsEnumerable()
            //                  join pageADetails in PageADetails.AsEnumerable()
            //                  on pageA.Field<Guid>("ID") equals pageADetails.Field<Guid>("PageADetailID")
            //                  select new
            //                  {
            //                      id = pageA.Field<object>("ID"),
            //                      pageAName = pageA.Field<object>("Name"),
            //                      pageATypeName = pageA.Field<object>("TypeName"),
            //                      pageADetailsName = pageADetails.Field<object>("Name")
            //                  }).ToList();


            return xml.convertListToDataTable(JoinResult);
        }
    }
}
