using System;
using System.Collections.Generic;
using System.Text;
using WeiXinPayCore.Entity;
using System.Reflection;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using System.IO;
using System.Xml;

namespace WeiXinPayCore
{
    /// <summary>
    /// 支付交易类
    /// </summary>
    public class PayHandle
    {
        /// <summary>
        /// 发送交易
        /// </summary>
        /// <param name="parmeter">发送交易实体</param>
        /// <returns></returns>
        public WeiXinPayBackParameters Send(WeiXinPayParameters parmeter)
        {
            var type = parmeter.GetType();
            foreach (var att in type.GetTypeInfo().GetCustomAttributes())
            {
                if (att is TradeAttribute)
                {
                    var attr = att as TradeAttribute;
                    HttpClient client = null;
                    //有证书
                    if (attr.RequireCertificate)
                    {
                        //证书路径
                        var certificatePathPro = type.GetProperty("CertificatePath");
                        var certificatePath = certificatePathPro.GetValue(parmeter).ToString();
                        //证书密码
                        var certificatePasswordPro = type.GetProperty("MchID");
                        var certificatePassword = certificatePasswordPro.GetValue(parmeter).ToString();
                        var handler = new HttpClientHandler();
                        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                        handler.SslProtocols = SslProtocols.Tls12;
                        handler.ClientCertificates.Add(new X509Certificate2(certificatePath, certificatePassword));
                        client = new HttpClient(handler);
                    }
                    else//无证书
                    {
                        client = new HttpClient();
                    }
                    var url = attr.URL;
                    var response = client.PostAsync(url, new System.Net.Http.StringContent(parmeter.ToXML())).Result;
                    //处理返回异常
                    if(response.StatusCode!=System.Net.HttpStatusCode.OK)
                    {
                        throw new WeiXinPayCoreException($"http通迅错误，StatusCode：{response.StatusCode}  内容：{response.RequestMessage.Content}");
                    }
                    var result = response.Content.ReadAsStringAsync().Result;

                    return XMLToEntity(result, type);
                }
            }

            return null;

        }
        /// <summary>
        /// xml字符串转实体
        /// </summary>
        /// <param name="xml">xml字符串</param>
        /// <param name="type">入参实体</param>
        /// <returns></returns>
        WeiXinPayBackParameters XMLToEntity(string xml, Type type)
        {
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var backEntity = Activator.CreateInstance(assembly.GetType($"{type.FullName}Back")) as WeiXinPayBackParameters;
           
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            if (xmlDoc.HasChildNodes)
            {
                foreach (XmlNode node in xmlDoc.ChildNodes[0].ChildNodes)
                {
                    var name = node.Name;
                    var value = node.InnerText;
                    SetEntityProperty(backEntity, name, value);
                }
            }

            return backEntity;
        }
        /// <summary>
        /// 把value赋给实体中属性的特性等于name的属性
        /// </summary>
        /// <param name="backParameters">实体</param>
        /// <param name="name">名称</param>
        /// <param name="value">值 </param>
        void SetEntityProperty(WeiXinPayBackParameters backParameters,string name,string value)
        {
            if(!string.IsNullOrEmpty(name)&&!string.IsNullOrEmpty(value))
            {
                var entityType = backParameters.GetType();
                foreach (var pro in entityType.GetProperties())
                {
                    foreach (var att in pro.GetCustomAttributes(false))
                    {
                        if (att is TradeFieldAttribute)
                        {
                            var attr = att as TradeFieldAttribute;
                            if(attr.Name==name && pro.CanWrite)
                            {
                               
                                pro.SetValue(backParameters, Convert.ChangeType(value, pro.PropertyType));
                            }
                            break;
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 异步发送交易
        /// </summary>
        /// <param name="parmeter">发送交易实体</param>
        /// <returns></returns>
        public async Task<WeiXinPayBackParameters> SendAsync(WeiXinPayParameters parmeter)
        {
            var type = parmeter.GetType();
            foreach (var att in type.GetTypeInfo().GetCustomAttributes())
            {
                if (att is TradeAttribute)
                {
                    var attr = att as TradeAttribute;
                    HttpClient client = null;
                    //有证书
                    if (attr.RequireCertificate)
                    {
                        //证书路径
                        var certificatePathPro = type.GetProperty("CertificatePath");
                        var certificatePath = certificatePathPro.GetValue(parmeter).ToString();
                        //证书密码
                        var certificatePasswordPro = type.GetProperty("MchID");
                        var certificatePassword = certificatePasswordPro.GetValue(parmeter).ToString();
                        var handler = new HttpClientHandler();
                        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                        handler.SslProtocols = SslProtocols.Tls12;
                        handler.ClientCertificates.Add(new X509Certificate2(certificatePath, certificatePassword));
                        client = new HttpClient(handler);
                    }
                    else//无证书
                    {
                        client = new HttpClient();
                    }
                    var url = attr.URL;
                    var response = await client.PostAsync(url, new System.Net.Http.StringContent(parmeter.ToXML()));
                    var result = await response.Content.ReadAsStringAsync();
                    return XMLToEntity(result, type);
                }
            }
            return null;

        }
    }
}
