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
            Run();
        }

        private void Run() {
            Xml xml = new Xml();

            Queries q = new Queries();
            var result = q.Start(xml);          

            dgvResult.DataSource = result;

            //This is added because I cant remove the last column from the Datasource in the result set.
            //dgvResult.Columns[dgvResult.Columns.Count - 1].Visible = false;
        }
    }
}
