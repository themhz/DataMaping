using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        Xml xml;
        public Form1()
        {
            InitializeComponent();
            tableList.SelectedValueChanged += new EventHandler(tableList_SelectedValueChanged);
            relationsList.SelectedValueChanged += new EventHandler(relationsList_SelectedValueChanged);
            string dataSetPath = @"../../testReport.xml";
            string dataSetPathSchema = @"../../dsBuildingHeatInsulation.xsd";
            xml = new Xml(dataSetPath, dataSetPathSchema);
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
            tableList.Items.Clear();
            
            foreach (var table in xml.getDataSet().Tables)
            {
                tableList.Items.Add(table.ToString());
            }
            numOfTables.Text = xml.getDataSet().Tables.Count.ToString();
        }
    }
}
