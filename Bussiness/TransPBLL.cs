using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class TransPBLL
    {
        #region 增,删,改
        public static string PointSalesInit(string sChannel, string sPhone, string sAccount, decimal dPrice,int iCount,string sTranIP)
        {          
            return TransPDAL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount, sTranIP);            
        }

        public static int PointSalesCommit(string sTranID,string sAccount,decimal dPrice)
        {
            int iRes = -1;
            if (TranIDVal(sTranID))
            {
                int iState = TranIDStateSel(sTranID);
                if (iState == 0)
                {
                    iRes = TransPDAL.PointSalesCommit(sTranID, sAccount, dPrice);                    
                    if (0 == iRes)
                    {
                        PInfoSendToU.pointpaysend(sTranID);
                    }
                }
                else if(iState == 1)
                {
                    iRes = 0;
                }
            }
            return iRes;
        }
        #endregion

        #region 用户订单查询
        public static TransP UserTranSel(string sTranID)
        {
            return TransPDAL.TransSelectByID(sTranID);
        }

        public static TransP TranPSelPointsByID(string sTranID)
        {
            return TransPDAL.TranPSelPointsByID(sTranID);
        }

        public static int TranPSelPointByID(string sTranID)
        {
            return TransPDAL.TranPSelPointByID(sTranID);
        }

        public static int TranPSelUserIDByID(string sTranID)
        {
            return TransPDAL.TranPSelUserIDByID(sTranID);
        }

        public static string getDingDanId(string rtcoid)//根据日期获取订单号
        {
            return TransPDAL.getDingDanId(rtcoid);
        
        }
        public static string UserTranSelByUID(int iUserID, int iPage, int iNum)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            if(iPage == 1)
            {
                IDataReader drTran = TransPDAL.TransSelOneByUID(iUserID,iNum);
                while (drTran.Read())
                {
                    sbText.Append("{");
                    sbText.AppendFormat("time:'{0}',price:'{1}',points:'{2}',name:'{3}'", drTran["time"].ToString(), drTran["price"].ToString(), drTran["points"].ToString(), drTran["name"].ToString());
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
        #endregion

        public static void TransMsgSave(string sAccount,string sTranID,decimal dPrice,string sCode,string sCodeMsg,string sBid)
        {
            TransPDAL.TransMsgSave(sAccount, sTranID, dPrice, sCode, sCodeMsg, sBid);
        }

        public static int TranPSelChannelIDByID(string sTranID)
        {
            return TransPDAL.TranPSelChannelIDByID(sTranID);
        }

        public static int TranPSelPointsByUser(int iUserID)
        {
            return TransPDAL.TranPSelPointsByUser(iUserID);
        }

        public static int TranPTodayNumSel(double dPrice, DateTime dtBegin, DateTime dtEnd)
        {
            return TransPDAL.TranPTodayNumSel(dPrice, dtBegin, dtEnd);
        }

        public static double TranPTodayPriceSel(int iUserID, DateTime dtBegin, DateTime dtEnd)
        {
            return TransPDAL.TranPTodayPriceSel(iUserID, dtBegin, dtEnd);
        }

        public static List<TextTwo> TranPTop20PaySel(DateTime dtBegin, DateTime dtEnd)
        {
            return TransPDAL.TranPTop20PaySel(dtBegin, dtEnd);
        }

        public static string TranPTop20PayJsonSel(DateTime dtBegin, DateTime dtEnd)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            List<TextTwo> dgObject = TranPTop20PaySel(dtBegin,dtEnd);
            int i = 1;
            int iNum = 1;
            double dPrice = 0;
            foreach (TextTwo ttObject in dgObject)
            {
                if(i == 1)
                {
                    dPrice = double.Parse(ttObject.second);
                }
                else
                {
                    double dUserPrice = double.Parse(ttObject.second);
                    if (dUserPrice < dPrice)
                    {
                        dPrice = dUserPrice;
                        iNum = i;
                    }
                }
                string sAccount = ttObject.first.Trim();
                string sAccountC = string.Format("{0}***", sAccount.Remove(sAccount.Length - 3));
                sbText.Append("{");
                sbText.AppendFormat("account:'{0}',price:'{1}',num:'{2}'", sAccountC, ttObject.second,iNum.ToString());
                sbText.Append("},");
                i++;
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static string Top20PayJsonAllSel(DateTime dtBegin, DateTime dtEnd)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            List<TextTwo> dgObject = TranPTop20PaySel(dtBegin, dtEnd);
            int i = 1;
            int iNum = 1;
            double dPrice = 0;
            foreach (TextTwo ttObject in dgObject)
            {
                if (i == 1)
                {
                    dPrice = double.Parse(ttObject.second);
                }
                else
                {
                    double dUserPrice = double.Parse(ttObject.second);
                    if (dUserPrice < dPrice)
                    {
                        dPrice = dUserPrice;
                        iNum = i;
                    }
                }
                sbText.Append("{");
                sbText.AppendFormat("account:'{0}',price:'{1}',num:'{2}'", ttObject.first, ttObject.second, iNum.ToString());
                sbText.Append("},");
                i++;
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
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
            return TransPDAL.TranPSelStateByID(sTranID);
        }

        public static string GiftPointsSend(int iUserID, int iGiftPoint, string sTranIP)
        {
            return TransPDAL.GiftPointsSend(iUserID, iGiftPoint, sTranIP);
        }

        public static string YRTSalesInit(int iUserID, int iPoint,decimal dPrice, string sTranIP)
        { 
            return  TransPDAL.YRTSalesInit(iUserID,iPoint,dPrice,sTranIP);
        }

        public static int YRTSalesCommit(string sTranID, int iUserID, int iPoint)
        {
            return TransPDAL.YRTSalesCommit(sTranID, iUserID, iPoint);
        }

        public static string FirstGiftSend(int iUserID, int iGiftPoint, string sTranIP, int iChannid)
        {
            return TransPDAL.FirstGiftSend(iUserID, iGiftPoint, sTranIP,iChannid);
        }

        public static string SelIsTranByUID(int iUserID)
        {
            return TransPDAL.SelIsTranByUID(iUserID);
        }

        public static bool UserIsTranVal(int iUserID)
        {
            bool bRes = false;
            string sTranID = TransPBLL.SelIsTranByUID(iUserID);
            if (sTranID.Length > 32)
            {
                bRes = true;
            }
            return bRes;
        }
    }
}
