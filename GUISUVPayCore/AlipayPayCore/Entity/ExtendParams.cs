namespace AlipayPayCore.Entity
{
    public class ExtendParams
    {
        /// <summary>
        ///系统商编号 该参数作为系统商返佣数据提取的依据，请填写系统商签约协议的PID 
        /// </summary>
        [TradeField(" sys_service_provider_id  ", Length = 64, IsRequire = false)]
        public string SysServiceProviderId
        { get; set; }
        /// <summary>
        ///使用花呗分期要进行的分期数
        /// </summary>
        [TradeField(" hb_fq_num  ", Length = 5, IsRequire = false)]
        public string HbFqNum  
        { get; set; }
        /// <summary>
        ///使用花呗分期需要卖家承担的手续费比例的百分值，传入100代表100% 
        /// </summary>
        [TradeField(" hb_fq_seller_percent  ", Length = 3, IsRequire = false)]
        public string HbFqSellerPercent 
        { get; set; }

    }
}