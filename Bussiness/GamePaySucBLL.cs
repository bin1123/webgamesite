using System;
using DataAccess;
using DataEnity;

using Common;

namespace Bussiness
{
    public class GamePaySucBLL
    {
        public static int GamePaySucAdd(int payuserid, int guserid, int point, string sTranID, string sGameName,int iServerID)
        {
            return GamePaySucDAL.GamePaySucAdd(payuserid, guserid, point,sTranID,sGameName,iServerID);
        }

        public static int UPointSelByGNTime(string sGameAbbre, int iPayUserID, DateTime dtBeginTime, DateTime dtEndTime)
        {
            return GamePaySucDAL.UPointSelByGNTime(sGameAbbre, iPayUserID, dtBeginTime, dtEndTime);
        }
    }
}
