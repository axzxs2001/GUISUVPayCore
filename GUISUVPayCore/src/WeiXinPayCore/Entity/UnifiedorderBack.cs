using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 统一下单返回实体类
    /// </summary>
    public class UnifiedOrderBack: WeiXinPayBackParameters
    {       

        /// <summary>
        ///预支付交易会话标识
        /// </summary>
        [TradeField("prepay_id", Length = 64, IsRequire = true)]
        public string PrepayID { get; set; }
        /// <summary>
        /// 二维码链接
        /// </summary>
        [TradeField("code_url",Length =64,IsRequire =false)]
        public string CodeURL { get; set; }


    }
}
