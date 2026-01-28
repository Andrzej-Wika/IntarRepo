using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;


namespace IntarRepo
{
    public static class impulsConnector
    {
        public static bool PoprawnePoswiadczenia(string database, string user, string password)
        {
            bool wynik = true;

            try
            {
                OracleConnection cnn = UstawPolaczenie(database, user, password);
                cnn.Open();
                cnn.Close();
            }
            catch
            {
                wynik = false;
            }

            return wynik;
        }
        public static string Tekst(RepositoryEntry r, string database, string user, string password)
        {
            OracleConnection cnn = UstawPolaczenie(database, user, password);
            OracleCommand cmd = new OracleCommand();
            string wynik = "";
            List<String> lista = new List<String>();

            OracleParameter pType = new OracleParameter("type", OracleDbType.Varchar2, 255);
            OracleParameter pName = new OracleParameter("pName", OracleDbType.Varchar2, 255);
            OracleParameter pOwner = new OracleParameter("owner", OracleDbType.Varchar2, 255);

            cmd.Connection = cnn;

            try
            {
                cnn.Open();

                if (r.type.ToUpper() != "CONTENT")
                {
                    cmd.Parameters.Add(pType);
                    cmd.Parameters.Add(pName);
                    cmd.Parameters.Add(pOwner);

                    pType.Value = r.type;
                    pName.Value = r.name;
                    pOwner.Value = r.owner;

                    cmd.CommandText = string.Format("select dbms_metadata.get_ddl(:1,:2,:3) from dual");
                    wynik = cmd.ExecuteScalar().ToString();
                }
                else
                {
                    string parameters = r.parameters.Replace("{", "").Replace("}", "") + "||";
                    string select = parameters.Split('|')[0];
                    string where = parameters.Split('|')[1];
                    string order = parameters.Split('|')[2];

                    cmd.CommandText = "select " + select + " from " + r.owner + "." + r.name;

                    if (!String.IsNullOrEmpty(where))
                    {
                        cmd.CommandText += " where " + where;
                    }

                    if (!String.IsNullOrEmpty(order))
                    {
                        cmd.CommandText += " order by " + order;
                    }


                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        wynik = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            wynik += reader.GetName(i) + ";";
                        }
                        wynik += "\n";

                        while (reader.Read())
                        {
                            object[] values = new object[reader.FieldCount];
                            reader.GetValues(values);
                            foreach (object value in values)
                            {
                                wynik += value.ToString() + ";";
                            }
                            wynik += "\n";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                wynik = ex.Message;
            }
            finally
            {
                cnn.Close();
            }

            return wynik;
        }

        public static string Grant(RepositoryEntry r, string database, string user, string password)
        {

            OracleConnection cnn = UstawPolaczenie(database, user, password);
            OracleCommand cmd = new OracleCommand();
            string wynik = "";
            List<String> lista = new List<String>();
            OracleDataReader reader;

            OracleParameter pName = new OracleParameter("name", OracleDbType.Varchar2, 255);
            OracleParameter pOwner = new OracleParameter("owner", OracleDbType.Varchar2, 255);

            cmd.Connection = cnn;
            cmd.Parameters.Add(pName);
            cmd.Parameters.Add(pOwner);

            try
            {
                cnn.Open();

                pName.Value = r.name;
                pOwner.Value = r.owner;

                cmd.CommandText = string.Format("SELECT privilege,grantee FROM DBA_TAB_PRIVS WHERE TABLE_NAME = :1 AND OWNER = :2");
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lista.Add($"GRANT {reader.GetString(0)} ON {r.name} TO {reader.GetString(1)}");
                    }
                }
            }
            catch
            {
                wynik = "";
            }
            finally
            {
                cnn.Close();
            }

            lista.Sort();

            wynik = "";
            foreach (string s in lista)
            {
                wynik += s + ";\n";
            }

            return wynik;
        }

        public static void Wykonaj(string tekst, string database, string user, string password, string stary, RepositoryEntry r, string opis)
        {
            OracleConnection cnn = UstawPolaczenie(database, user, password);

            OracleCommand cmd = new OracleCommand();

            OracleParameter pObiekt = new OracleParameter("obiekt", OracleDbType.Varchar2, 200);
            OracleParameter pWlasciciel = new OracleParameter("wlasciciel", OracleDbType.Varchar2, 200);
            OracleParameter pStaryTekst = new OracleParameter("stary_tekst", OracleDbType.Clob);
            OracleParameter pNowyTekst = new OracleParameter("nowy_tekst", OracleDbType.Clob);
            OracleParameter pOpis = new OracleParameter("opis", OracleDbType.Varchar2, 2000);
            OracleParameter pTypObiektu = new OracleParameter("typ_obiektu", OracleDbType.Varchar2, 20);

            cmd.Connection = cnn;
            cmd.Parameters.Add(pObiekt);
            cmd.Parameters.Add(pWlasciciel);
            cmd.Parameters.Add(pStaryTekst);
            cmd.Parameters.Add(pNowyTekst);
            cmd.Parameters.Add(pOpis);
            cmd.Parameters.Add(pTypObiektu);

            OracleTransaction tr = null;

            try
            {
                cnn.Open();
                tr = cnn.BeginTransaction();

                pObiekt.Value = r.name;
                pWlasciciel.Value = r.owner;
                pStaryTekst.Value = stary;
                pNowyTekst.Value = tekst;

                if (string.IsNullOrEmpty(opis))
                {
                    pOpis.Value = DBNull.Value;
                }
                else
                {
                    pOpis.Value = opis;
                }

                pTypObiektu.Value = r.type;

                cmd.CommandText = "insert into awi_repo_wersja ( obiekt, wlasciciel, stary_tekst,nowy_tekst,opis_modyfikacji, typ_obiektu ) values ( :1,:2,:3,:4,:5, :6)";
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandText = tekst;
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch
            {
                tr.Rollback();
                throw;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static void WykonajGrant(string tekst, string database, string user, string password, string stary, RepositoryEntry r, string opis)
        {
            OracleConnection cnn = UstawPolaczenie(database, user, password);
            OracleCommand cmd = new OracleCommand();

            OracleParameter pObiekt = new OracleParameter("obiekt", OracleDbType.Varchar2, 200);
            OracleParameter pWlasciciel = new OracleParameter("wlasciciel", OracleDbType.Varchar2, 200);
            OracleParameter pStaryTekst = new OracleParameter("stary_tekst", OracleDbType.Clob);
            OracleParameter pNowyTekst = new OracleParameter("nowy_tekst", OracleDbType.Clob);
            OracleParameter pOpis = new OracleParameter("opis", OracleDbType.Varchar2, 2000);
            OracleParameter pTypObiektu = new OracleParameter("typ_obiektu", OracleDbType.Varchar2, 20);

            cmd.Connection = cnn;
            cmd.Parameters.Add(pObiekt);
            cmd.Parameters.Add(pWlasciciel);
            cmd.Parameters.Add(pStaryTekst);
            cmd.Parameters.Add(pNowyTekst);
            cmd.Parameters.Add(pOpis);
            cmd.Parameters.Add(pTypObiektu);

            OracleTransaction tr = null;

            try
            {
                cnn.Open();
                tr = cnn.BeginTransaction();

                pObiekt.Value = r.name;
                pWlasciciel.Value = r.owner;
                pStaryTekst.Value = stary;
                pNowyTekst.Value = tekst;

                if (string.IsNullOrEmpty(opis))
                {
                    pOpis.Value = DBNull.Value;
                }
                else
                {
                    pOpis.Value = opis;
                }

                pTypObiektu.Value = r.type;

                cmd.CommandText = "insert into awi_repo_wersja ( obiekt, wlasciciel, stary_tekst,nowy_tekst,opis_modyfikacji, typ_obiektu ) values ( :1,:2,:3,:4,:5, :6)";
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();

                string[] line = tekst.Trim().Split(';');

                for (int i = 0; i < line.Length - 1; i++)
                {
                    cmd.CommandText = line[i];
                    cmd.ExecuteNonQuery();
                }

                tr.Commit();
            }
            catch
            {
                tr.Rollback();
                throw;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static void Wykonaj(string tekst1, string tekst2, string database, string user, string password, string stary, RepositoryEntry r, string opis)
        {
            OracleConnection cnn = UstawPolaczenie(database, user, password);
            OracleCommand cmd = new OracleCommand();


            OracleParameter pObiekt = new OracleParameter("obiekt", OracleDbType.Varchar2, 200);
            OracleParameter pWlasciciel = new OracleParameter("wlasciciel", OracleDbType.Varchar2, 200);
            OracleParameter pStaryTekst = new OracleParameter("stary_tekst", OracleDbType.Clob);
            OracleParameter pNowyTekst = new OracleParameter("nowy_tekst", OracleDbType.Clob);
            OracleParameter pOpis = new OracleParameter("opis", OracleDbType.Varchar2, 2000);
            OracleParameter pTypObiektu = new OracleParameter("typ_obiektu", OracleDbType.Varchar2, 20);

            cmd.Connection = cnn;
            cmd.Parameters.Add(pObiekt);
            cmd.Parameters.Add(pWlasciciel);
            cmd.Parameters.Add(pStaryTekst);
            cmd.Parameters.Add(pNowyTekst);
            cmd.Parameters.Add(pOpis);
            cmd.Parameters.Add(pTypObiektu);

            OracleTransaction tr = null;

            try
            {
                cnn.Open();
                tr = cnn.BeginTransaction();

                pObiekt.Value = r.name;
                pWlasciciel.Value = r.owner;
                pStaryTekst.Value = stary;
                pNowyTekst.Value = tekst1 + tekst2;

                if (string.IsNullOrEmpty(opis))
                {
                    pOpis.Value = DBNull.Value;
                }
                else
                {
                    pOpis.Value = opis;
                }

                pTypObiektu.Value = r.type;

                cmd.CommandText = "insert into awi_repo_wersja ( obiekt, wlasciciel, stary_tekst,nowy_tekst,opis_modyfikacji, typ_obiektu ) values ( :1,:2,:3,:4,:5, :6)";
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();

                cmd.CommandText = tekst1;
                cmd.ExecuteNonQuery();

                cmd.CommandText = tekst2;
                cmd.ExecuteNonQuery();

                tr.Commit();
            }
            catch
            {
                tr.Rollback();
                throw;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static List<RepositoryEntry> WszystkieObiektyOracle(string database, string user, string password, string owner, string filtr = " 1=1 ")
        {
            List<RepositoryEntry> wynik = new List<RepositoryEntry>();
            OracleConnection cnn = UstawPolaczenie(database, user, password);
            OracleCommand cmd = new OracleCommand();
            OracleDataReader reader;

            OracleParameter pWlasciciel = new OracleParameter("wlasciciel", OracleDbType.Varchar2, 200);

            cmd.Connection = cnn;
            cmd.Parameters.Add(pWlasciciel);
            OracleTransaction tr = null;

            try
            {
                cnn.Open();
                tr = cnn.BeginTransaction();

                pWlasciciel.Value = owner;

                cmd.CommandText = "SELECT OBJECT_NAME,OWNER,OBJECT_TYPE FROM DBA_OBJECTS WHERE OWNER = :1 AND OBJECT_TYPE IN ('TABLE','VIEW','FUNCTION','PROCEDURE','TRIGGER','PACKAGE','SYNONYM','SEQUENCE','TYPE','JOB') AND " + filtr;
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RepositoryEntry entry = new RepositoryEntry();
                        entry.name = reader.GetString(0);
                        entry.owner = reader.GetString(1);
                        entry.type = reader.GetString(2);
                        entry.type = (entry.type == "JOB") ? "PROCOBJ" : entry.type;
                        entry.database = database;
                        wynik.Add(entry);
                    }
                }

                tr.Commit();
            }
            catch
            {
                tr.Rollback();
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return wynik;
        }

        public static bool IstniejeWBazie(string database, string user, string password, string owner, string object_name)
        {
            OracleConnection cnn = UstawPolaczenie(database, user, password);
            OracleCommand cmd = new OracleCommand();
            OracleDataReader reader;
            bool wynik = false;

            OracleParameter pWlasciciel = new OracleParameter("wlasciciel", OracleDbType.Varchar2, 200);
            OracleParameter pNazwa = new OracleParameter("nazwa", OracleDbType.Varchar2, 2000);

            cmd.Connection = cnn;
            cmd.Parameters.Add(pWlasciciel);
            cmd.Parameters.Add(pNazwa);
            OracleTransaction tr = null;

            try
            {
                cnn.Open();
                tr = cnn.BeginTransaction();

                pWlasciciel.Value = owner;
                pNazwa.Value = object_name;

                cmd.CommandText = "SELECT OBJECT_NAME,OWNER,OBJECT_TYPE FROM DBA_OBJECTS WHERE OWNER = :1 AND OBJECT_NAME = :2";
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        wynik = true;
                    }
                }

                tr.Commit();
            }
            catch
            {
                tr.Rollback();
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return wynik;
        }

        private static OracleConnection UstawPolaczenie(string database, string user, string password)
        {
            var connectionString = Program.Configuration
                .GetConnectionString("IMPULSConnectionString")
                ?? throw new InvalidOperationException(
                    "Brak connection stringa 'IMPULSConnectionString' w konfiguracji.");

            connectionString = connectionString
                .Replace("{database}", database)
                .Replace("{user}", user)
                .Replace("{password}", password);

            return new OracleConnection(connectionString);
        }
    }

}
