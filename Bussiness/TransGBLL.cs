using System;
using System.Text;
using System.Data;
using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class TransGBLL
    {
        #region 增,删,改
        /// <summary>
        /// 兑换定单生成
        /// </summary>
        /// <param name="sGame">游戏缩写</param>
        /// <param name="iGamePoints">彩游点数</param>
        /// <param name="iGameMoney">游戏币数</param>
        /// <param name="sAccount">账号</param>
        /// <returns></returns>
        public static string GameSalesInit(string sGame, int iGamePoints,string sAccount,string sPhone,int iGUserID,string sTranIP)
        {
            if (iGamePoints < 1)
            {
                return "";
            }
            else
            {
                return TransGDAL.GameSalesInit(sGame, iGamePoints, sAccount, sPhone, iGUserID, sTranIP);
            }
        }

        public static string GameSalesGiftInit(string sGame, int iGamePoints, string sAccount)
        {
            return TransGDAL.GameSalesGiftInit(sGame, iGamePoints, sAccount);
        }

        public static int GameSalesCommit(string sTranID,string sAccount,string sGameName)
        {
            int iRes = TransGDAL.GaemSalesCommit(sTranID, sAccount, sGameName);
            if(0 == iRes)
            {
                PInfoSendToU.gamepaysend(sTranID);
            }
            return iRes;
        }
        #endregion

        #region 用户订单查询
        public static int TransGamePointsSelByTID(string sTranID)
        {
            return TransGDAL.TransSelGPointsByID(sTranID);
        }

        public static int TransSelPointsByID(string sTranID)
        {
            return TransGDAL.TransSelPointsByID(sTranID);
        }

        public static string TransTimeSelByTID(string sTranID)
        {
            return TransGDAL.TransSelTimeByID(sTranID);
        }

        public static TransG UserTranSel(string sTranID)
        {
            return TransGDAL.TransSelectByID(sTranID);
        }

        public static int TransSelGPointsByGame(int iGameID, DateTime dBeginDate, DateTime dEndDate)
        {
            return TransGDAL.TransSelGPointsByGame(iGameID, dBeginDate, dEndDate);
        }

        public static int TransSelGPointsByUser(int iUserID)
        { 
            return TransGDAL.TransSelGPointsByUser(iUserID);
        }

        public static string UserTranSelByUID(int iUID, int iPage, int iNum)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            if (iPage == 1)
            {
                IDataReader drTran = TransGDAL.TransSelOneByUID(iUID, iNum);
                while (drTran.Read())
                {
                    sbText.Append("{");
                    sbText.AppendFormat("time:'{0}',gamepoints:'{1}',points:'{2}',gamename:'{3}',servername:'{4}'",
                                         drTran["time"].ToString(), drTran["gamepoints"].ToString(), drTran["points"].ToString(), drTran["gamename"].ToString(), drTran["servername"].ToString());
                    sbText.Append("},");
                }
                drTran.Close();
                drTran.Dispose();
            }
            if (sbText.Length == 7)
            {
                return "";
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static int TransSelGUserIDByTID(string sTranID) 
        {
            return TransGDAL.TransSelGUserIDByTID(sTranID);
        }

        public static string GSalesInitRes(string sTrans)
        {
            string sRes = string.Empty;
            switch (sTrans)
            { 
                case "1":
                    sRes = "订单生成失败，请联系客服！";
                    break;
                case "2":
                    sRes = "充值游戏不存在!";
                    break;
                case "3":
                    sRes = "卡类型不存在!";
                    break;
                case "4":
                    sRes = "充值账号不存在!";
                    break;
                case "5":
                    sRes = "余额为零，请充值！谢谢！";
                    break;
                case "6":
                    sRes = "余额不足！请充值！谢谢！";
                    break;
                default:
                    sRes = "0";
                    break;
            }
            return sRes;
        }

        public static bool TranIDVal(string sTranID)
        {
            bool bRes = false;
            int iLen = sTranID.Trim().Length;
            if (iLen > 32)
            {
                bRes = true;
            }
            return bRes;
        }

        public static int TranIDStateSel(string sTranID)
        {
            return TransGDAL.TransSelStateByID(sTranID);
        }
        #endregion
    }
}
