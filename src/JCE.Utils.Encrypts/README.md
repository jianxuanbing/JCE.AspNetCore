# JCE.Utils.Encrypts 

基于`.Net Core`实现加密以及解密工具，支持`AES`、`DES`、`RSA`、`MD5`、`SHA1`、`SHA256`、`SHA384`、`SHA512`、`HMACSHA1`、`HMACSHA256`、`HMACSHA384`、`HMACSHA512`、`HMACMD5`等常用`Hash`操作。

# 使用说明

## AES 操作

#### AES 加密

```csharp
var data = "plantext";
var key = "123456";

AESCryptor.Encrypt(data,key);
AESCryptor.Encrypt(data,key,encoding,iv,keySize,blockSize,cipherMode,paddingMode,outType);
```

#### AES 解密

```csharp
var data = "xxxxxx";
var key = "123456";
AESCryptor.Decrypt(data,key);
AESCryptor.Decrtpt(data,key,encoding,iv,keySize,blockSize,cipherMode,paddingMode,outType)
```

## DES 操作

#### DES 加密

```csharp
var data = "plantext";
var key = "123456";

DESCryptor.Encrypt(data,key);
DESCryptor.Encrypt(data,key,encoding,iv,keySize,blockSize,cipherMode,paddingMode,outType);
```

#### DES解密

```csharp
var data = "xxxxxx";
var key = "123456";

DESCryptor.Decrypt(data,key);
DESCryptor.Decrypt(data,key,encoding,iv,keySize,blockSize,cipherMode,paddingMode,outType);
```

## RSA 操作

#### RSA 创建密钥对

```csharp
string xmlPrivateKey;
string xmlPublicKey;
RSACryptor.CreateKey(out xmlPrivateKey,out xmlPublicKey,RsaSize.R1024);
RSACryptor.CreateKey(out xmlPrivateKey,out xmlPublicKey,1024);
```

#### RSA 签名

```csharp
string data = "plaintext";
string key = "123456"
RSACryptor.Sign(data,key,RsaType.Rsa);
RSACryptor.Sign(data,key,Encoding.UTF8);
RSACryptor.Sign(data,key,Encoding.UTF8,RsaType.Rsa);
RSACryptor.Sign(data,key,Encoding.UTF8,RsaType.Rsa,RSASignaturePadding.Pkcs1);
```

#### RSA 验签

```csharp
string data = "plaintext";
string sign = "xxxx";
string key = "123456"
RSACryptor.Verify(data,sign,key,RsaType.Rsa);
RSACryptor.Verify(data,sign,key,Encoding.UTF8);
RSACryptor.Verify(data,sign,key,Encoding.UTF8,RsaType.Rsa);
RSACryptor.Verify(data,sign,key,Encoding.UTF8,RsaType.Rsa,RSASignaturePadding.Pkcs1);
```

#### RSA 加密

```csharp
string data = "plaintext";
string key = "123456"
RSACryptor.Encrypt(data,key);
RSACryptor.Encrypt(data,key,Encoding.UTF8);
RSACryptor.Encrypt(data,key,Encoding.UTF8,RSASignaturePadding.Pkcs1);
```

#### RSA 解密

```csharp
string data = "plaintext";
string key = "123456"
RSACryptor.Decrypt(data,key);
RSACryptor.Decrypt(data,key,Encoding.UTF8);
RSACryptor.Decrypt(data,key,Encoding.UTF8,RSASignaturePadding.Pkcs1);
```

#### RSA 分块加密

```csharp
string data = "plaintext";
string key = "123456"
RSACryptor.EncryptBlock(data,key);
RSACryptor.EncryptBlock(data,key,Encoding.UTF8);
RSACryptor.EncryptBlock(data,key,Encoding.UTF8,RSASignaturePadding.Pkcs1);
```

#### RSA 分块解密

```csharp
string data = "plaintext";
string key = "123456"
RSACryptor.DecryptBlock(data,key);
RSACryptor.DecryptBlock(data,key,Encoding.UTF8);
RSACryptor.DecryptBlock(data,key,Encoding.UTF8,RSASignaturePadding.Pkcs1);
```

## Hash 操作

#### MD5

```csharp
string text = "plaintext";

MD5Cryptor.Encrypt16(text);
MD5Cryptor.Encrypt16(text,Encoding.UTF8);

MD5Cryptor.Encrypt32(text);
MD5Cryptor.Encrypt32(text,Encoding.UTF8);

MD5Cryptor.Encrypt64(text);
MD5Cryptor.Encrypt64(text,Encoding.UTF8);

MD5Cryptor.Encrypt(text,Encoding.UTF8,startIndex,length);
```

#### SHA

```csharp
string text = "plaintext";

SHACryptor.Sha1(text,Encoding.UTF8);
SHACryptor.Sha256(text,Encoding.UTF8);
SHACryptor.Sha386(text,Encoding.UTF8);
SHACryptor.Sha512(text,Encoding.UTF8);
```

#### HMAC

```csharp
string text = "plaintext";
string key = "123456";

HmacCryptor.HmacSha1(text,key,Encoding.UTF8);
HmacCryptor.HmacSha256(text,key,Encoding.UTF8);
HmacCryptor.HmacSha384(text,key,Encoding.UTF8);
HmacCryptor.HmacSha512(text,key,Encoding.UTF8);
HmacCryptor.HmacMd5(text,key,Encoding.UTF8);
```

## Base64 操作

#### Base64 加密

```csharp
string text = "plaintext";

Base64Cryptor.Encrypt(text);
Base64Cryptor.Encrypt(text,Encoding.UTF8);
```

#### Base64 解密

```csharp
string text = "plaintext";

Base64Cryptor.Decrypt(text);
Base64Cryptor.Decrypt(text,Encoding.UTF8);
```