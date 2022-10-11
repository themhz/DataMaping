using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WindowsFormsApp1.forms.Childs {
    public partial class Annex1 : Form {
       
        public Annex1() {
            InitializeComponent();
            //txtJsonQuery.Text = "{\"select\":[\"PageA.ID\", \"PageA.Name\", \"PageA.RecNumber\", \"PageADetails.Density\", \"PageADetails.Index\"]," +
            //    "\"from\":\"PageA\"," +
            //    "\"join\":[" +
            //    "[\"PageA.ID\", \"PageADetails.PageADetailID\"]," +
            //    "]," +
            //    "\"filter\":[\"PageA.Name = 'Δοκός σε ενδιάμεσο όροφο  (6cm - Β ζώνη) (Νέο κτήριο)' and PageA.RecNumber > '0004'\"]}";

            //{
            //    "select": [
            //      "PageA.ID",
            //        "PageA.Name",
            //        "PageA.RecNumber",
            //        "PageADetails.Density",
            //        "PageADetails.Index"
            //      ],
            //      "from": "PageBLevels",
            //      "join": [
            //        ["PageAOpeningsPerLevel.PageBLevelID","PageBLevels.ID"],
            //        ["PageAOpenings.ID","PageAOpeningsPerLevel.PageAOpeningID"],
            //        ["PageAOpeningElements.PageAOpeningID","PageAOpenings.ID"]
            //      ]
            //    }

            string text = "{\"select\":[\"*\"]," +
                                "\"from\":\"ThermalBridgeCategories\"," +
                                "\"join\":[" +
                                "[\"ThermalBridgeLevels.ThermalBridgeCategoryID\", \"ThermalBridgeCategories.ID\"]," +
                                "[\"ThermalBridgeElements.ThermalBridgeLevelID\", \"ThermalBridgeLevels.ID\"]," +
                                "]}";

            //string json = JsonConvert.SerializeObject(text, Formatting.Indented);
            JToken json = JToken.Parse(text);
            txtJsonQuery.Text = json.ToString(Formatting.Indented);
            
            Select();
        }

        private void Run() {
            Xml xml = new Xml();
            string query = "{\"query\":" + txtJsonQuery.Text + "}";
            JObject json = JObject.Parse(query);
            JToken from = json["query"]["from"];

            List<String> tables = new List<string>();
            foreach(string table in from)
            {
                tables.Add(table);
            }
            //tables.Add("PageBLevels");
            //tables.Add("PageAOpeningsPerLevel");
            //tables.Add("PageAOpenings");
            //tables.Add("PageAOpeningElements");

            
            QueryEngine qe = new QueryEngine();
            List<DataTable> dataTables = new List<DataTable>();
            JToken joins = json["query"]["join"];
            DataTable result = null;
                        
            for(int i=0; i<joins.Count();i++)
            {
                if (i == 0)
                {
                    
                    //joinpart[0].Split('.')[1]
                    DataTable Table1 = xml.DataSet.Tables[tables[0]];
                    DataTable Table2 = xml.DataSet.Tables[tables[1]];
                    result = qe.Join(Table1, Table2, joins[i][0].ToString().Split('.')[1] + "=" + joins[i][1].ToString().Split('.')[1], "");
                }
                else
                {
                    DataTable table = xml.DataSet.Tables[tables[i]];

                    result = qe.Join(result, table, "PageAOpeningsPerLevel.PageAOpeningID=ID", "");
                }
                
                
            }
            //DataTable Table1 = xml.DataSet.Tables[tables[0]];
            //DataTable Table2 = xml.DataSet.Tables[tables[1]];
            //DataTable Table3 = xml.DataSet.Tables[tables[2]];
            //DataTable Table4 = xml.DataSet.Tables[tables[3]];

            
                //result = qe.Select(result, Table3, "PageAOpeningsPerLevel.PageAOpeningID=ID", "");            
                //result = qe.Select(result, Table4, "PageAOpenings.ID=PageAOpeningID", "");
            

            for(int i=0; i < result.Columns.Count; i++)
            {
                result.Columns[i].ColumnName = result.Columns[i].ColumnName.Replace("EYABYMSJMZUWPRZZVRSBZZZZ_", "");
            }

            dgvResult.DataSource = result.Select();

            //This is added because I cant remove the last column from the Datasource in the result set.
            if(dgvResult.Columns.Count - 1>=0)
                dgvResult.Columns[dgvResult.Columns.Count - 1].Visible = false;
        }
   


        private void button1_Click(object sender, EventArgs e)
        {
            
            //Form activParent = this.MdiParent;
            
            try
            {
                Select();                
                lblStatus.Text = "query executed successfully";
                lblStatus.ForeColor = Color.Green;
                //MDIParent1.Self.toolStripStatusLabel.Text = "query executed successfully";                
                //MDIParent1.Self.toolStripStatusLabel.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
                lblStatus.ForeColor = Color.Red;
                //MDIParent1.Self.toolStripStatusLabel.Text = ex.Message;                
                //MDIParent1.Self.toolStripStatusLabel.ForeColor = Color.Red;


            }
        }

        private void Select()
        {
           
            //{
            //"select":["PageA.ID", "PageA.Name", "PageA.RecNumber", "PageADetails.Density", "PageADetails.Index"],
            //"from":"ThermalBridgeCategories",
            //"join":[
            //["ThermalBridgeLevels.ThermalBridgeCategoryID", "ThermalBridgeCategories.ID"],
            //["ThermalBridgeElements.ThermalBridgeLevelID", "ThermalBridgeLevels.ID"],
            //]
            //}
            Xml xml = new Xml();
            QueryEngine qe = new QueryEngine();
            DataTable dt = qe.ExecuteQuery(txtJsonQuery.Text, xml);

            dgvResult.Columns.Clear();
            dgvResult.DataSource = dt;

            #region Comments

            ////Queries q = new Queries();
            ////var result = q.Start(xml, txtJsonQuery.Text);

            ////Tests t = new Tests();
            //QueryEngine qe = new QueryEngine();
            //DataTable Table1 = xml.DataSet.Tables["PageBLevels"];
            //DataTable Table2 = xml.DataSet.Tables["PageAOpeningsPerLevel"];
            //DataTable Table3 = xml.DataSet.Tables["PageAOpenings"];
            //DataTable Table4 = xml.DataSet.Tables["PageAOpeningElements"];

            //var result = qe.Join(Table1, Table2, "ID=PageBLevelID", "");
            ////xml.DataSet.Tables.Add(result);

            //result = qe.Join(result, Table3, "PageAOpeningsPerLevel_PageAOpeningID=ID", "");
            ////xml.DataSet.Tables.Add(result2);

            ////var result3 = qe.Select(result2, Table4, "PageAOpenings_ID=PageAOpeningID", "");
            ////xml.DataSet.Tables.Add(result3);
            ////result = t.Select(xml, "PageBLevels", "PageAOpeningsPerLevel", "ID=PageBLevelID", "");

            //dgvResult.DataSource = result;

            ////This is added because I cant remove the last column from the Datasource in the result set.
            //if (dgvResult.Columns.Count - 1 >= 0)
            //    dgvResult.Columns[dgvResult.Columns.Count - 1].Visible = false;
            #endregion Comments
        }
    }
}
