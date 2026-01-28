using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IntarRepo
{
    public class ListBoxEntry
    {
        public RepositoryEntry Entry { get; set; }

        public ListBoxEntry(RepositoryEntry entry)
        {
            this.Entry = entry;
        }
        public override string ToString()
        {
            string wynik = "";

            if (Entry.type != "CONTENT")
            {
                wynik = String.Format("[{0}] {2}_{3}{4}_{5}", Entry.znak, Entry.database, Entry.owner, Entry.grant ? "GRANT_" : "", Entry.name, Entry.type);
            }
            else
            {
                wynik = String.Format("[{0}] {2}_{3}_{4}", Entry.znak, Entry.database, Entry.owner, Entry.name, "CONTENT");
            }

            return wynik;
        }

    }
}
