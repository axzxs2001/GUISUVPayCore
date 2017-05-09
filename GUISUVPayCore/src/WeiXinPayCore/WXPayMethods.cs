using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WeiXinPayCore.Entity;

namespace WeiXinPayCore
{
   public class WXPayMethods
    {
        PayHandle _ph = null;
        string[] app = null;
        
        public WXPayMethods()
        {
            _ph = new PayHandle();
            app = File.ReadAllLines(@"D:\cert.txt");
          
        }

        private WeiXinPayParameters Chang(WeiXinPayParameters wxParameters)
        {
            var para = new WeiXinPayParameters();
            para = wxParameters;

            para.AppID = app[0];
            para.MchID = app[1];
            para.Key = app[3];
            return para;
        }

        /// <summary>
        /// 统一下单交易
        /// </summary>
        /// <param name="unifiedOrder">统一下单交易实体</param>
        /// <returns>统一下单交易返回实体</returns>
        public UnifiedOrderBack UnifiedOrder(WeiXinPayParameters unifiedOrder)
        {
            unifiedOrder = Chang(unifiedOrder) as UnifiedOrder;
            var unifiedOrderBack=_ph.Send(unifiedOrder) as UnifiedOrderBack;
            return unifiedOrderBack;
        }

        /// <summary>
        /// 退单交易
        /// </summary>
        /// <param name="refund">退单实体</param>
        /// <returns>退单返回实体</returns>
        public RefundBack Refund(WeiXinPayParameters refund)
        {
            refund = Chang(refund) as Refund;
            var refundBack = _ph.Send(refund) as RefundBack;
            return refundBack;
        }

        /// <summary>
        /// 查询订单交易
        /// </summary>
        /// <param name="orderQuery">查询订单实体</param>
        /// <returns>查询订单返回实体</returns>
        public OrderQueryBack OederQuery(WeiXinPayParameters orderQuery)
        {
            orderQuery = Chang(orderQuery) as OrderQuery;
            var orderQueryBack = _ph.Send(orderQuery) as OrderQueryBack;
            return orderQueryBack;
        }

        /// <summary>
        /// 查询退单交易
        /// </summary>
        /// <param name="refundQuery">查询退单交易实体</param>
        /// <returns>查询退单交易实体返回</returns>
        public RefundQueryBack RefundQuery(WeiXinPayParameters refundQuery)
        {
            refundQuery = Chang(refundQuery) as RefundQuery;
            var refundQueryBack = _ph.Send(refundQuery) as RefundQueryBack;
            return refundQueryBack;
        }
        /// <summary>
        /// 关闭订单交易
        /// </summary>
        /// <param name="closeOrder">关闭订单交易实体</param>
        /// <returns></returns>
        public CloseOrderBack CloseOrder(WeiXinPayParameters closeOrder)
        {
            closeOrder = Chang(closeOrder) as CloseOrder;
            var closeOrderBack = _ph.Send(closeOrder) as CloseOrderBack;
            return closeOrderBack;
        }
        /// <summary>
        /// 下载对账单交易
        /// </summary>
        /// <param name="downLoadBill">下载对账单实体</param>
        /// <returns></returns>
        public DownLoadBillBack DownLoadBill(WeiXinPayParameters downLoadBill)
        {
            downLoadBill = Chang(downLoadBill) as DownLoadBill;
            var downLoadBillBack = _ph.Send(downLoadBill) as DownLoadBillBack;
            return downLoadBillBack;
        }
        /// <summary>
        /// 交易保障交易
        /// </summary>
        /// <param name="report">交易保障实体</param>
        /// <returns></returns>
        public  ReportBack Report(WeiXinPayParameters report)
        {
            report = Chang(report) as Report;
            var reportBack = _ph.Send(report) as ReportBack;
            return reportBack;
        }
        /// <summary>
        /// 转换短链接交易
        /// </summary>
        /// <param name="shortUrl">转换短链接交易实体</param>
        /// <returns></returns>
        public ShortURLBack ShortUrl(WeiXinPayParameters shortUrl)
        {
            shortUrl = Chang(shortUrl) as ShortURL;
            var shortUrlBack = _ph.Send(shortUrl) as ShortURLBack;
            return shortUrlBack;
        }
    }
}