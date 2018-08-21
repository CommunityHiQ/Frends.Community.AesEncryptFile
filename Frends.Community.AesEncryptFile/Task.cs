using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;

#pragma warning disable 1591

namespace Frends.Community.AesEncryptFile
{
    public class AesEncryptTask
    {
        /// <summary>
        /// Encrypt a file with AES. Result file is prefixed with random generated salt bytes. Random GUID is used as filename if nothing is specified. "Salted__" is added to 
        /// filename if OpenSSL is chosen as a decryption method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Output AesEncryptFile(
            [PropertyTab] Input input,
            [PropertyTab] Options options)
        {
            var result = new Output();
            FileInfo sourceFileInfo = new FileInfo(input.SourceFile);

            // check for obvious input errors
            if (string.IsNullOrWhiteSpace(input.SourceFile))
            {
                throw new ArgumentException("Input.SourcePath has to be defined", "Input.SourcePath");
            }
            if (!sourceFileInfo.Exists)
            {
                throw new ArgumentException("Source file does not exist!", "Input.SourcePath");
            }
            if(string.IsNullOrWhiteSpace(input.Password))
            {
                throw new ArgumentException("Options.Password was not properly defined", "Options.SourcePath");
            }

            // if destination file is not given, create a new file to the source file location with a new guid as its name
            // otherwise use Options.DestinationFile
            string destinationFile = string.IsNullOrWhiteSpace(input.DestinationFile)
                ? Path.Combine(sourceFileInfo.DirectoryName, Guid.NewGuid().ToString())
                : input.DestinationFile;


            using (var fileReader = new FileStream(input.SourceFile, FileMode.Open, FileAccess.Read))
            using (var aes = new AesManaged())
            {
                // aes block size is always 128
                aes.BlockSize = 128;

                // set cipher mode
                switch (options.CipherMode)
                {
                    case Cipher.CBC:
                        aes.Mode = CipherMode.CBC;
                        break;
                    case Cipher.ECB:
                        aes.Mode = CipherMode.ECB;
                        break;
                }

                // set key size
                switch (options.KeySize)
                {
                    case KeySize.AES128:
                        aes.KeySize = 128;
                        break;
                    case KeySize.AES192:
                        aes.KeySize = 192;
                        break;
                    case KeySize.AES256:
                        aes.KeySize = 256;
                        break;
                }

                // set padding mode
                switch (options.PaddingMode)
                {
                    case Padding.ANSIX923:
                        aes.Padding = PaddingMode.ANSIX923;
                        break;
                    case Padding.ISO10126:
                        aes.Padding = PaddingMode.ISO10126;
                        break;
                    case Padding.PKCS7:
                        aes.Padding = PaddingMode.PKCS7;
                        break;
                    case Padding.Zeros:
                        aes.Padding = PaddingMode.Zeros;
                        break;
                    case Padding.None:
                        aes.Padding = PaddingMode.None;
                        break;
                }

               // set byte array length
               int byteArrayLength = 0;
               switch (options.ByteArrayLength)
               {
                   case ByteArrayLength.Eight:
                       byteArrayLength = 8;
                       break;
                   case ByteArrayLength.Sixteen:
                       byteArrayLength = 16;
                       break;
                   case ByteArrayLength.ThirtyTwo:
                       byteArrayLength = 32;
                       break;
                   case ByteArrayLength.SixtyFour:
                       byteArrayLength = 64;
                       break;
                }

                using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
                {
                    byte[] saltBytes = new byte[byteArrayLength];
                    rng.GetBytes(saltBytes);

                    // derive Key and IV bytes
                    Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input.Password, saltBytes, 10000);
                    aes.Key = pbkdf2.GetBytes(aes.KeySize / 8);
                    aes.IV = pbkdf2.GetBytes(aes.BlockSize / 8);

                    byte[] salted = Encoding.ASCII.GetBytes("Salted__");

                    using (var fileWriter = new FileStream(destinationFile, FileMode.Create))
                    using (var cs = new CryptoStream(fileWriter, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        if (options.ByteArrayLength == ByteArrayLength.Eight && options.DecryptionMethod == DecryptionMethod.OpenSSL)
                        {
                            // Write "Salted__" first if decryption is done by OpenSSL and eight bytes are selected
                            fileWriter.Write(salted, 0, salted.Length);
                        }

                        // Then saltBytes
                        fileWriter.Write(saltBytes, 0, saltBytes.Length);

                        // read & write 1mb at a time
                        var buffer = new byte[1048576];
                        int data;

                        // read source file contents and crypt it to destination file
                        while ((data = fileReader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            cs.Write(buffer, 0, data);
                        }
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                }
            }

            result.OutputPath = destinationFile;

            return result;
        }
    }
}
