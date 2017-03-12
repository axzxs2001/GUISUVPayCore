using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore
{
    /// <summary>
    /// 微信支付异常类
    /// </summary>
    public class WeiXinPayCoreException:Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        public WeiXinPayCoreException(string message):base(message)
        {

        }
    }
}
