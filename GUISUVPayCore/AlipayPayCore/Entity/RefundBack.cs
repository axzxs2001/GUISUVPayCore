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
        /// <summary>
        /// 买家支付宝用户号，该参数已废弃，请不要使用 
        /// </summary>
        [TradeField("open_id", Length = 32, IsRequire = true)]
        public string OpenId
        { get; set; }

        /// <summary>
        /// 用户的登录id 
        /// </summary>
        [TradeField("buyer_logon_id", Length = 1000, IsRequire = true)]
        public string BuyerLogonId
        { get; set; }

        /// <summary>
        /// 本次退款是否发生了资金变化  
        /// </summary>
        [TradeField("fund_change", Length = 1, IsRequire = true)]
        public string FundChange
        { get; set; }

        /// <summary>
        /// 退款总金额   
        /// </summary>
        [TradeField("refund_fee",IsRequire = true)]
        public decimal RefundFee
        { get; set; }


        /// <summary>
        /// 退款支付时间    
        /// </summary>
        [TradeField("gmt_refund_pay",Length =32, IsRequire = true)]
        public string GmtRefundPay
        { get; set; }

        /// <summary>
        /// 买家在支付宝的用户id    
        /// </summary>
        [TradeField("buyer_user_id", Length = 28, IsRequire = true)]
        public string BuyerUserId
        { get; set; }

    }
}
