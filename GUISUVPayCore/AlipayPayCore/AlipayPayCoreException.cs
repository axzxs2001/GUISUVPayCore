using System;
using System.Collections.Generic;
using System.Text;

namespace AlipayPayCore
{
    /// <summary>
    /// 支付宝支付异常类
    /// </summary>
    public class AlipayPayCoreException:Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        public AlipayPayCoreException(string message):base(message)
        {

        }
    }
}
