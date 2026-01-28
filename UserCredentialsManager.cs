namespace IntarRepo
{
    using Microsoft.Win32;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class UserCredentialsManager
    {
        private static readonly string RegistryKeyPath = @"SOFTWARE\INTAR\INTARRepo";
        private static readonly string UserNameValueName = "UserName";
        private static readonly string PasswordValueName = "Password";

        // Metoda do zapisania danych do rejestru
        public void SaveCredentials(string database,string username, string password)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey($"{RegistryKeyPath}\\{database}"))
            {
                if (key != null)
                {
                    // Zaszyfrowanie danych
                    string encryptedUserName = Encrypt(username);
                    string encryptedPassword = Encrypt(password);

                    // Zapisanie zaszyfrowanych danych do rejestru
                    key.SetValue(UserNameValueName, encryptedUserName);
                    key.SetValue(PasswordValueName, encryptedPassword);
                }
            }
        }

        // Metoda do odczytania danych z rejestru
        public (string UserName, string Password) LoadCredentials(string database)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey($"{RegistryKeyPath}\\{database}"))
            {
                if (key != null)
                {
                    // Odczytanie zaszyfrowanych danych z rejestru
                    string encryptedUserName = key.GetValue(UserNameValueName)?.ToString();
                    string encryptedPassword = key.GetValue(PasswordValueName)?.ToString();

                    // Deszyfrowanie danych
                    string decryptedUserName = Decrypt(encryptedUserName);
                    string decryptedPassword = Decrypt(encryptedPassword);

                    return (decryptedUserName, decryptedPassword);
                }
                else
                {
                    return (null,null);
                }
            }
        }

        // Prosta metoda do szyfrowania danych (AES)
        private string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = GenerateKey();
                aes.IV = new byte[16]; // Inicjalizujemy zerową wartością

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        // Prosta metoda do deszyfrowania danych (AES)
        private string Decrypt(string encryptedText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = GenerateKey();
                aes.IV = new byte[16]; // Inicjalizujemy zerową wartością

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        // Generowanie klucza do szyfrowania (stały klucz dla przykładu, powinien być lepiej zabezpieczony)
        private byte[] GenerateKey()
        {
            // Przykładowy klucz, w rzeczywistości powinien być generowany dynamicznie i przechowywany w bezpiecznym miejscu
            string key = "MySecretKey12345";
            return Encoding.UTF8.GetBytes(key.PadRight(32)); // AES wymaga 256-bitowego klucza
        }
    }

    //class UserCredentialsEntry

}
