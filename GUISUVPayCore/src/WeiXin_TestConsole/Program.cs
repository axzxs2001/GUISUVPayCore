using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeiXinPayCore;
using WeiXinPayCore.Entity;
using System.IO;
using System.Text;
using QRCoder;
using System.Drawing;

namespace WeiXin_TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.WriteLine("1、统一下单  2、退单 3、查询订单 4、查询退单");
            switch (Console.ReadLine())
            {
                case "1":
                    UnifiedOrder();
                    break;
                case "2":
                    Refund("170412084313");
                    break;
                case "3":
                    OrderQuery("170408102028");
                    break;
                case "4":
                    QueryRefundBack("170412084313");
                    break;
            }
        }

         static void Refund(string str)
        {
            var payHandle = new PayHandle();
            var apps = File.ReadAllLines(@"D:\cert.txt");
            var refund = new Refund() {
                CertificatePath = @"D:\apiclient_cert.p12",
                OutTradeNo=str,
                AppID = apps[0],
                MchID = apps[1],
                Key = apps[3],
                OutRefundNo="123456789",
                TotalFee=1,
                RefundFee=1,
                OpUserID="大连医卫信息"
            };

            var refundBack = payHandle.Send(refund) as RefundBack;
            if (refundBack.ResultCode == "SUCCESS")
            {
                Console.WriteLine("退费成功 ");
            }
        }

        /// <summary>
        /// 扫码统一下单
        /// </summary>
        static void UnifiedOrder()
        {
            var apps = File.ReadAllLines(@"D:\cert.txt");
            var payHandle = new PayHandle();
            var unifiedOrder = new UnifiedOrder()
            {
                AppID = apps[0],
                MchID = apps[1],
                Key = apps[3],
                Body = "test",
                OutTradeNo = DateTime.Now.ToString("yyMMddhhmmss"),
                ToalFee = 1,
                SpbillCreateIP = "8.8.8.8",
                NotifyURL = "http://www.abcd.com",
                TradeType = "NATIVE",
                ProductID = "123456"
            };
            var unifiedOrderBack = payHandle.Send(unifiedOrder) as UnifiedOrderBack;
            if(unifiedOrderBack.ResultCode=="SUCCESS")
            {
                SavaQR(unifiedOrderBack.CodeURL);
            }
        }
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="str">订单号</param>
        /// <returns></returns>
        static OrderQueryBack OrderQuery(string str)
        {
            var apps = File.ReadAllLines(@"D:\cert.txt");
            var payHandle = new PayHandle();
            var orderQuery = new OrderQuery
            {
                AppID = apps[0],
                MchID = apps[1],
                Key = apps[3],
               
               //// NotifyURL = "http://www.abcd.com",
              
                OutTradeNo = str,
                //TODO 此处有问题
                TransactionID="123456"
            };
            var orderQueryBack = payHandle.Send(orderQuery) as OrderQueryBack;
            
            return orderQueryBack;
        }
        /// <summary>
        /// 查询退单
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static RefundQueryBack QueryRefundBack(string str)
        {
            var apps = File.ReadAllLines(@"D:\cert.txt");
            var payHandle = new PayHandle();
            var refundQuery = new RefundQuery()
            {
                AppID = apps[0],
                MchID = apps[1],
                Key = apps[3],
                OutTradeNo = str
            };
            var refundQueryBack = payHandle.Send(refundQuery) as RefundQueryBack;
            return refundQueryBack;
        }

        #region 生成二维码
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="qrUrl">返回用来生成二维码的路径</param>
        static  void SavaQR(string qrUrl)
        {
            try
            {
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(qrUrl, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(20);
                qrCodeImage.Save($@"D:\\wx.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);              
            }
            catch
            {
                throw new WeiXinPayCoreException("生成二维码失败");
            }
        }
        #endregion 


    }
}
