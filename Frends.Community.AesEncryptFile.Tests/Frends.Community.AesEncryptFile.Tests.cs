using NUnit.Framework;
using System;
using System.IO;

namespace Frends.Community.AesEncryptFile.Tests
{
    [TestFixture]
    class TestClass
    {

        [Test]
        public void Encrypt_ShouldThrowArgumentExceptionWithNonexistingSourceFile()
        {
            try
            {
                AesEncryptTask.AesEncryptFile(
                    new Input { SourceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestFiles\SourceFileThatDoesNotExist.txt"), Password = "54321" },
                    new Options { CipherMode = Cipher.CBC, KeySize = KeySize.AES256, PaddingMode = Padding.PKCS7, ByteArrayLength = ByteArrayLength.Sixteen, DecryptionMethod = DecryptionMethod.Other });
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Source file does not exist!\r\nParameter name: Input.SourcePath", e.Message);
            }
        }

        [Test]
        public void Encrypt_ShouldEncryptFile_AES256ECB()
        {
            Output result = AesEncryptTask.AesEncryptFile(
                new Input { SourceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestFiles\SourceFile.txt"), DestinationFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestFiles\Encrypted_AES256ECB.ed"), Password = "54321" },
                new Options { CipherMode = Cipher.ECB, KeySize = KeySize.AES256, PaddingMode = Padding.PKCS7, ByteArrayLength = ByteArrayLength.ThirtyTwo, DecryptionMethod = DecryptionMethod.Other });

            FileInfo fi = new FileInfo(result.OutputPath);
            Assert.IsTrue(fi.Exists);
        }

        [Test]
        public void Encrypt_ShouldEncryptFile_AES256CBC()
        {
            Output result = AesEncryptTask.AesEncryptFile(
                new Input { SourceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestFiles\SourceFile.txt"),  DestinationFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestFiles\Encrypted_AES256CBC.ed"), Password = "54321" },
                new Options { CipherMode = Cipher.CBC, KeySize = KeySize.AES256, PaddingMode = Padding.PKCS7, ByteArrayLength = ByteArrayLength.SixtyFour, DecryptionMethod = DecryptionMethod.Other });

            FileInfo fi = new FileInfo(result.OutputPath);
            Assert.IsTrue(fi.Exists);
        }
        [Test]
        public void Encrypt_ShouldEncryptFile_OpenSSLTest()
        {
            Output result = AesEncryptTask.AesEncryptFile(
                new Input { SourceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestFiles\SourceFile.txt"), DestinationFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestFiles\Encrypted_OpenSSL.ed"), Password = "54321" },
                new Options { CipherMode = Cipher.CBC, KeySize = KeySize.AES256, PaddingMode = Padding.PKCS7, ByteArrayLength = ByteArrayLength.Eight, DecryptionMethod = DecryptionMethod.OpenSSL });

            FileInfo fi = new FileInfo(result.OutputPath);
            Assert.IsTrue(fi.Exists);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            // AppDomain.CurrentDomain.BaseDirectory refers to Frends.Community.AesEncrypt.Tests\bin\Debug(or Release)\ -folder
            System.IO.DirectoryInfo di2 = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestFiles"));

            foreach (FileInfo filee in di2.GetFiles())
            {
                if (System.IO.Path.GetFileName(filee.FullName) != "SourceFile.txt")
                {
                    filee.Delete();
                }

            }
            var di1 = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..", "TestResults"));

            if (di1.Exists)
                Directory.Delete(di1.FullName, true);
        }
    }
}

