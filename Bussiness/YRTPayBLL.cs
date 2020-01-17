using System;

using DataAccess;
using DataEnity;
using Common;

namespace Bussiness
{
    public class YRTPayBLL
    {
        public static int YRTPayAdd(YRTPay yrtPayObject)
        {
            return YRTPayDAL.Add(yrtPayObject);
        }

        public static string ParmVal(int iUserID, int iPoint, string tid, string pass)
        {
            string sRes = string.Empty;
            if(iUserID < 1000)
            {
                sRes = "uid < 1000";
            }
            else if(iPoint < 10)
            {
                sRes = "vcpoints < 10";
            }
            else if(tid.Length < 1)
            {
                sRes = "tid length < 1";
            }
            else if (pass.Length < 1)
            {
                sRes = "pass length < 1";
            }
            return sRes;
        }

        public static bool PassVal(string uid,string vcpoints,string tid,string pass)
        {
            string key = "daaa5376b6bb11e2yrt87ff842b2b627011";
            //$pass = md5($uid.$vcpoints.$tid.$key)
            string sPassMD5 = ProvideCommon.MD5(string.Format("{0}{1}{2}{3}",uid,vcpoints,tid,key));
            if (sPassMD5 == pass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IPVal()
        {
            string sAllowIP = "";
            string sPayIP = ProvideCommon.GetRealIP();
            if (sAllowIP.IndexOf(sPayIP) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string Pay(string sTID,int iPoint,int iUserID,string sOfferName)
        {
            string sRes = string.Empty;
            if (iPoint > 2000)
            {
                sRes = string.Format("{\"uid\":\"{0}\",\"vcpoints\":\"{1}\",\"tid\":\"{2}\",\"offer_name\":\"{3}\",\"status\":\"1004\"}",
                                     iUserID, iPoint, sTID, sOfferName);
            }
            else
            { 
                decimal dPrice = Convert.ToDecimal(iPoint/10);
                string sTranIP = ProvideCommon.GetRealIP();
                string sTranID = TransPBLL.YRTSalesInit(iUserID, iPoint, dPrice, sTranIP);
                if(sTranID.Length < 10)
                {
                    return "tranid init err";
                }
                YRTPay yrtPayObject = new YRTPay();
                yrtPayObject.OfferName = sOfferName;
                yrtPayObject.Point = iPoint;
                yrtPayObject.TID = sTID;
                yrtPayObject.TranID = sTranID;
                yrtPayObject.TranIP = ProvideCommon.GetRealIP();
                yrtPayObject.TranTime = DateTime.Now;
                yrtPayObject.UserID = iUserID;
                int iAddNum = YRTPayAdd(yrtPayObject);
                if (iAddNum > 0)
                {
                    int iTranRes = TransPBLL.YRTSalesCommit(sTranID, iUserID, iPoint);
                    if (iTranRes == 0)
                    {
                        sRes = string.Format("{\"uid\":\"{0}\",\"vcpoints\":\"{1}\",\"tid\":\"{2}\",\"offer_name\":\"{3}\",\"status\":\"success\"}",
                                         iUserID, iPoint, sTID, sOfferName);
                    }
                    else
                    {
                        sRes = iTranRes.ToString();
                    }
                }
                else
                {
                    sRes = string.Format("{\"uid\":\"{0}\",\"vcpoints\":\"{1}\",\"tid\":\"{2}\",\"offer_name\":\"{3}\",\"status\":\"yrtpay insert err\"}",
                                         iUserID, iPoint, sTID, sOfferName);
                }
            }
            return sRes;
        }
    }
}
