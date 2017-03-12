using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Trade("https://api.mch.weixin.qq.com/pay/unifiedorder")]
    public class Unifiedorder:CommonParameters
    {
        /// <summary>
        /// 商品描述
        /// </summary>
        [TradeField("body", Length = 128, IsRequire = true)]
        public string Body
        { get; set; }
        /// <summary>
        /// 商品详情
        /// </summary>
        [TradeField("detail", Length = 6000)]
        public string Detail
        { get; set; }
        /// <summary>
        /// 商品详情
        /// </summary>
        [TradeField("attach", Length = 127)]
        public string Attach
        { get; set; }
        /// <summary>
        /// 商品详情
        /// </summary>
        [TradeField("out_trade_no", Length = 127)]
        public string OutTradeNo
        { get; set; }
    }
}
