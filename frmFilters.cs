using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntarRepo
{
    public partial class frmFilters : Form
    {
        public IntarRepository repository { get; set; }
        public string path { get; set; }
        public frmMain main { get; set; }
        public frmFilters()
        {
            InitializeComponent();
        }

        private void frmFilters_Load(object sender, EventArgs e)
        {
            foreach (string f in Directory.GetFiles(path, "*.cfg"))
            {
                // Ładuje tylko te filtry, które wcześniej były wybrane
                filtercheckedListBox1.Items.Add(Path.GetFileNameWithoutExtension(f), (from string s in repository.ConfigFiles where s == Path.GetFileNameWithoutExtension(f) select s).Any());
            }
            filtercheckedListBox1.CheckOnClick = true;
            filtercheckedListBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string lista = "";
            foreach (object filter in filtercheckedListBox1.CheckedItems)
            {
                lista += filter.ToString().Trim() + ";";
            }
            RegistryManager rm = new RegistryManager();
            string error;

            if (rm.SaveConfigFilesList(path, lista, out error))

                MessageBox.Show("Pliki konfiguracyjne zostały zapisane!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
                MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
