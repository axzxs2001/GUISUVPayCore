using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 公用参数
    /// </summary>
    public class WeiXinPayBackParameters
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
        /// 公众账号ID
        /// </summary>
        [TradeField("appid", Length = 32, IsRequire = true)]
        public string AppID
        { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        [TradeField("mch_id", Length = 32, IsRequire = true)]
        public string MchID
        { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        [TradeField("device_info", Length = 32)]
        public string DeviceInfo
        { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        [TradeField("nonce_str", Length = 32, IsRequire = true)]
        public string NonceStr
        {
            get;set;
        }
        /// <summary>
        /// 签名
        /// </summary>
        [TradeField("sign", Length = 32, IsRequire = true)]
        public string Sign
        {
            get;
            set;
        }
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
        /// 交易类型
        /// </summary>
        [TradeField("trade_type", Length = 16, IsRequire = true)]
        public string TradeType { get; set; }
    }
}
