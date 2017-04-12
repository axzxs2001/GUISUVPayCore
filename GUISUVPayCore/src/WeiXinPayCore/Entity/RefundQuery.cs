using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    [Trade("https://api.mch.weixin.qq.com/pay/orderquery",RequireCertificate =false)]
   public class RefundQuery:WeiXinPayParameters
    {
        /// <summary>
        /// 微信订单号（微信订单号,商户订单号,商户退款单号,微信退款单号四选一）
        /// </summary>
        [TradeField("transaction_id", Length = 32)]
        public string TransactionID { get; set; }
        /// <summary>
        /// 商户订单号（微信订单号,商户订单号,商户退款单号,微信退款单号四选一）
        /// </summary>
        [TradeField("out_trade_no", Length = 32)]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 商户退款单号（微信订单号,商户订单号,商户退款单号,微信退款单号四选一）
        /// </summary>
        [TradeField("out_refund_no", Length = 32)]
        public string OutRefundNo { get; set; }
        /// <summary>
        /// 微信退款单号（微信订单号,商户订单号,商户退款单号,微信退款单号四选一）
        /// </summary>
        [TradeField("refund_id", Length = 28)]
        public string RefundID { get; set; }
    }
}
