using System;

using Common;
using Bussiness;

namespace UserCenter.Pay
{
    public partial class PTPayMulti : pagebase.PageBase
    {
        protected string sMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                if (Request.HttpMethod == "POST")
                {
                    string sFromHost = Request.UrlReferrer.Host;
                    SetFromHost(sFromHost);
                    int iPayUserID = GetUserID();//支付游戏的账号
                    string sAccount = CYRequest.GetFormString("gameaccount");//充值游戏账号
                    string sGameAbbre = CYRequest.GetFormString("gameservername");
                    int iUserID = UserBll.UserIDSel(sAccount);//充值游戏数字账号
                    if (iUserID < 1000)
                    {
                        Response.Redirect("PayGErr.aspx?err=201");
                        return;
                    }
                    else
                    {
                        string sGameIsLogin = PayAll.ValUserLoginGame(sGameAbbre, iUserID.ToString());
                        if("1" == sGameIsLogin) 
                        {
                            Response.Redirect("PayGErr.aspx?err=202");
                            return;
                        }
                    }
                    string sPhone = CYRequest.GetFormString("gamephone");
                    string sPayNums = CYRequest.GetFormString("gamepaynums");//平台币充值到游戏
                    string sPayAccount = GetAccount();//支付平台币账号
                    int iPayPoints = 0;
                    int.TryParse(sPayNums, out iPayPoints);
                    int iPUserPoints = UserPointsBLL.UPointAllSel(iPayUserID);
                    if (iPUserPoints > 0)
                    {
                        if (!UserPointsBLL.UPointCheck(iPayUserID))
                        {
                            Response.Redirect("PayGErr.aspx?err=203");
                            return;
                        }
                    }
                    else
                    {
                        Response.Redirect("PayGErr.aspx?err=204");
                        return;
                    }
                    if (iPUserPoints >= iPayPoints && (iPayPoints > 79 || iUserID < 10000))
                    {
                        string sReturn = string.Empty;
                        if (sGameAbbre.IndexOf("sq") == -1)
                        {
                            sReturn = PayAll.GamePay(sGameAbbre, iPayUserID, sPayAccount, iPayPoints, sPhone, iUserID);
                        }
                        else
                        { 
                            string sRoleID = CYRequest.GetFormString("gamerole");
                            if (sRoleID == "" || sRoleID == "unsafe string")
                            {
                                Response.Redirect("PayGErr.aspx?err=205");
                                return;
                            }
                            else
                            {
                                sReturn = PayAll.sqPay(sGameAbbre, iPayUserID, sPayAccount, iPayPoints, sPhone, iUserID, sRoleID);
                            }
                        }
                        string sRes = sReturn.Split('|')[0];
                        if (sRes == "0")
                        {
                            Server.Transfer(string.Format("PayGSucc.aspx?gname={0}&TranID={1}&gn={2}",sGameAbbre,sReturn.Split('|')[1],sGameAbbre));
                        }
                        else
                        {
                            //sMsg = "<script>alert('游戏充值失败，如有问题请联系客服！');location.href='default.aspx';</script>";
                            Response.Redirect("PayGErr.aspx?err=206");
                            return;
                        }
                    }
                    else
                    {
                        SetPoints(iPUserPoints);
                        //sMsg = "<script>alert('账号余额不足！请充值武林币！');location.href='default.aspx';</script>";
                        Response.Redirect("PayPErr.aspx?err=204");
                        return;
                    }
                }
            }
            else
            {
                //sMsg = "<script>alert('充值账号不存在！');location.href='default.aspx';</script>";
                Response.Redirect("PayGErr.aspx?err=201");
                return;
            }
        }
    }
}
