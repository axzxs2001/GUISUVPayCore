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
        [TradeField("out_trade_no", Length = 64, IsRequire = false)]
        public string OutTradeNo
        { get; set; }

        /// <summary>
        /// 支付宝交易号，和商户订单号不能同时为空
        /// </summary>
        [TradeField("trade_no", Length = 64, IsRequire = false)]
        public string TradeNo
        { get; set; }

        /// <summary>
        /// 需要退款的金额，该金额不能大于订单金额,单位为元，支持两位小数
        /// </summary>
        [TradeField("refund_amount",Length =9,IsRequire =true)]
        public decimal RefundAmount
        { get; set; }

        /// <summary>
        /// 退款的原因说明
        /// </summary>
        [TradeField("refund_reason", Length = 256)]
        public string RefundReason
        { get; set; }

        /// <summary>
        /// 标识一次退款请求，同一笔交易多次退款需要保证唯一，如需部分退款，则此参数必传。
        /// </summary>
        [TradeField("out_request_no", Length = 64)]
        public string OutRequestNo
        { get; set; }
        /// <summary>
        /// 商户的操作员编号
        /// </summary>
        [TradeField("operator_id", Length = 30)]
        public string OperatorId
        { get; set; }
        /// <summary>
        ///商户的门店编号
        /// </summary>
        [TradeField("store_id", Length = 32)]
        public string Store_Id
        { get; set; }
        /// <summary>
        ///商户的终端编号
        /// </summary>
        [TradeField("terminal_id", Length = 32)]
        public string TerminalId
        { get; set; }
    }
}
