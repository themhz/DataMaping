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
    public partial class Annex1 : Form {
        public Annex1() {
            InitializeComponent();            
            txtJsonQuery.Text = "{\"query\":{\"select\":[\"PageA.ID\", \"PageA.Name\", \"PageA.RecNumber\", \"PageADetails.Density\", \"PageADetails.Index\"],\"from\":[\"PageA\", \"PageADetails\"],\"join\":[[\"ID\", \"PageADetailID\"]],\"filter\":[\"PageA.Name = 'Δοκός σε ενδιάμεσο όροφο  (6cm - Β ζώνη) (Νέο κτήριο)' and PageA.RecNumber > '0004'\"]}}";
            Run();
        }   

        private void Run() {
            Xml xml = new Xml();

            //Queries q = new Queries();
            //var result = q.Start(xml, txtJsonQuery.Text);

            //Tests t = new Tests();
            List<String> tables = new List<string>();
            tables.Add("PageBLevels");
            tables.Add("PageAOpeningsPerLevel");
            tables.Add("PageAOpenings");
            tables.Add("PageAOpeningElements");

            
            QueryEngine qe = new QueryEngine();
            DataTable Table1 = xml.DataSet.Tables[tables[0]];
            DataTable Table2 = xml.DataSet.Tables[tables[1]];
            DataTable Table3 = xml.DataSet.Tables[tables[2]];
            DataTable Table4 = xml.DataSet.Tables[tables[3]];

            var result = qe.Select(Table1, Table2, "ID=PageBLevelID", "");
            

            result = qe.Select(result, Table3, "PageAOpeningsPerLevel.PageAOpeningID=ID", "");
            

            result = qe.Select(result, Table4, "PageAOpenings.ID=PageAOpeningID", "");
            

            for(int i=0; i < result.Columns.Count; i++)
            {
                result.Columns[i].ColumnName = result.Columns[i].ColumnName.Replace("EYABYMSJMZUWPRZZVRSBZZZZ_", "");
            }

            dgvResult.DataSource = result;

            //This is added because I cant remove the last column from the Datasource in the result set.
            if(dgvResult.Columns.Count - 1>=0)
                dgvResult.Columns[dgvResult.Columns.Count - 1].Visible = false;
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Select();
                lblMessage.Text = "query executed successfully";
                lblMessage.ForeColor = Color.Green;
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
            }
            
        }

        private void Select()
        {
            Xml xml = new Xml();

            //Queries q = new Queries();
            //var result = q.Start(xml, txtJsonQuery.Text);

            //Tests t = new Tests();
            QueryEngine qe = new QueryEngine();
            DataTable Table1 = xml.DataSet.Tables["PageBLevels"];
            DataTable Table2 = xml.DataSet.Tables["PageAOpeningsPerLevel"];
            DataTable Table3 = xml.DataSet.Tables["PageAOpenings"];
            DataTable Table4 = xml.DataSet.Tables["PageAOpeningElements"];

            var result = qe.Select(Table1, Table2, "ID=PageBLevelID", "");
            //xml.DataSet.Tables.Add(result);

            result = qe.Select(result, Table3, "PageAOpeningsPerLevel_PageAOpeningID=ID", "");
            //xml.DataSet.Tables.Add(result2);

            //var result3 = qe.Select(result2, Table4, "PageAOpenings_ID=PageAOpeningID", "");
            //xml.DataSet.Tables.Add(result3);
            //result = t.Select(xml, "PageBLevels", "PageAOpeningsPerLevel", "ID=PageBLevelID", "");

            dgvResult.DataSource = result;

            //This is added because I cant remove the last column from the Datasource in the result set.
            if (dgvResult.Columns.Count - 1 >= 0)
                dgvResult.Columns[dgvResult.Columns.Count - 1].Visible = false;
        }
    }
}
