using System.Collections.Generic;
using System.IO;

namespace IntarRepo
{
    public class FileListComparer
    {
        public string DirectoryPath { get; private set; }
        public List<RepositoryEntry> RepositoryEntryList { get; private set; }

        public FileListComparer(string directoryPath, List<RepositoryEntry> worklist)
        {
            DirectoryPath = directoryPath;
            RepositoryEntryList = worklist;
        }

        public List<string> GetMissingFiles()
        {
            // Zwróci listę plików, które są w katalogu, ale nie ma ich na liście.
            List<string> missingFiles = new List<string>();

            // Pobieramy wszystkie pliki w katalogu
            string[] filesInDirectory = System.IO.Directory.GetFiles(DirectoryPath);

            // Normalizujemy ścieżki plików w katalogu
            HashSet<string> normalizedFilePathsInDirectory = new HashSet<string>();
            foreach (string file in filesInDirectory)
            {
                normalizedFilePathsInDirectory.Add(Path.GetFullPath(file).ToLower());
            }

            // Sprawdzamy, które pliki z listy są faktycznie obecne w katalogu
            foreach (RepositoryEntry entry in RepositoryEntryList)
            {
                string fullPath = Path.GetFullPath(entry.FileName).ToLower();
                if (!normalizedFilePathsInDirectory.Contains(fullPath))
                {
                    missingFiles.Add(fullPath);
                }
            }

            return missingFiles;
        }

        public List<string> GetExtraFiles()
        {
            // Zwróci listę plików, które są na liście, ale nie ma ich w katalogu.
            List<string> extraFiles = new List<string>();

            // Pobieramy wszystkie pliki w katalogu
            string[] filesInDirectory = Directory.GetFiles(DirectoryPath,"*.sql");

            // Normalizujemy ścieżki plików z listy
            HashSet<string> normalizedFilePathsInList = new HashSet<string>();
            foreach (RepositoryEntry entry in RepositoryEntryList)
            {
                normalizedFilePathsInList.Add(entry.FileName);
            }

            // Sprawdzamy, które pliki w katalogu nie są obecne na liście
            foreach (string filePath in filesInDirectory)
            {
                string fullPath = Path.GetFileName(filePath);
                if (!normalizedFilePathsInList.Contains(fullPath))
                {
                    extraFiles.Add(fullPath);
                }
            }

            return extraFiles;
        }
    }

}
