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

            string text = "{\"select\":[\"PageA.ID\", \"PageA.Name\", \"PageA.RecNumber\", \"PageADetails.Density\", \"PageADetails.Index\"]," +
                                "\"from\":\"ThermalBridgeCategories\"," +
                                "\"join\":[" +
                                "[\"ThermalBridgeLevels.ThermalBridgeCategoryID\", \"ThermalBridgeCategories.ID\"]," +
                                "[\"ThermalBridgeElements.ThermalBridgeLevelID\", \"ThermalBridgeLevels.ID\"]," +
                                "]}";

            //string json = JsonConvert.SerializeObject(text, Formatting.Indented);
            JToken json = JToken.Parse(text);

            txtJsonQuery.Text = json.ToString(Formatting.Indented);
            ;
            //Select();
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

//            {
//                "select":["PageA.ID", "PageA.Name", "PageA.RecNumber", "PageADetails.Density", "PageADetails.Index"],
//"from":"ThermalBridgeCategories",
//"join":[
//["ThermalBridgeLevels.ThermalBridgeCategoryID", "ThermalBridgeCategories.ID"],
//["ThermalBridgeElements.ThermalBridgeLevelID", "ThermalBridgeLevels.ID"],
//]
//}
            Xml xml = new Xml();
            JObject json = JObject.Parse(txtJsonQuery.Text);

            QueryEngine qe = new QueryEngine();
            string from = json["from"].ToString();
            DataTable selector = xml.DataSet.Tables[from];
            DataTable result = qe.Select(selector, "");

            
            
            JToken joins = json["join"];
            

            for (int i = 0; i < joins.Count(); i++)
            {
                if (i == 0)
                {
                    if(joins[i][0].ToString().Split('.')[0].ToString() == from)
                    {
                        DataTable Table1 = xml.DataSet.Tables[joins[i][0].ToString().Split('.')[0].ToString()];
                        DataTable Table2 = xml.DataSet.Tables[joins[i][1].ToString().Split('.')[0].ToString()];
                        result = qe.Join(Table1, Table2, joins[i][0].ToString().Split('.')[1] + "=" + joins[i][1].ToString().Split('.')[1], "");
                    }
                    else if(joins[i][1].ToString().Split('.')[0].ToString() == from)
                    {
                        DataTable Table1 = xml.DataSet.Tables[joins[i][1].ToString().Split('.')[0].ToString()];
                        DataTable Table2 = xml.DataSet.Tables[joins[i][0].ToString().Split('.')[0].ToString()];
                        result = qe.Join(Table1, Table2, joins[i][1].ToString().Split('.')[1] + "=" + joins[i][0].ToString().Split('.')[1], "");
                    }
                    else
                    {
                        //there was an error in your select
                    }
                    
                }
                else
                {
                    
                    DataTable table = xml.DataSet.Tables[joins[i][0].ToString().Split('.')[0].ToString()];                                            
                    result = qe.Join(result, table, joins[i][1].ToString().Split('.')[0].ToString() + "_" + joins[i][1].ToString().Split('.')[1] + "=" + joins[i][0].ToString().Split('.')[1], "");
                }

            }

            foreach(DataColumn col in result.Columns)
            {
                col.ColumnName = col.ColumnName.Replace("EYABYMSJMZUWPRZZVRSBZZZZ_", "");
            }
            dgvResult.DataSource = result;

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
        }
    }
}
