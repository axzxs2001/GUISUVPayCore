using System;
using System.Collections.Generic;
using System.Text;

namespace AlipayPayCore.Entity
{
    /// <summary>
    /// 统一收单线下交易预创建 返回值 
    /// </summary>
    public class PrecreateBack:AlipayPayBackParameters
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        [TradeField("out_trade_no", Length = 64, IsRequire = true)]
        public string OutTradeNo
        { get; set; }

        /// <summary>
        /// 当前预下单请求生成的二维码码串，可以用二维码生成工具根据该码串值生成对应的二维码 
        /// </summary>
        [TradeField("qr_code", Length = 64, IsRequire = true)]
        public string QrCode
        { get; set; }

    }
}
