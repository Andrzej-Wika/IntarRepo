using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace IntarRepo
{
    public partial class frmExecute : Form
    {
        string _sciezkaRepozytorium;
        string _sciezkaKatalogRoboczy;
        public string sciezka { get; set; }
        public string baza { get; set; }
        public frmExecute(string sciezkaRepozytorium,string sciezkaKatalogRoboczy)
        {
            InitializeComponent();
            _sciezkaRepozytorium = sciezkaRepozytorium;
            _sciezkaKatalogRoboczy = sciezkaKatalogRoboczy;
        }

        private void frmExecute_Load(object sender, EventArgs e)
        {
            repozytoriumBtn.Text = "[Repozytorium] "  + _sciezkaRepozytorium;
            sprawdzanyBtn.Text = "[Katalog roboczy] " + _sciezkaKatalogRoboczy;
        }

        private void sprawdzanyBtn_CheckedChanged(object sender, EventArgs e)
        {
            sciezka = _sciezkaKatalogRoboczy;
        }

        private void repozytoriumBtn_CheckedChanged(object sender, EventArgs e)
        {
            sciezka = _sciezkaRepozytorium;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baza = comboBox1.Text;
        }
    }
}
