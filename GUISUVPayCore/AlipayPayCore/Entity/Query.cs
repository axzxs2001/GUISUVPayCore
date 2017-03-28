using System;
using System.Collections.Generic;
using System.Text;

namespace AlipayPayCore.Entity
{
    /// <summary>
    /// 统一收单线下交易查询
    /// </summary>
    [Trade("alipay.trade.query")]
    public class Query:AlipayPayParameters
    {
        /// <summary>
        /// 商户订单号,64个字符以内、只能包含字母、数字、下划线；需保证在商户端不重复
        /// </summary>
        [TradeField("out_trade_no", Length = 64, IsRequire = false)]
        public string OutTradeNo
        { get; set; }

        /// <summary>
        /// 支付宝交易号，和商户订单号不能同时为空
        /// </summary>
        [TradeField("trade_no", Length = 64, IsRequire = false)]
        public string TradeNo
        { get; set; }
    }
}
