using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 查询订单交易
    /// </summary>
    [Trade("https://api.mch.weixin.qq.com/pay/orderquery", RequireCertificate = false)]
   public class OrderQuery:WeiXinPayParameters
    {
        /// <summary>
        /// 微信订单号（与商户订单号二选一）
        /// </summary>
        [TradeField("transaction_id", Length = 32, IsRequire = true)]
        public string TransactionID { get; set; }
        /// <summary>
        /// 商户订单号（与微信订单号二选一）
        /// </summary>
        [TradeField("out_trade_no",Length =32,IsRequire =true)]
        public string OutTradeNo { get; set; }
    }
}
