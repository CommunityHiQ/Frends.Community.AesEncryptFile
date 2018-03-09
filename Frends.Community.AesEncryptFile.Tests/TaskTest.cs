using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frends.Community.AesEncryptFile;
using System.IO;

namespace Frends.Community.AesEncryptFile.Tests
{
    [TestClass]
    public class TaskTest
    {
        [TestMethod]
        public void Encrypt_ShouldThrowArgumentExceptionWithNonexistingSourceFile()
        {
            try
            {
                Task.Encrypt(
                    new Input { SourceFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\TestFiles\SourceFileThatDoesNotExist.txt" },
                    new Options { CipherMode = Cipher.CBC, KeySize = KeySize.AES256, Password = "password" });
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                StringAssert.Equals("Source file does not exist!", e.Message);
            }
        }

        [TestMethod]
        public void Encrypt_ShouldEncryptFile_AES256CBC()
        {
            Output result = Task.Encrypt(
                new Input { SourceFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\TestFiles\SourceFile.txt" },
                new Options { CipherMode = Cipher.CBC, KeySize = KeySize.AES256, Password = "password" });

            FileInfo fi = new FileInfo(result.OutputPath);

            Assert.IsTrue(fi.Exists);
        }

        [TestMethod]
        public void Encrypt_ShouldEncryptFile_AES256ECB()
        {
            Output result = Task.Encrypt(
                new Input { SourceFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\TestFiles\SourceFile.txt" },
                new Options { CipherMode = Cipher.ECB, KeySize = KeySize.AES256, Password = "password" });

            FileInfo fi = new FileInfo(result.OutputPath);

            Assert.IsTrue(fi.Exists);
        }


        [TestMethod]
        public void Encrypt_ShouldEncryptFile_OpenSSLTest()
        {
            Output result = Task.Encrypt(
                new Input { SourceFile = $@"C:\temp\VM-jako\TEST.txt" },
                new Options { CipherMode = Cipher.CBC, KeySize = KeySize.AES256, Password = "54321", PaddingMode = Padding.PKCS7, ByteArrayLength=ByteArrayLength.Eight, DecryptionMethod=DecryptionMethod.OpenSSL });

            FileInfo fi = new FileInfo(result.OutputPath);

            Assert.IsTrue(fi.Exists);


            
        }



    }
}
