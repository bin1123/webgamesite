using System;
using DataAccess;
using DataEnity;

using Common;

namespace Bussiness
{
    public class GameGiftBLL
    {
        public static int GameGiftAdd(int iServerID, int iUserID, string sGift, string sGameName,string sGiftThing,string sGiftTranID)
        {
            return GameGiftDAL.GameGiftAdd(iServerID,iUserID,sGift,sGameName,sGiftThing,sGiftTranID);
        }

        public static int UCountSelByGift(int iUserID, string sGift)
        {
            return GameGiftDAL.UCountSelByGift(iUserID, sGift);
        }
    }
}
