using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    [Trade("https://api.mch.weixin.qq.com/pay/downloadbill",RequireCertificate =false)]
  public class DownLoadBill:WeiXinPayParameters
    {
        /// <summary>
        /// 对账单日期
        /// </summary>
        [TradeField("bill_date",Length =8,IsRequire =true)]
        public string BillDate {get; set; }
        /// <summary>
        /// 对账单类型
        /// </summary>
        [TradeField("bill_type",Length =8,IsRequire =true)]
        public string BillType { get; set; }
        /// <summary>
        /// 压缩账单
        /// </summary>
        [TradeField("tar_type",Length =8,IsRequire =false)]
        public string TarType { get; set; }

    }
}
