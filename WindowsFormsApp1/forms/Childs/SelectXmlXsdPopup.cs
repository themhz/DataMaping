using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.forms.Childs
{
    public partial class SelectXmlXsdPopup : Form
    {
        public Xml xml { get; set; }
        
        string dataSetPath = "";
        string dataSetPathSchema = "";

        public SelectXmlXsdPopup()
        {
            InitializeComponent();
        }

        private void btnReadxml_Click(object sender, EventArgs e)
        {

            TableTreeView.Self.Test(txtXml.Text, txtXsd.Text);
            //MessageBox.Show("test");
            //readXml();

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
                

                List<string> tables = new List<string>();


                foreach (var table in xml.GetDataSet().Tables)
                {
                    tables.Add(table.ToString());
                    //tableList.Items.Add(table.ToString());
                }
                tables.Sort();

                numOfTables.Text = xml.GetDataSet().Tables.Count.ToString();
            }
        }

        private string openSelectFileBox(string ext = "")
        {

            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"C:\Users\themis\Documents\xmlOutput";
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
    }
}
