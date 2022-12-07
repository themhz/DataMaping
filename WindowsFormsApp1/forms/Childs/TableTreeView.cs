using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.interfaces;

namespace WindowsFormsApp1.forms.Childs {

    public partial class TableTreeView : Form, IForm {
        public Xml xml { get; set; }
        public static TableTreeView Self;
        //String dataSetPath = @"C:\Users\themis\Desktop\test\dataHeatInsulation.xml";
        //String dataSetPathSchema = @"C:\Users\themis\Desktop\test\dsBuildingHeatInsulation.xsd";

        public TableTreeView() {
            InitializeComponent();
            xml = new Xml();
            Self = this;
            loadTreeView();
        }

        public void Test(string txtXml, string txtXsd)
        {
            //MessageBox.Show(txtXml , txtXsd);
            xml.setXml(txtXml);
            xml.setXsd(txtXsd);
            xml.reload();
            loadTreeView();
        }

        public void loadTreeView() {

            DatabaseTreeView.Nodes.Clear();
            DataSet dataset = xml.GetDataSet();
            List<string> tables = new List<string>();
            foreach (var table in dataset.Tables)
            {
                tables.Add(table.ToString());
            }
            tables.Sort();

            foreach (var table in tables) {
                TreeNode node = new TreeNode();
                node.Text = table.ToString();
                DatabaseTreeView.Nodes.Add(node);
            }
        }

        private void DatabaseTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
            DatabaseTreeView.SelectedNode.Nodes.Clear();
            addRowsToDgvTableRows(DatabaseTreeView.SelectedNode.Text);
            List<string> tableRelations = getTableRelations(DatabaseTreeView.SelectedNode.Text);
            lblPath.Text = getParentPath(DatabaseTreeView.SelectedNode);

            List<string> tables = new List<string>();
            foreach (var table in tableRelations)
            {
                tables.Add(table.ToString());
            }
            tables.Sort();

            foreach (var table in tables) {
                DatabaseTreeView.SelectedNode.Nodes.Add(new TreeNode().Text=table);
            }
        }

        private string getParentPath(TreeNode treeNode)
        {
            string path = "";
            TreeNode treenode = treeNode;
            Stack<TreeNode> treeNodeStack = new Stack<TreeNode>();
            
            while (treenode != null && !treeNodeStack.Contains(treenode))
            {
                treeNodeStack.Push(treenode);
                treenode = treenode.Parent;
            }
            
            while(treeNodeStack.Count()>0)
            {
                path += treeNodeStack.Pop().Text + (treeNodeStack.Count() > 0 ? " - " : "") ;
            }
            return path;
        }
        private List<string> getTableRelations(string tableName) {                        
            DataSet dataset = xml.GetDataSet();
            List<string> tables = new List<string>();
            foreach (var table in dataset.Tables[tableName].ChildRelations) {
                tables.Add(((System.Data.DataRelation)table).ChildTable.ToString());
            }

            return tables;
        }

        private void addRowsToDgvTableRows(string tableName) {
           DataSet dataset = xml.GetDataSet();
           var table = dataset.Tables[tableName];
                      
           populateDataGridView(table);
        }

        private void populateDataGridView(DataTable dataTable) {
            dgvTableRows.Rows.Clear();
            dgvTableRows.Columns.Clear();

            foreach (DataColumn dataColumn in dataTable.Columns) {
                dgvTableRows.Columns.Add(dataColumn.ColumnName, dataColumn.ColumnName);
            }

            foreach (DataRow dataRow in dataTable.Rows) {
                dgvTableRows.Rows.Add(dataRow.ItemArray);
            }
        }

        private DataTable JoinDataTables(DataTable t1, DataTable t2, params Func<DataRow, DataRow, bool>[] joinOn) {
            DataTable result = new DataTable();
            foreach (DataColumn col in t1.Columns) {
                if (result.Columns[col.ColumnName] == null)
                    result.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (DataColumn col in t2.Columns) {
                if (result.Columns[col.ColumnName] == null)
                    result.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (DataRow row1 in t1.Rows) {
                var joinRows = t2.AsEnumerable().Where(row2 =>
                {
                    foreach (var parameter in joinOn) {
                        if (!parameter(row1, row2)) return false;
                    }
                    return true;
                });
                foreach (DataRow fromRow in joinRows) {
                    DataRow insertRow = result.NewRow();
                    foreach (DataColumn col1 in t1.Columns) {
                        insertRow[col1.ColumnName] = row1[col1.ColumnName];
                    }
                    foreach (DataColumn col2 in t2.Columns) {
                        insertRow[col2.ColumnName] = fromRow[col2.ColumnName];
                    }
                    result.Rows.Add(insertRow);
                }
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new SelectXmlXsdPopup();
            form.MdiParent = MDIParent1.Self;
            form.Text = "XML-XSL";
            form.Show();
        }
    }
}
