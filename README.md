# Frends.Community.AesEncryptFile
Frends community task for encrypting a file with AES
- [Installing](#installing)
- [Tasks](#tasks)
     - [AesEncryptFile](#AesEncryptFile)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)


# Installing

You can install the task via FRENDS UI Task View or you can find the nuget package from the following nuget feed
'Insert nuget feed here'

# Tasks

## AesEncryptFile

### Input

| Property | Type | Description | Example |
| ---------|------|-------------|---------|
| SourceFile | String | File to encrypt | `c:\temp\foobar.txt` |
| DestinationFile | String | Destination file path. It left undefined, then the task will write the encrypted file to the same location as the source file using a new guid as file name | `c:\temp\encrypt.ed` |
| Password | String | Password to use as encryption key | `password123` |

### Output

| Property | Type | Description | Example |
| ---------|------|-------------|---------|
| OutputPath | String | Path to the encrypted file | `c:\temp\encrypt.ed` |

# Task Properties

## Options

| Property | Type | Description | Example |
| ---------|------|-------------|---------|
| CipherMode | Cipher | CipherMode to use when encrypting a file, when unsure, use `CBC` | `CBC` |
| PaddingMode| Padding | Padding mode to use when encrypting a file, when unsure, use `PKCS7` | `PKCS7` |
| KeySize | KeySize | Key size for file encryption, when unsure, use `256` | `AES256` |
| ByteArrayLength | Int | Array length used in defining the salt byte array of encryption. When Decrypting by OpenSSL, use `8` | `8` |
| DecryptionMethod | String | Defines how the encrypted file is planned to be decrypted, when unsure, use `Other` | `Other` |

# Building

Clone a copy of the repo

`git clone https://github.com/CommunityHiQ/Frends.Community.AesEncryptFile`

Restore dependencies

`nuget restore FreFrends.Community.AesEncryptFile`

Rebuild the project

Run Tests with nunit3. Tests can be found under

`Frends.Community.AesEncryptFile\bin\Release\Frends.Community.AesEncryptFile.Tests.dll`

Create a nuget package

`nuget pack Frends.Community.AesEncryptFile.nuspec`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version | Changes |
| ----- | ----- |
| 1.0.0 | First version. |
| 2.0.0 | Task renamed and refactored, it is not backward compatible. Updated documentation. |
