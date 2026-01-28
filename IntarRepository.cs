using System.Text.RegularExpressions;

namespace IntarRepo
{
    public class IntarRepository
    {
        public delegate void LoadRepositoryErrorEventHandler(string errors, string information);
        public delegate void ProgressEventHandler(int percent);
        public delegate void ObjectCheckedEventHandler(string name);
        public event ProgressEventHandler ProgressEvent;
        public event ObjectCheckedEventHandler ObjectCheckedEvent;
        public event LoadRepositoryErrorEventHandler LoadRepositoryError;
        public List<RepositoryEntry> Entries { get; set; }
        public List<RepositoryEntry> WorkList { get; set; }
        public List<RepositoryEntry> DuplicatedEntries { get; set; }
        public List<string> ConfigFiles { get; set; }
        public List<RepositoryEntry> MissingObjects { get; set; }
        public IntarRepository()
        {
            Entries = new List<RepositoryEntry>();
            DuplicatedEntries = new List<RepositoryEntry>();
            WorkList = new List<RepositoryEntry>();
            ConfigFiles = new List<string>();
            MissingObjects = new List<RepositoryEntry>();
        }
        public bool LoadRepositories(string path)
        {
            bool result = true;

            Entries.Clear();
            DuplicatedEntries.Clear();
            if (ConfigFiles.Count == 0)
            {
                RegistryManager rm = new RegistryManager();
                string[] conf = null;
                try
                {
                    conf = rm.LoadConfigFilesList(path).Split(';');
                }
                catch (Exception e)
                {
                    conf = null;
                }

                foreach (string f in Directory.GetFiles(path, "*.cfg"))
                {
                    if (conf != null)
                    {
                        if (conf.Contains(Path.GetFileNameWithoutExtension(f))) ConfigFiles.Add(Path.GetFileNameWithoutExtension(f));
                    }
                    else
                    {
                        ConfigFiles.Add(Path.GetFileNameWithoutExtension(f));
                    }
                }
            }
            foreach (string f in Directory.GetFiles(path, "*.cfg"))
            {
                if (ConfigFiles.Contains(Path.GetFileNameWithoutExtension(f)))
                {
                    RepositoryConfig rc = new RepositoryConfig(f);
                    if (!rc.Validate())
                    {
                        string errors = "";
                        foreach (var error in rc.ValidationErrors)
                        {
                            errors += error + "\n";
                        }
                        TriggerLoadRepositoryError(errors, $"{Path.GetFileName(f)} - Błędne repozytorium");
                        result = false;
                    }
                    else
                    {
                        foreach (RepositoryConfigEntry entry in rc.RepositoryEntries)
                        {
                            if (!Entries.Contains(new RepositoryEntry(entry)))
                            {
                                Entries.Add(new RepositoryEntry(entry));
                            }
                            else
                            {
                                DuplicatedEntries.Add(new RepositoryEntry(entry));
                            }
                        }
                    }
                }
            }

            List<RepositoryEntry> tmp = new List<RepositoryEntry>();
            foreach (RepositoryEntry entry in Entries)
            {
                if (entry.owner != "PUBLIC")
                {
                    if (entry.type != "SEQUENCE" && entry.type != "CONTENT" && entry.type != "TRIGGER" && entry.type != "PROCOBJ" && entry.type != "TYPE")
                    {
                        RepositoryEntry g = new RepositoryEntry();
                        g.grant = true;
                        g.database = entry.database;
                        g.znak = entry.znak;
                        g.type = entry.type;
                        g.owner = entry.owner;
                        g.name = entry.name;

                        tmp.Add(g);
                    }
                }
            }

            foreach (RepositoryEntry entry in tmp)
            {
                Entries.Add(entry);
            }

            WorkList.Clear();
            foreach (RepositoryEntry entry in Entries)
            {
                WorkList.Add(entry);
            }

            return result;
        }
        public void SaveFilters(List<RepositoryEntry> listFiltrow, string sciezka)
        {
            using (StreamWriter filtrPlik = File.AppendText(sciezka))
            {
                filtrPlik.WriteLine($"owner;name;type;parameters;grant");

                foreach (RepositoryEntry r in listFiltrow)
                {
                    string type = r.content ? "CONTENT" : r.name;
                    bool grant = r.content ? false : r.grant;
                    filtrPlik.WriteLine($"{r.owner};{type};{r.type};{r.parameters};{r.grant}");
                }
            }
        }
        public bool ApplyFilter(string[] path)
        {
            bool result = true;
            List<RepositoryEntry> list = new List<RepositoryEntry>();

            foreach (string filterpath in path)
            {
                FilterConfig fc = new FilterConfig(filterpath);
                if (!fc.Validate())
                {
                    string errors = "";
                    foreach (var error in fc.ValidationErrors)
                    {
                        errors += error + "\n";
                    }
                    MessageBox.Show(errors, $"{Path.GetFileName(filterpath)} - Błędny filtr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = false;
                }
                else
                {
                    List<RepositoryEntry> filters = OpenFilter(fc.FilterEntries);
                    if (filters.Count > 0)
                    {
                        foreach (RepositoryEntry f in filters)
                        {
                            foreach (RepositoryEntry r in Entries)
                            {
                                if (r.owner == f.owner && r.name == f.name && r.type == f.type && r.grant == f.grant && r.content == f.content)
                                {
                                    f.znak = r.znak;
                                    list.Add(r);
                                    break;
                                }
                            }
                        }
                    }
                }
                if (!result) break;
            }

            if (result && list.Count > 0)
            {
                WorkList.Clear();
                foreach (RepositoryEntry rf in list)
                {
                    WorkList.Add(rf);
                }
            }
            return result;
        }
        public bool RunCode(RepositoryEntry r, string folder, string database, string user, string password, string description, out string error)
        {
            bool result = true;
            string stary;

            if (!File.Exists($"{folder}\\{r.FileName}"))
            {
                error = "Brak skryptu do uruchomienia!";
                return false;
            }

            string tekst = File.ReadAllText($"{folder}\\{r.FileName}");

            if (r.grant)
            {
                try
                {
                    stary = impulsConnector.Grant(r, database, user, password);
                    impulsConnector.WykonajGrant(tekst, database, user, password, stary, r, description);
                    error = "Skrypt zakonczył się bezbłędnie";
                    result = true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    result = false;
                }
            }
            else if (r.type != "PACKAGE" && r.type != "TRIGGER" && r.type != "CONTENT")
            {
                try
                {
                    stary = impulsConnector.Tekst(r, database, user, password);
                    impulsConnector.Wykonaj(tekst, database, user, password, stary, r, description);
                    error = "Skrypt zakonczył się bezbłędnie";
                    result = true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    result = false;
                }
            }
            else if (r.type == "TRIGGER")
            {
                int at = tekst.IndexOf("ALTER TRIGGER");
                tekst = tekst.Substring(0, --at);

                try
                {
                    stary = impulsConnector.Tekst(r, database, user, password);
                    impulsConnector.Wykonaj(tekst, database, user, password, stary, r, description);
                    error = "Skrypt zakonczył się bezbłędnie";
                    result = true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    result = false;
                }
            }
            else if (r.type == "PACKAGE")
            {
                int pb = tekst.IndexOf("CREATE OR REPLACE EDITIONABLE PACKAGE BODY");
                string package = tekst.Substring(0, --pb);
                string body = tekst.Substring(pb);

                try
                {
                    stary = impulsConnector.Tekst(r, database, user, password);
                    impulsConnector.Wykonaj(package, body, database, user, password, stary, r, description);
                    error = "Skrypt zakonczył się bezbłędnie";
                    result = true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    result = false;
                }
            }
            else
            {
                error = "Niepoprawny obiekt";
                result = false;
            }
            return result;
        }
        private List<RepositoryEntry> OpenFilter(List<FilterConfigEntry> fce)
        {
            List<RepositoryEntry> result = new List<RepositoryEntry>();

            foreach (FilterConfigEntry entry in fce)
            {
                RepositoryEntry r = new RepositoryEntry();

                r.owner = entry.Owner;
                r.name = entry.Name;

                r.content = false;
                r.type = entry.Type;
                r.parameters = entry.Type;
                r.grant = entry.Grant == "TRUE";

                result.Add(r);
            }

            return result;
        }
        public void CompareRepositoryWitDatabase(string repositoryFolder, string workFolder, string database, string user, string password)
        {
            string znak = " ";
            bool rowne = false;
            string tekst_repository;
            string tekst_database;
            List<RepositoryEntry> tmp = new List<RepositoryEntry>();

            int i = 0;
            foreach (RepositoryEntry r in WorkList)
            {
                i++;
                string tekst = (!r.grant) ? impulsConnector.Tekst(r, database, user, password) : impulsConnector.Grant(r, database, user, password);
                bool istnieje = impulsConnector.IstniejeWBazie(database, user, password, r.owner, r.name);

                if (File.Exists($"{workFolder}\\{r.FileName}")) File.Delete($"{workFolder}\\{r.FileName}");

                if (tekst.Length > 0 && istnieje)
                {
                    using (StreamWriter sw = File.CreateText($"{workFolder}\\{r.FileName}"))
                    {
                        sw.Write(tekst);
                    }
                }
                TriggerProgressEvent((int)Math.Round(i / (decimal)WorkList.Count * 100, 0));

                if (!r.grant)
                {
                    if (!File.Exists($"{repositoryFolder}\\{r.FileName}"))
                    {
                        znak = "X";
                    }
                    else
                    {
                        if (tekst.Length > 0 && istnieje)
                        {
                            switch (r.type)
                            {
                                case "TABLE":
                                case "VIEW":
                                case "FUNCTION":
                                case "PROCEDURE":
                                case "PACKAGE":
                                case "SYNONYM":
                                case "PROCOBJ":
                                    rowne = File.ReadLines($"{repositoryFolder}\\{r.FileName}").SequenceEqual<string>(File.ReadLines($"{workFolder}\\{r.FileName}"));
                                    znak = rowne ? " " : "+";
                                    break;
                                case "SEQUENCE":
                                    tekst_repository = File.ReadAllText($"{repositoryFolder}\\{r.FileName}");
                                    tekst_database = File.ReadAllText($"{workFolder}\\{r.FileName}");
                                    tekst_repository = RemoveGroupsFromPattern(tekst_repository, "START\\s+WITH\\s+(\\d+)", new int[] { 1 });
                                    tekst_database = RemoveGroupsFromPattern(tekst_database, "START\\s+WITH\\s+(\\d+)", new int[] { 1 }); ;
                                    znak = tekst_repository == tekst_database ? " " : "+";
                                    break;
                                case "TRIGGER":
                                    tekst_repository = File.ReadAllText($"{repositoryFolder}\\{r.FileName}");
                                    tekst_database = File.ReadAllText($"{workFolder}\\{r.FileName}");
                                    tekst_repository = RemoveGroupsFromPattern(tekst_repository, @";(\s*)ALTER\s+TRIGGER\s+""?\w+""?\.""?\w+""?\s+(ENABLE|DISABLE)(\s*)$", new int[] { 1, 3 });
                                    tekst_database = RemoveGroupsFromPattern(tekst_database, @";(\s*)ALTER\s+TRIGGER\s+""?\w+""?\.""?\w+""?\s+(ENABLE|DISABLE)(\s*)$", new int[] { 1, 3 }); ;
                                    //tekst_repository = RemoveGroupsFromPattern(tekst_repository, @"^\s*CREATE\s+OR\s+REPLACE\s+EDITIONABLE\s+TRIGGER.+?(\s+)(?=WHEN)", new int[] { 1 });
                                    //tekst_database = RemoveGroupsFromPattern(tekst_database, @"^\s*CREATE\s+OR\s+REPLACE\s+EDITIONABLE\s+TRIGGER.+?(\s+)(?=WHEN)", new int[] { 1 }); ;
                                    tekst_repository = RemoveGroupsFromPattern(tekst_repository, @"(\s+)(?=WHEN)", new int[] { 1 });
                                    tekst_database = RemoveGroupsFromPattern(tekst_database, @"(\s+)(?=WHEN)", new int[] { 1 }); ;
                                    znak = tekst_repository == tekst_database ? " " : "+";
                                    break;
                                case "TYPE":
                                    tekst_repository = File.ReadAllText($"{repositoryFolder}\\{r.FileName}");
                                    tekst_database = File.ReadAllText($"{workFolder}\\{r.FileName}");
                                    tekst_repository = RemoveGroupsFromPattern(tekst_repository, @"^(\s*)CREATE", new int[] { 1 });
                                    tekst_database = RemoveGroupsFromPattern(tekst_database, @"^(\s*)CREATE", new int[] { 1 }); ;
                                    tekst_repository = RemoveGroupsFromPattern(tekst_repository, @"(\s*)$", new int[] { 1 });
                                    tekst_database = RemoveGroupsFromPattern(tekst_database, @"(\s*)$", new int[] { 1 }); ;
                                    znak = tekst_repository == tekst_database ? " " : "+";
                                    break;
                                default:
                                    znak = "+";
                                    break;
                            }
                        }
                        else
                        {
                            znak = "-";
                        }
                    }

                }
                else
                {
                    if (File.Exists($"{repositoryFolder}\\{r.FileName}") && File.Exists($"{workFolder}\\{r.FileName}"))
                    {
                        string[] grant_repository_lines = File.ReadAllLines($"{repositoryFolder}\\{r.FileName}");
                        string[] grant_database_lines = File.ReadAllLines($"{workFolder}\\{r.FileName}");

                        List<GrantDescription> grants_repository = new List<GrantDescription>();
                        List<GrantDescription> grants_database = new List<GrantDescription>();


                        foreach (string l in grant_repository_lines)
                        {
                            GrantDescription gd = new GrantDescription(l);
                            if (gd.user != "BPSC_APP_SERVER") grants_repository.Add(gd);
                        }

                        foreach (string l in grant_database_lines)
                        {
                            GrantDescription gd = new GrantDescription(l);
                            if (gd.user != "BPSC_APP_SERVER") grants_database.Add(gd);
                        }

                        znak = CompareGrantDescriptions(grants_repository, grants_database) ? " " : "+";
                    }
                    else if (!File.Exists($"{repositoryFolder}\\{r.FileName}") && !File.Exists($"{workFolder}\\{r.FileName}"))
                    {
                        znak = " ";
                    }
                    else if ((File.Exists($"{repositoryFolder}\\{r.FileName}") && !File.Exists($"{workFolder}\\{r.FileName}")))
                    {
                        znak = "-";
                    }
                    if ((!File.Exists($"{repositoryFolder}\\{r.FileName}") && File.Exists($"{workFolder}\\{r.FileName}")))
                    {
                        znak = "X";
                    }
                }

                r.znak = znak;
                tmp.Add(r);
            }
            WorkList.Clear();
            foreach (RepositoryEntry entry in tmp)
            {
                WorkList.Add(entry);
            }
        }

        public void AllObjectsBAZA_BPSC(string database, string user, string password)
        {
            MissingObjects.Clear();
            List<RepositoryEntry> objectsFromDb = impulsConnector.WszystkieObiektyOracle(database, user, password, "BAZA_BPSC");

            foreach (RepositoryEntry entry in objectsFromDb)
            {
                entry.database = "BPSC";
            }
            MissingObjects = objectsFromDb.Except(WorkList).ToList();
        }

        public void FillMissingObjects(string database, string user, string password)
        {
            string owner = WorkList.FirstOrDefault<RepositoryEntry>().owner;
            MissingObjects.Clear();
            List<RepositoryEntry> objectsFromDb = owner != "BAZA_BPSC" ? impulsConnector.WszystkieObiektyOracle(database, user, password, owner) : impulsConnector.WszystkieObiektyOracle(database, user, password, owner, " ((OBJECT_NAME LIKE 'INTAR%') OR (OBJECT_NAME LIKE 'KBJ%') OR (OBJECT_NAME LIKE 'UE_%') OR (OBJECT_NAME LIKE 'AWI_%') OR (OBJECT_NAME LIKE 'MSZ_%')  OR (OBJECT_NAME LIKE 'MKR_%')) ");

            foreach (RepositoryEntry entry in objectsFromDb)
            {
                entry.database = "BPSC";
            }
            MissingObjects = objectsFromDb.Except(WorkList).ToList();
        }
        protected virtual void OnProgressEvent(int percent)
        {
            if (ProgressEvent != null)
            {
                ProgressEvent(percent);
            }
        }
        public void TriggerProgressEvent(int percent)
        {
            OnProgressEvent(percent);
        }
        protected virtual void OnObjectCheckedEvent(string name)
        {
            if (ObjectCheckedEvent != null)
            {
                ObjectCheckedEvent(name);
            }
        }
        public void TriggerObjectCheckedEvent(string name)
        {
            OnObjectCheckedEvent(name);
        }
        public void OnLoadRepositoryError(string error, string information)
        {
            if (LoadRepositoryError != null)
            {
                LoadRepositoryError(error, information);
            }
        }
        public void TriggerLoadRepositoryError(string error, string information)
        {
            OnLoadRepositoryError(error, information);
        }

        private string RemoveGroupsFromPattern(string input, string pattern, int[] groupsToRemove)
        {
            // Kompilacja wyrażenia regularnego
            Regex regex = new Regex(pattern);

            // Zamiana dla każdego dopasowania
            return regex.Replace(input, match =>
            {
                string result = match.Value;

                // Iteracja przez grupy do usunięcia
                foreach (int groupIndex in groupsToRemove)
                {
                    if (groupIndex > 0 && groupIndex < match.Groups.Count)
                    {
                        if (!String.IsNullOrEmpty(match.Groups[groupIndex].Value)) result = result.Replace(match.Groups[groupIndex].Value, "");
                    }
                }

                return result;
            });
        }

        private bool CompareGrantDescriptions(List<GrantDescription> repo, List<GrantDescription> data)
        {

            if ((repo.Count != data.Count)) return false;

            // Znalezienie elementów wspólnych
            var common = repo.Intersect(data).ToList();
            if (common.Count != repo.Count) return false;

            // Znalezienie elementów występujących tylko w list1
            var onlyInRepo = repo.Except(data).ToList();
            if (onlyInRepo.Count > 0) return false;

            // Znalezienie elementów występujących tylko w list2
            var onlyInData = data.Except(repo).ToList();
            if (onlyInData.Count > 0) return false;

            return true;

        }
    }

    public class RepositoryEntry : IComparable
    {

        public RepositoryEntry()
        {
            znak = " ";
        }
        public RepositoryEntry(RepositoryConfigEntry r)
        {
            database = r.Database ?? "";
            owner = r.Owner ?? "";
            name = r.Name ?? "";
            type = r.Type ?? "";
            znak = " ";
        }

        public string database { set; get; } = "";
        public string owner { set; get; } = "";
        public string name { set; get; } = "";
        public string type { set; get; } = "";
        public string? parameters { get; set; }
        public string znak { set; get; } = " ";
        public bool grant { set; get; } = false;
        public bool content { set; get; } = false;
        public string FileName
        {
            get
            {
                string wynik = "";

                if (type != "CONTENT")
                {
                    wynik = String.Format("{2}_{3}{4}_{5}.sql", znak, database, owner, grant ? "GRANT_" : "", name, type);
                }
                else
                {
                    wynik = String.Format("{2}_{3}_{4}.sql", znak, database, owner, name, "CONTENT");
                }

                return wynik;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            RepositoryEntry x = obj as RepositoryEntry;
            if (x != null)
                return (x.database == this.database) && (x.owner == this.owner) && (x.name == this.name) && (x.type == this.type) ? 1 : 0;
            else
                throw new ArgumentException("Object is not a RepositoryEntry");
        }

        public override bool Equals(object obj)
        {
            RepositoryEntry x = obj as RepositoryEntry;
            if (x != null)
                return (x.database == this.database) && (x.owner == this.owner) && (x.name == this.name) && (x.type == this.type);
            else
                throw new ArgumentException("Object is not a RepositoryEntry");
        }
        public override int GetHashCode()
        {
            int hash = 17;  // Początkowa wartość hash (dowolna liczba pierwsza)

            hash = hash * 31 + (name != null ? name.GetHashCode() : 0);
            hash = hash * 31 + (owner != null ? owner.GetHashCode() : 0);
            hash = hash * 31 + (database != null ? database.GetHashCode() : 0);
            hash = hash * 31 + (type != null ? type.GetHashCode() : 0);

            return hash;
        }
    }
}
