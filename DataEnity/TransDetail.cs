namespace DataEnity
{
    public class TransDetail
    {
        public string TranID
        {
            get;
            set;
        }

        /// <summary>
        /// 点卡充值类型
        /// </summary>
        public int cTypeID
        {
            get;
            set;
        }

        /// <summary>
        ///交易点卡数量
        /// </summary>
        public int Count
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }

        public int TranPoints
        {
            get;
            set;
        }

        public int TranGiftPoints
        {
            get;
            set;
        }
    }
}
