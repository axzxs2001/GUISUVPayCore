using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 微支付回调实体类
    /// </summary>
    [Trade("回到地址：可配置的",RequireCertificate =false)]
   public  class Notify:WeiXinPayParameters
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
        /// 用户标识
        /// </summary>
        [TradeField("open_id", Length = 128, IsRequire = true)]
        public string OpenID { get; set; }
        /// <summary>
        /// 是否关注公众号
        /// </summary>
        [TradeField("is_subscribe", Length = 1, IsRequire = false)]
        public string IsSubscribe { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        [TradeField("trade_type", Length = 16, IsRequire = true)]
        public string TradeType { get; set; }
        /// <summary>
        /// 付款银行
        /// </summary>
        [TradeField("bank_type", Length = 16, IsRequire = true)]
        public string BankType { get; set; }
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
        [TradeField("fee_type", Length = 8, IsRequire = false)]
        public string FeeType { get; set; }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        [TradeField("cash_fee", IsRequire = true)]
        public int CashFee { get; set; }
        /// <summary>
        /// 现金支付货币类型
        /// </summary>
        [TradeField("cash_fee_type", Length = 16, IsRequire = false)]
        public string CashFeeType { get; set; }
        /// <summary>
        /// 总代金卷金额
        /// </summary>
        [TradeField("coupon_fee",IsRequire =false)]
        public string CouponFee { get; set; }
        /// <summary>
        /// 代金卷使用数量
        /// </summary>
        [TradeField("coupon_count", IsRequire = false)]
        public int CouponCount { get; set; }
        /// <summary>
        /// 代金卷类型
        /// </summary>
        [TradeField("coupon_type_$n", Length = 8, IsRequire = false)]
        public string CouponTypeSn { get; set; }
        /// <summary>
        /// 代金卷ID
        /// </summary>
        [TradeField("coupon_id_$n", Length = 20, IsRequire = false)]
        public string CouponIDSn { get; set; }
        /// <summary>
        /// 单个代金卷支付金额
        /// </summary>
        [TradeField("coupon_fee_$n", IsRequire = false)]
        public int CouponFeeSn { get; set; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        [TradeField("transaction_id", Length = 32, IsRequire = true)]
        public string TransactionID { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        [TradeField("out_trade_no", Length = 32, IsRequire = true)]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 商家数据包
        /// </summary>
        [TradeField("attach", Length = 128, IsRequire = false)]
        public string Attach { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        [TradeField("time_end", Length = 14, IsRequire = true)]
        public string TimeEnd { get; set; }
    }
}
