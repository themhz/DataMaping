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
            
        }

        private void Run() {
            Xml xml = new Xml();

            Queries q = new Queries();
            var result = q.Start(xml, txtJsonQuery.Text);          

            dgvResult.DataSource = result;

            //This is added because I cant remove the last column from the Datasource in the result set.
            if(dgvResult.Columns.Count - 1>=0)
                dgvResult.Columns[dgvResult.Columns.Count - 1].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Run();
                lblMessage.Text = "query executed successfully";
                lblMessage.ForeColor = Color.Green;
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
            }
            
        }
    }
}
