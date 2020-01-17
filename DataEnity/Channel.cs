namespace DataEnity
{
    public class Channel
    {
        public int ChannelID
        {
            get;
            set;
        }

        public string ChannelName
        {
            get;
            set;
        }

        /// <summary>
        /// 渠道缩写
        /// </summary>
        public string Abbre
        {
            get;
            set;
        }

        /// <summary>
        /// 渠道说明
        /// </summary>
        public string Memo
        {
            get;
            set;
        }
    }
}
