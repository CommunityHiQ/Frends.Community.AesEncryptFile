﻿using System;
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
                    new Options { CipherMode = Cipher.CBC, KeySize = KeySize.AES256, Password = "54321", PaddingMode = Padding.PKCS7, ByteArrayLength = ByteArrayLength.Sixteen, DecryptionMethod = DecryptionMethod.Other });
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                StringAssert.Equals("Source file does not exist!", e.Message);
            }
        }

       

        [TestMethod]
        public void Encrypt_ShouldEncryptFile_AES256ECB()
        {
            Output result = Task.Encrypt(
                new Input { SourceFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\TestFiles\SourceFile.txt" },
                new Options { DestinationFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\TestFiles\Encrypted_AES256ECB.ed", CipherMode = Cipher.ECB, KeySize = KeySize.AES256, Password = "54321", PaddingMode = Padding.PKCS7, ByteArrayLength = ByteArrayLength.ThirtyTwo, DecryptionMethod = DecryptionMethod.Other });

            FileInfo fi = new FileInfo(result.OutputPath);

            Assert.IsTrue(fi.Exists);
        }

        [TestMethod]
        public void Encrypt_ShouldEncryptFile_AES256CBC()
        {
            Output result = Task.Encrypt(
                new Input { SourceFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\TestFiles\SourceFile.txt" },
                new Options { DestinationFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\TestFiles\Encrypted_AES256CBC.ed", CipherMode = Cipher.CBC, KeySize = KeySize.AES256, Password = "54321", PaddingMode = Padding.PKCS7, ByteArrayLength = ByteArrayLength.SixtyFour, DecryptionMethod = DecryptionMethod.Other });

            FileInfo fi = new FileInfo(result.OutputPath);

            
            Assert.IsTrue(fi.Exists);
        
    }


        [TestMethod]
        public void Encrypt_ShouldEncryptFile_OpenSSLTest()
        {
            
            Output result = Task.Encrypt(
                new Input { SourceFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\TestFiles\SourceFile.txt" },
                new Options { DestinationFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\TestFiles\Encrypted_OpenSSL.ed", CipherMode = Cipher.CBC, KeySize = KeySize.AES256, Password = "54321", PaddingMode = Padding.PKCS7, ByteArrayLength=ByteArrayLength.Eight, DecryptionMethod=DecryptionMethod.OpenSSL });

            FileInfo fi = new FileInfo(result.OutputPath);

            Assert.IsTrue(fi.Exists);


        }

        [TestCleanup]
        public void Cleanup()
        {

            // AppDomain.CurrentDomain.BaseDirectory refers to Frends.Community.AesEncrypt.Tests\bin\Debug(or Release)\ -folder
            System.IO.DirectoryInfo di2 = new DirectoryInfo($@"{AppDomain.CurrentDomain.BaseDirectory}\TestFiles");

            foreach (FileInfo filee in di2.GetFiles())
            {
                if (System.IO.Path.GetFileName(filee.FullName) != "SourceFile.txt")
                {
                    filee.Delete();
                }

            }



        }



    }
}
