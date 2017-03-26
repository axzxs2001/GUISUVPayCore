using System;
using System.Collections.Generic;
using System.Text;

namespace AlipayPayCore.Entity
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
        /// <param name="method">接口链接</param>
        public TradeAttribute(string method)
        {
            Method = method;
        }
        /// <summary>
        /// 接口链接
        /// </summary>
        public string Method
        { get; private set; }

        /// <summary>
        /// 是否需要证书
        /// </summary>
        public bool RequireCertificate
        { get; set; }
    }
}
