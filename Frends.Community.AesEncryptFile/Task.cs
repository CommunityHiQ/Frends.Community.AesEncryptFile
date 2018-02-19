using System;
using System.IO;
using System.Security.Cryptography;

using Frends.Tasks.Attributes;

namespace Frends.Community.AesEncryptFile
{
    public class Task
    {
        /// <summary>
        /// Encrypt a file using given options. Result file is prefixed with IV bytes.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Output Encrypt(
            [CustomDisplay(DisplayOption.Tab)] Input input,
            [CustomDisplay(DisplayOption.Tab)] Options options)
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
            if(string.IsNullOrWhiteSpace(options.Password))
            {
                throw new ArgumentException("Options.Password was not properly defined", "Options.SourcePath");
            }

            // if destination file is not given, create a new file to the source file location with a new guid as its name
            // otherwise use Options.DestinationFile
            string destinationFile = string.IsNullOrWhiteSpace(options.DestinationFile)
                ? Path.Combine(sourceFileInfo.DirectoryName, Guid.NewGuid().ToString())
                : options.DestinationFile;

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
                switch(options.KeySize)
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
                switch(options.PaddingMode)
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

                // derive Key and IV bytes
                var pbkdf2 = new Rfc2898DeriveBytes(options.Password, 16);
                aes.Key = pbkdf2.GetBytes(aes.KeySize / 8);
                aes.IV = pbkdf2.Salt;
                
                using (var fileWriter = new FileStream(destinationFile, FileMode.Create))
                using (var cs = new CryptoStream(fileWriter, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // write IV first
                    fileWriter.Write(aes.IV, 0, aes.IV.Length);

                    // read & write 1mb at a time
                    var buffer = new byte[1048576];
                    int data;

                    // read source file contents and crypt it to destination file
                    while ((data = fileReader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, data);
                    }
                }
            }

            result.OutputPath = destinationFile;

            return result;
        }
    }
}
