using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.classes;
using WindowsFormsApp1.forms.Childs;
using WindowsFormsApp1.interfaces;

namespace WindowsFormsApp1 {
    public partial class MDIParent1 : Form {
        private int childFormNumber = 0;
        public static MDIParent1 Self;
        public MDIParent1() {
            InitializeComponent();
            Self = this;
        }


        private void OpenFile(object sender, EventArgs e) {
            FileExplorer.OpenFile(this);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            FileExplorer.SaveFile(this);
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e) {
        }
        

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (Form childForm in MdiChildren) {
                childForm.Close();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) {
            Form form = new TableTreeView();
            form.MdiParent = this;
            form.Text = "TableTreeView " + childFormNumber++;
            form.Show();
        }


        private void ShowNewForm(object sender, EventArgs e) {
            Form form = new DataMaper();
            form.MdiParent = this;
            form.Text = "DataMaper " + childFormNumber++;
            form.Show();
        }

        private void QueryEditor_Click(object sender, EventArgs e) {
            Form form = new QueryEditor();
            form.MdiParent = this;
            form.Text = "Annex1 " + childFormNumber++;
            form.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Xml Files (*.Xml)|";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                Form activeChild = this.ActiveMdiChild;             

                if (activeChild != null)
                {                    
                    ((IForm)activeChild).xml.setXml(FileName);
                    ((IForm)activeChild).xml.reload();                 
                }

                toolStripStatusLabel.Text = $"File loaded {FileName}, please execute the query again";
                txtFileLoadedLocation.Text = "";
            }

            MessageBox.Show("Also select an XSD ");

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Xml Files (*.Xsd)|";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                Form activeChild = this.ActiveMdiChild;

                if (activeChild != null)
                {
                    ((IForm)activeChild).xml.setXsd(FileName);
                    ((IForm)activeChild).xml.reload();
                }

                toolStripStatusLabel.Text = $"File loaded {FileName}, please execute the query again";
                txtFileLoadedLocation.Text = "";
            }
        }
    }
}
