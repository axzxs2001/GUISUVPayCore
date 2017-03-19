using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 关闭订单返回实体
    /// </summary>
    class CloseOrderBack:WeiXinPayBackParameters
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
        /// 业务结果描述
        /// </summary>
        [TradeField("result_msg",Length =32,IsRequire =true)]
        public string ResultMsg { get; set; }
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
       
    }
}
