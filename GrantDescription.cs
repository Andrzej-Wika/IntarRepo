using IntarRepo.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace IntarRepo
{
    public class GrantDescription
    {
        const string pattern = @"GRANT\s(.+)\sON\s([\w_\$]+)\sTO\s(\w+)";
        public string operation { get; set; }
        public string resource { get; set; }
        public string user { get; set; }

        public GrantDescription(string line)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(line);

            // Sprawdzenie, czy dopasowanie było sukcesem
            if (match.Success)
            {
                // Utworzenie obiektu GrantDescription i przypisanie wartości grup

                operation = match.Groups[1].Value;  // Pierwsza grupa: operacja (np. "READ")
                resource = match.Groups[2].Value;   // Druga grupa: zasób (np. "Resource1")
                user = match.Groups[3].Value;        // Trzecia grupa: użytkownik (np. "User1")

            }
            else
            {
                throw new Exception($"Nie można dopasować GRANT'A: {line}");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is GrantDescription other)
            {
                return this.operation == other.operation &&
                       this.resource == other.resource &&
                       this.user == other.user;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (operation + resource + user).GetHashCode();
        }

        public override string ToString()
        {
            return $"Operation: {operation}, Resource: {resource}, User: {user}";
        }

    }
}
