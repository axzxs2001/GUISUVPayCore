using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;

namespace WeiXinPayCore.Entity
{
    /// <summary>
    /// 公用参数
    /// </summary>
    public class WeiXinPayParameters
    {
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
            get
            {
                return Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            }
        }
        /// <summary>
        /// 签名
        /// </summary>
        [TradeField("sign", Length = 32, IsRequire = true)]
        public string Sign
        {
            get
            {
                return CreateSign();
            }
        }
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <returns></returns>
        string CreateSign()
        {
            var fieldDic = new SortedDictionary<string, string>();
            foreach (var pro in this.GetType().GetProperties())
            {
                foreach (var att in pro.GetCustomAttributes(false))
                {
                    if (att is TradeFieldAttribute)
                    {
                        var attr = (att as TradeFieldAttribute);
                        //不作为生成sign验证码
                        if(attr.Name=="sign")
                        {
                            continue;
                        }
                        var fieldName = attr.Name;
                        var fieldValue = Convert.ChangeType(pro.GetValue(this), pro.PropertyType);

                        //取属性默认值 
                        var defaultValue = (pro.PropertyType.GetTypeInfo().IsValueType == true ? Activator.CreateInstance(pro.PropertyType) : null);
                        //值类型
                        if (defaultValue != null)
                        {
                            //不为默认值
                            if (fieldValue.ToString() != defaultValue.ToString())
                            {
                                ValidateLength();
                                fieldDic.Add(fieldName, fieldValue.ToString());
                            }
                        }
                        else//引用类型
                        {
                            //不为空
                            if (fieldValue != null)
                            {
                                ValidateLength();
                                fieldDic.Add(fieldName, fieldValue.ToString());
                            }
                        }
                        //验证超长
                        void ValidateLength()
                        {
                            if (attr.Length!=0&& attr.Length < Encoding.UTF8.GetByteCount(fieldValue.ToString()))
                            {
                                throw new WeiXinPayCoreException($"{fieldName}的值：{fieldValue}长度超过{attr.Length}");
                            }
                        }
                        break;
                    }
                }
            }

            //在string后加入API KEY
            var fieldStr = new StringBuilder();
            foreach (var field in fieldDic)
            {
                fieldStr.Append($"&{field.Key}={field.Value}");
            }
            if (string.IsNullOrEmpty(Key))
            {
                throw new WeiXinPayCoreException("Key不能为空");
            }
            fieldStr.Append($"&key={Key}");
            return EncoderMd5();
            string EncoderMd5()
            {
                //MD5加密
                var md5 = MD5.Create();

                var urlStr = fieldStr.ToString().Trim('&');
                var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(urlStr));
                var charBuilder = new StringBuilder();
                foreach (byte b in bs)
                {
                    charBuilder.Append(b.ToString("x2"));
                }
                //所有字符转为大写
                return charBuilder.ToString().ToUpper();
            };

        }
        /// <summary>
        /// 签名类型
        /// </summary>
        [TradeField("sign_type", Length = 32)]
        public string SignType
        { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key
        { private get; set; }

        /// <summary>
        /// 转换成xml
        /// </summary>
        /// <returns></returns>
        public string ToXML()
        {
            var xmlBuilder = new StringBuilder("<xml>");
            foreach (var pro in this.GetType().GetProperties())
            {
                foreach (var att in pro.GetCustomAttributes(false))
                {
                    if (att is TradeFieldAttribute)
                    {
                        var attr = att as TradeFieldAttribute;
                        //获取值
                        var value = pro.GetValue(this);
                        ValidateValue();
                        //验证必填值不能为空
                        void ValidateValue()
                        {
                            //判断引用类型，必填值为空的，抛异常
                            if(!pro.PropertyType.GetTypeInfo().IsValueType&&attr.IsRequire&& value==null)
                            {
                                throw new WeiXinPayCoreException($"{pro.Name}的值为必填，不能为空");
                            }
                        }
                        //处理string,datetime类型和其他引用类型
                        if (pro.PropertyType == typeof(string) || pro.PropertyType == typeof(DateTime)||!pro.PropertyType.GetTypeInfo().IsValueType)
                        {
                            if (value != null)
                            {
                                if (attr.Length < Encoding.UTF8.GetByteCount(value.ToString()))
                                {
                                    throw new WeiXinPayCoreException($"{pro.Name}的值：{value}超过{attr.Length}长度");
                                }
                                xmlBuilder.Append($"<{attr.Name}><![CDATA[{value}]]></{attr.Name}>");
                            }
                        }
                        else 
                        {
                            xmlBuilder.Append($"<{attr.Name}>{value}</{attr.Name}>");
                        }
                        break;
                    }
                }
            }
            xmlBuilder.Append("</xml>");
            return xmlBuilder.ToString();
        }

    }
}
