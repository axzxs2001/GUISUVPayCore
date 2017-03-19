using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 查询退款返回实体
    /// </summary>
    class RefundQueryBack:WeiXinPayBackParameters
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
        [TradeField("transaction_id", Length = 32, IsRequire = true)]
        public string TransactionID { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        [TradeField("out_trade_no", Length = 32, IsRequire = true)]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        [TradeField("total_fee", IsRequire = true)]
        public int TotalFee { get; set; }
        /// <summary>
        /// 应结订单金额
        /// </summary>
        [TradeField("settlement_total_fee", IsRequire = false)]
        public string SettlementTotal_Fee { get; set; }
        /// <summary>
        /// 货币种类
        /// </summary>
        [TradeField("refund_fee_type", Length = 8, IsRequire = false)]
        public string RefundFeeType { get; set; }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        [TradeField("cash_fee", IsRequire = true)]
        public int CashFee { get; set; }
        /// <summary>
        /// 退款笔数
        /// </summary>
        [TradeField("refund_count",IsRequire =true)]
        public int RefundCount { get; set; }
        /// <summary>
        /// 商户退款单号
        /// </summary>
        [TradeField("out_refund_no_$n",Length =32,IsRequire =true)]
        public string OutRefundNoSn { get; set; }
        /// <summary>
        /// 微信退款单号
        /// </summary>
        [TradeField("refund_id_$n",Length =28,IsRequire =true)]
        public string RefundIDSn { get; set; }
        /// <summary>
        /// 退款渠道
        /// </summary>
        [TradeField("refund_channel_$n",Length =16,IsRequire =false)]
        public string RefundChannelSn { get; set; }
        /// <summary>
        /// 申请退款金额
        /// </summary>
        [TradeField("refund_fee_$n",IsRequire =true)]
        public int RefundFeeSn { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        [TradeField("settlement_refund_fee_$n",IsRequire =false)]
        public int SettlementRefundFeeSn { get; set; }
        /// <summary>
        /// 代金卷类型
        /// </summary>
        [TradeField("coupon_type_$n", Length = 8, IsRequire = false)]
        public string CouponTypeSn { get; set; }
        /// <summary>
        /// 总代金卷退款金额
        /// </summary>
        [TradeField("coupon_refund_fee_$n", IsRequire = false)]
        public int CouponRefundFeeSn { get; set; }
        /// <summary>
        /// 退款代金券使用数量
        /// </summary>
        [TradeField("coupon_refund_count_$n", IsRequire = false)]
        public int CouponRefundCountSn { get; set; }
        /// <summary>
        /// 退款代金卷ID
        /// </summary>
        [TradeField("coupon_refund_id_$n", Length = 20, IsRequire = false)]
        public string CouponIDSn { get; set; }
        /// <summary>
        /// 单个代金卷退款金额
        /// </summary>
        [TradeField("coupon_refund_fee_$n_$m", IsRequire = false)]
        public int CouponRefundFeeSnSm { get; set; }
        /// <summary>
        /// 退款状态
        /// </summary>
        [TradeField("refund_status_$n",Length =16,IsRequire =true)]
        public string RefundStatusSn { get; set; }
        /// <summary>
        /// 退款资金来源
        /// </summary>
        [TradeField("refund_account_$n",Length =30,IsRequire =false)]
        public string RefundAccountSn { get; set; }
        /// <summary>
        /// 退款入账账户
        /// </summary>
        [TradeField("refund_recv_accout_$n",Length =64,IsRequire =true)]
        public string RefundRecvAccoutSn { get; set; }
        /// <summary>
        /// 退款成功时间
        /// </summary>
        [TradeField("refund_account_$n",Length =20,IsRequire =false)]
        public string RefundAccoutSn { get; set; }
    }
}
