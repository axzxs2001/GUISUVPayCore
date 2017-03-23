using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 统一下单实体类
    /// </summary>
    [Trade("https://api.mch.weixin.qq.com/pay/unifiedorder", RequireCertificate = false)]
    public class UnifiedOrder : WeiXinPayParameters
    {
        /// <summary>
        /// 商品描述
        /// </summary>
        [TradeField("body", Length = 128, IsRequire = true)]
        public string Body { get; set; }
        /// <summary>
        /// 商品详情
        /// </summary>
        [TradeField("detail", Length = 6000, IsRequire = false)]
        public string Detail { get; set; }
        /// <summary>
        /// 附加数据
        /// </summary>
        [TradeField("attach", Length = 127, IsRequire = false)]
        public string Attach { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        [TradeField("out_trade_no", Length = 32, IsRequire = true)]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 标价币种
        /// </summary>
        [TradeField("fee_type,", Length = 16, IsRequire = false)]
        public string FeeType { get; set; }
        /// <summary>
        /// 标价金额
        /// </summary>
        [TradeField("total_fee", IsRequire = true)]
        public int ToalFee { get; set; }
        /// <summary>
        /// 终端IP
        /// </summary>
        [TradeField("spbill_create_ip", Length = 16, IsRequire = true)]
        public string SpbillCreateIP { get; set; }
        /// <summary>
        /// 交易起始时间
        /// </summary>
        [TradeField("time_start", Length = 14, IsRequire = false)]
        public string TimeStart { get; set; }
        /// <summary>
        /// 交易结束时间
        /// </summary>
        [TradeField("time_expire", Length = 14, IsRequire = false)]
        public string TimeExpire { get; set; }
        /// <summary>
        /// 商品标记
        /// </summary>
        [TradeField("goods_tag", Length = 32, IsRequire = false)]
        public string GoodsTag { get; set; }
        /// <summary>
        /// 通知地址
        /// </summary>
        [TradeField("notify_url", Length = 256, IsRequire = true)]
        public string NotifyURL { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        [TradeField("trade_type", Length = 16, IsRequire = true)]
        public string TradeType { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        string _productID;
        /// <summary>
        /// 商品ID
        /// </summary>
        [TradeField("product_id", Length = 32, IsRequire = false)]
        public string ProductID
        {
            get
            {
                if (TradeType != null && TradeType.ToUpper() == "NATIVE" && string.IsNullOrEmpty(_productID))
                {
                    throw new WeiXinPayCoreException($"TradeType=NATIVE时，ProductID不能为空！");
                }
                return _productID;
            }
            set
            {
                _productID = value;
            }
        }
            /// <summary>
            /// 指定支付方式
            /// </summary>
            [TradeField("limit_pay", Length = 32, IsRequire = false)]
            public string LimitPay { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        [TradeField("oppid", Length = 128, IsRequire = false)]
        public string OpenID { get; set; }
    }
}
