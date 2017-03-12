using System;
using System.Collections.Generic;
using System.Text;
using WeiXinPayCore.Entity;
using System.Reflection;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;

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
                    HttpClient client=null;
                    //有证书
                    if (attr.RequireCertificate)
                    {
                        //证书路径
                        var certificatePathPro = type.GetProperty("CertificatePath");
                        var certificatePath= certificatePathPro.GetValue(parmeter).ToString();
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
            return backEntity;
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
                    var response =await client.PostAsync(url, new System.Net.Http.StringContent(parmeter.ToXML()));
                    var result =await response.Content.ReadAsStringAsync();
                    return XMLToEntity(result, type);
                }
            }
            return null;

        }
    }
}
