using DataAccess;
using DataEnity;
using System.Collections.Generic;
using System.Text;

namespace Bussiness
{
    public class GameInfoBLL
    {
        public static string GameInfoAbbreSel(string sAbbre)
        {
            return GameInfoDAL.GameInfoAbbreSel(sAbbre);
        }

        public static List<GameInfo> GameInfoSel()
        {
            return GameInfoDAL.GameInfoSel();
        }

        public static string GameInfoJsonSel()
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            List<GameInfo> lgObject = GameInfoSel();
            foreach(GameInfo gObject in lgObject)
            {
                if(gObject.GameName.Trim().Length > 1)
                {
                    sbText.Append("{");
                    sbText.AppendFormat("gamename:'{0}',abbre:'{1}'", gObject.GameName, gObject.abbre.Trim());
                    sbText.Append("},");
                }
            }
            int iIndex = sbText.Length - 1;  
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static List<ObjectThree> GameInfoSelC()
        {
            return GameInfoDAL.GameInfoSelC();
        }

        public static int GameInfoIDSel(string sAbbre)
        {
            return GameInfoDAL.GameInfoIDSel(sAbbre);
        }
    }
}
