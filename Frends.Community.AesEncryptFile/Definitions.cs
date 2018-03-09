using System.ComponentModel;
using Frends.Tasks.Attributes;

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
    }

    public class Options
    {
        /// <summary>
        /// If set, will write the encrypted file to this path and overwrite possible existing file
        /// </summary>
        public string DestinationFile { get; set; }

        /// <summary>
        /// Password of the encrypted file
        /// </summary>
        [PasswordPropertyText]
        public string Password { get; set; }

        /// <summary>
        /// Cipher mode to use
        /// </summary>
        [DefaultValue(Cipher.CBC)]
        public Cipher CipherMode { get; set; }

        /// <summary>
        /// Padding mode to use
        /// </summary>
        [DefaultValue(Padding.PKCS7)]
        public Padding PaddingMode { get; set; }

        /// <summary>
        /// Possible key sizes: 128, 256, 512
        /// </summary>
        [DefaultValue(KeySize.AES256)]
        public KeySize KeySize { get; set; }

        /// <summary>
        /// Possible options: 8, 16, 32, 64
        /// </summary>
        [DefaultValue(ByteArrayLength.Eight)]
        public ByteArrayLength ByteArrayLength { get; set; }

        /// <summary>
        /// Possible options: OpenSSL, Other
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
