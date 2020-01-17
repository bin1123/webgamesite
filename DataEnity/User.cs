namespace DataEnity
{
    public class User
    {
        #region 用户实体层
        public int uid
        {
            get;
            set;
        }

        public string account
        {
            get;
            set;
        }

        public string password
        {
            get;
            set;
        }
        /// <summary>
        /// 用户来源类型id
        /// </summary>
        public int typeid
        {
            get;
            set;
        }

        public int state
        {
            get;
            set;
        }
        #endregion
    }
}
