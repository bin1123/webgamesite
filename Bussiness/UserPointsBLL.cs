
using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class UserPointsBLL
    {
        public static UserPoints UPointsSel(int iUserID)
        { 
            UserPoints upObject = UserPointsDAL.UserPointsSelect(iUserID);
            return upObject;
        }

        public static int UPointSel(int iUserID)
        {
            return UserPointsDAL.UserPointSel(iUserID);
        }

        public static int UGPointSel(int iUserID)
        {
            return UserPointsDAL.UGPointSel(iUserID);
        }

        public static int UPointAllSel(int iUserID)
        {
            UserPoints upObject = UserPointsDAL.UserPointsSelect(iUserID);
            int iPoints = upObject.Points + upObject.GiftPoints;
            return iPoints;
        }

        /// <summary>
        /// 用户真实点数查询
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public static int URealPointsSel(int iUserID)
        { 
            int iPoints = 0;
            if (iUserID > 999)
            {
                int iPPoints = TransPBLL.TranPSelPointsByUser(iUserID);
                int iGPoints = TransGBLL.TransSelGPointsByUser(iUserID);
                if (iPPoints == -1 || iGPoints == 1)
                {
                    iPoints = -1;
                }
                else
                {
                    iPoints = iPPoints + iGPoints;
                }
            }
            else
            {
                iPoints = -1;
            }
            return iPoints;
        }

        public static int UPointUpdate(int iUserID, int iUserPoints)
        {
            return UserPointsDAL.UPointUpdate(iUserID, iUserPoints);
        }

        /// <summary>
        /// 用户点数校验
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public static bool UPointCheck(int iUserID)
        {
            bool bRes = false;
            if(iUserID < 1000)
            {
                return bRes;
            }
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            if (iUserPoints == 0)
            {
                bRes = true;
                return bRes;
            }
            int iPoints = URealPointsSel(iUserID);//用户实际点数
            if(iPoints == -1)
            {
                return bRes;
            }
            if(iUserPoints != -1 && iPoints != -1)
            {
                if (iUserPoints > iPoints)
                {
                    if(iPoints < 0)
                    {
                        iPoints = 0;
                    }
                    int iRes = UserPointsDAL.UPointUpdate(iUserID, iPoints);
                    if (iRes > 0)
                    {
                        bRes = true;
                    }
                }
                else
                {
                    bRes = true;
                }
            }
            return bRes;
        }

        public static int UGiftPointUpdate(int iUserID, int iGiftPoints)
        {
            return UserPointsDAL.UGiftPointUpdate(iUserID, iGiftPoints);
        }
    }
}
