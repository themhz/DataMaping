﻿using System;
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
            var result = q.Anex1(xml);

            //var persons = from a in TableA.Rows.Cast<DataRow>()
            //              where TableB.Rows.Cast<DataRow>().Any(
            //                      r => object.Equals(r["pension_id"], a["pension_id"]))
            //              select a;


            dgvResult.DataSource = result;
        }
    }
}