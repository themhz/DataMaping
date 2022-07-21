using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.forms.Childs {
    public partial class TableTreeView : Form {
        Xml Xml;
        String dataSetPath = @"C:\Users\themis\Desktop\test\dataHeatInsulation.xml";
        String dataSetPathSchema = @"C:\Users\themis\Desktop\test\dsBuildingHeatInsulation.xsd";

        public TableTreeView() {
            InitializeComponent();
            Xml = new Xml(dataSetPath, dataSetPathSchema);
            loadTreeView();            
        }

        public void loadTreeView() {
            
            DataSet dataset = Xml.GetDataSet();
            foreach(var table in dataset.Tables) {
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
            foreach (var table in tableRelations) {
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
            DataSet dataset = Xml.GetDataSet();
            List<string> tables = new List<string>();
            foreach (var table in dataset.Tables[tableName].ChildRelations) {
                tables.Add(((System.Data.DataRelation)table).ChildTable.ToString());
            }

            return tables;
        }

        private void addRowsToDgvTableRows(string tableName) {
           DataSet dataset = Xml.GetDataSet();
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
    }
}
