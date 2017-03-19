using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 申请退款
    /// </summary>
    [Trade("https://api.mch.weixin.qq.com/secapi/pay/refund",RequireCertificate =true)]
    class Refund:WeiXinPayParameters
    {
        /// <summary>
        /// 微信订单号（与商户订单号二选一）
        /// </summary>
        [TradeField("transaction_id", Length = 28, IsRequire = true)]
        public string TransactionID { get; set; }
        /// <summary>
        /// 商户订单号（与微信订单号二选一）
        /// </summary>
        [TradeField("out_trade_no", Length = 32, IsRequire = true)]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 商户退款单号
        /// </summary>
        [TradeField("out_refund_no",Length =32,IsRequire =true)]
        public string OutRefundNo { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        [TradeField("total_fee",IsRequire =true)]
        public int TotalFee { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        [TradeField("refund_fee",IsRequire =true)]
        public int RefundFee { get; set; }
        /// <summary>
        /// 货币种类
        /// </summary>
        [TradeField("refund_fee_type",Length =8,IsRequire =false)]
        public string RefundFeeType { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        [TradeField("op_user_id",Length =32,IsRequire =true)]
        public string OpUserID { get; set; }
        /// <summary>
        /// 退款资金来源
        /// </summary>
        [TradeField("refund_account",Length =30,IsRequire =false)]
        public string RefundAccount { get; set; }
    }
}
