using System;
using System.Collections.Generic;
using System.Text;

namespace AlipayPayCore.Entity
{
    /// <summary>
    /// 交易字段特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TradeFieldAttribute : Attribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">名称</param>
        public TradeFieldAttribute(string name)
        {
            Name = name;
        }
        /// <summary>
        /// 交易字段名称
        /// </summary>
        public string Name
        {
            get;
            private set;
        }
        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length
        { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequire
        { get; set; }

        /// <summary>
        /// 是否为公共参数
        /// </summary>
    
        public bool IsPublic
        { get; set; }

   
    }
}
