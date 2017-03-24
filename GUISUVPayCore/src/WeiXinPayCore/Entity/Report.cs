using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 交易保障实体
    /// </summary>
    [Trade("https://api.mch.weixin.qq.com/payitil/report",RequireCertificate =false)]
  public class Report:WeiXinPayParameters
    {
        /// <summary>
        /// 接口url
        /// </summary>
        [TradeField("interface_url", Length = 127, IsRequire = true)]
        public string InterfaceURL { get; set; }
        /// <summary>
        /// 接口耗时
        /// </summary>
        [TradeField("excute_time",IsRequire =true)]
        public int ExcuteTime { get; set; }
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
        /// 商户订单号
        /// </summary>
        [TradeField("out_trade_no", Length = 32, IsRequire = true)]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 访问接口IP
        /// </summary>
        [TradeField("user_ip",Length =16,IsRequire =true)]
        public string UserIP { get; set; }
        /// <summary>
        /// 商户上报时间
        /// </summary>
        [TradeField("time",Length =14,IsRequire =false)]
        public string Time { get; set; }
    }
}
