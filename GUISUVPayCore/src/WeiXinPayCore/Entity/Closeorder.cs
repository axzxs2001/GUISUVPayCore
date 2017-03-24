using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 关闭订单实体
    /// </summary>
    [Trade("https://api.mch.weixin.qq.com/pay/closeorder ",RequireCertificate =false)]
    public class CloseOrder:WeiXinPayParameters
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        [TradeField("out_trade_no",Length =32,IsRequire =true)]
        public string OutTradeNO { get; set; }
    }
}
