using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WeiXinPayCore.Entity;

namespace WeiXinPayCore
{
    class WXPayMethods
    {
        PayHandle _ph = null;
        string[] app = null;
        public WXPayMethods()
        {
            _ph = new PayHandle();
            app = File.ReadAllLines(@"D:\cert.txt");
        }

        /// <summary>
        /// 统一下单交易
        /// </summary>
        /// <param name="unifiedOrder">统一下单交易实体</param>
        /// <returns>统一下单交易返回实体</returns>
        public UnifiedOrderBack UnifiedOrder(UnifiedOrder unifiedOrder)
        {
            unifiedOrder.AppID = app[0];
            unifiedOrder.MchID = app[1];
            unifiedOrder.Key = app[2];
            var unifiedOrderBack=_ph.Send(unifiedOrder) as UnifiedOrderBack;
            return unifiedOrderBack;
        }

        /// <summary>
        /// 退单交易
        /// </summary>
        /// <param name="refund">退单实体</param>
        /// <returns>退单返回实体</returns>
        public RefundBack Refund(Refund refund)
        {
            refund.AppID = app[0];
            refund.MchID = app[1];
            refund.Key = app[2];
            var refundBack = _ph.Send(refund) as RefundBack;
            return refundBack;
        }

        /// <summary>
        /// 查询订单交易
        /// </summary>
        /// <param name="orderQuery">查询订单实体</param>
        /// <returns>查询订单返回实体</returns>
        public OrderQueryBack OederQuery(OrderQuery orderQuery)
        {
            orderQuery.AppID=app[0];
            orderQuery.MchID=app[1];
            orderQuery.Key=app[2];
            var orderQueryBack = _ph.Send(orderQuery) as OrderQueryBack;
            return orderQueryBack;
        }

        /// <summary>
        /// 查询退单交易
        /// </summary>
        /// <param name="refundQuery">查询退单交易实体</param>
        /// <returns>查询退单交易实体返回</returns>
        public RefundQueryBack RefundQuery(RefundQuery refundQuery)
        {
            refundQuery.AppID = app[0];
            refundQuery.MchID = app[1];
            refundQuery.Key = app[2];
            var refundQueryBack = _ph.Send(refundQuery) as RefundQueryBack;
            return refundQueryBack;
        }


        
    }
}