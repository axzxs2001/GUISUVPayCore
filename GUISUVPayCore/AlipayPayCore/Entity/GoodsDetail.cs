using System.Collections.Generic;

namespace AlipayPayCore.Entity
{
    public class GoodsDetail
    {
        /// <summary>
        ///商品的编号
        /// </summary>
        [TradeField("goods_id", Length = 32, IsRequire = true)]
        public string GoodsId 
        { get; set; }
        /// <summary>
        ///支付宝定义的统一商品编号
        /// </summary>
        [TradeField("alipay_goods_id", Length = 32, IsRequire = false)]
        public string AlipayGoodsId
        { get; set; }

        /// <summary>
        ///商品名称
        /// </summary>
        [TradeField("goods_name", Length = 256, IsRequire = true)]
        public string GoodsName
        { get; set; }
        /// <summary>
        ///商品数量
        /// </summary>
        [TradeField("Quantity", Length = 10, IsRequire = true)]
        public string Quantity
        { get; set; }
        /// <summary>
        ///商品单价，单位为元
        /// </summary>
        [TradeField("price", Length = 9, IsRequire = true)]
        public decimal Price
        { get; set; }
        /// <summary>
        ///商品类目
        /// </summary>
        [TradeField(" goods_category", Length = 24, IsRequire = false)]
        public string GoodsCategory 
        { get; set; }
        /// <summary>
        ///商品描述信息 
        /// </summary>
        [TradeField("body", Length = 1000, IsRequire = false)] 
        public string Body
        { get; set; }
        /// <summary>
        ///商品的展示地址 
        /// </summary>
        [TradeField("show_url", Length = 400, IsRequire = false)]
        public string ShowUrl 
        { get; set; }
       

    }
}