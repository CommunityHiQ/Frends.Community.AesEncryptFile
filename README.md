# Frends.Community.AesEncryptFile

frends Community Task for AesEncryptTask

[![Actions Status](https://github.com/CommunityHiQ/Frends.Community.AesEncryptFile/workflows/PackAndPushAfterMerge/badge.svg)](https://github.com/CommunityHiQ/Frends.Community.AesEncryptFile/actions) ![MyGet](https://img.shields.io/myget/frends-community/v/Frends.Community.AesEncryptFile) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 

- [Installing](#installing)
- [Tasks](#tasks)
     - [AesEncryptTask](#AesEncryptTask)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

You can install the Task via frends UI Task View or you can find the NuGet package from the following NuGet feed
https://www.myget.org/F/frends-community/api/v3/index.json and in Gallery view in MyGet https://www.myget.org/feed/frends-community/package/nuget/Frends.Community.AesEncryptFile

# Tasks

## AesEncryptTask

Repeats a message

### Input

| Property | Type | Description | Example |
| ---------|------|-------------|---------|
| SourceFile | String | File to encrypt | `c:\temp\foobar.txt` |
| DestinationFile | String | Destination file path. It left undefined, then the task will write the encrypted file to the same location as the source file using a new guid as file name | `c:\temp\encrypt.ed` |
| Password | String | Password to use as encryption key | `password123` |

### Returns

| Property | Type | Description | Example |
| ---------|------|-------------|---------|
| OutputPath | String | Path to the encrypted file | `c:\temp\encrypt.ed` |

## Options

| Property | Type | Description | Example |
| ---------|------|-------------|---------|
| CipherMode | Cipher | CipherMode to use when encrypting a file, when unsure, use `CBC` | `CBC` |
| PaddingMode| Padding | Padding mode to use when encrypting a file, when unsure, use `PKCS7` | `PKCS7` |
| KeySize | KeySize | Key size for file encryption, when unsure, use `256` | `AES256` |
| ByteArrayLength | Int | Array length used in defining the salt byte array of encryption. When Decrypting by OpenSSL, use `8` | `8` |
| DecryptionMethod | String | Defines how the encrypted file is planned to be decrypted, when unsure, use `Other` | `Other` |

Usage:
To fetch result use syntax:

`#result.Replication`

# Building

Clone a copy of the repository

`git clone https://github.com/CommunityHiQ/Frends.Community.AesEncryptFile.git`

Rebuild the project

`dotnet build`

Run tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repository on GitHub
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
| 2.0.1 | Converted to support .Net Framework 4.7.1 and .Net Standard 2.0