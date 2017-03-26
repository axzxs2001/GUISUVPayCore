using System;
using System.Collections.Generic;
using System.Text;

namespace AlipayPayCore.Entity
{
    public class Precreate
    { /// <summary>
      /// 商户订单号,64个字符以内、只能包含字母、数字、下划线；需保证在商户端不重复
      /// </summary>
        [TradeField("out_trade_no", Length = 64, IsRequire = true)]
        public string OutTradeNo
        { get; set; }
        /// <summary>
        /// 卖家支付宝用户ID。 如果该值为空，则默认为商户签约账号对应的支付宝用户ID
        /// </summary>
        [TradeField("seller_id ", Length = 28, IsRequire = false)]
        public string SellerId
        { get; set; }
        /// <summary>
        ///订单总金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000] 如果同时传入了【打折金额】，【不可打折金额】，【订单总金额】三者，则必须满足如下条件：【订单总金额】=【打折金额】+【不可打折金额】 
        /// </summary>
        [TradeField("total_amount", Length = 11, IsRequire = true)]
        public decimal TotalAmount
        { get; set; }
        /// <summary>
        ///可打折金额. 参与优惠计算的金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000] 如果该值未传入，但传入了【订单总金额】，【不可打折金额】则该值默认为【订单总金额】-【不可打折金额】
        /// </summary>
        [TradeField("discountable_amount", Length = 11, IsRequire = false)]
        public string DiscountableAmount
        { get; set; }
        /// <summary>
        ///不可打折金额. 不参与优惠计算的金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000] 如果该值未传入，但传入了【订单总金额】,【打折金额】，则该值默认为【订单总金额】-【打折金额】
        /// </summary>
        [TradeField("undiscountable_amount", Length = 11, IsRequire = false)]
        public decimal UndiscountableAmount
        { get; set; }
        /// <summary>
        ///买家支付宝账号
        /// </summary>
        [TradeField("buyer_logon_id", Length = 100, IsRequire = true)]
        public string BuyerLogonId
        { get; set; }
        /// <summary>
        ///订单标题
        /// </summary>
        [TradeField("subject ", Length = 256, IsRequire = true)]
        public string Subject 
        { get; set; }
        /// <summary>
        ///对交易或商品的描述
        /// </summary>
        [TradeField("body", Length = 128, IsRequire = false)]
        public string Body 
        { get; set; }
        /// <summary>
        ///订单包含的商品列表信息.Json格式. 其它说明详见：“商品明细说明
        /// </summary>
        [TradeField("goods_detail ", Length = 11, IsRequire = false)]
        public List<GoodsDetail> GoodsDetail 
        { get; set; }
        /// <summary>
        ///商户操作员编号
        /// </summary>
        [TradeField("operator_id", Length = 28, IsRequire = false)]
        public string OperatorId
        { get; set; }
        /// <summary>
        ///商户门店编号 
        /// </summary>
        [TradeField("store_id ", Length = 32, IsRequire = false)]
        public string StoreId
        { get; set; }
        /// <summary>
        ///商户机具终端编号 
        /// </summary>
        [TradeField("terminal_id", Length = 32, IsRequire = false)]
        public string TerminalId
        { get; set; }
        /// <summary>
        ///业务扩展参数 
        /// </summary>
        [TradeField("extend_params  ", Length = 0, IsRequire = false)]
        public List<ExtendParams> ExtendParams
        { get; set; }
        /// <summary>
        ///该笔订单允许的最晚付款时间，逾期将关闭交易。取值范围：1m～15d。m-分钟，h-小时，d-天，1c-当天（1c-当天的情况下，无论交易何时创建，都在0点关闭）。 该参数数值不接受小数点， 如 1.5h，可转换为 90m
        /// </summary>
        [TradeField("timeout_express", Length = 6, IsRequire = false)]
        public string TimeoutExpress
        { get; set; }
        /// <summary>
        ///描述分账信息，json格式。 
        /// </summary>
        [TradeField("royalty_info ", Length = 0, IsRequire = false)]
        public List<RoyaltyInfo> RoyaltyInfo 
        { get; set; }
     

    }
}
