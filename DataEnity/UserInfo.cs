using System;

namespace DataEnity
{
    public class UserInfo
    {
        #region 用户信息实体
        public int uid
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 身份证
        /// </summary>
        public string Credennum
        {
            get;
            set;
        }

        /// <summary>
        /// 问题
        /// </summary>
        public string question
        {
            get;
            set;
        }

        /// <summary>
        /// 回答
        /// </summary>
        public string Answer
        {
            get;
            set;
        }

        public string regip
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
        #endregion
    }
}
