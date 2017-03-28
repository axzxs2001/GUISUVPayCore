using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using AlipayPayCore;
using AlipayPayCore.Entity;
using QRCoder;

namespace Alipay_TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Program>();
            Console.WriteLine(list.GetType().IsConstructedGenericType);
            var arr = new Program[9];
            Console.WriteLine(arr.GetType().IsArray);
            Console.WriteLine(arr.GetType().IsConstructedGenericType);
            return;

            while (true)
            {
                //Send();
                //continue;
                try
                {
                    System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Console.WriteLine("1、统一下单  2、退单");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            UnifiedOrder();
                            break;
                        case "2":
                            Refund();
                            break;
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }

        #region 交易
        static void UnifiedOrder()
        {
            var apps = File.ReadAllLines(@"D:\alipay\app.txt");
            Console.WriteLine("请输入订单号：");
            var tradeNo = Console.ReadLine();
            var precreate = new Precreate()
            {
                AppID = apps[0],
                Charset = "utf-8",
                SignType = "RSA",
                Timestamp =DateTime .Now.ToString("yyyy-MM-dd HH:mm:ss"),// "2017-03-25 03:07:50",
                Version = "1.0",
                OutTradeNo = tradeNo,
                TotalAmount = 0.01m,
                NotifyUrl = "http://a.b.com",
                Subject = "test123"
            };
            var payHandle = new AlipayPayCore.PayHandle();
            var backPreccreate = payHandle.Send(precreate) as PrecreateBack;
            if (backPreccreate.Code == "10000")
            {
                SavaQR(backPreccreate.QrCode);
            }
            else
            {
                Console.WriteLine($"Code:{backPreccreate.Code} Message:{backPreccreate.Message}");
            }
        }
        static void Refund()
        {
            var apps = File.ReadAllLines(@"D:\alipay\app.txt");
            Console.WriteLine("请输入订单号：");
            var tradeNo = Console.ReadLine();
            var refund = new Refund()
            {
                AppID = apps[0],
                Charset = "utf-8",
                SignType = "RSA",
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Version = "1.0",
                OutTradeNo = tradeNo,
                RefundAmount = 0.01m,
                RefundReason = "his收费失败"
            };
            var payHandle = new AlipayPayCore.PayHandle();
            var backRefund = payHandle.Send(refund) as RefundBack;
            if (backRefund.Code == "10000")
            {
                Console.WriteLine($"code:{backRefund.Code} msg:{backRefund.Message}");
            }
            else
            {
                Console.WriteLine($"code:{backRefund.Code} msg:{backRefund.Message} SubCode:{backRefund.SubCode}  SubMessage:{backRefund.SubMessage}");
            }
        }
        #region 生成二维码
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="qrUrl">返回用来生成二维码的路径</param>
        static void SavaQR(string qrUrl)
        {
            try
            {
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(qrUrl, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(20);
                qrCodeImage.Save($@"D:\\alipay.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch
            {
                throw new AlipayPayCoreException("生成二维码失败");
            }
        }
        #endregion

        #endregion


        #region 测试用代码

        static void Send()
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var client = new HttpClient();
            var url = "https://openapi.alipay.com/gateway.do?charset=utf-8";
            var apps = File.ReadAllLines(@"D:\alipay\app.txt");
            var BizContent = "{\"out_trade_no\":\"121213\",\"refund_amount\":0.01}";
            var privatepem = File.ReadAllText(@"D:\alipay\rsa_private_key.pem");
            var signCharts = $@"app_id={apps[0]}&biz_content={BizContent}&charset=utf-8&format=json&method=alipay.trade.refund&sign_type=RSA&timestamp=2017-03-28 11:42:50&version=1.0";
            var sign = RSASignCharSet(signCharts, privatepem, null, "RSA");
            var json = $@"biz_content={WebUtility.UrlEncode(BizContent)}&method={WebUtility.UrlEncode("alipay.trade.refund")}&version={WebUtility.UrlEncode("1.0")}&app_id={WebUtility.UrlEncode(apps[0])}&format=json&timestamp={WebUtility.UrlEncode("2017-03-28 11:42:50")}&sign_type={WebUtility.UrlEncode("RSA")}&charset={WebUtility.UrlEncode("utf -8")}&sign={WebUtility.UrlEncode(sign)}";
      

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContinueTimeout = 1000;
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            byte[] postData = Encoding.UTF8.GetBytes(json);
            var reqStream = request.GetRequestStreamAsync().Result;
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Dispose();

            var rsp = (HttpWebResponse)request.GetResponseAsync().Result;

            var arr = new byte[rsp.ContentLength];
            rsp.GetResponseStream().Read(arr, 0, arr.Length);
            var result = Encoding.UTF8.GetString(arr);
            var qian = result.Split(new string[] { ",\"sign\":\"", "{\"alipay_trade_precreate_response\":" }, StringSplitOptions.None)[1];
            var sss = result.Split(new string[] { "sign\":\"" }, StringSplitOptions.None)[1].Trim('}', '"');
           var vaResult= RSACheckContent(qian, sss, "utf-8","RSA");
            Console.WriteLine("验证结果" + vaResult);

            var dicc = Json.JsonParser.FromJson(result);
            foreach (var ddd in dicc)
            {
                if (ddd.Key.ToLower() != "sign")
                {

                    var dddd = ddd.Value as IDictionary<string, object>;
                    object vv;
                    dddd.TryGetValue("code", out vv);

                    Console.WriteLine(vv);
                }
            }
            Console.WriteLine(result);

            Console.Read();
        }

        #region 验证
        /// <summary>
        /// 验证返回答名与值
        /// </summary>
        /// <param name="signContent"></param>
        /// <param name="sign"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static bool RSACheckContent(string signContent, string sign, string charset,string signType)
        {
            try
            {
                var sPublicKeyPEM = File.ReadAllText("D:/alipay/alipay_rsa_public_key.pem");
                if ("RSA2".Equals(signType))
                {
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.PersistKeyInCsp = false;
                    RSACryptoServiceProviderExtension.LoadPublicKeyPEM(rsa, sPublicKeyPEM);

                    bool bVerifyResultOriginal = rsa.VerifyData(Encoding.GetEncoding(charset).GetBytes(signContent), "SHA256", Convert.FromBase64String(sign));
                    return bVerifyResultOriginal;
                }
                else
                {
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.PersistKeyInCsp = false;
                    RSACryptoServiceProviderExtension.LoadPublicKeyPEM(rsa, sPublicKeyPEM);
                    var sha1 = SHA1.Create();
                    var signBase64 = Convert.FromBase64String(sign);
                    bool bVerifyResultOriginal = rsa.VerifyData(Encoding.GetEncoding(charset).GetBytes(signContent), sha1, signBase64);
                    Console.WriteLine(bVerifyResultOriginal);
                    return bVerifyResultOriginal;
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
                return false;
            }

        }
        /// <summary>
        /// AES 加密
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="bizContent"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        //public static string AesEncrypt(string encryptKey, string bizContent, string charset)
        //{

        //    Byte[] keyArray = Convert.FromBase64String(encryptKey);
        //    Byte[] toEncryptArray = null;

        //    if (string.IsNullOrEmpty(charset))
        //    {
        //        toEncryptArray = Encoding.UTF8.GetBytes(bizContent);
        //    }
        //    else
        //    {
        //        toEncryptArray = Encoding.GetEncoding(charset).GetBytes(bizContent);
        //    }

        //    System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();
        //    rDel.Key = keyArray;
        //    rDel.Mode = System.Security.Cryptography.CipherMode.CBC;
        //    rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
        //    rDel.IV = AES_IV;

        //    System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateEncryptor(rDel.Key, rDel.IV);
        //    Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);


        //    return Convert.ToBase64String(resultArray);

        //}
        public static string AseEncrypt(string bizContent)
        {

            var buffer = Encoding.UTF8.GetBytes(bizContent);

            var iv = GetRandomData(128);
            var keyAes = GetRandomData(256);


            byte[] result;
            using (var aes = Aes.Create())
            {
                aes.Key = keyAes;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var resultStream = new MemoryStream())
                {
                    using (var aesStream = new CryptoStream(resultStream, encryptor, CryptoStreamMode.Write))
                    using (var plainStream = new MemoryStream(buffer))
                    {
                        plainStream.CopyTo(aesStream);
                    }

                    result = resultStream.ToArray();
                }
            }
            return Convert.ToBase64String(result);
        }
        private static byte[] GetRandomData(int bits)
        {
            var result = new byte[bits / 8];
            RandomNumberGenerator.Create().GetBytes(result);
            return result;
        }
#endregion

        public static string RSASignCharSet(string data, string privateKeyPem, string charset, string signType)
        {
            byte[] signatureBytes = null;
            try
            {
                RSACryptoServiceProvider rsaCsp = null;
                //字符串获取
                var datas = Convert.FromBase64String(privateKeyPem);
                rsaCsp = DecodeRSAPrivateKey(datas, signType);
                byte[] dataBytes = null;
                if (string.IsNullOrEmpty(charset))
                {
                    dataBytes = Encoding.UTF8.GetBytes(data);
                }
                else
                {
                    dataBytes = Encoding.GetEncoding(charset).GetBytes(data);
                }
                if (null == rsaCsp)
                {
                    throw new Exception("您使用的私钥格式错误，请检查RSA私钥配置" + ",charset = " + charset);
                }
                if ("RSA2".Equals(signType))
                {

                    signatureBytes = rsaCsp.SignData(dataBytes, "SHA256");

                }
                else
                {
                    signatureBytes = rsaCsp.SignData(dataBytes, "SHA1");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("您使用的私钥格式错误，请检查RSA私钥配置" + ",charset = " + charset);
            }
            return Convert.ToBase64String(signatureBytes);
        }
        private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey, string signType)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // --------- Set up stream to decode the asn.1 encoded RSA private key ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);  //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();    //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------ all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);


                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                CspParameters CspParameters = new CspParameters();
                CspParameters.Flags = CspProviderFlags.UseMachineKeyStore;

                int bitLen = 1024;
                if ("RSA2".Equals(signType))
                {
                    bitLen = 2048;
                }

                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(bitLen, CspParameters);
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                binr.Dispose();
            }
        }
        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)		//expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();	// data size in next byte
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte(); // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;     // we already have the data size
            }

            while (binr.ReadByte() == 0x00)
            {	//remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);		//last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }
        #endregion
    }

    public static class RSACryptoServiceProviderExtension
    {

        #region Methods

        /// <summary>Extension method which initializes an RSACryptoServiceProvider from a DER public key blob.</summary>
        public static void LoadPublicKeyDER(RSACryptoServiceProvider provider, byte[] DERData)
        {
            byte[] RSAData = RSACryptoServiceProviderExtension.GetRSAFromDER(DERData);
            byte[] publicKeyBlob = RSACryptoServiceProviderExtension.GetPublicKeyBlobFromRSA(RSAData);
            provider.ImportCspBlob(publicKeyBlob);
        }

        /// <summary>Extension method which initializes an RSACryptoServiceProvider from a PEM public key string.</summary>
        public static void LoadPublicKeyPEM(RSACryptoServiceProvider provider, string sPEM)
        {
            byte[] DERData = RSACryptoServiceProviderExtension.GetDERFromPEM(sPEM);
            RSACryptoServiceProviderExtension.LoadPublicKeyDER(provider, DERData);
        }

        /// <summary>Returns a public key blob from an RSA public key.</summary>
        internal static byte[] GetPublicKeyBlobFromRSA(byte[] RSAData)
        {
            byte[] data = null;
            UInt32 dwCertPublicKeyBlobSize = 0;
            if (RSACryptoServiceProviderExtension.CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING,
                new IntPtr((int)CRYPT_OUTPUT_TYPES.RSA_CSP_PUBLICKEYBLOB), RSAData, (UInt32)RSAData.Length, CRYPT_DECODE_FLAGS.NONE,
                data, ref dwCertPublicKeyBlobSize))
            {
                data = new byte[dwCertPublicKeyBlobSize];
                if (!RSACryptoServiceProviderExtension.CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING,
                    new IntPtr((int)CRYPT_OUTPUT_TYPES.RSA_CSP_PUBLICKEYBLOB), RSAData, (UInt32)RSAData.Length, CRYPT_DECODE_FLAGS.NONE,
                    data, ref dwCertPublicKeyBlobSize))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return data;
        }

        /// <summary>Converts DER binary format to a CAPI CERT_PUBLIC_KEY_INFO structure containing an RSA key.</summary>
        internal static byte[] GetRSAFromDER(byte[] DERData)
        {
            byte[] data = null;
            byte[] publicKey = null;
            CERT_PUBLIC_KEY_INFO info;
            UInt32 dwCertPublicKeyInfoSize = 0;
            IntPtr pCertPublicKeyInfo = IntPtr.Zero;
            if (RSACryptoServiceProviderExtension.CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING, new IntPtr((int)CRYPT_OUTPUT_TYPES.X509_PUBLIC_KEY_INFO),
                DERData, (UInt32)DERData.Length, CRYPT_DECODE_FLAGS.NONE, data, ref dwCertPublicKeyInfoSize))
            {
                data = new byte[dwCertPublicKeyInfoSize];
                if (RSACryptoServiceProviderExtension.CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING, new IntPtr((int)CRYPT_OUTPUT_TYPES.X509_PUBLIC_KEY_INFO),
                    DERData, (UInt32)DERData.Length, CRYPT_DECODE_FLAGS.NONE, data, ref dwCertPublicKeyInfoSize))
                {
                    GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                    try
                    {
                        info = (CERT_PUBLIC_KEY_INFO)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CERT_PUBLIC_KEY_INFO));
                        publicKey = new byte[info.PublicKey.cbData];
                        Marshal.Copy(info.PublicKey.pbData, publicKey, 0, publicKey.Length);
                    }
                    finally
                    {
                        handle.Free();
                    }
                }
                else
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return publicKey;
        }

        /// <summary>Extracts the binary data from a PEM file.</summary>
        internal static byte[] GetDERFromPEM(string sPEM)
        {
            UInt32 dwSkip, dwFlags;
            UInt32 dwBinarySize = 0;

            if (!RSACryptoServiceProviderExtension.CryptStringToBinary(sPEM, (UInt32)sPEM.Length, CRYPT_STRING_FLAGS.CRYPT_STRING_BASE64HEADER, null, ref dwBinarySize, out dwSkip, out dwFlags))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            byte[] decodedData = new byte[dwBinarySize];
            if (!RSACryptoServiceProviderExtension.CryptStringToBinary(sPEM, (UInt32)sPEM.Length, CRYPT_STRING_FLAGS.CRYPT_STRING_BASE64HEADER, decodedData, ref dwBinarySize, out dwSkip, out dwFlags))
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            return decodedData;
        }

        #endregion Methods

        #region P/Invoke Constants

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_ACQUIRE_CONTEXT_FLAGS : uint
        {
            CRYPT_NEWKEYSET = 0x8,
            CRYPT_DELETEKEYSET = 0x10,
            CRYPT_MACHINE_KEYSET = 0x20,
            CRYPT_SILENT = 0x40,
            CRYPT_DEFAULT_CONTAINER_OPTIONAL = 0x80,
            CRYPT_VERIFYCONTEXT = 0xF0000000
        }

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_PROVIDER_TYPE : uint
        {
            PROV_RSA_FULL = 1
        }

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_DECODE_FLAGS : uint
        {
            NONE = 0,
            CRYPT_DECODE_ALLOC_FLAG = 0x8000
        }

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_ENCODING_FLAGS : uint
        {
            PKCS_7_ASN_ENCODING = 0x00010000,
            X509_ASN_ENCODING = 0x00000001,
        }

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_OUTPUT_TYPES : int
        {
            X509_PUBLIC_KEY_INFO = 8,
            RSA_CSP_PUBLICKEYBLOB = 19,
            PKCS_RSA_PRIVATE_KEY = 43,
            PKCS_PRIVATE_KEY_INFO = 44
        }

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_STRING_FLAGS : uint
        {
            CRYPT_STRING_BASE64HEADER = 0,
            CRYPT_STRING_BASE64 = 1,
            CRYPT_STRING_BINARY = 2,
            CRYPT_STRING_BASE64REQUESTHEADER = 3,
            CRYPT_STRING_HEX = 4,
            CRYPT_STRING_HEXASCII = 5,
            CRYPT_STRING_BASE64_ANY = 6,
            CRYPT_STRING_ANY = 7,
            CRYPT_STRING_HEX_ANY = 8,
            CRYPT_STRING_BASE64X509CRLHEADER = 9,
            CRYPT_STRING_HEXADDR = 10,
            CRYPT_STRING_HEXASCIIADDR = 11,
            CRYPT_STRING_HEXRAW = 12,
            CRYPT_STRING_NOCRLF = 0x40000000,
            CRYPT_STRING_NOCR = 0x80000000
        }

        #endregion P/Invoke Constants

        #region P/Invoke Structures

        /// <summary>Structure from Crypto API.</summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_OBJID_BLOB
        {
            internal UInt32 cbData;
            internal IntPtr pbData;
        }

        /// <summary>Structure from Crypto API.</summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_ALGORITHM_IDENTIFIER
        {
            internal IntPtr pszObjId;
            internal CRYPT_OBJID_BLOB Parameters;
        }

        /// <summary>Structure from Crypto API.</summary>
        [StructLayout(LayoutKind.Sequential)]
        struct CRYPT_BIT_BLOB
        {
            internal UInt32 cbData;
            internal IntPtr pbData;
            internal UInt32 cUnusedBits;
        }

        /// <summary>Structure from Crypto API.</summary>
        [StructLayout(LayoutKind.Sequential)]
        struct CERT_PUBLIC_KEY_INFO
        {
            internal CRYPT_ALGORITHM_IDENTIFIER Algorithm;
            internal CRYPT_BIT_BLOB PublicKey;
        }

        #endregion P/Invoke Structures

        #region P/Invoke Functions

        /// <summary>Function for Crypto API.</summary>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptDestroyKey(IntPtr hKey);

        /// <summary>Function for Crypto API.</summary>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptImportKey(IntPtr hProv, byte[] pbKeyData, UInt32 dwDataLen, IntPtr hPubKey, UInt32 dwFlags, ref IntPtr hKey);

        /// <summary>Function for Crypto API.</summary>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptReleaseContext(IntPtr hProv, Int32 dwFlags);

        /// <summary>Function for Crypto API.</summary>
        [DllImport("advapi32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptAcquireContext(ref IntPtr hProv, string pszContainer, string pszProvider, CRYPT_PROVIDER_TYPE dwProvType, CRYPT_ACQUIRE_CONTEXT_FLAGS dwFlags);

        /// <summary>Function from Crypto API.</summary>
        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptStringToBinary(string sPEM, UInt32 sPEMLength, CRYPT_STRING_FLAGS dwFlags, [Out] byte[] pbBinary, ref UInt32 pcbBinary, out UInt32 pdwSkip, out UInt32 pdwFlags);

        /// <summary>Function from Crypto API.</summary>
        [DllImport("crypt32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptDecodeObjectEx(CRYPT_ENCODING_FLAGS dwCertEncodingType, IntPtr lpszStructType, byte[] pbEncoded, UInt32 cbEncoded, CRYPT_DECODE_FLAGS dwFlags, IntPtr pDecodePara, ref byte[] pvStructInfo, ref UInt32 pcbStructInfo);

        /// <summary>Function from Crypto API.</summary>
        [DllImport("crypt32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptDecodeObject(CRYPT_ENCODING_FLAGS dwCertEncodingType, IntPtr lpszStructType, byte[] pbEncoded, UInt32 cbEncoded, CRYPT_DECODE_FLAGS flags, [In, Out] byte[] pvStructInfo, ref UInt32 cbStructInfo);

        #endregion P/Invoke Functions

    }
}