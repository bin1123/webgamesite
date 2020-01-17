using System;
using System.Web;

using DataAccess;
using Common;

namespace Bussiness
{
    public class PayAll
    {
        public static string CreatePay(string sChannel,string sPhone, string sAccount, decimal dPrice, int iCount,string sBankName)
        {
            string sTranDirect = string.Empty;     
            switch(sChannel)
            {
                case "alipay":
                case "ibank":
                    sTranDirect = AliPay.PayDirect(sChannel,sPhone,sAccount,dPrice,iCount,sBankName);
                    break;
                case "yp-szx":
                case "yp-dx":
                case "yp-lt":
                case "yp-zt":
                case "yp-sd":
                case "yp-jcard":
                case "yp-bank":
                    sTranDirect = YeePayBuy.PayDirect(sChannel, sPhone, sAccount, dPrice, iCount);
                    break;
                case "szfbank":
                    sTranDirect = QdbPayBuy.PayBegin(sChannel, sPhone, sAccount, dPrice, iCount);
                    break;
                case "vpay":
                    sTranDirect = VPayBuy.PayBegin(sChannel, sPhone, sAccount, dPrice, iCount);
                    break;
            }
            return sTranDirect;
        }

        public static string CreatePay(string sChannel, string sPhone, string sAccount, decimal dPrice, int iCount, string sBankName, HttpContext Context)
        {
            string sTranDirect = string.Empty;
            return sTranDirect;
        }

        public static string GamePay(string sGameAbbre,int iUserID,string sUserName,int iPayPoints,string sPhone,int iGUserID)
        {
            string sReturn = string.Empty;
            string sGame = GameInfoBLL.GameInfoAbbreSel(sGameAbbre).TrimEnd();
            switch(sGame)
            {
                case "sssg":
                    sReturn = sssgGame.sssgPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "sxd":
                    sReturn = sxdGame.sxdPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "lj":
                    sReturn = ljGame.LJPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "dxz":
                    sReturn = dxzGame.dxzPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "djj":
                    sReturn = djjGame.djjPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "txj":
                    sReturn = txjGame.txjPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "sjsg":
                    sReturn = sjsgGame.sjsgPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "tzcq":
                    sReturn = tzcqGame.tzcqPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "by":
                    sReturn = byGame.byPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "swjt":
                    sReturn = swjtGame.swjtPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "gcld":
                    sReturn = gcldGame.gcldPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "khbd":
                    sReturn = khbdGame.khbdPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "hyjft":
                    sReturn = hyjftGame.hyjftPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "nslm":
                    sReturn = nslmGame.nslmPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "yjxy":
                    sReturn = yjxyGame.yjxyPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "dtgzt":
                    sReturn = tgztGame.tgztPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "mhxy":
                    sReturn = mhxyGame.mhxyPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "qxz":
                    sReturn = qxzGame.qxzPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "qszg":
                    sReturn = qszgGame.qszgPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "wwsg":
                    sReturn = wwsgGame.wwsgPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "dntg":
                    sReturn = dntgGame.dntgPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "jy":
                    sReturn = jyGame.jyPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "sskc":
                    sReturn = sskcGame.jyPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "ktpd":
                    sReturn = ktpdGame.ktpdPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "mhtj":
                    sReturn = mhtjGame.mhtjPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "jjp":
                    sReturn = jjpGame.jjpPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "sgyjz":
                    sReturn = sgyjzGame.sgyjzPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "dtgzter":
                    sReturn = tgzt2Game.tgztPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
                case "zwx":
                    sReturn = zwxGame.zwxPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID);
                    break;
            }
            return sReturn;
        }

        public static string sqPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID,string sRoleID)
        {
            string sReturn = string.Empty;
            sReturn = sqGame.sqPay(sGameAbbre, iUserID, sUserName, iPayPoints, sPhone, iGUserID,sRoleID);
            return sReturn;
        }

        public static string GameQuickPay(string sGameAbbre,string sUserName,decimal dPirce,string sTranID)
        {
            if (!TransGBLL.TranIDVal(sTranID))
            {
                return "traniderr";
            }
            int iState = TransGBLL.TranIDStateSel(sTranID);
            if (iState == 1)
            {
                return "0";
            }
            else if(iState != 0)
            {
                return string.Format("state:{0}", iState);
            }
            string sReturn = string.Empty;
            string sGame = GameInfoBLL.GameInfoAbbreSel(sGameAbbre).TrimEnd();
            switch (sGame)
            {
                case "sssg":
                    sReturn = sssgGame.sssgQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "sxd":
                    sReturn = sxdGame.sxdQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "lj":
                    sReturn = ljGame.LJQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "dxz":
                    sReturn = dxzGame.dxzQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "djj":
                    sReturn = djjGame.djjQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "txj":
                    sReturn = txjGame.txjQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "sjsg":
                    sReturn = sjsgGame.sjsgQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "tzcq":
                    sReturn = tzcqGame.tzcqQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "by":
                    sReturn = byGame.byQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "swjt":
                    sReturn = swjtGame.swjtQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "gcld":
                    sReturn = gcldGame.gcldQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "khbd":
                    sReturn = khbdGame.khbdQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "hyjft":
                    sReturn = hyjftGame.hyjftQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "nslm":
                    sReturn = nslmGame.nslmQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "yjxy":
                    sReturn = yjxyGame.yjxyQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "dtgzt":
                    sReturn = tgztGame.tgztQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "mhxy":
                    sReturn = mhxyGame.mhxyQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "qxz":
                    sReturn = qxzGame.qxzQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "qszg":
                    sReturn = qszgGame.qszgQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "wwsg":
                    sReturn = wwsgGame.wwsgQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "dntg":
                    sReturn = dntgGame.dntgQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "jy":
                    sReturn = jyGame.jyQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "sskc":
                    sReturn = sskcGame.jyQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "ktpd":
                    sReturn = ktpdGame.ktpdQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "mhtj":
                    sReturn = mhtjGame.mhtjQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "jjp":
                    sReturn = jjpGame.jjpQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "sgyjz":
                    sReturn = sgyjzGame.sgyjzQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "dtgzter":
                    sReturn = tgzt2Game.tgztQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
                case "zwx":
                    sReturn = zwxGame.zwxQucikPay(sGameAbbre, sUserName, dPirce, sTranID);
                    break;
            }
            return sReturn;
        }

        public static string sqQuickPay(string sGameAbbre, string sUserName, decimal dPirce, string sTranID,string sRoleID)
        {
            if (!TransGBLL.TranIDVal(sTranID))
            {
                return "traniderr";
            }
            int iState = TransGBLL.TranIDStateSel(sTranID);
            if (iState == 1)
            {
                return "0";
            }
            else if (iState != 0)
            {
                return string.Format("state:{0}", iState);
            }
            string sReturn = string.Empty;
            sReturn = sqGame.sqQucikPay(sGameAbbre, sUserName, dPirce, sTranID,sRoleID);
            return sReturn;
        }

        public static string QuickPay(string sChannel, string sPhone, string sAccount, decimal dPrice, int iCount, string sBankName,string sGameNameC)
        {
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranDirect = string.Empty;
            string sGameName = sGameNameC.Split('|')[0];
            switch (sChannel)
            {
                case "alipay":
                case "ibank":
                    string sPTranID = TransPBLL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount,sTranIP);
                    decimal dFeeScale = ChannelBLL.FeeScaleSel(sChannel);
                    int iGamePoints = Convert.ToInt32(dPrice * 10 * dFeeScale);
                    int iPayUserID = UserBll.UserIDSel(sAccount);
                    string sGTranID = TransGBLL.GameSalesInit(sGameName, iGamePoints, sAccount, sPhone, iPayUserID,sTranIP);
                    TranQuickBLL.TranQuickAdd(sGTranID, sPTranID);
                    sTranDirect = AliPay.QuickPayDirect(sPTranID,dPrice,sChannel,sBankName,sAccount,sGameNameC);
                    break;
                case "yp-szx":
                case "yp-dx":
                case "yp-lt":
                case "yp-zt":
                case "yp-sd":
                case "yp-jcard":
                case "yp-bank":
                    sTranDirect = YeePayBuy.QuickPayDirect(sChannel,sPhone,sAccount,dPrice,iCount,sGameNameC);
                    break;
                case "szfbank":
                    string sTranID = TransPBLL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount, sTranIP);
                    dFeeScale = ChannelBLL.FeeScaleSel(sChannel);
                    iGamePoints = Convert.ToInt32(dPrice * 10 * dFeeScale);
                    iPayUserID = UserBll.UserIDSel(sAccount);
                    sGTranID = TransGBLL.GameSalesInit(sGameName, iGamePoints, sAccount, sPhone, iPayUserID, sTranIP);
                    TranQuickBLL.TranQuickAdd(sGTranID, sTranID);
                    sTranDirect = QdbPayBuy.QuickPayBegin(sTranID, sAccount, dPrice, sGameNameC);
                    break;
                case "vpay":
                    string sOrderID = TransPBLL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount,sTranIP);
                    dFeeScale = ChannelBLL.FeeScaleSel(sChannel);
                    iGamePoints = Convert.ToInt32(dPrice * 10 * dFeeScale);
                    iPayUserID = UserBll.UserIDSel(sAccount);
                    sGTranID = TransGBLL.GameSalesInit(sGameName, iGamePoints, sAccount, sPhone, iPayUserID,sTranIP);
                    TranQuickBLL.TranQuickAdd(sGTranID, sOrderID);
                    sTranDirect = VPayBuy.QuickPayBegin(sOrderID, sAccount, dPrice, sGameNameC);
                    break;
            }
            return sTranDirect;
        }

        public static string GetGameName(string sGameAbbre)
        {
            return ServerDAL.ServerTitleSel(sGameAbbre);
        }

        public static string ValUserLoginGame(string sGameAbbre,string sUserID)
        {
            string sReturn = string.Empty;
            string sGame = GameInfoBLL.GameInfoAbbreSel(sGameAbbre).TrimEnd();
            switch (sGame)
            {
                case "sssg":
                    sReturn = sssgGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "sxd":
                    sReturn = sxdGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "lj":
                    sReturn = ljGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "dxz":
                    sReturn = dxzGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "djj":
                    sReturn = djjGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "txj":
                    sReturn = txjGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "tzcq":
                    sReturn = tzcqGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "by":
                    sReturn = byGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "swjt":
                    sReturn = swjtGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "khbd":
                    sReturn = khbdGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "hyjft":
                    sReturn = hyjftGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "nslm":
                    sReturn = nslmGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "yjxy":
                    sReturn = yjxyGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "dtgzt":
                    sReturn = tgztGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "mhxy":
                    sReturn = mhxyGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "qxz":
                    sReturn = qxzGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "qszg":
                    sReturn = qszgGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "wwsg":
                    sReturn = wwsgGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "dntg":
                    sReturn = dntgGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "jy":
                    sReturn = jyGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "sskc":
                    sReturn = sskcGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "ktpd":
                    sReturn = ktpdGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "mhtj":
                    sReturn = mhtjGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "jjp":
                    sReturn = jjpGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "sgyjz":
                    sReturn = sgyjzGame.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "dtgzter":
                    sReturn = tgzt2Game.GameisLogin(sUserID, sGameAbbre);
                    break;
                case "zwx":
                    sReturn = zwxGame.GameisLogin(sUserID, sGameAbbre);
                    break;
            }
            return sReturn;
        }
    }
}
