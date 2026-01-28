using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace IntarRepo
{
    public class FilterConfig
    {
        private readonly string _filePath;

        // Kolekcja do przechowywania błędów walidacji
        public List<string> ValidationErrors { get; private set; }
        public List<FilterConfigEntry> FilterEntries { get; private set; } = new List<FilterConfigEntry>();

        private static readonly Regex komentarzRegex = new Regex(@"^//.*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex headeRegex = new Regex(@"^\s*OWNER\s*;\s*NAME\s*;\s*TYPE\s*;\s*PARAMETERS\s*;\s*GRANT\s*[;]?\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex ownerRegex = new Regex(@"^(BAZA_BPSC|EROZRYS|NAVIGATOR|B2B|B2C|INTEGRACJA|PUBLIC)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex nameRegex = new Regex(@"^[a-zA-Z0-9_]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex parametersRegex = new Regex(@"^[a-zA-Z0-9_]?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex typeRegex = new Regex(@"^(TABLE|VIEW|FUNCTION|PROCEDURE|TRIGGER|PACKAGE|SYNONYM|SEQUENCE|TYPE|PROCOBJ)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex grantRegex = new Regex(@"^(FALSE|TRUE)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex newlineRegex = new Regex(@"^\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Konstruktor z parametrami ścieżki do pliku i typem pliku
        public FilterConfig(string filePath)
        {
            _filePath = filePath;

            ValidationErrors = new List<string>(); // Inicjalizacja listy błędów
        }

        // Metoda Validate, która zwraca true, jeśli plik jest zgodny ze specyfikacją
        public bool Validate()
        {
            Stack<char> stack = new Stack<char>();
            int currentLevel = 0;

            ValidationErrors.Clear(); // Czyszczenie listy błędów przed rozpoczęciem walidacji

            try
            {
                var lines = File.ReadAllLines(_filePath);
                bool headerChecked = false;
                int i = 0;

                foreach (var line in lines)
                {
                    i++;
                    var trimmedLine = line.Trim();

                    // Pomijanie pustych linii i komentarzy
                    if (string.IsNullOrEmpty(trimmedLine) || komentarzRegex.IsMatch(trimmedLine))
                    {
                        continue;
                    }

                    // Sprawdzenie nagłówka
                    if (!headerChecked)
                    {

                        if (!headeRegex.IsMatch(trimmedLine))
                        {
                            ValidationErrors.Add("File has a bad format");
                            return false;
                        }
                        headerChecked = true;
                        continue;
                    }


                    if (trimmedLine.StartsWith("["))
                    {
                        if (currentLevel >= 20)
                        {
                            ValidationErrors.Add($"Nesting grather than twenty levels {i}.{line}");
                            continue;
                        }
                        stack.Push('[');
                        currentLevel++;
                        continue;
                    }
                    else if (trimmedLine.StartsWith("]"))
                    {
                        if (stack.Count == 0 || stack.Pop() != '[')
                        {
                            ValidationErrors.Add($"Missing [ {i}.{line}");
                            continue;
                        }
                        currentLevel--;
                        continue;
                    }

                    var parts = trimmedLine.Split(';');
                    ValidateFilter(parts, line);
                }

                if (stack.Count != 0)
                {
                    ValidationErrors.Add($"Missing ] on the end of file");
                }

                // Jeśli lista błędów jest pusta, walidacja przebiegła pomyślnie
                return ValidationErrors.Count == 0;
            }
            catch (Exception ex)
            {
                ValidationErrors.Add($"Error reading file: {ex.Message}");
                return false;
            }
        }

        private void ValidateFilter(string[] parts, string line)
        {
            var owner = parts[0].Trim();
            var name = parts[1].Trim();
            var type = parts[2].Trim();
            var parameters = parts[3].Trim();
            var grant = parts[4].Trim();

            if (!ownerRegex.IsMatch(owner))
            {
                ValidationErrors.Add($"Invalid owner: {owner} in line: {line}");
            }

            if (!nameRegex.IsMatch(name))
            {
                ValidationErrors.Add($"Invalid name: {name} in line: {line}");
            }

            if (!typeRegex.IsMatch(type))
            {
                ValidationErrors.Add($"Invalid type: {type} in line: {line}");
            }

            if (!parametersRegex.IsMatch(parameters))
            {
                ValidationErrors.Add($"Invalid parameters: {parameters} in line: {line}");
            }

            if (!grantRegex.IsMatch(grant))
            {
                ValidationErrors.Add($"Invalid grant: {grant} in line: {line}");
            }

            FilterEntries.Add(new FilterConfigEntry(owner, name, type, parameters, grant));
        }
    }
    public class FilterConfigEntry
    {
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Parameters { get; set; }
        public string Grant { get; set; }

        public FilterConfigEntry(string owner, string name, string type, string parameters, string grant)
        {
            Owner = owner;
            Name = name;
            Type = type;
            Parameters= parameters;
            Grant = grant;
        }
    }

}
