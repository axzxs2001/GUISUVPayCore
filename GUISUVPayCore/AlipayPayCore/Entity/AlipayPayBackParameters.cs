using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;


namespace AlipayPayCore.Entity
{
    /// <summary>
    /// 返回参数
    /// </summary>
    public class AlipayPayBackParameters
    {
        /// <summary>
        /// 网关返回码
        /// </summary>
        [TradeField("code", IsRequire = true)]
        public string Code
        { get; set; }

        /// <summary>
        /// 网关返回码描述
        /// </summary>
        [TradeField("msg", IsRequire = true)]
        public string Message
        { get; set; }
        /// <summary>
        /// 业务返回码
        /// </summary>
        [TradeField("sub_code")]
        public string SubCode
        { get; set; }
        /// <summary>
        /// 业务返回码描述
        /// </summary>
        [TradeField("sub_msg")]
        public string SubMessage
        { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [TradeField("sign", IsRequire = true)]
        public string Sign
        { get; set; }

        /// <summary>
        /// JSON转实体类
        /// </summary>
        public void JsonToEntity(string json)
        {
            var type = this.GetType();
            var valueDic = Json.JsonParser.FromJson(json);
            var signContent = "";
            var sign = "";
            foreach (var pari in valueDic)
            {
                if (pari.Key.ToLower() == "sign")
                {
                    sign = pari.Value.ToString();
                }
                if (pari.Key.ToLower() != "sign")
                {
                    signContent = Json.JsonParser.ToJson((pari.Value as IDictionary<string, object>));
                    BiuldEntity(signContent, this);
                }
            }
            if (Code == "10000")
            {
                if (!RSACheckContent(signContent, sign, "utf-8", "RSA"))
                {
                    throw new AlipayPayCoreException("返回报文验证失败");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="backEntity"></param>
        /// <returns></returns>
        public void BiuldEntity(string json, AlipayPayBackParameters backEntity)
        {
            var valueDic = Json.JsonParser.FromJson(json);
            var type = backEntity.GetType();
            foreach (var pro in type.GetProperties())
            {
                foreach (var att in pro.GetCustomAttributes(false))
                {
                    if (att is TradeFieldAttribute)
                    {
                        var atts = att as TradeFieldAttribute;
                        if (pro.PropertyType.IsConstructedGenericType)
                        {
                            object value;
                            valueDic.TryGetValue(atts.Name, out value);
                            if (value != null && (value as IList).Count > 0)
                            {
                                var proValue = Activator.CreateInstance(pro.PropertyType) as IList;
                                foreach (IDictionary<string,object> itemJson in (value as IList))
                                {
                                    var proItemType = pro.PropertyType.GetGenericArguments()[0];
                                    var item = Activator.CreateInstance(proItemType) as AlipayPayBackParameters;
                                    BiuldEntity(Json.JsonParser.ToJson(itemJson), item);
                                    proValue.Add(item);
                                }
                                pro.SetValue(backEntity, proValue);
                            }
                        }
                        else
                        {
                            object value;
                            valueDic.TryGetValue(atts.Name, out value);
                            if (value != null)
                            {
                                pro.SetValue(backEntity, Convert.ChangeType(value, pro.PropertyType));
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 验证返回答名与值
        /// </summary>
        /// <param name="signContent"></param>
        /// <param name="sign"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        bool RSACheckContent(string signContent, string sign, string charset, string signType)
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
                    var rsa = new RSACryptoServiceProvider();
                    rsa.PersistKeyInCsp = false;
                    RSACryptoServiceProviderExtension.LoadPublicKeyPEM(rsa, sPublicKeyPEM);
                    var sha1 = SHA1.Create();
                    var signBase64 = Convert.FromBase64String(sign);
                    var bVerifyResultOriginal = rsa.VerifyData(Encoding.GetEncoding(charset).GetBytes(signContent), sha1, signBase64);

                    return bVerifyResultOriginal;
                }
            }
            catch
            {
                return false;
            }

        }
    }
}
