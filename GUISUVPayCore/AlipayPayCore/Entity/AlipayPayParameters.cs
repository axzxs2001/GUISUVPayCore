using System;
using System.Collections.Generic;
using System.Text;

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
        [TradeField("app_id", Length = 32, IsRequire = true)]
        public string AppID
        { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        [TradeField("method", Length = 128, IsRequire = true)]
        public abstract string Method
        { get; set; }
        /// <summary>
        /// 仅支持JSON
        /// </summary>
        [TradeField("format", Length = 40, IsRequire = false)]
        public string Format
        { get; set; }
        /// <summary>
        /// 请求使用的编码格式，如utf-8,gbk,gb2312等
        /// </summary>
        [TradeField("charset", Length = 10, IsRequire = true)]
        public string Charset
        { get; set; }
        /// <summary>
        /// 商户生成签名字符串所使用的签名算法类型，目前支持RSA2和RSA，推荐使用RSA2
        /// </summary>
        [TradeField("sign_type", Length = 10, IsRequire = true)]
        public string SignType 
        { get; set; }
        /// <summary>
        /// 商户请求参数的签名串，详见签名
        /// </summary>
        [TradeField("sign", Length = 256, IsRequire = true)]
        public string Sign
        { get; set; }
        /// <summary>
        /// 发送请求的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        [TradeField("timestamp", Length = 19, IsRequire = true)]
        public string Timestamp
        { get; set; }
        /// <summary>
        /// 调用的接口版本，固定为：1.0
        /// </summary>
        [TradeField("version", Length = 3, IsRequire = true)]
        public string Version
        { get; set; }
        /// <summary>
        /// 支付宝服务器主动通知商户服务器里指定的页面http/https路径。
        /// </summary>
        [TradeField("notify_url", Length = 256, IsRequire = false)]
        public string NotifyUrl
        { get; set; }
        /// <summary>
        /// 详见应用授权概述
        /// </summary>
        [TradeField("app_auth_token", Length = 40, IsRequire = false)]
        public string AppAuthToken
        { get; set; }
        /// <summary>
        /// 支付宝服务器主动通知商户服务器里指定的页面http/https路径。
        /// </summary>
        [TradeField("biz_content", Length = 0, IsRequire = true)]
        public string BizContent
        { get; set; }
       
        /// <summary>
        /// 转JSON
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return null;
        }

    }
}
