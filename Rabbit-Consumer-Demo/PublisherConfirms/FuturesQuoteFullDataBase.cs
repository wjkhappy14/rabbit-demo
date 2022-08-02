namespace PublisherConfirms
{

    public class FuturesQuoteFullDataBase
    {
        public FuturesQuoteFullDataBase()
        {
        }
        public string Time { get; set; }

        public string CommodityNo { get; set; }

        public string ContractNo { get; set; }

        public string BidPrice { get; set; }

        public string BidPrice2 { get; set; }

        public string BidPrice3 { get; set; }

        public string BidPrice4 { get; set; }

        public string BidPrice5 { get; set; }

        public string BidPrice6 { get; set; }

        public string BidPrice7 { get; set; }

        public string BidPrice8 { get; set; }

        public string BidPrice9 { get; set; }

        public string BidPrice10 { get; set; }

        public long BidSize { get; set; }

        public long BidSize2 { get; set; }

        public long BidSize3 { get; set; }
        public long BidSize4 { get; set; }

        public long BidSize5 { get; set; }

        public long BidSize6 { get; set; }

        public long BidSize7 { get; set; }

        public long BidSize8 { get; set; }

        public long BidSize9 { get; set; }

        public long BidSize10 { get; set; }

        public string AskPrice { get; set; }

        public string AskPrice2 { get; set; }

        public string AskPrice3 { get; set; }

        public string AskPrice4 { get; set; }

        public string AskPrice5 { get; set; }

        public string AskPrice6 { get; set; }

        public string AskPrice7 { get; set; }

        public string AskPrice8 { get; set; }

        public string AskPrice9 { get; set; }

        public string AskPrice10 { get; set; }

        public long AskSize { get; set; }

        public long AskSize2 { get; set; }

        public long AskSize3 { get; set; }

        public long AskSize4 { get; set; }

        public long AskSize5 { get; set; }

        public long AskSize6 { get; set; }

        public long AskSize7 { get; set; }

        public long AskSize8 { get; set; }

        public long AskSize9 { get; set; }

        public long AskSize10 { get; set; }

        public string LastPrice { get; set; }

        public long LastSize { get; set; }

        public string OpenPrice { get; set; }

        public string HighPrice { get; set; }

        public string LowPrice { get; set; }

        public string NowClosePrice { get; set; }

        public string ClosePrice { get; set; }

        public long Volume { get; set; }

        public long TotalVolume { get; set; }

        public string PreSettlePrice { get; set; }
        public long TotalQty { get; set; }

        public long PositionQty { get; set; }
        public long PrePositionQty { get; set; }

        public long CurrentTime { get; set; }

        public override string ToString()
        {
            return "";
        }
    }
}
