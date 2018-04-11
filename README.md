# Frends.Community.AesEncryptFile
Frends community task for encrypting a file with AES


# Installing

You can install the task via FRENDS UI Task View or you can find the nuget package from the following nuget feed
'Insert nuget feed here'

# Tasks

## Encrypt

### Input

| Property | Type | Description | Example |
| ---------|------|-------------|---------|
| SourceFile | String | File to encrypt | c:\temp\foobar.txt |

### Output

| Property | Type | Description | Example |
| ---------|------|-------------|---------|
| OutputPath | String | Path to the encrypted file | c:\temp\encrypt.ed |

# Task Properties

## Options

| Property | Type | Description | Example |
| ---------|------|-------------|---------|
| DestinationFile | String | Destination file path. It left undefined, then the task will write the encrypted file to the same location as the source file using a new guid as file name | c:\temp\encrypt.ed |
| Password | String | Password to use as encryption key | password123 |
| CipherMode | Cipher | CipherMode to use when encrypting a file, when unsure, use CBC | CBC |
| PaddingMode| Padding | Padding mode to use when encrypting a file, when unsure, use PKCS7 | PKCS7 |
| KeySize | KeySize | Key size for file encryption, when unsure, use 256 | AES256 |
| ByteArrayLength | Int | Array length used in defining the salt byte array of encryption. When Decrypting by OpenSSL, use 8 | 8 |
| DecryptionMethod | String | Defines how the encrypted file is planned to be decrypted, when unsure, use Other | Other |
