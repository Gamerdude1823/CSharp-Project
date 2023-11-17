using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class FileEncryptor
{
    static void Main()
    {
        try
        {
            string inputFile = "input.txt";
            string encryptedFile = "encrypted.bin";
            string decryptedFile = "decrypted.txt";

            string key = "ThisIsASecretKey"; // Replace this with a secure key management mechanism

            // Encrypt the file
            EncryptFile(inputFile, encryptedFile, key);

            // Decrypt the file
            DecryptFile(encryptedFile, decryptedFile, key);

            Console.WriteLine("File encryption and decryption completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void EncryptFile(string inputFile, string outputFile, string key)
    {
        try
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                        {
                            using (CryptoStream cs = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                            {
                                fsInput.CopyTo(cs);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error encrypting file: {ex.Message}");
        }
    }

    static void DecryptFile(string inputFile, string outputFile, string key)
    {
        try
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                        {
                            using (CryptoStream cs = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
                            {
                                fsInput.CopyTo(cs);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error decrypting file: {ex.Message}");
        }
    }
}