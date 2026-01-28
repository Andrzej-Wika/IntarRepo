namespace IntarRepo
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikKonfiguracyjnyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otworzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.poswiadczeniaMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porownanieKoduMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BPSCRepozytoriumMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TESTRepozytoriumMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KPPRepozytoriumMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.uaktualnieniePlikuMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uruchomienieKoduMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rozpocznijTworzenieFiltruMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszFiltrDoPlikuMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.załadujFiltrZPlikuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usunFiltryStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pokazujTylkoBledyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porównajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.legendaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.obiektyJakoDrzewoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.sortujListedrzewoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sprawdźDodatkowePlikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zduplikowanePozycjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.obiektyKtórychBrakWRepozytoriumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bPSCToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tESTToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kPPToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsWdrozenieWersji15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGenerowaniePlikucfg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmUaktualnieniePlikuRepozytoriumGitNaPodstawieKataloguRoboczego = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.czescioweŁadowanieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.listaPlikow = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.filtrZapiszFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.otworzFiltrFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikKonfiguracyjnyMenuItem,
            this.poswiadczeniaMenuItem,
            this.porownanieKoduMenuItem,
            this.uruchomienieKoduMenuItem,
            this.filtryMenuItem,
            this.pomocToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(779, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikKonfiguracyjnyMenuItem
            // 
            this.plikKonfiguracyjnyMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.otworzToolStripMenuItem});
            this.plikKonfiguracyjnyMenuItem.Name = "plikKonfiguracyjnyMenuItem";
            this.plikKonfiguracyjnyMenuItem.Size = new System.Drawing.Size(120, 20);
            this.plikKonfiguracyjnyMenuItem.Text = "Plik konfiguracyjny";
            // 
            // otworzToolStripMenuItem
            // 
            this.otworzToolStripMenuItem.Name = "otworzToolStripMenuItem";
            this.otworzToolStripMenuItem.Size = new System.Drawing.Size(289, 22);
            this.otworzToolStripMenuItem.Text = "Otwórz folder z plikiem konfiguracyjnym";
            this.otworzToolStripMenuItem.Click += new System.EventHandler(this.otworzToolStripMenuItem_Click);
            // 
            // poswiadczeniaMenuItem
            // 
            this.poswiadczeniaMenuItem.Name = "poswiadczeniaMenuItem";
            this.poswiadczeniaMenuItem.Size = new System.Drawing.Size(96, 20);
            this.poswiadczeniaMenuItem.Text = "Poświadczenia";
            this.poswiadczeniaMenuItem.Click += new System.EventHandler(this.poswiadczeniaMenuItem_Click);
            // 
            // porownanieKoduMenuItem
            // 
            this.porownanieKoduMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BPSCRepozytoriumMenuItem,
            this.TESTRepozytoriumMenuItem,
            this.KPPRepozytoriumMenuItem,
            this.toolStripMenuItem1,
            this.uaktualnieniePlikuMenuItem});
            this.porownanieKoduMenuItem.Enabled = false;
            this.porownanieKoduMenuItem.Name = "porownanieKoduMenuItem";
            this.porownanieKoduMenuItem.Size = new System.Drawing.Size(112, 20);
            this.porownanieKoduMenuItem.Text = "Porównanie kodu";
            // 
            // BPSCRepozytoriumMenuItem
            // 
            this.BPSCRepozytoriumMenuItem.Name = "BPSCRepozytoriumMenuItem";
            this.BPSCRepozytoriumMenuItem.Size = new System.Drawing.Size(448, 22);
            this.BPSCRepozytoriumMenuItem.Text = "BPSC <=> Repozytorium Git";
            this.BPSCRepozytoriumMenuItem.ToolTipText = "Pobiera obiekty z Oracle z instancji BPSC i zapisuje je w formie skryptów w katal" +
    "ogu roboczym. Następnie porównuje  je z repozytorim Git";
            this.BPSCRepozytoriumMenuItem.Click += new System.EventHandler(this.BPSCRepozytoriumMenuItem_Click);
            // 
            // TESTRepozytoriumMenuItem
            // 
            this.TESTRepozytoriumMenuItem.Name = "TESTRepozytoriumMenuItem";
            this.TESTRepozytoriumMenuItem.Size = new System.Drawing.Size(448, 22);
            this.TESTRepozytoriumMenuItem.Text = "TEST <=> Repozytorium Git";
            this.TESTRepozytoriumMenuItem.ToolTipText = "Pobiera obiekty z Oracle z instancji TEST i zapisuje je w formie skryptów w katal" +
    "ogu roboczym. Następnie porównuje  je z repozytorim Git";
            this.TESTRepozytoriumMenuItem.Click += new System.EventHandler(this.TESTRepozytoriumMenuItem_Click);
            // 
            // KPPRepozytoriumMenuItem
            // 
            this.KPPRepozytoriumMenuItem.Name = "KPPRepozytoriumMenuItem";
            this.KPPRepozytoriumMenuItem.Size = new System.Drawing.Size(448, 22);
            this.KPPRepozytoriumMenuItem.Text = "KPP <=> Repozytorium Git";
            this.KPPRepozytoriumMenuItem.ToolTipText = "Pobiera obiekty z Oracle z instancji KPP i zapisuje je w formie skryptów w katalo" +
    "gu roboczym. Następnie porównuje  je z repozytorim Git";
            this.KPPRepozytoriumMenuItem.Click += new System.EventHandler(this.KPPRepozytoriumMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(445, 6);
            // 
            // uaktualnieniePlikuMenuItem
            // 
            this.uaktualnieniePlikuMenuItem.Name = "uaktualnieniePlikuMenuItem";
            this.uaktualnieniePlikuMenuItem.Size = new System.Drawing.Size(448, 22);
            this.uaktualnieniePlikuMenuItem.Text = "Uaktualnienie pliku repozytorium Git na podstawie katalogu roboczego";
            this.uaktualnieniePlikuMenuItem.Click += new System.EventHandler(this.uaktualnieniePlikuMenuItem_Click_1);
            // 
            // uruchomienieKoduMenuItem
            // 
            this.uruchomienieKoduMenuItem.Enabled = false;
            this.uruchomienieKoduMenuItem.Name = "uruchomienieKoduMenuItem";
            this.uruchomienieKoduMenuItem.Size = new System.Drawing.Size(124, 20);
            this.uruchomienieKoduMenuItem.Text = "Uruchomienie kodu";
            this.uruchomienieKoduMenuItem.Click += new System.EventHandler(this.uruchomienieKoduMenuItem_Click);
            // 
            // filtryMenuItem
            // 
            this.filtryMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rozpocznijTworzenieFiltruMenuItem,
            this.zapiszFiltrDoPlikuMenuItem,
            this.załadujFiltrZPlikuToolStripMenuItem,
            this.usunFiltryStripMenuItem,
            this.toolStripSeparator2,
            this.pokazujTylkoBledyMenuItem});
            this.filtryMenuItem.Enabled = false;
            this.filtryMenuItem.Name = "filtryMenuItem";
            this.filtryMenuItem.Size = new System.Drawing.Size(45, 20);
            this.filtryMenuItem.Text = "Filtry";
            // 
            // rozpocznijTworzenieFiltruMenuItem
            // 
            this.rozpocznijTworzenieFiltruMenuItem.Name = "rozpocznijTworzenieFiltruMenuItem";
            this.rozpocznijTworzenieFiltruMenuItem.Size = new System.Drawing.Size(213, 22);
            this.rozpocznijTworzenieFiltruMenuItem.Text = "Rozpocznij tworzenie filtru";
            this.rozpocznijTworzenieFiltruMenuItem.Click += new System.EventHandler(this.rozpocznijTworzenieFiltruToolStripMenuItem_Click);
            // 
            // zapiszFiltrDoPlikuMenuItem
            // 
            this.zapiszFiltrDoPlikuMenuItem.Enabled = false;
            this.zapiszFiltrDoPlikuMenuItem.Name = "zapiszFiltrDoPlikuMenuItem";
            this.zapiszFiltrDoPlikuMenuItem.Size = new System.Drawing.Size(213, 22);
            this.zapiszFiltrDoPlikuMenuItem.Text = "Zapisz filtr do pliku";
            this.zapiszFiltrDoPlikuMenuItem.Click += new System.EventHandler(this.zapiszFiltrDoPlikuMenuItem_Click);
            // 
            // załadujFiltrZPlikuToolStripMenuItem
            // 
            this.załadujFiltrZPlikuToolStripMenuItem.Name = "załadujFiltrZPlikuToolStripMenuItem";
            this.załadujFiltrZPlikuToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.załadujFiltrZPlikuToolStripMenuItem.Text = "Załaduj filtr z pliku";
            this.załadujFiltrZPlikuToolStripMenuItem.Click += new System.EventHandler(this.załadujFiltrZPlikuToolStripMenuItem_Click);
            // 
            // usunFiltryStripMenuItem
            // 
            this.usunFiltryStripMenuItem.Name = "usunFiltryStripMenuItem";
            this.usunFiltryStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.usunFiltryStripMenuItem.Text = "Usun wszystkie filtry";
            this.usunFiltryStripMenuItem.Click += new System.EventHandler(this.usunFiltryStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(210, 6);
            // 
            // pokazujTylkoBledyMenuItem
            // 
            this.pokazujTylkoBledyMenuItem.Name = "pokazujTylkoBledyMenuItem";
            this.pokazujTylkoBledyMenuItem.Size = new System.Drawing.Size(213, 22);
            this.pokazujTylkoBledyMenuItem.Text = "Pokazuj tylko błędy";
            this.pokazujTylkoBledyMenuItem.Click += new System.EventHandler(this.pokazujTylkoBledyMenuItem_Click);
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.porównajToolStripMenuItem,
            this.toolStripSeparator3,
            this.legendaToolStripMenuItem,
            this.toolStripSeparator4,
            this.obiektyJakoDrzewoToolStripMenuItem,
            this.toolStripSeparator5,
            this.sortujListedrzewoToolStripMenuItem,
            this.toolStripSeparator1,
            this.sprawdźDodatkowePlikiToolStripMenuItem,
            this.zduplikowanePozycjeToolStripMenuItem,
            this.obiektyKtórychBrakWRepozytoriumToolStripMenuItem,
            this.tsWdrozenieWersji15,
            this.toolStripSeparator6,
            this.czescioweŁadowanieToolStripMenuItem});
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            // 
            // porównajToolStripMenuItem
            // 
            this.porównajToolStripMenuItem.Name = "porównajToolStripMenuItem";
            this.porównajToolStripMenuItem.Size = new System.Drawing.Size(547, 22);
            this.porównajToolStripMenuItem.Text = "Porównanie kodów plików wygenerowane z repozytorium i bazy danych";
            this.porównajToolStripMenuItem.Click += new System.EventHandler(this.porównajToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(544, 6);
            // 
            // legendaToolStripMenuItem
            // 
            this.legendaToolStripMenuItem.Name = "legendaToolStripMenuItem";
            this.legendaToolStripMenuItem.Size = new System.Drawing.Size(547, 22);
            this.legendaToolStripMenuItem.Text = "Legenda";
            this.legendaToolStripMenuItem.Click += new System.EventHandler(this.legendaToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(544, 6);
            // 
            // obiektyJakoDrzewoToolStripMenuItem
            // 
            this.obiektyJakoDrzewoToolStripMenuItem.Checked = true;
            this.obiektyJakoDrzewoToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.obiektyJakoDrzewoToolStripMenuItem.Name = "obiektyJakoDrzewoToolStripMenuItem";
            this.obiektyJakoDrzewoToolStripMenuItem.Size = new System.Drawing.Size(547, 22);
            this.obiektyJakoDrzewoToolStripMenuItem.Text = "Obiekty jako drzewo";
            this.obiektyJakoDrzewoToolStripMenuItem.Click += new System.EventHandler(this.obiektyJakoDrzewoToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(544, 6);
            // 
            // sortujListedrzewoToolStripMenuItem
            // 
            this.sortujListedrzewoToolStripMenuItem.Name = "sortujListedrzewoToolStripMenuItem";
            this.sortujListedrzewoToolStripMenuItem.Size = new System.Drawing.Size(547, 22);
            this.sortujListedrzewoToolStripMenuItem.Text = "Sortuj listę/drzewo";
            this.sortujListedrzewoToolStripMenuItem.Click += new System.EventHandler(this.sortujListedrzewoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(544, 6);
            // 
            // sprawdźDodatkowePlikiToolStripMenuItem
            // 
            this.sprawdźDodatkowePlikiToolStripMenuItem.Name = "sprawdźDodatkowePlikiToolStripMenuItem";
            this.sprawdźDodatkowePlikiToolStripMenuItem.Size = new System.Drawing.Size(547, 22);
            this.sprawdźDodatkowePlikiToolStripMenuItem.Text = "Sprawdzenie czy w repozytorium nie ma dodatkowych obiektów oprócz podanych w *.cf" +
    "g";
            this.sprawdźDodatkowePlikiToolStripMenuItem.Click += new System.EventHandler(this.sprawdźDodatkowePlikiToolStripMenuItem_Click);
            // 
            // zduplikowanePozycjeToolStripMenuItem
            // 
            this.zduplikowanePozycjeToolStripMenuItem.Name = "zduplikowanePozycjeToolStripMenuItem";
            this.zduplikowanePozycjeToolStripMenuItem.Size = new System.Drawing.Size(547, 22);
            this.zduplikowanePozycjeToolStripMenuItem.Text = "Sprawdzenie czy w plikach *.cfg nie wpisano tych samych obiektów";
            this.zduplikowanePozycjeToolStripMenuItem.Click += new System.EventHandler(this.zduplikowanePozycjeToolStripMenuItem_Click);
            // 
            // obiektyKtórychBrakWRepozytoriumToolStripMenuItem
            // 
            this.obiektyKtórychBrakWRepozytoriumToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bPSCToolStripMenuItem1,
            this.tESTToolStripMenuItem1,
            this.kPPToolStripMenuItem1});
            this.obiektyKtórychBrakWRepozytoriumToolStripMenuItem.Name = "obiektyKtórychBrakWRepozytoriumToolStripMenuItem";
            this.obiektyKtórychBrakWRepozytoriumToolStripMenuItem.Size = new System.Drawing.Size(547, 22);
            this.obiektyKtórychBrakWRepozytoriumToolStripMenuItem.Text = "Obiekty, które są w bazie danych a brak ich w repozytorium";
            // 
            // bPSCToolStripMenuItem1
            // 
            this.bPSCToolStripMenuItem1.Name = "bPSCToolStripMenuItem1";
            this.bPSCToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.bPSCToolStripMenuItem1.Text = "BPSC";
            this.bPSCToolStripMenuItem1.Click += new System.EventHandler(this.bPSCToolStripMenuItem1_Click);
            // 
            // tESTToolStripMenuItem1
            // 
            this.tESTToolStripMenuItem1.Name = "tESTToolStripMenuItem1";
            this.tESTToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.tESTToolStripMenuItem1.Text = "TEST";
            this.tESTToolStripMenuItem1.Click += new System.EventHandler(this.tESTToolStripMenuItem1_Click);
            // 
            // kPPToolStripMenuItem1
            // 
            this.kPPToolStripMenuItem1.Name = "kPPToolStripMenuItem1";
            this.kPPToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.kPPToolStripMenuItem1.Text = "KPP";
            this.kPPToolStripMenuItem1.Click += new System.EventHandler(this.kPPToolStripMenuItem1_Click);
            // 
            // tsWdrozenieWersji15
            // 
            this.tsWdrozenieWersji15.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.tsGenerowaniePlikucfg,
            this.toolStripSeparator7,
            this.tsmUaktualnieniePlikuRepozytoriumGitNaPodstawieKataloguRoboczego});
            this.tsWdrozenieWersji15.Name = "tsWdrozenieWersji15";
            this.tsWdrozenieWersji15.Size = new System.Drawing.Size(547, 22);
            this.tsWdrozenieWersji15.Text = "Operacje masowe";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(448, 22);
            this.toolStripMenuItem2.Text = "Generowanie pliku *.cfg - BPSC";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // tsGenerowaniePlikucfg
            // 
            this.tsGenerowaniePlikucfg.Name = "tsGenerowaniePlikucfg";
            this.tsGenerowaniePlikucfg.Size = new System.Drawing.Size(448, 22);
            this.tsGenerowaniePlikucfg.Text = "Generowanie pliku *.cfg - TEST";
            this.tsGenerowaniePlikucfg.Click += new System.EventHandler(this.tsGenerowaniePlikucfg_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(445, 6);
            // 
            // tsmUaktualnieniePlikuRepozytoriumGitNaPodstawieKataloguRoboczego
            // 
            this.tsmUaktualnieniePlikuRepozytoriumGitNaPodstawieKataloguRoboczego.Name = "tsmUaktualnieniePlikuRepozytoriumGitNaPodstawieKataloguRoboczego";
            this.tsmUaktualnieniePlikuRepozytoriumGitNaPodstawieKataloguRoboczego.Size = new System.Drawing.Size(448, 22);
            this.tsmUaktualnieniePlikuRepozytoriumGitNaPodstawieKataloguRoboczego.Text = "Uaktualnienie pliku repozytorium Git na podstawie katalogu roboczego";
            this.tsmUaktualnieniePlikuRepozytoriumGitNaPodstawieKataloguRoboczego.Click += new System.EventHandler(this.tsmUaktualnieniePlikuRepozytoriumGitNaPodstawieKataloguRoboczego_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(544, 6);
            // 
            // czescioweŁadowanieToolStripMenuItem
            // 
            this.czescioweŁadowanieToolStripMenuItem.Enabled = false;
            this.czescioweŁadowanieToolStripMenuItem.Name = "czescioweŁadowanieToolStripMenuItem";
            this.czescioweŁadowanieToolStripMenuItem.Size = new System.Drawing.Size(547, 22);
            this.czescioweŁadowanieToolStripMenuItem.Text = "Przetwarzanie tylko wybranych *.cfg";
            this.czescioweŁadowanieToolStripMenuItem.Click += new System.EventHandler(this.czescioweŁadowanieToolStripMenuItem_Click);
            // 
            // listaPlikow
            // 
            this.listaPlikow.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.listaPlikow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listaPlikow.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listaPlikow.ForeColor = System.Drawing.SystemColors.Window;
            this.listaPlikow.FormattingEnabled = true;
            this.listaPlikow.Location = new System.Drawing.Point(0, 24);
            this.listaPlikow.Name = "listaPlikow";
            this.listaPlikow.Size = new System.Drawing.Size(779, 598);
            this.listaPlikow.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(53, 598);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(629, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // filtrZapiszFileDialog
            // 
            this.filtrZapiszFileDialog.Filter = "AWIRepo filters|*.arf";
            // 
            // otworzFiltrFileDialog
            // 
            this.otworzFiltrFileDialog.Filter = "AWIrepo files|*.arf";
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.DefaultExt = "txt";
            this.saveFileDialog2.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*\"";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(779, 622);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listaPlikow);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "INTAR Repozytorium kodów";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikKonfiguracyjnyMenuItem;
        private System.Windows.Forms.ListBox listaPlikow;
        private System.Windows.Forms.ToolStripMenuItem porownanieKoduMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem otworzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uaktualnieniePlikuMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem TESTRepozytoriumMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BPSCRepozytoriumMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KPPRepozytoriumMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem porównajToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog filtrZapiszFileDialog;
        private System.Windows.Forms.OpenFileDialog otworzFiltrFileDialog;
        private System.Windows.Forms.ToolStripMenuItem poswiadczeniaMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uruchomienieKoduMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rozpocznijTworzenieFiltruMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszFiltrDoPlikuMenuItem;
        private System.Windows.Forms.ToolStripMenuItem załadujFiltrZPlikuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem pokazujTylkoBledyMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem legendaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem obiektyJakoDrzewoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usunFiltryStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem sortujListedrzewoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem sprawdźDodatkowePlikiToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.ToolStripMenuItem zduplikowanePozycjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem czescioweŁadowanieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem obiektyKtórychBrakWRepozytoriumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bPSCToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tESTToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem kPPToolStripMenuItem1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem tsWdrozenieWersji15;
        private System.Windows.Forms.ToolStripMenuItem tsGenerowaniePlikucfg;
        private System.Windows.Forms.ToolStripMenuItem tsmUaktualnieniePlikuRepozytoriumGitNaPodstawieKataloguRoboczego;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}

