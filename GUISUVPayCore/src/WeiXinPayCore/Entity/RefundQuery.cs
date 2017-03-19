using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinPayCore.Entity
{
    [Trade("https://api.mch.weixin.qq.com/pay/orderquery",RequireCertificate =false)]
    class RefundQuery:WeiXinPayParameters
    {
    }
}
