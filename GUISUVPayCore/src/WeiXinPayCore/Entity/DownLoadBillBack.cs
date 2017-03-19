using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 下载对账单返回实体
    /// </summary>
    class DownLoadBillBack:WeiXinPayBackParameters
    {
        //失败返回以下内容
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
        //成功时以文本表格的方式返回
        //TODO 成功时用什么属性接受成功返回的内容
    }
}
