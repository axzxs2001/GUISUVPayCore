using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 申请退款返回实体
    /// </summary>
    class RefundBack:WeiXinPayBackParameters
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        [TradeField("return_code", Length = 16, IsRequire = true)]
        public string ReturnCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        [TradeField("return_msg", Length = 128, IsRequire = false)]
        public string ReturnMsg { get; set; }
        /// <summary>
        /// 业务结果
        /// </summary>
        [TradeField("result_code", Length = 16, IsRequire = true)]
        public string ResultCode { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        [TradeField("err_code", Length = 32, IsRequire = false)]
        public string ErrCode { get; set; }
        /// <summary>
        /// 错误代码描述
        /// </summary>
        [TradeField("err_code_des", Length = 128, IsRequire = false)]
        public string ErrCodeDes { get; set; }
        /// <summary>
        /// 微信订单号
        /// </summary>
        [TradeField("transaction_id", Length = 28, IsRequire = true)]
        public string TransactionID { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        [TradeField("out_trade_no", Length = 32, IsRequire = true)]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 商户退款单号
        /// </summary>
        [TradeField("out_refund_no", Length = 32, IsRequire = true)]
        public string OutRefundNo { get; set; }
        /// <summary>
        /// 微信退款单号
        /// </summary>
        [TradeField("refund_id",Length =28,IsRequire =true)]
        public string RefundID { get; set; }
        /// <summary>
        /// 退款渠道
        /// </summary>
        [TradeField("refund_channel",Length =16,IsRequire =false)]
        public string RefundChannel { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        [TradeField("refund_fee", IsRequire = true)]
        public int RefundFee { get; set; }
        /// <summary>
        /// 应结退款金额
        /// </summary>
        [TradeField("settlement_refund_fee",IsRequire =false)]
        public string SettlementRefund { get; set; }
        /// <summary>
        /// 标价金额（单位：分）
        /// </summary>
        [TradeField("total_fee", IsRequire = true)]
        public int TotalFee { get; set; }
        /// <summary>
        /// 应结订单金额
        /// </summary>
        [TradeField("settlement_total_fee", IsRequire = false)]
        public string SettlementTotal_Fee { get; set; }
        /// <summary>
        /// 标价币种
        /// </summary>
        [TradeField("fee_type", Length = 8, IsRequire = false)]
        public string FeeType { get; set; }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        [TradeField("cash_fee", IsRequire = true)]
        public int CashFee { get; set; }
        /// <summary>
        /// 现金支付币种
        /// </summary>
        [TradeField("cash_fee_type", Length = 16, IsRequire = false)]
        public string CashFeeType { get; set; }
        /// <summary>
        /// 现金退款金额
        /// </summary>
        [TradeField("cash_refund_fee",IsRequire =false)]
        public int CashRefundFee { get; set; }
        /// <summary>
        /// 代金卷类型
        /// </summary>
        [TradeField("coupon_type_$n", Length = 8, IsRequire = false)]
        public string CouponTypeSn { get; set; }
        /// <summary>
        /// 代金卷退款总金额
        /// </summary>
        [TradeField("coupon_refund_fee",IsRequire =false)]
        public string CouponRefundFee { get; set; }
        /// <summary>
        /// 单个代金卷退款金额
        /// </summary>
        [TradeField("coupon_refund_fee_$n",IsRequire =false)]
        public int CouponRefundFeeSn { get; set; }
        /// <summary>
        /// 退款代金券使用数量
        /// </summary>
        [TradeField("coupon_refund_count",IsRequire =false)]
        public int CouponRefundCount { get; set; }
        /// <summary>
        /// 代金卷ID
        /// </summary>
        [TradeField("coupon_id_$n", Length = 20, IsRequire = false)]
        public string CouponIDSn { get; set; }
    }
}
