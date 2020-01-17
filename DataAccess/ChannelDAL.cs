using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class ChannelDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static decimal FeeScaleSel(string sAbbre)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcChannel = dbUCenter.GetStoredProcCommand("SP_Channel_SelectFeeScaleByID");

            dbUCenter.AddInParameter(dcChannel, "@abbre", DbType.String, sAbbre);

            IDataReader drChannel = dbUCenter.ExecuteReader(dcChannel);
            decimal dFeeScale = 1;
            if (drChannel.Read())
            {
                decimal.TryParse(drChannel["feescale"].ToString(), out dFeeScale);
            }
            drChannel.Close();
            dcChannel.Dispose();
            return dFeeScale;
        }

        public static decimal FeeScaleSelByID(int channelid)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcChannel = dbUCenter.GetStoredProcCommand("SP_Channel_SelectFeeScaleByCID");

            dbUCenter.AddInParameter(dcChannel, "@channelid", DbType.Int32, channelid);

            IDataReader drChannel = dbUCenter.ExecuteReader(dcChannel);
            decimal dFeeScale = 1;
            if (drChannel.Read())
            {
                decimal.TryParse(drChannel["feescale"].ToString(), out dFeeScale);
            }
            drChannel.Close();
            dcChannel.Dispose();
            return dFeeScale;
        }

        public static int ChannelIDSelByAbbre(string sAbbre)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcChannel = dbUCenter.GetStoredProcCommand("Channel_SelectIDByAbbre");

            dbUCenter.AddInParameter(dcChannel, "@abbre", DbType.String, sAbbre);

            IDataReader drChannel = dbUCenter.ExecuteReader(dcChannel);
            int iChannelID = 0;
            if (drChannel.Read())
            {
                int.TryParse(drChannel[0].ToString(), out iChannelID);
            }
            drChannel.Close();
            dcChannel.Dispose();
            return iChannelID;
        }
    }
}
