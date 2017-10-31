using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Encrypts.Asymmetric;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Encrypts.Test.Asymmetric
{
    public class RSACryptorTest:TestBase
    {
        public RSACryptorTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test_CreateKey()
        {
            string privateKey;
            string publicKey;
            int size = 1024;
            RSACryptor.CreateKey(out privateKey,out publicKey,size);
            Output.WriteLine("密钥长度:" + size);
            Output.WriteLine("私钥:" + privateKey);
            Output.WriteLine("公钥:" + publicKey);
            Output.WriteLine("java版私钥:"+privateKey.RsaPrivateKeyDotNet2Java());
            Output.WriteLine("java版公钥:" + publicKey.RsaPublicKeyDotNet2Java());
        }

        [Fact]
        public void Test_CreateKeyToBase64String()
        {
            string privateKey;
            string publicKey;
            RSACryptor.CreateKey(out privateKey, out publicKey, RsaSize.R1024);
            Output.WriteLine("私钥:" + privateKey);
            Output.WriteLine("公钥:" + publicKey);
            string privateKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(privateKey));
            string publicKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(publicKey));
            Output.WriteLine("Base64私钥:" + privateKeyBase64);
            Output.WriteLine("Base64公钥:" + publicKeyBase64);
        }

        [Fact]
        public void Test_Java2DotNetKey()
        {
            var privateKey =
                "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAJcxsxE1UTAHO8pKZ9BPPrHo0CzWRRrMG5GqjKeFv3ecldyWhERHRUoeZuY9y4+DWYDJxzY150SA7fDonsoqsZtRaq+lZOH3lvdD+fIRt3rrH3mhaQjc4RpkBM5QaO95gjbUvqQghhHarj++udlLDOdALzSJLBuHC7N31JKOquB/AgMBAAECgYB0sfhq5MMIFd0xEmK0JiXWvUHICY2G1FjHAmLTfei9Ek+c1VO7O2MghPyY4sM1voSbYaHvloUsm3KLSZAdq/wuEpA9BC8MknU0CQe/ga3MB5+tRI3CeI8zIRWPbTc7UobqDDcOylX/JcY8f2F9rh668/c6e1LIFltXeupOFriXWQJBAP2MLctWBiSoCC7MqzUGck3sGBRNqvftVoyEw5k8tZOPXKetPHO+SV/FZe/gDBok1FIBEEL5mkvPeLpyVJUjjiMCQQCYqBQTqXFZKsXGFGtpDniZ+8BP3ml/YTxbkXglNePSeQa6LtmLkPMuS1yQzuUQ0VrUDfOYRmrv4UoUDeVH39P1AkEAlW9jJi7DXCN0/zA9z/jGscpuvriwBYPquNMe/VfcpOWf9GuT75u9XybW17QzLc17HgHmdbLrD7duLmVoGKZmBQJAC78rRFQOj9D1xQc/OKdPanHv23V+4rllvpoUB7D85e5AMUV3ogC2ZcKQzefNwpyJg7XLH9WTVwAcBi0Hjp9PnQJAGluCOY9pAUl10zMJwgeS/hgUbV9w01z4b58w6DLNPd+4wHDSbNIgEVQxzopiodVGI+b11geGIr/S93GgEonlIw==";
            var publicKey =
                "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCXMbMRNVEwBzvKSmfQTz6x6NAs1kUazBuRqoynhb93nJXcloRER0VKHmbmPcuPg1mAycc2NedEgO3w6J7KKrGbUWqvpWTh95b3Q/nyEbd66x95oWkI3OEaZATOUGjveYI21L6kIIYR2q4/vrnZSwznQC80iSwbhwuzd9SSjqrgfwIDAQAB";
            var dotNetPrivateKey = privateKey.RsaPrivateKeyJava2DotNet();
            var dotNetPublicKey = publicKey.RsaPublicKeyJava2DotNet();
            Output.WriteLine(".net私钥:"+ dotNetPrivateKey);
            Output.WriteLine(".net公钥:"+ dotNetPublicKey);

            Assert.Equal(privateKey, dotNetPrivateKey.RsaPrivateKeyDotNet2Java());
            Assert.Equal(publicKey, dotNetPublicKey.RsaPublicKeyDotNet2Java());
        }

        [Fact]
        public void Test_Encrypt()
        {
            var privateKey =
                "MIICXgIBAAKBgQC+s9ulHlOyAFOr4qHArvW6IVcq9SZnZEs4EPJvzjNGtIq58iNzOEvcMITNAeGqnlLvw3aFoqXqmFc1/yXXuJJGnmoDTjYo3TcdAL4/EeCDXtPhKLeU+iiNNLyp0KpVK7iQlzyFLqVXeb5ii8eYonADeczz58V7xGKma0ZytG3ozwIDAQABAoGBAIrvuK+k169Qo6UP+W7LSUWxMrOeZbtgC5kuZ6LjZOI3ePaeHgu80S/7vVUq8MGAlcYO7xPPevfew3MYj/aJhy9fA+YnJEbusUkfH/N7M35aEmpZAM+jnRftQhjn0Y2IXQ5gGmhEIQ9McDnCqZATYN3D1zQUQ94TtFKh06KEVK75AkEA7854UKO9A594wegTH0KZK8przssYjy1SLXIKRkd2HLuVZa89paBrGgbfIkBf6SrBVAZ4kOFg7FoRq/zs87xe9QJBAMuUh3DgRs6P+FseXh2GnEKdFm7Hjqk/OpDEmpu9PpTF3C5mgf1kEAEelAMm918e+zA9xxXjKizK9VzBeb4wRjMCQGtjJnXWHTqWG1maN5X0GOuHRifgwyq6vOYk/3zhW38acZiLlSuqXsvU7+9CrLCZuOGL1Ens455z5x4BsYqkfFECQQC5+gAZL+m/fmpl40IbOwO5HwSFQyyilK6N3e1X5PQsuOxCP0b1EtpEC4kvsANAFG9oTKd46UN4FBk+GUl8Y2FdAkEAnfAcLBjBBtaPvPuaEMrLdRzYJ1cfax6gpUjSy4LjElq3j1LyFlHg8uASJ6kZmTjLM0x9XTwYS2/zITOW7Zw4xQ==";
            var publicKey =
                "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC+s9ulHlOyAFOr4qHArvW6IVcq9SZnZEs4EPJvzjNGtIq58iNzOEvcMITNAeGqnlLvw3aFoqXqmFc1/yXXuJJGnmoDTjYo3TcdAL4/EeCDXtPhKLeU+iiNNLyp0KpVK7iQlzyFLqVXeb5ii8eYonADeczz58V7xGKma0ZytG3ozwIDAQAB";
            string source = "只是告诉素数生成器帮我生成一个接近位的素数而已然后生成这是什么鬼额额额为啥呢呢";
            Output.WriteLine("加密字符串长度:"+source.Length);
            var result = RSACryptor.Encrypt(source, publicKey);
            Output.WriteLine(result);
        }

        [Fact]
        public void Test_SignAndVerify()
        {
            var opensslPrivateKey =
                "MIICWwIBAAKBgQDCBtp6c2QRrJo+Z7zGpm9/9nCK83m0CUnDgiwa5eyQ4ltBDbncGztm9U1HY0lI+GkZgSIUNe0is0wP/iYe30ANWE8s73kS3P5MuA24nPI4R//BL7AgmhlLll9a+FBYaUrpCjZcNI4w1qXyR0n6X5HeU3UzybOdie1o35stHmCgBQIDAQABAoGAaD0YWVru8xPg1hATekHmez/h3LTLuK6Yw4GGwniuLHR/hCakqJy0wC6fcu/jamGSzVH0BhmmqdLb1We8AS/9j330Xc/0Kqw/7OdMwr+6qXjLXJ9oEgW2BePWQMavUibD5cvOwKBYReCsvGDBwI0tdlU/SgN9Cg5hU62VsHcd32ECQQDsXUU+W6k1vebv1NqFkGz2Xly9VHrSvwSqiByG+kFFHJ8/pJOJ5K54f3rc8jUeNcXoJ4zO6+RSD7n3eGuPH8tpAkEA0iUyCL5ZTFFWbKXi925Kl5PQi11f+t+p/OPTSP1SUXw4eWaZWN2YnBf18IEEE3zcidRFPwgLf+VJwysCFwboPQJAAY0DHUugqpeaYkx1Opcd/+fSl/Nr8uIJ98x403Hk570uVk6QIUF825GKjtSQAKi9qa5IwDrP/rHXuIXzvraosQJAAyiN9PWvb+c1DlL7804UDu0o0D9qBuI/ss5VyZ4NE65zRtfU7DIAbjAqASBfSE+zHNs04zqiuZxfnHBUCraO3QJAeBsgY+ECntfPBN2Hky5T29Zpl2P1aWStihR9LePUiAwTQXZLa8F3D5MX43YJ4Pf2MqMkI/fsLioTXHj/b38tMA==";
            var opensslPublicKey =
                "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDCBtp6c2QRrJo+Z7zGpm9/9nCK83m0CUnDgiwa5eyQ4ltBDbncGztm9U1HY0lI+GkZgSIUNe0is0wP/iYe30ANWE8s73kS3P5MuA24nPI4R//BL7AgmhlLll9a+FBYaUrpCjZcNI4w1qXyR0n6X5HeU3UzybOdie1o35stHmCgBQIDAQAB";
            var source = "只是告诉素数生成器帮我生成一个接近位的素数而已然后生成这是什么鬼额额额为啥呢呢123456";
            var signResult = RSACryptor.Sign(source, opensslPrivateKey);
            Output.WriteLine("签名结果:"+signResult);
            var verifyResult = RSACryptor.Verify(source, signResult, opensslPublicKey);
            Output.WriteLine("验签结果:"+verifyResult);
            Assert.True(verifyResult);
        }
        [Fact]
        public void Test_EncryptAndDecrypt()
        {
            var opensslPrivateKey =
                "MIICWwIBAAKBgQDCBtp6c2QRrJo+Z7zGpm9/9nCK83m0CUnDgiwa5eyQ4ltBDbncGztm9U1HY0lI+GkZgSIUNe0is0wP/iYe30ANWE8s73kS3P5MuA24nPI4R//BL7AgmhlLll9a+FBYaUrpCjZcNI4w1qXyR0n6X5HeU3UzybOdie1o35stHmCgBQIDAQABAoGAaD0YWVru8xPg1hATekHmez/h3LTLuK6Yw4GGwniuLHR/hCakqJy0wC6fcu/jamGSzVH0BhmmqdLb1We8AS/9j330Xc/0Kqw/7OdMwr+6qXjLXJ9oEgW2BePWQMavUibD5cvOwKBYReCsvGDBwI0tdlU/SgN9Cg5hU62VsHcd32ECQQDsXUU+W6k1vebv1NqFkGz2Xly9VHrSvwSqiByG+kFFHJ8/pJOJ5K54f3rc8jUeNcXoJ4zO6+RSD7n3eGuPH8tpAkEA0iUyCL5ZTFFWbKXi925Kl5PQi11f+t+p/OPTSP1SUXw4eWaZWN2YnBf18IEEE3zcidRFPwgLf+VJwysCFwboPQJAAY0DHUugqpeaYkx1Opcd/+fSl/Nr8uIJ98x403Hk570uVk6QIUF825GKjtSQAKi9qa5IwDrP/rHXuIXzvraosQJAAyiN9PWvb+c1DlL7804UDu0o0D9qBuI/ss5VyZ4NE65zRtfU7DIAbjAqASBfSE+zHNs04zqiuZxfnHBUCraO3QJAeBsgY+ECntfPBN2Hky5T29Zpl2P1aWStihR9LePUiAwTQXZLa8F3D5MX43YJ4Pf2MqMkI/fsLioTXHj/b38tMA==";
            var opensslPublicKey =
                "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDCBtp6c2QRrJo+Z7zGpm9/9nCK83m0CUnDgiwa5eyQ4ltBDbncGztm9U1HY0lI+GkZgSIUNe0is0wP/iYe30ANWE8s73kS3P5MuA24nPI4R//BL7AgmhlLll9a+FBYaUrpCjZcNI4w1qXyR0n6X5HeU3UzybOdie1o35stHmCgBQIDAQAB";
            var source = "只是告诉素数生成器帮我生成一个接近位的素数而已然后生成这是什么鬼额额额为啥呢呢";
            var encryptResult = RSACryptor.Encrypt(source, opensslPublicKey);
            Output.WriteLine("加密结果:" + encryptResult);
            var decryptResult = RSACryptor.Decrypt(encryptResult, opensslPrivateKey);
            Output.WriteLine("加密结果:" + decryptResult);            
        }

        [Fact]
        public void Test_EncryptBlock()
        {
            var opensslPrivateKey =
                "MIICWwIBAAKBgQDCBtp6c2QRrJo+Z7zGpm9/9nCK83m0CUnDgiwa5eyQ4ltBDbncGztm9U1HY0lI+GkZgSIUNe0is0wP/iYe30ANWE8s73kS3P5MuA24nPI4R//BL7AgmhlLll9a+FBYaUrpCjZcNI4w1qXyR0n6X5HeU3UzybOdie1o35stHmCgBQIDAQABAoGAaD0YWVru8xPg1hATekHmez/h3LTLuK6Yw4GGwniuLHR/hCakqJy0wC6fcu/jamGSzVH0BhmmqdLb1We8AS/9j330Xc/0Kqw/7OdMwr+6qXjLXJ9oEgW2BePWQMavUibD5cvOwKBYReCsvGDBwI0tdlU/SgN9Cg5hU62VsHcd32ECQQDsXUU+W6k1vebv1NqFkGz2Xly9VHrSvwSqiByG+kFFHJ8/pJOJ5K54f3rc8jUeNcXoJ4zO6+RSD7n3eGuPH8tpAkEA0iUyCL5ZTFFWbKXi925Kl5PQi11f+t+p/OPTSP1SUXw4eWaZWN2YnBf18IEEE3zcidRFPwgLf+VJwysCFwboPQJAAY0DHUugqpeaYkx1Opcd/+fSl/Nr8uIJ98x403Hk570uVk6QIUF825GKjtSQAKi9qa5IwDrP/rHXuIXzvraosQJAAyiN9PWvb+c1DlL7804UDu0o0D9qBuI/ss5VyZ4NE65zRtfU7DIAbjAqASBfSE+zHNs04zqiuZxfnHBUCraO3QJAeBsgY+ECntfPBN2Hky5T29Zpl2P1aWStihR9LePUiAwTQXZLa8F3D5MX43YJ4Pf2MqMkI/fsLioTXHj/b38tMA==";
            var opensslPublicKey =
                "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDCBtp6c2QRrJo+Z7zGpm9/9nCK83m0CUnDgiwa5eyQ4ltBDbncGztm9U1HY0lI+GkZgSIUNe0is0wP/iYe30ANWE8s73kS3P5MuA24nPI4R//BL7AgmhlLll9a+FBYaUrpCjZcNI4w1qXyR0n6X5HeU3UzybOdie1o35stHmCgBQIDAQAB";
            var source = "只是告诉素数生成器帮我生成一个接近位的素数而已然后生成这是什么鬼额额额为啥呢呢";
            var result = source + source + source + source + source;
            var encryptResult = RSACryptor.EncryptBlock(result, opensslPublicKey);
            Output.WriteLine("加密结果:" + encryptResult);
            var decryptResult = RSACryptor.DecryptBlock(encryptResult, opensslPrivateKey);
            Output.WriteLine("解密结果:"+decryptResult);

            Assert.Equal(result,decryptResult);
        }
    }
}
