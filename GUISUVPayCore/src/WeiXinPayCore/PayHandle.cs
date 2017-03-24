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

                    var assembly = this.GetType().GetTypeInfo().Assembly;
                    var backEntity = Activator.CreateInstance(assembly.GetType($"{type.FullName}Back")) as WeiXinPayBackParameters;
                     backEntity.XMLToEntity(result, backEntity);
                    return backEntity;
                }
            }

            return null;

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
                    var assembly = this.GetType().GetTypeInfo().Assembly;
                    var backEntity = Activator.CreateInstance(assembly.GetType($"{this.GetType().FullName}Back")) as WeiXinPayBackParameters;
                    backEntity.XMLToEntity(result, backEntity);
                    return backEntity;
                }
            }
            return null;

        }
    }
}
