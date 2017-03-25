using System.Collections.Generic;

namespace AlipayPayCore.Entity
{
    public class RoyaltyInfo
    {
        /// <summary>
        ///分账类型 卖家的分账类型，目前只支持传入ROYALTY（普通分账类型）。
        /// </summary>
        [TradeField("royalty_type", Length = 150, IsRequire = false)]
        public string RoyaltyType
        { get; set; }
        /// <summary>
        ///分账明细的信息，可以描述多条分账指令，json数组。
        /// </summary> 
        [TradeField(" royalty_detail_infos", Length = 2500, IsRequire = true)]
        public List<RoyaltyDetailInfos> RoyaltyDetailInfos
        { get; set; }
        /// <summary>
        ///二级商户信息,当前只对特殊银行机构特定场景下使用此字段
        /// </summary> 
        [TradeField(" sub_merchant ", Length = 0, IsRequire = false)]
        public List<SubMerchant> SubMerchant
        { get; set; }
        /// <summary>
        ///支付宝店铺的门店ID
        /// </summary> 
        [TradeField(" alipay_store_id ", Length = 32, IsRequire = false)]
        public string AlipayStoreId 
        { get; set; }

    }
}