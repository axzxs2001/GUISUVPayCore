using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    [Trade("https://api.mch.weixin.qq.com/tools/shorturl",RequireCertificate =false)]
   public class ShortURL:WeiXinPayParameters
    {
        /// <summary>
        /// URL链接
        /// </summary>
        [TradeField("long_url",Length =512,IsRequire =true)]
        public string LongURL { get; set; }
    }
}
