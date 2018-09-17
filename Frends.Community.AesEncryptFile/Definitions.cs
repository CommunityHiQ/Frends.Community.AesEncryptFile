using System.ComponentModel;
using Frends.Tasks.Attributes;

#pragma warning disable 1591

namespace Frends.Community.AesEncryptFile
{
    #region Enumerations
    /// <summary>
    /// AES key size
    /// </summary>
    public enum KeySize { AES128, AES192, AES256 }

    /// <summary>
    /// Padding mode to use when encrypting
    /// </summary>
    public enum Padding { ANSIX923, ISO10126, PKCS7, Zeros, None }

    /// <summary>
    /// Cipher to use for encryption
    /// </summary>
    public enum Cipher { CBC, ECB }

    /// <summary>
    /// Byte array length used for salt
    /// </summary>
    public enum ByteArrayLength { Eight=8, Sixteen=16, ThirtyTwo=32, SixtyFour=64 }

    /// <summary>
    /// Decryption target
    /// </summary>
    public enum DecryptionMethod { OpenSSL, Other }
    #endregion

    #region Classes
    public class Input
    {
        /// <summary>
        /// File to encrypt
        /// </summary>
        [DefaultValue("c:\\temp\\file_to_encrypt.txt")]
        [DefaultDisplayType(DisplayType.Text)]
        public string SourceFile { get; set; }

        /// <summary>
        /// Use random GUID if not set. If file allready exsits it will be overwriten.
        /// </summary>
        public string DestinationFile { get; set; }

        /// <summary>
        /// Password of the encrypted file
        /// </summary>
        [PasswordPropertyText]
        public string Password { get; set; }

    }

    public class Options
    {
        /// <summary>
        /// CipherMode to use when encrypting a file, when unsure, use CBC.
        /// </summary>
        [DefaultValue(Cipher.CBC)]
        public Cipher CipherMode { get; set; }

        /// <summary>
        /// Padding mode to use when encrypting a file, when unsure, use PKCS7.
        /// </summary>
        [DefaultValue(Padding.PKCS7)]
        public Padding PaddingMode { get; set; }

        /// <summary>
        /// Key size for file encryption, when unsure, use 256.
        /// </summary>
        [DefaultValue(KeySize.AES256)]
        public KeySize KeySize { get; set; }

        /// <summary>
        /// Array length used in defining the salt byte array of encryption. When Decrypting by OpenSSL, use 8.
        /// </summary>
        [DefaultValue(ByteArrayLength.Eight)]
        public ByteArrayLength ByteArrayLength { get; set; }

        /// <summary>
        /// Defines how the encrypted file is planned to be decrypted, when unsure, use Other.  "Salted__" is added to 
        /// filename if OpenSSL is chosen as a decryption method.
        /// </summary>
        [DefaultValue(DecryptionMethod.Other)]
        public DecryptionMethod DecryptionMethod { get; set; }
    }

    public class Output
    {
        /// <summary>
        /// Path to encrypted file
        /// </summary>
        public string OutputPath { get; set; }
    }
    #endregion
}
