using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeiXinPayCore;
using WeiXinPayCore.Entity;
using System.IO;
using System.Text;

namespace WeiXin_TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {

            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Console.WriteLine("1、统一下单  ");
            switch (Console.ReadLine())
            {
                case "1":
                    UnifiedOrder();
                    break;
            }


        }
        static void UnifiedOrder()
        {
            var apps = File.ReadAllLines(@"D:\cert.txt");
            var payHandle = new PayHandle();
            var unifiedOrder = new UnifiedOrder()
            {
                AppID = apps[0],
                MchID = apps[1],
                Key= apps[3],
                Body = "test",
                OutTradeNo = DateTime.Now.ToString("yyMMddhhmmss"),
                ToalFee = 1,
                SpbillCreateIP = "8.8.8.8",
                NotifyURL = "http://www.abcd.com",
                TradeType = "NATIVE"
            };
            var unifiedOrderBack = payHandle.Send(unifiedOrder) as UnifiedOrderBack;

        }
    }
}
