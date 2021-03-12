using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;



namespace OuatTianYaHtmlMaker
{
    public class OuatTools
    {
        static readonly HashAlgorithmName sha512 = HashAlgorithmName.SHA512;
        static readonly int RSAMax = 256;

        public static string RSAEncryptData(string text, string pubKeyfile)
        {
            string ret;
            try
            {
                X509Certificate2 x509 = new X509Certificate2(pubKeyfile);

                byte[] datas = Encoding.UTF8.GetBytes(text);
                byte[] buffer;
                RSA rsa = (RSA)x509.GetRSAPublicKey();
                int count = datas.Length / 256 + 1;
                StringBuilder sb1 = new StringBuilder();
                int copylen = RSAMax;
                for (int i = 0; i < count; i++)
                {
                    if ((datas.Length - i * RSAMax) < RSAMax)
                    {
                        copylen = datas.Length - i * RSAMax;
                    }

                    buffer = new byte[copylen];
                    Array.Copy(datas, i * RSAMax, buffer, 0, copylen);
                    byte[] edatas = rsa.Encrypt(buffer, RSAEncryptionPadding.Pkcs1);
                    ret = Convert.ToBase64String(edatas);
                    sb1.Append(ret);
                    sb1.Append(',');
                }
                ret = sb1.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ret = null;
            }
            return ret;
        }

        public static string RSADecryptData(string ciphertext, string prvKeyfile, string pw)
        {
            X509Certificate2 x509 = new X509Certificate2(prvKeyfile, pw, X509KeyStorageFlags.Exportable);
            RSA rsa = x509.GetRSAPrivateKey();
            RSAParameters para = rsa.ExportParameters(true);
            rsa.ImportParameters(para);
            string ret;

            string[] buffer = ciphertext.Split(',', StringSplitOptions.RemoveEmptyEntries);
            byte[] datas;
            List<byte> a1 = new List<byte>();


            for (int i = 0; i < buffer.Length; i++)
            {
                datas = rsa.Decrypt(Convert.FromBase64String(buffer[i]), RSAEncryptionPadding.Pkcs1);

                a1.AddRange(datas);
            }
            byte[] bf = a1.ToArray();
            ret = Encoding.UTF8.GetString(bf);
            return ret;
        }
        public static string RSASignData(string text, string prvKeyfile, string pw)
        {
            X509Certificate2 x509 = new X509Certificate2(prvKeyfile, pw);
            byte[] datas = Encoding.UTF8.GetBytes(text);
            RSA rsa = x509.GetRSAPrivateKey();
            byte[] sign = rsa.SignData(datas, sha512, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(sign);
        }
        public static bool RSAVerifyData(string text, string pubKeyfile, string signstr)
        {
            X509Certificate2 x509 = new X509Certificate2(pubKeyfile);
            byte[] datas = Encoding.UTF8.GetBytes(text);
            byte[] sign = Convert.FromBase64String(signstr);
            RSA rsa = x509.GetRSAPublicKey();
            bool verify = rsa.VerifyData(datas, sign, sha512, RSASignaturePadding.Pkcs1);

            return verify;
        }
        public static string U2A(string theText)
        {
            string[] sp = { "&#", ";" };
            string[] chars = theText.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder buffer = new StringBuilder(chars.Length);
            char c;
            for (int i = 0; i < chars.Length; i++)
            {
                c = (char)int.Parse(chars[i]);
                buffer.Append(c);
            }
            string ret = buffer.ToString();
            return ret;
            
        }
        public static string A2U(string theText)
        {
            try
            {
                string output = string.Empty;
                StringBuilder sb1 = new StringBuilder();

                for (int i = 0; i < theText.Length; i++)
                {
                    sb1.Append($"&#{(int)theText[i]};");
                }

                output = sb1.ToString();
                return output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return "";
        }


        public static string AESEncryptData(string text, string key, string strIV, char padding = '*')
        {
            byte[] bKey, bIV;
            PaddingKeyIV(key, strIV, padding, out bKey, out bIV);

            return AESEncryptData(text, bKey, bIV);
        }

        private static void PaddingKeyIV(string key, string strIV, char padding, out byte[] bKey, out byte[] bIV)
        {
            bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length, padding)), bKey, bKey.Length);
            bIV = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(strIV.PadRight(bIV.Length, padding)), bIV, bIV.Length);
        }

        public static string AESEncryptData(string text, byte[] key, byte[] IV)
        {
            byte[] edatas = null;
            string ret = null;

            byte[] datas = Encoding.UTF8.GetBytes(text);
            byte[] bKey = PaddingBytes(key, 32);
            byte[] bIV = PaddingBytes(IV, 16);

            using (Aes Aes = Aes.Create())
            {
                try
                {
                    Aes.Padding = PaddingMode.PKCS7;
                    using (MemoryStream Memory = new MemoryStream())
                    {
                        using (CryptoStream Encryptor = new CryptoStream(Memory, Aes.CreateEncryptor(bKey, bIV),
                         CryptoStreamMode.Write))
                        {
                            Encryptor.Write(datas, 0, datas.Length);
                            Encryptor.FlushFinalBlock();
                            edatas = Memory.ToArray();
                        }
                    }
                    ret = Convert.ToBase64String(edatas);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    edatas = null;
                    ret = null;
                }

                return ret;
            }
        }

        private static byte[] PaddingBytes(byte[] source, int count, byte padding = (byte)'*')
        {
            byte[] bytes = new byte[count];
            int copy = count;
            for (int i = 0; i < count; i++)
            {
                bytes[i] = padding;
            }

            if (source.Length <= count)
            {
                copy = source.Length;
            }

            for (int i = 0; i < copy; i++)
            {
                bytes[i] = source[i];
            }
            return bytes;
        }

        public static string AESDecryptData(string etext, string key, string strIV, char padding = '*')
        {
            byte[] bKey, bIV;
            PaddingKeyIV(key, strIV, padding, out bKey, out bIV);
            return AESDecryptData(etext, bKey, bIV);
        }

        public static string AESDecryptData(string etext, byte[] key, byte[] IV)
        {
            byte[] edatas = Convert.FromBase64String(etext);
            byte[] bKey = PaddingBytes(key, 32);
            byte[] bIV = PaddingBytes(IV, 16);

            byte[] datas = null;
            string ret = null;

            using (Aes Aes = Aes.Create())
            {
                try
                {
                    using (MemoryStream Memory = new MemoryStream(edatas))
                    {
                        using (CryptoStream Decryptor = new CryptoStream(Memory, Aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Read))
                        {
                            using (MemoryStream tempMemory = new MemoryStream())
                            {
                                byte[] Buffer = new byte[1024];
                                Int32 readBytes = 0;
                                while ((readBytes = Decryptor.Read(Buffer, 0, Buffer.Length)) > 0)
                                {
                                    tempMemory.Write(Buffer, 0, readBytes);
                                }

                                datas = tempMemory.ToArray();
                            }
                        }
                        ret = Encoding.UTF8.GetString(datas);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    datas = null;
                    ret = null;
                }
                return ret;
            }
        }
        private static byte[] getRandomBytes(string text = "RandomBytes")
        {
            byte[] ret;
            string head = new Random().Next(1, int.MaxValue / 2).ToString();
            Thread.Sleep(5);
            string taill = new Random().Next(int.MinValue / 2, int.MaxValue).ToString();
            ret = HashAlgorithm.Create(sha512.Name)
                .ComputeHash(Encoding.UTF8.GetBytes(text));
            ret = HashAlgorithm.Create(sha512.Name)
                .ComputeHash(Encoding.UTF8.GetBytes(head + ret + taill));
            return ret;
        }

        public static string RsaSignAesEncryptData(string text, string ReaderPubkeyfile, string AuthorPrvkeyfile, string pw, out string eKeyIv, out string eSign)
        {
            string name = new X509Certificate2(ReaderPubkeyfile).GetNameInfo(X509NameType.SimpleName, false);
            byte[] key = getRandomBytes(text);
            byte[] iv = getRandomBytes(name);
            key = PaddingBytes(key, 32);
            iv = PaddingBytes(iv, 16);
            string etext = AESEncryptData(text, key, iv);
            string sign = RSASignData(text, AuthorPrvkeyfile, pw);
            eSign = AESEncryptData(sign, key, iv);
            string asekeyiv = Convert.ToBase64String(key) + "," + Convert.ToBase64String(iv);
            eKeyIv = RSAEncryptData(asekeyiv, ReaderPubkeyfile);
            return etext;
        }

        public static bool RsaSignAesDecryptData(string etext, string AuthorPubkeyfile, string ReaderPrvkeyfile, string pw, string eKeyIv, string eSign, out string text)
        {
            string[] keyIv = RSADecryptData(eKeyIv, ReaderPrvkeyfile, pw).Split(',');
            text = AESDecryptData(etext, Convert.FromBase64String(keyIv[0]), Convert.FromBase64String(keyIv[1]));
            string sign = AESDecryptData(eSign, Convert.FromBase64String(keyIv[0]), Convert.FromBase64String(keyIv[1]));
            bool verify = RSAVerifyData(text, AuthorPubkeyfile, sign);
            return verify;
        }
    }
}

