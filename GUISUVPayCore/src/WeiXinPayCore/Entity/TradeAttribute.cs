using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 交易类型特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TradeAttribute : Attribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="url">接口链接</param>
        public TradeAttribute(string url)
        {
            URL = url;
        }
        /// <summary>
        /// 接口链接
        /// </summary>
        public string URL
        { get; private set; }

        /// <summary>
        /// 是否需要证书
        /// </summary>
        public bool RequireCertificate
        { get; set; }
    }
}
