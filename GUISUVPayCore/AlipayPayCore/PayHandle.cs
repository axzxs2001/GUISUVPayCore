using AlipayPayCore.Entity;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Reflection.Emit;
namespace AlipayPayCore
{
    public class PayHandle
    { /// <summary>
      /// 发送交易
      /// </summary>
      /// <param name="parmeter">发送交易实体</param>
      /// <returns></returns>
        public AlipayPayBackParameters Send(AlipayPayParameters parmeter)
        {
            var charset = "utf-8";
            var type = parmeter.GetType().GetTypeInfo();
            var url = "https://openapi.alipay.com/gateway.do?charset="+ charset;
            var content = parmeter.ToString();
            var result = Request(url, content, charset);
            //json转实体
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var backEntity = Activator.CreateInstance(assembly.GetType($"{type.FullName}Back")) as AlipayPayBackParameters;
            backEntity.JsonToEntity(result);
            return backEntity;
        }

        string Request(string url, string content, string charset)
        {
            //发送数据
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContinueTimeout = 1000;
            request.ContentType = $"application/x-www-form-urlencoded;charset={charset}";
            var postData = Encoding.GetEncoding(charset).GetBytes(content);
            var requestStream = request.GetRequestStreamAsync().Result;
            requestStream.Write(postData, 0, postData.Length);
            requestStream.Dispose();
            //接收数据
            var rsp = (HttpWebResponse)request.GetResponseAsync().Result;
            var dataArr = new byte[rsp.ContentLength];
            var responseSteam = rsp.GetResponseStream();
            responseSteam.Read(dataArr, 0, dataArr.Length);
            responseSteam.Dispose();
            var result = Encoding.GetEncoding(charset).GetString(dataArr);
            return result;

        }

    }
}
