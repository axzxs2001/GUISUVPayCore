using System;
using System.Collections.Generic;
using System.Text;

namespace AlipayPayCore.Entity
{
    /// <summary>
    /// 统一收单线下交易查询返回值 
    /// </summary>
    public class QueryBack : AlipayPayBackParameters
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        [TradeField("out_trade_no", Length = 64)]
        public string OutTradeNo
        { get; set; }
        /// <summary>
        /// 支付宝交易号，和商户订单号不能同时为空
        /// </summary>
        [TradeField("trade_no", Length = 64)]
        public string TradeNo
        { get; set; }
        /// <summary>
        /// 买家支付宝用户号，该参数已废弃，请不要使用 
        /// </summary>
        [TradeField("open_id", Length = 32)]
        public string OpenId
        { get; set; }

        /// <summary>
        /// 用户的登录id 
        /// </summary>
        [TradeField("buyer_logon_id", Length = 100)]
        public string BuyerLogonId
        { get; set; }

        /// <summary>
        /// 交易状态：WAIT_BUYER_PAY（交易创建，等待买家付款）、TRADE_CLOSED（未付款交易超时关闭，或支付完成后全额退款）、TRADE_SUCCESS（交易支付成功）、TRADE_FINISHED（交易结束，不可退款） 
        /// </summary>
        [TradeField("trade_status", Length = 32)]
        public string TradeStatus
        { get; set; }
        /// <summary>
        ///交易的订单金额，单位为元，两位小数。该参数的值为支付时传入的total_amount 
        /// </summary>
        [TradeField("total_amount", Length = 11)]
        public decimal TotalAmount
        { get; set; }

        /// <summary>
        ///实收金额，单位为元，两位小数。该金额为本笔交易，商户账户能够实际收到的金额 
        /// </summary>
        [TradeField("receipt_amount", Length = 11)]
        public decimal ReceiptAmount
        { get; set; }
        /// <summary>
        ///买家实付金额，单位为元，两位小数。该金额代表该笔交易买家实际支付的金额，不包含商户折扣等金额 
        /// </summary>
        [TradeField("buyer_pay_amount", Length = 11)]
        public decimal BuyerPayAmount
        { get; set; }
    }
}
