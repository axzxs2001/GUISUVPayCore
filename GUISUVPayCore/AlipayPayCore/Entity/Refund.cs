using System;
using System.Collections.Generic;
using System.Text;

namespace AlipayPayCore.Entity
{
    /// <summary>
    /// 统一收单交易退款接口
    /// </summary>
    [Trade("alipay.trade.refund")]
    public class Refund:AlipayPayParameters
    {
        /// <summary>
        /// 商户订单号,64个字符以内、只能包含字母、数字、下划线；需保证在商户端不重复
        /// </summary>
        [TradeField("out_trade_no", Length = 64, IsRequire = true)]
        public string OutTradeNo
        { get; set; }

        /// <summary>
        /// 需要退款的金额，该金额不能大于订单金额,单位为元，支持两位小数
        /// </summary>
        [TradeField("refund_amount ",IsRequire =true)]
        public decimal RefundAmount
        { get; set; }
    }
}
