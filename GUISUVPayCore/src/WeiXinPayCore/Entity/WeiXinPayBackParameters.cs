using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using System.Xml;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 公用参数
    /// </summary>
    public class WeiXinPayBackParameters
    {
        /// <summary>
        ///返回状态码
        /// </summary>
        [TradeField("return_code", Length = 16, IsRequire = true)]
        public string ReturnCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        [TradeField("return_msg", Length = 128, IsRequire = false)]
        public string ReturnMsg { get; set; }
        /// <summary>
        /// 公众账号ID
        /// </summary>
        [TradeField("appid", Length = 32, IsRequire = true)]
        public string AppID
        { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        [TradeField("mch_id", Length = 32, IsRequire = true)]
        public string MchID
        { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        [TradeField("device_info", Length = 32)]
        public string DeviceInfo
        { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        [TradeField("nonce_str", Length = 32, IsRequire = true)]
        public string NonceStr
        {
            get; set;
        }
        /// <summary>
        /// 签名
        /// </summary>
        [TradeField("sign", Length = 32, IsRequire = true)]
        public string Sign
        {
            get;
            set;
        }
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
        /// 交易类型
        /// </summary>
        [TradeField("trade_type", Length = 16, IsRequire = true)]
        public string TradeType { get; set; }



        /// <summary>
        /// xml字符串转实体
        /// </summary>
        /// <param name="xml">xml字符串</param>
        /// <param name="type">入参实体</param>
        /// <returns></returns>
        public void XMLToEntity(string xml, WeiXinPayBackParameters backEntity)
        {
        
            
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            if (xmlDoc.HasChildNodes)
            {
                foreach (XmlNode node in xmlDoc.ChildNodes[0].ChildNodes)
                {
                    var name = node.Name;
                    var value = node.InnerText;
                    SetEntityProperty(backEntity, name, value);
                }
            }        
        }

        /// <summary>
        /// 把value赋给实体中属性的特性等于name的属性
        /// </summary>
        /// <param name="backParameters">实体</param>
        /// <param name="name">名称</param>
        /// <param name="value">值 </param>
        void SetEntityProperty(WeiXinPayBackParameters backParameters, string name, string value)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
            {
                var entityType = backParameters.GetType();
                foreach (var pro in entityType.GetProperties())
                {
                    foreach (var att in pro.GetCustomAttributes(false))
                    {
                        if (att is TradeFieldAttribute)
                        {
                            var attr = att as TradeFieldAttribute;
                            if (attr.Name == name && pro.CanWrite)
                            {

                                pro.SetValue(backParameters, Convert.ChangeType(value, pro.PropertyType));
                            }
                            break;
                        }
                    }
                }

            }
        }
    }
}
