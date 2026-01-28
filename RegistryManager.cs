namespace IntarRepo
{
    using Microsoft.Win32;
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;

    internal class RegistryManager
    {
        private static readonly string RegistryKeyPath = @"SOFTWARE\INTAR\INTARRepo\Repositories";

        // Metoda do zapisania danych do rejestru
        public bool SaveConfigFilesList(string repository_path, string config_files_list, out string error)
        {
            error = "OK";

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey($"{RegistryKeyPath}"))
            {
                if (key == null)
                {
                    error = "Nie można utworzyć klucza w rejestrze";
                    return false;
                }

                bool found = false;

                // Przejrzyj wszystkie podklucze
                foreach (string subKeyName in key.GetSubKeyNames())
                {
                    // Sprawdź, czy nazwa podklucza zaczyna się od "Repositry" i kończy się liczbą
                    if (subKeyName.StartsWith("Repositry") && int.TryParse(subKeyName.Substring(9), out _))
                    {
                        // Otwórz podklucz
                        using (RegistryKey subKey = key.OpenSubKey(subKeyName, writable: true))
                        {
                            if (subKey != null)
                            {
                                string pathValue = subKey.GetValue("path") as string;
                                if (pathValue != null && pathValue.Equals(repository_path, StringComparison.OrdinalIgnoreCase))
                                {
                                    subKey.SetValue("lista", config_files_list);
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (!found)
                {
                    // Generowanie nowej nazwy klucza RepositryX, np. Repositry1, Repositry2 itd.
                    int newKeyIndex = key.GetSubKeyNames().Length + 1;
                    string newSubKeyName = $"Repositry{newKeyIndex}";

                    // Tworzenie nowego podklucza i ustawienie wartości "path"
                    using (RegistryKey newSubKey = key.CreateSubKey(newSubKeyName))
                    {
                        if (newSubKey != null)
                        {
                            newSubKey.SetValue("path", repository_path);
                            newSubKey.SetValue("lista", config_files_list);
                        }
                        else
                        {
                            error = "Nie udało się utworzyć klucza w rejestrze !";              }
                    }
                }

            }
            return true;
        }

        public string LoadConfigFilesList(string repository_path)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey($"{RegistryKeyPath}"))
            {
                if (key == null)
                {
                    return null;
                }

                // Przejrzyj wszystkie podklucze
                foreach (string subKeyName in key.GetSubKeyNames())
                {
                    // Sprawdź, czy nazwa podklucza zaczyna się od "Repositry" i kończy się liczbą
                    if (subKeyName.StartsWith("Repositry") && int.TryParse(subKeyName.Substring(9), out _))
                    {
                        // Otwórz podklucz
                        using (RegistryKey subKey = key.OpenSubKey(subKeyName, writable: true))
                        {
                            if (subKey != null)
                            {
                                string pathValue = subKey.GetValue("path") as string;
                                if (pathValue != null && pathValue.Equals(repository_path, StringComparison.OrdinalIgnoreCase))
                                {
                                    return subKey.GetValue("lista").ToString();
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
