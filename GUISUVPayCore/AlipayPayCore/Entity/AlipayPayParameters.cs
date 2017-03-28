using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;
using System.IO;
using System.Net;

namespace AlipayPayCore.Entity
{
    /// <summary>
    /// 公共参数
    /// </summary>
    public abstract class AlipayPayParameters
    {
        /// <summary>
        /// 支付宝分配给开发者的应用ID
        /// </summary>
        [TradeField("app_id", Length = 32, IsRequire = true, IsPublic = true)]
        public string AppID
        { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        [TradeField("method", Length = 128, IsRequire = true, IsPublic = true)]
        public string Method
        {
            get
            {
                var type = this.GetType().GetTypeInfo();
                foreach (var att in type.GetCustomAttributes(false))
                {
                    if (att is TradeAttribute)
                    {
                        var atts = att as TradeAttribute;
                        return atts.Method;
                    }
                }
                throw new AlipayPayCoreException("Method为空");
            }
        }
        /// <summary>
        /// 仅支持JSON
        /// </summary>
        [TradeField("format", Length = 40, IsRequire = false, IsPublic = true)]
        public string Format
        { get; set; }
        /// <summary>
        /// 请求使用的编码格式，如utf-8,gbk,gb2312等
        /// </summary>
        [TradeField("charset", Length = 10, IsRequire = true, IsPublic = true)]
        public string Charset
        { get; set; }
        /// <summary>
        /// 商户生成签名字符串所使用的签名算法类型，目前支持RSA2和RSA，推荐使用RSA2
        /// </summary>
        [TradeField("sign_type", Length = 10, IsRequire = true, IsPublic = true)]
        public string SignType
        { get; set; }
        /// <summary>
        /// 商户请求参数的签名串，详见签名
        /// </summary>
        [TradeField("sign", Length = 256, IsRequire = true, IsPublic = true)]
        public string Sign
        { get; set; }
        /// <summary>
        /// 发送请求的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        [TradeField("timestamp", Length = 19, IsRequire = true, IsPublic = true)]
        public string Timestamp
        { get; set; }
        /// <summary>
        /// 调用的接口版本，固定为：1.0
        /// </summary>
        [TradeField("version", Length = 3, IsRequire = true, IsPublic = true)]
        public string Version
        { get; set; }
        /// <summary>
        /// 支付宝服务器主动通知商户服务器里指定的页面http/https路径。
        /// </summary>
        [TradeField("notify_url", Length = 256, IsRequire = false, IsPublic = true)]
        public string NotifyUrl
        { get; set; }
        /// <summary>
        /// 详见应用授权概述
        /// </summary>
        [TradeField("app_auth_token", Length = 40, IsRequire = false, IsPublic = true)]
        public string AppAuthToken
        { get; set; }
        /// <summary>
        /// 支付宝服务器主动通知商户服务器里指定的页面http/https路径。
        /// </summary>
        [TradeField("biz_content", Length = 0, IsRequire = true, IsPublic = true)]
        public string BizContent
        { get; set; }

        /// <summary>
        /// 转传输字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sortDic = new SortedDictionary<string, dynamic>();
            var contentDic = new SortedDictionary<string, dynamic>();
            foreach (var pro in this.GetType().GetProperties())
            {
                foreach (var att in pro.GetCustomAttributes(false))
                {
                    if (att is TradeFieldAttribute)
                    {
                        var attr = att as TradeFieldAttribute;
                        //获取值
                        var value = pro.GetValue(this);
                        ValidateValue();
                        //验证必填值不能为空
                        void ValidateValue()
                        {
                            //判断引用类型，必填值为空的，抛异常
                            if (attr.Name.Trim() != "sign" && attr.Name.Trim() != "biz_content" && !pro.PropertyType.GetTypeInfo().IsValueType && attr.IsRequire && value == null)
                            {
                                throw new AlipayPayCoreException($"{pro.Name}的值为必填，不能为空");
                            }
                        }
                        //判断不为空
                        if (value != null)
                        {
                            if (attr.Length < Encoding.UTF8.GetByteCount(value.ToString()))
                            {
                                throw new AlipayPayCoreException($"{pro.Name}的值：{value}超过{attr.Length}长度");
                            }
                            //判断值类型和引用类型
                            if (pro.PropertyType.GetTypeInfo().IsValueType)
                            {
                                //判断是否为默认值 
                                if (pro.GetValue(this).ToString() != Activator.CreateInstance(pro.PropertyType).ToString())
                                {
                                    AddDic();
                                }
                            }
                            else
                            {
                                AddDic();
                            }
                            //添加数据
                            void AddDic()
                            {
                                if (attr.IsPublic)
                                {
                                    sortDic.Add(attr.Name.Trim(), value);
                                }
                                else
                                {
                                    contentDic.Add(attr.Name.Trim(), value);
                                }
                            }
                        }
                        break;
                    }
                }
            }
            //字典转json
            var bizContent = Json.JsonParser.ToJson(contentDic);
            //添加json数据
            sortDic.Add("biz_content", bizContent);
            var signCharBuild = new StringBuilder();
            foreach (var pair in sortDic)
            {
                signCharBuild.Append($"{pair.Key}={pair.Value}&");
            }
            var privatepem = File.ReadAllText(@"D:\alipay\rsa_private_key.pem");
            var signString = signCharBuild.ToString().TrimEnd('&');
            var sign = RSASignCharSet(signString, privatepem, null, "RSA");
            sortDic.Add("sign", sign);
            var charBuild = new StringBuilder();
            foreach (var pair in sortDic)
            {
                charBuild.Append($"{pair.Key}={WebUtility.UrlEncode(pair.Value)}&");
            }
            return charBuild.ToString().TrimEnd('&');
        }

        #region 数据加密处理
        string AseEncrypt(string bizContent)
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
        byte[] GetRandomData(int bits)
        {
            var result = new byte[bits / 8];
            RandomNumberGenerator.Create().GetBytes(result);
            return result;
        }
        /// <summary>
        /// 生成加密数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privateKeyPem"></param>
        /// <param name="charset"></param>
        /// <param name="signType"></param>
        /// <returns></returns>
        string RSASignCharSet(string data, string privateKeyPem, string charset, string signType)
        {
            byte[] signatureBytes = null;
            try
            {
                //字符串获取
                var datas = Convert.FromBase64String(privateKeyPem);
                var rsaCsp = DecodeRSAPrivateKey(datas, signType);
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

        RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey, string signType)
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
        int GetIntegerSize(BinaryReader binr)
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
}
