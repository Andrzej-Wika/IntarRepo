using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using static IntarRepo.IntarRepository;
using System.Reflection;

namespace IntarRepo
{
    public partial class frmMain : Form
    {
        float fontSize;

        string password_BPSC = "";
        string user_BPSC = "";
        string password_TEST = "";
        string user_TEST = "";
        string password_KPP = "";
        string user_KPP = "";

        Color kolorRepozytorium;

        System.Windows.Forms.TreeView treeView1 = new System.Windows.Forms.TreeView();

        TreeNode TABLENode = new TreeNode("Tabele");
        TreeNode VIEWNode = new TreeNode("Widoki");
        TreeNode FUNCTIONNode = new TreeNode("Funkcje");
        TreeNode PROCEDURENode = new TreeNode("Procedury");
        TreeNode TRIGGERNode = new TreeNode("Wyzwalacze");
        TreeNode PACKAGENode = new TreeNode("Pakiety");
        TreeNode SYNONYMNode = new TreeNode("Synonimy");
        TreeNode SEQUENCENode = new TreeNode("Sekwencje");
        TreeNode TYPENode = new TreeNode("Typy");
        TreeNode PROCOBJNode = new TreeNode("Zadania");

        List<RepositoryEntry> listFiltrow = new List<RepositoryEntry>();

        private IntarRepository repository = new IntarRepository();

        public frmMain()
        {
            InitializeComponent();
        }

        private bool WprowadzHaslo(string haslo)
        {
            if (String.IsNullOrEmpty(haslo))
            {
                MessageBox.Show("Wprawdź hasło!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;

        }

        private bool ZakonczZaznaczanie()
        {
            if (rozpocznijTworzenieFiltruMenuItem.Text == "Zakończ tworzenie filtru")
            {
                MessageBox.Show("Zakończ tworzenie filtru!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        public void Porownaj(string database)
        {
            progressBar1.Maximum = 100;
            ProgressEventHandler handler1 = (i) => { progressBar1.Value = i;
                                                     Application.DoEvents();
                                                   };
            ObjectCheckedEventHandler handler2 = (s) => Application.DoEvents();
            repository.ProgressEvent += handler1;
            repository.ObjectCheckedEvent += handler2;

            string user;
            string password;
            switch (database)
            {
                case "BPSC":
                    user = user_BPSC;
                    password = password_BPSC;
                    break;
                case "TEST":
                    user = user_TEST;
                    password = password_TEST;
                    break;
                case "KPP":
                    user = user_KPP;
                    password = password_KPP;
                    break;
                default:
                    user = user_BPSC;
                    password = password_BPSC;
                    break;
            }

            repository.CompareRepositoryWitDatabase(folderBrowserDialog1.SelectedPath, Properties.Settings.Default.katalogRoboczy, database, user, password);
            repository.ProgressEvent -= handler1;
            repository.ObjectCheckedEvent -= handler2;
        }

        private void PorownanieBazyDanych(string database)
        {
            switch (database)
            {
                case "TEST":
                    if (obiektyJakoDrzewoToolStripMenuItem.Checked)
                      treeView1.BackColor = Color.OrangeRed;
                    else
                      listaPlikow.BackColor = Color.OrangeRed;
                    break;
                case "BPSC":
                    if (obiektyJakoDrzewoToolStripMenuItem.Checked)
                        treeView1.BackColor = Color.BlueViolet;
                    else
                        listaPlikow.BackColor = Color.BlueViolet;
                    break;
                case "KPP":
                    if (obiektyJakoDrzewoToolStripMenuItem.Checked)
                        treeView1.BackColor = Color.DarkOliveGreen;
                    else
                        listaPlikow.BackColor = Color.DarkOliveGreen;
                    break;
            }

            Application.DoEvents();
            Porownaj(database);
            timer1.Start();

            uaktualnieniePlikuMenuItem.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            timer1.Stop();
        }

        private void otworzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            string folder;

            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.folder;
            result = folderBrowserDialog1.ShowDialog();
            folder = (result == DialogResult.OK) ? folderBrowserDialog1.SelectedPath : "";

            if (folder.Length > 0)
            {
                if (folder != Properties.Settings.Default.folder) repository = new IntarRepository();

                Properties.Settings.Default.folder = folder;
                Properties.Settings.Default.Save();

                repository.LoadRepositoryError += (errors, information) => { MessageBox.Show(errors,information,MessageBoxButtons.OK,MessageBoxIcon.Error); };

                if (repository.LoadRepositories(folderBrowserDialog1.SelectedPath))
                {
                    filtryMenuItem.Enabled = true;
                    czescioweŁadowanieToolStripMenuItem.Enabled = true;
                    Odswiez();
                }

                repository.LoadRepositoryError -= (errors, information) => { MessageBox.Show(errors, information, MessageBoxButtons.OK, MessageBoxIcon.Error); };
            }
        }

        private void WyrownanieKodu()
        {
            RepositoryEntry r = (obiektyJakoDrzewoToolStripMenuItem.Checked) ? (RepositoryEntry)treeView1.SelectedNode.Tag : ((ListBoxEntry)listaPlikow.Items[listaPlikow.SelectedIndex]).Entry;

            if (r.znak == "X" || r.znak == "+")
            {
                if (File.Exists(String.Format("{0}\\{1}", folderBrowserDialog1.SelectedPath, r.FileName)))
                {
                    File.Delete(String.Format("{0}\\{1}", folderBrowserDialog1.SelectedPath, r.FileName));
                }
                File.Copy($"{Properties.Settings.Default.katalogRoboczy}\\{r.FileName}", String.Format("{0}\\{1}", folderBrowserDialog1.SelectedPath, r.FileName));
                r.znak = " ";
            }

            if (obiektyJakoDrzewoToolStripMenuItem.Checked)
            {
                treeView1.SelectedNode.Text = String.Format("[{0}] {1}{2}", r.znak, r.grant ? "GRANT_" : "", r.name);
            }
            else
            {
                listaPlikow.Items.Insert(listaPlikow.SelectedIndex, new ListBoxEntry(r));
                listaPlikow.Items.RemoveAt(listaPlikow.SelectedIndex);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            fontSize = listaPlikow.Font.Size;

            treeView1.BackColor = SystemColors.ControlDarkDark;
            treeView1.Dock = DockStyle.Fill;
            treeView1.Font = new Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            treeView1.ForeColor = SystemColors.Window;

            kolorRepozytorium = listaPlikow.BackColor;

            if (obiektyJakoDrzewoToolStripMenuItem.Checked)
                treeView1.Font = new Font(treeView1.Font.FontFamily, fontSize );
            else
                listaPlikow.Font = new Font(listaPlikow.Font.FontFamily, fontSize );
        }

        private void porównajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (obiektyJakoDrzewoToolStripMenuItem.Checked)
            {
                if (!(treeView1.SelectedNode is null))
                {
                    RepositoryEntry r = (RepositoryEntry)treeView1.SelectedNode.Tag;
                    System.Diagnostics.Process.Start("notepad++.exe", $"\"{Properties.Settings.Default.katalogRoboczy}\\{r.FileName}\" \"{folderBrowserDialog1.SelectedPath}\\{r.FileName}\"");

                }
            }
            else
            {
                if (!(listaPlikow.SelectedItem is null))
                {
                    ListBoxEntry r = (ListBoxEntry)listaPlikow.SelectedItem;
                    System.Diagnostics.Process.Start("notepad++.exe", $"\"{Properties.Settings.Default.katalogRoboczy}\\{r.Entry.FileName}\" \"{folderBrowserDialog1.SelectedPath}\\{r.Entry.FileName}\"");
                }
            }
        }

        private void poswiadczeniaMenuItem_Click(object sender, EventArgs e)
        {
            UserCredentialsManager us = new UserCredentialsManager();
            var bpsc = us.LoadCredentials("BPSC");
            var test = us.LoadCredentials("TEST");
            var kpp = us.LoadCredentials("KPP");

            frmPassword f = new frmPassword();
            try
            {
                f.txtPassword_BPSC.Text = bpsc.Password;
                f.txtUser_BPSC.Text = bpsc.UserName;
                f.txtPassword_TEST.Text = test.Password;
                f.txtUser_TEST.Text = test.UserName;
                f.txtPassword_KPP.Text = kpp.Password;
                f.txtUser_KPP.Text = kpp.UserName;
            }
            catch { }

            if (f.ShowDialog() == DialogResult.OK)
            {
                password_BPSC = f.txtPassword_BPSC.Text;
                user_BPSC = f.txtUser_BPSC.Text;
                password_TEST = f.txtPassword_TEST.Text;
                user_TEST = f.txtUser_TEST.Text;
                password_KPP = f.txtPassword_KPP.Text;
                user_KPP = f.txtUser_KPP.Text;

                if (impulsConnector.PoprawnePoswiadczenia("BPSC",user_BPSC, password_BPSC))
                {
                    porownanieKoduMenuItem.Enabled = true;
                    uruchomienieKoduMenuItem.Enabled = true;
                    poswiadczeniaMenuItem.BackColor = Color.GreenYellow;

                    if (((string.IsNullOrEmpty(bpsc.UserName) || (bpsc.UserName != user_BPSC)) && MessageBox.Show("Czy zapisać poświadczenia BPSC w rejestrze Windows ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        || (bpsc.UserName == user_BPSC))
                    {
                        us.SaveCredentials("BPSC",user_BPSC, password_BPSC);
                    }
                }
                else
                {
                    password_BPSC = null;
                    user_BPSC = null;
                    MessageBox.Show("Niepoprawne poświadczenia BPSC", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (impulsConnector.PoprawnePoswiadczenia("TEST",user_TEST, password_TEST))
                {
                    porownanieKoduMenuItem.Enabled = true;
                    uruchomienieKoduMenuItem.Enabled = true;
                    poswiadczeniaMenuItem.BackColor = Color.GreenYellow;

                    if (((string.IsNullOrEmpty(test.UserName) || (test.UserName != user_TEST)) && MessageBox.Show("Czy zapisać poświadczenia BPSC w rejestrze Windows ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        || (test.UserName == user_TEST))
                    {
                        us.SaveCredentials("TEST", user_TEST, password_TEST);
                    }
                }
                else
                {
                    password_TEST = null;
                    user_TEST = null;
                    MessageBox.Show("Niepoprawne poświadczenia TEST", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (impulsConnector.PoprawnePoswiadczenia("KPP", user_KPP, password_KPP))
                {
                    porownanieKoduMenuItem.Enabled = true;
                    uruchomienieKoduMenuItem.Enabled = true;
                    poswiadczeniaMenuItem.BackColor = Color.GreenYellow;

                    if (((string.IsNullOrEmpty(kpp.UserName) || (kpp.UserName != user_KPP)) && MessageBox.Show("Czy zapisać poświadczenia BPSC w rejestrze Windows ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        || (kpp.UserName == user_KPP))
                    {
                        us.SaveCredentials("KPP", user_KPP, password_KPP);
                    }
                }
                else
                {
                    password_KPP = null;
                    user_KPP = null;
                    MessageBox.Show("Niepoprawne poświadczenia KPP", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void rozpocznijTworzenieFiltruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (obiektyJakoDrzewoToolStripMenuItem.Checked)
            {
                MessageBox.Show("W trybie oglądania: 'drzewo' nie można tworzyć filtrów !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (listaPlikow.SelectionMode == SelectionMode.One)
                {
                    listaPlikow.SelectionMode = SelectionMode.MultiSimple;
                    rozpocznijTworzenieFiltruMenuItem.Text = "Zakończ tworzenie filtru";
                }
                else
                {
                    listFiltrow.Clear();
                    foreach (RepositoryEntry s in listaPlikow.SelectedItems)
                    {
                        listFiltrow.Add(s);
                    }
                    listaPlikow.SelectionMode = SelectionMode.One;
                    rozpocznijTworzenieFiltruMenuItem.Text = "Rozpocznij tworzenie filtru";
                    zapiszFiltrDoPlikuMenuItem.Enabled = true;
                }
            }
        }

        private void zapiszFiltrDoPlikuMenuItem_Click(object sender, EventArgs e)
        {
            filtrZapiszFileDialog.FileName = Properties.Settings.Default.filtr;
            DialogResult result = filtrZapiszFileDialog.ShowDialog();
            string file = (result == DialogResult.OK) ? filtrZapiszFileDialog.FileName : "";


            if (!string.IsNullOrEmpty(file))
            {
                repository.SaveFilters(listFiltrow, file);
            }
        }

        private void załadujFiltrZPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            otworzFiltrFileDialog.InitialDirectory = Properties.Settings.Default.folder;
            otworzFiltrFileDialog.Multiselect = true;

            DialogResult result = otworzFiltrFileDialog.ShowDialog();
            string[] f = (result == DialogResult.OK) ? otworzFiltrFileDialog.FileNames : new string[0];

            if (f.Length > 0)
            {
                repository.ApplyFilter(f);
                Odswiez();
            }
        }

        private void uruchomienieKoduMenuItem_Click(object sender, EventArgs e)
        {
            string error;

            RepositoryEntry r = null;

            try
            {
                r = (obiektyJakoDrzewoToolStripMenuItem.Checked) ? (RepositoryEntry)treeView1.SelectedNode.Tag : ((ListBoxEntry)listaPlikow.Items[listaPlikow.SelectedIndex]).Entry;
            } 
            catch
            {
                r = null;
            }


            if (r is null)
            {
                MessageBox.Show("Wybierz skrypt do uruchomienia!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmExecute f = new frmExecute(folderBrowserDialog1.SelectedPath, Properties.Settings.Default.katalogRoboczy);

            if (f.ShowDialog() == DialogResult.OK)
            {
                string user;
                string password;
                switch (f.baza)
                {
                    case "BPSC":
                        user = user_BPSC;
                        password = password_BPSC;
                        break;
                    case "TEST":
                        user = user_TEST;
                        password = password_TEST;
                        break;
                    case "KPP":
                        user = user_KPP;
                        password = password_KPP;
                        break;
                    default:
                        user = user_KPP;
                        password = password_KPP;
                        break;
                }
                if (repository.RunCode(r, f.sciezka, f.baza, user, password, f.Opis.Text, out error))
                {
                    MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pokazujTylkoBledyMenuItem_Click(object sender, EventArgs e)
        {
            pokazujTylkoBledyMenuItem.Checked = !pokazujTylkoBledyMenuItem.Checked;
            Odswiez();
        }

        private void TylkoBledy()
        {
            List<RepositoryEntry> doUsuniecia = new List<RepositoryEntry>();
            
            foreach (RepositoryEntry r in repository.WorkList)
            {
                if (string.IsNullOrWhiteSpace(r.znak))
                {
                    doUsuniecia.Add(r);
                }
            }

            if (obiektyJakoDrzewoToolStripMenuItem.Checked)
            {
                foreach (RepositoryEntry r in doUsuniecia)
                {
                    try
                    {
                        TreeNode n = FindNodeByTag(treeView1.Nodes, r);
                        n.Remove();
                    }
                    catch { }
                }
            }
            else
            {
                foreach (RepositoryEntry r in doUsuniecia)
                {
                    try
                    {
                        var u = (from ListBoxEntry i in listaPlikow.Items where i.Entry == r select i).First<ListBoxEntry>();
                        listaPlikow.Items.Remove(u);
                    }
                    catch { }
                }

                if (obiektyJakoDrzewoToolStripMenuItem.Checked)
                    treeView1.Font = new Font(treeView1.Font.FontFamily, fontSize + 5);
                else
                    listaPlikow.Font = new Font(listaPlikow.Font.FontFamily, fontSize + 5);
            }
        }
      
        private void BPSCRepozytoriumMenuItem_Click(object sender, EventArgs e)
        {
            if (!WprowadzHaslo(password_BPSC)) return;

            PorownanieBazyDanych("BPSC");
            Odswiez();
        }

        private void KPPRepozytoriumMenuItem_Click(object sender, EventArgs e)
        {
            if (!WprowadzHaslo(password_KPP)) return;

            PorownanieBazyDanych("KPP");
            Odswiez();
        }

        private void TESTRepozytoriumMenuItem_Click(object sender, EventArgs e)
        {
            if (!WprowadzHaslo(password_TEST)) return;

            PorownanieBazyDanych("TEST");
            Odswiez();
        }

        private void uaktualnieniePlikuMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void legendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHelp f = new frmHelp();
            f.ShowDialog();
        }

        private void usunFiltryStripMenuItem_Click(object sender, EventArgs e)
        {
            repository.WorkList.Clear();
            foreach (RepositoryEntry entry in repository.Entries)
            {
                repository.WorkList.Add(entry);
            }

            Odswiez();
        }

        private void obiektyJakoDrzewoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obiektyJakoDrzewoToolStripMenuItem.Checked = !obiektyJakoDrzewoToolStripMenuItem.Checked;

            Odswiez();
        }

        private void Odswiez()
        {
            Color kolor;

            if (obiektyJakoDrzewoToolStripMenuItem.Checked)
            {
                treeView1.Sorted = sortujListedrzewoToolStripMenuItem.Checked;

                treeView1.Nodes.Clear();
                TABLENode.Nodes.Clear();
                VIEWNode.Nodes.Clear();
                FUNCTIONNode.Nodes.Clear();
                PROCEDURENode.Nodes.Clear();
                TRIGGERNode.Nodes.Clear();
                PACKAGENode.Nodes.Clear();
                SYNONYMNode.Nodes.Clear();
                SEQUENCENode.Nodes.Clear();
                TYPENode.Nodes.Clear();
                PROCOBJNode.Nodes.Clear();

                treeView1.Nodes.Add(TABLENode);
                treeView1.Nodes.Add(VIEWNode);
                treeView1.Nodes.Add(FUNCTIONNode);
                treeView1.Nodes.Add(PROCEDURENode);
                treeView1.Nodes.Add(TRIGGERNode);
                treeView1.Nodes.Add(PACKAGENode);
                treeView1.Nodes.Add(SYNONYMNode);
                treeView1.Nodes.Add(SEQUENCENode);
                treeView1.Nodes.Add(TYPENode);
                treeView1.Nodes.Add(PROCOBJNode);

                kolor = Controls.Contains(treeView1) ? treeView1.BackColor : listaPlikow.BackColor;

                Controls.Clear();

                Controls.Add(progressBar1);
                Controls.Add(treeView1);
                Controls.Add(menuStrip1);
                treeView1.BackColor = kolor;

                foreach (RepositoryEntry entry in repository.WorkList)
                {
                    switch (entry.type)
                    {
                        case "TABLE":
                            TABLENode.Nodes.Add(new TreeNode(String.Format("[{0}] {1}{2}", entry.znak, entry.grant ? "GRANT_" : "", entry.name)) { Tag = entry });
                            break;
                        case "VIEW":
                            VIEWNode.Nodes.Add(new TreeNode(String.Format("[{0}] {1}{2}", entry.znak, entry.grant ? "GRANT_" : "", entry.name, entry.type)) { Tag = entry });
                            break;
                        case "FUNCTION":
                            FUNCTIONNode.Nodes.Add(new TreeNode(String.Format("[{0}] {1}{2}", entry.znak, entry.grant ? "GRANT_" : "", entry.name, entry.type)) { Tag = entry });
                            break;
                        case "PROCEDURE":
                            PROCEDURENode.Nodes.Add(new TreeNode(String.Format("[{0}] {1}{2}", entry.znak, entry.grant ? "GRANT_" : "", entry.name, entry.type)) { Tag = entry });
                            break;
                        case "TRIGGER":
                            TRIGGERNode.Nodes.Add(new TreeNode(String.Format("[{0}] {1}{2}", entry.znak, entry.grant ? "GRANT_" : "", entry.name, entry.type)) { Tag = entry });
                            break;
                        case "PACKAGE":
                            PACKAGENode.Nodes.Add(new TreeNode(String.Format("[{0}] {1}{2}", entry.znak, entry.grant ? "GRANT_" : "", entry.name, entry.type)) { Tag = entry });
                            break;
                        case "SYNONYM":
                            SYNONYMNode.Nodes.Add(new TreeNode(String.Format("[{0}] {1}{2}", entry.znak, entry.grant ? "GRANT_" : "", entry.name, entry.type)) { Tag = entry });
                            break;
                        case "SEQUENCE":
                            SEQUENCENode.Nodes.Add(new TreeNode(String.Format("[{0}] {1}{2}", entry.znak, entry.grant ? "GRANT_" : "", entry.name, entry.type)) { Tag = entry });
                            break;
                        case "TYPE":
                            TYPENode.Nodes.Add(new TreeNode(String.Format("[{0}] {1}{2}", entry.znak, entry.grant ? "GRANT_" : "", entry.name, entry.type)) { Tag = entry });
                            break;
                        case "PROCOBJ":
                            PROCOBJNode.Nodes.Add(new TreeNode(String.Format("[{0}] {1}{2}", entry.znak, entry.grant ? "GRANT_" : "", entry.name, entry.type)) { Tag = entry });
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                listaPlikow.Sorted = sortujListedrzewoToolStripMenuItem.Checked;

                kolor = Controls.Contains(treeView1) ? treeView1.BackColor : listaPlikow.BackColor;

                Controls.Clear();

                Controls.Add(progressBar1);
                Controls.Add(listaPlikow);
                Controls.Add(menuStrip1);
                listaPlikow.BackColor = kolor;

                listaPlikow.Items.Clear();
                foreach (RepositoryEntry entry in repository.WorkList)
                {
                    listaPlikow.Items.Add(new ListBoxEntry(entry));
                }
            }
            if (pokazujTylkoBledyMenuItem.Checked) TylkoBledy();
        }
        private void sortujListedrzewoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortujListedrzewoToolStripMenuItem.Checked = !sortujListedrzewoToolStripMenuItem.Checked;

            if (obiektyJakoDrzewoToolStripMenuItem.Checked)
                treeView1.Sorted = sortujListedrzewoToolStripMenuItem.Checked;
            else
                listaPlikow.Sorted = sortujListedrzewoToolStripMenuItem.Checked;
            Odswiez();
        }


        private TreeNode FindNodeByTag(TreeNodeCollection nodes, object tag)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag != null && node.Tag.Equals(tag) && (((RepositoryEntry)node.Tag).znak == ((RepositoryEntry)tag).znak) )
                {
                    return node;
                }

                TreeNode foundNode = FindNodeByTag(node.Nodes, tag);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }

        private void sprawdźDodatkowePlikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                FileListComparer flc = new FileListComparer(folderBrowserDialog1.SelectedPath, repository.WorkList);
                List<string> files = flc.GetExtraFiles();

                if (files.Count == 0)
                {
                    MessageBox.Show("W repozytorium znajdują się tylko potrzebne pliki.", "Dodatkowe pliki w rep." + Path.GetFileName(folderBrowserDialog1.SelectedPath));
                } else
                {
                    if (saveFileDialog2.ShowDialog() == DialogResult.OK )
                    {
                        using (StreamWriter sw = new StreamWriter(saveFileDialog2.FileName))
                        {
                            foreach (string file in files)
                            {
                                sw.WriteLine(file);
                            }
                        }
                    }
                }
            }
            else MessageBox.Show("Wybierz repozytorium !!","",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void zduplikowanePozycjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                FileListComparer flc = new FileListComparer(folderBrowserDialog1.SelectedPath, repository.WorkList);

                if (repository.DuplicatedEntries.Count == 0)
                {
                    MessageBox.Show("W repozytorium nie ma zduplikowanych wpisów", "Zduplikowane wpisy w " + Path.GetFileName(folderBrowserDialog1.SelectedPath));
                }
                else
                {
                    if (saveFileDialog2.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(saveFileDialog2.FileName)) File.Delete(saveFileDialog2.FileName);
                        using (StreamWriter sw = new StreamWriter(saveFileDialog2.FileName))
                        {
                            foreach (RepositoryEntry entry in repository.DuplicatedEntries)
                            {
                                sw.WriteLine(entry.name);
                            }
                        }
                    }
                }
            }
            else MessageBox.Show("Wybierz repozytorium !!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void bPSCToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            MissingObject("BPSC");
        }

        private void MissingObject(string database)
        {
            string user;
            string password;

            switch (database)
            {
                case "BPSC":
                    user = user_BPSC;
                    password = password_BPSC;
                    break;
                case "TEST":
                    user = user_TEST;
                    password = password_TEST;
                    break;
                case "KPP":
                    user = user_KPP;
                    password = password_KPP;
                    break;
                default:
                    user = user_BPSC;
                    password = password_BPSC;
                    break;
            }

            if (!WprowadzHaslo(password_BPSC)) return;

            repository.FillMissingObjects(database,user, password);

            if (repository.MissingObjects.Count == 0)
            {
                MessageBox.Show("Repozytorium zawiera wszystkie konieczne pliki", "Brakujące pliki w " + Path.GetFileName(folderBrowserDialog1.SelectedPath));
            }
            else
            {
                if (saveFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(saveFileDialog2.FileName)) File.Delete(saveFileDialog2.FileName);
                    using (StreamWriter sw = new StreamWriter(saveFileDialog2.FileName))
                    {
                        foreach (RepositoryEntry entry in repository.MissingObjects)
                        {
                            sw.WriteLine($"{database};{entry.owner};{entry.name};{entry.type};");
                        }
                    }
                }
            }

        }

        private void tESTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MissingObject("TEST");
        }

        private void kPPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MissingObject("KPP");
        }

        private void uaktualnieniePlikuMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                WyrownanieKodu();
            }
            catch
            {
                MessageBox.Show("Nie wybrałeś pliku !");
            }
        }

        private void czescioweŁadowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFilters f = new frmFilters();
            f.repository = repository;
            f.path = folderBrowserDialog1.SelectedPath;

            if (f.ShowDialog() == DialogResult.OK)
            {
                repository.ConfigFiles.Clear();
                foreach (object filter in f.filtercheckedListBox1.CheckedItems)
                {
                    repository.ConfigFiles.Add(filter.ToString().Trim());
                }
                repository.LoadRepositoryError += (errors, information) => { MessageBox.Show(errors, information, MessageBoxButtons.OK, MessageBoxIcon.Error); };

                if (repository.LoadRepositories(folderBrowserDialog1.SelectedPath))
                {
                    Odswiez();
                }

                repository.LoadRepositoryError -= (errors, information) => { MessageBox.Show(errors, information, MessageBoxButtons.OK, MessageBoxIcon.Error); };
            }
        }

        private void tsGenerowaniePlikucfg_Click(object sender, EventArgs e)
        {
            if (!WprowadzHaslo(password_TEST)) return;

            repository.AllObjectsBAZA_BPSC("Test", user_TEST, password_TEST);

            if (repository.MissingObjects.Count == 0)
            {
                MessageBox.Show("Błąd podczas pobierania obiektów");
            }
            else
            {
                if (saveFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(saveFileDialog2.FileName)) File.Delete(saveFileDialog2.FileName);
                    using (StreamWriter sw = new StreamWriter(saveFileDialog2.FileName))
                    {
                        sw.WriteLine("DATABASE;OWNER;NAME;TYPE;DESCRIPTION");
                        foreach (RepositoryEntry entry in repository.MissingObjects)
                        {
                            sw.WriteLine($"BPSC;{entry.owner};{entry.name};{entry.type};");
                        }
                    }
                }
            }

        }

        private void tsmUaktualnieniePlikuRepozytoriumGitNaPodstawieKataloguRoboczego_Click(object sender, EventArgs e)
        {
            List<RepositoryEntry> lista = new List<RepositoryEntry>();

            if (obiektyJakoDrzewoToolStripMenuItem.Checked)
            {
                TABLENode = treeView1.Nodes[0];
                VIEWNode = treeView1.Nodes[1];
                FUNCTIONNode = treeView1.Nodes[2];
                PROCEDURENode = treeView1.Nodes[3];
                TRIGGERNode = treeView1.Nodes[4];
                PACKAGENode = treeView1.Nodes[5];
                SYNONYMNode = treeView1.Nodes[6]; ;
                SEQUENCENode = treeView1.Nodes[7];
                TYPENode = treeView1.Nodes[8];
                PROCOBJNode = treeView1.Nodes[9];

                foreach (TreeNode t in TABLENode.Nodes)
                {
                    lista.Add((RepositoryEntry)t.Tag);
                }

                foreach (TreeNode t in VIEWNode.Nodes)
                {
                    lista.Add((RepositoryEntry)t.Tag);
                }

                foreach (TreeNode t in FUNCTIONNode.Nodes)
                {
                    lista.Add((RepositoryEntry)t.Tag);
                }

                foreach (TreeNode t in PROCEDURENode.Nodes)
                {
                    lista.Add((RepositoryEntry)t.Tag);
                }

                foreach (TreeNode t in TRIGGERNode.Nodes)
                {
                    lista.Add((RepositoryEntry)t.Tag);
                }

                foreach (TreeNode t in PACKAGENode.Nodes)
                {
                    lista.Add((RepositoryEntry)t.Tag);
                }

                foreach (TreeNode t in SYNONYMNode.Nodes)
                {
                    lista.Add((RepositoryEntry)t.Tag);
                }

                foreach (TreeNode t in SEQUENCENode.Nodes)
                {
                    lista.Add((RepositoryEntry)t.Tag);
                }

                foreach (TreeNode t in TYPENode.Nodes)
                {
                    lista.Add((RepositoryEntry)t.Tag);
                }

                foreach (TreeNode t in PROCOBJNode.Nodes)
                {
                    lista.Add((RepositoryEntry)t.Tag);
                }

            }
            else {
                foreach (ListBoxEntry l in listaPlikow.Items)
                {
                    lista.Add(l.Entry);
                }
            }

            foreach (RepositoryEntry r in lista)
            {
                if (r.znak == "X" || r.znak == "+")
                {
                    if (File.Exists(String.Format("{0}\\{1}", folderBrowserDialog1.SelectedPath, r.FileName)))
                    {
                        File.Delete(String.Format("{0}\\{1}", folderBrowserDialog1.SelectedPath, r.FileName));
                    }
                    string katalogRoboczy = Properties.Settings.Default.katalogRoboczy;
                    File.Copy($"{katalogRoboczy}\\{r.FileName}", $"{folderBrowserDialog1.SelectedPath}\\{r.FileName}");
                    r.znak = " ";
                }

                if (obiektyJakoDrzewoToolStripMenuItem.Checked)
                {
                    TABLENode = treeView1.Nodes[0];
                    VIEWNode = treeView1.Nodes[1];
                    FUNCTIONNode = treeView1.Nodes[2];
                    PROCEDURENode = treeView1.Nodes[3];
                    TRIGGERNode = treeView1.Nodes[4];
                    PACKAGENode = treeView1.Nodes[5];
                    SYNONYMNode = treeView1.Nodes[6]; ;
                    SEQUENCENode = treeView1.Nodes[7];
                    TYPENode = treeView1.Nodes[8];
                    PROCOBJNode = treeView1.Nodes[9];

                    bool znalazl = false;
                    foreach(TreeNode n in TABLENode.Nodes)
                    {
                        if (n.Tag == r)
                        {
                            n.Text = String.Format("[{0}] {1}{2}", r.znak, r.grant ? "GRANT_" : "", r.name);
                            znalazl = true;
                            break;
                        }
                    }

                    if (!znalazl)
                    {
                        foreach (TreeNode n in VIEWNode.Nodes)
                        {
                            if (n.Tag == r)
                            {
                                n.Text = String.Format("[{0}] {1}{2}", r.znak, r.grant ? "GRANT_" : "", r.name);
                                znalazl = true;
                                break;
                            }
                        }
                    }

                    if (!znalazl)
                    {
                        foreach (TreeNode n in FUNCTIONNode.Nodes)
                        {
                            if (n.Tag == r)
                            {
                                n.Text = String.Format("[{0}] {1}{2}", r.znak, r.grant ? "GRANT_" : "", r.name);
                                znalazl = true;
                                break;
                            }
                        }
                    }

                    if (!znalazl)
                    {
                        foreach (TreeNode n in PROCEDURENode.Nodes)
                        {
                            if (n.Tag == r)
                            {
                                n.Text = String.Format("[{0}] {1}{2}", r.znak, r.grant ? "GRANT_" : "", r.name);
                                znalazl = true;
                                break;
                            }
                        }
                    }

                    if (!znalazl)
                    {
                        foreach (TreeNode n in TRIGGERNode.Nodes)
                        {
                            if (n.Tag == r)
                            {
                                n.Text = String.Format("[{0}] {1}{2}", r.znak, r.grant ? "GRANT_" : "", r.name);
                                znalazl = true;
                                break;
                            }
                        }
                    }

                    if (!znalazl)
                    {
                        foreach (TreeNode n in PACKAGENode.Nodes)
                        {
                            if (n.Tag == r)
                            {
                                n.Text = String.Format("[{0}] {1}{2}", r.znak, r.grant ? "GRANT_" : "", r.name);
                                znalazl = true;
                                break;
                            }
                        }
                    }

                    if (!znalazl)
                    {
                        foreach (TreeNode n in SYNONYMNode.Nodes)
                        {
                            if (n.Tag == r)
                            {
                                n.Text = String.Format("[{0}] {1}{2}", r.znak, r.grant ? "GRANT_" : "", r.name);
                                znalazl = true;
                                break;
                            }
                        }
                    }

                    if (!znalazl)
                    {
                        foreach (TreeNode n in SEQUENCENode.Nodes)
                        {
                            if (n.Tag == r)
                            {
                                n.Text = String.Format("[{0}] {1}{2}", r.znak, r.grant ? "GRANT_" : "", r.name);
                                znalazl = true;
                                break;
                            }
                        }
                    }

                    if (!znalazl)
                    {
                        foreach (TreeNode n in TYPENode.Nodes)
                        {
                            if (n.Tag == r)
                            {
                                n.Text = String.Format("[{0}] {1}{2}", r.znak, r.grant ? "GRANT_" : "", r.name);
                                znalazl = true;
                                break;
                            }
                        }
                    }

                    if (!znalazl)
                    {
                        foreach (TreeNode n in PROCOBJNode.Nodes)
                        {
                            if (n.Tag == r)
                            {
                                n.Text = String.Format("[{0}] {1}{2}", r.znak, r.grant ? "GRANT_" : "", r.name);
                                znalazl = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    int i;
                    for (i = 0; i < listaPlikow.Items.Count; i++)
                    {
                        ListBoxEntry lb = (ListBoxEntry)listaPlikow.Items[i];

                        RepositoryEntry ent = (RepositoryEntry)lb.Entry;
                        if (ent == r) break;
                    }

                    listaPlikow.Items.Insert(i, new ListBoxEntry(r));
                    listaPlikow.Items.RemoveAt(i);

                }
            }

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!WprowadzHaslo(password_BPSC)) return;

            repository.AllObjectsBAZA_BPSC("BPSC", user_BPSC, password_BPSC);

            if (repository.MissingObjects.Count == 0)
            {
                MessageBox.Show("Błąd podczas pobierania obiektów");
            }
            else
            {
                if (saveFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(saveFileDialog2.FileName)) File.Delete(saveFileDialog2.FileName);
                    using (StreamWriter sw = new StreamWriter(saveFileDialog2.FileName))
                    {
                        sw.WriteLine("DATABASE;OWNER;NAME;TYPE;DESCRIPTION");
                        foreach (RepositoryEntry entry in repository.MissingObjects)
                        {
                            sw.WriteLine($"BPSC;{entry.owner};{entry.name};{entry.type};");
                        }
                    }
                }
            }

        }
    }
}
