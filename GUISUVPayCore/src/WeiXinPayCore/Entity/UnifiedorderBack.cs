using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 统一下单返回实体类
    /// </summary>
    public class UnifiedorderBack: WeiXinPayBackParameters
    {
        /// <summary>
        ///返回状态码
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
        [TradeField("err_code",Length =32,IsRequire =false)]
        public string ErrCode { get; set; }
        /// <summary>
        /// 错误代码描述
        /// </summary>
        [TradeField("err_code_des", Length = 128, IsRequire = false)]
        public string ErrCodeDes { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        [TradeField("trade_type", Length = 16, IsRequire = true)]
        public string TradeType { get; set; }
        /// <summary>
        ///预支付交易会话标识
        /// </summary>
        [TradeField("prepay_id", Length = 64, IsRequire = true)]
        public string PrepayID { get; set; }
        /// <summary>
        /// 二维码链接
        /// </summary>
        [TradeField("code_url",Length =64,IsRequire =false)]
        public string CodeURL { get; set; }
    }
}
