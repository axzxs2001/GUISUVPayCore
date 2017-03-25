namespace AlipayPayCore.Entity
{
    public class SubMerchant
    {
        /// <summary>
        ///分账的比例，值为20代表按20%的比例分账
        /// </summary>
        [TradeField("merchant_id", Length = 11, IsRequire = true)]
        public string MerchantId 
        { get; set; }
    }
}