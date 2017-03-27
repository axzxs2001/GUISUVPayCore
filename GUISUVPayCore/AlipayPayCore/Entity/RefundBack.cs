using System;
using System.Collections.Generic;
using System.Text;

namespace AlipayPayCore.Entity
{
    /// <summary>
    /// 统一收单交易退款接口返回值 
    /// </summary>
    public class RefundBack : AlipayPayBackParameters
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        [TradeField("out_trade_no", Length = 64, IsRequire = true)]
        public string OutTradeNo
        { get; set; }
    }
}
