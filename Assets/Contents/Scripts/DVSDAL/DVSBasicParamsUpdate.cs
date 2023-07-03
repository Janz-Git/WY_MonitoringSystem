using System;
using System.Data;
using System.Text;
using DotNet.FrameWork.Data;
using MySql.Data.MySqlClient;

namespace DiBo.DAL
{
    /// <summary>
    /// 数据访问类:DVSBasicParamsUpdate
    /// </summary>
    public partial class DVSBasicParamsUpdate
    {
        public DVSBasicParamsUpdate()
        { }
        #region  BasicMethod

        public static Model.DVSBasicParamsUpdate SaveDVSBasicParamsUpdate(Model.DVSBasicParamsUpdate basicParams)
        {
            Model.DVSBasicParamsUpdate model = basicParams;

            var obj = DbHelperMySQL.ExecuteScalar("select ParamsID from DVSBasicParamsUpdate where DeviceID='" + basicParams.DeviceID + "'");
            if (obj != null)
            {
                model.ParamsID = int.Parse(obj.ToString());
                Update(model);
            }
            else
            {
                model.ParamsID = Add(model);
            }

            return model;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ParamsID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dvsbasicparamsupdate");
            strSql.Append(" where ParamsID=@ParamsID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ParamsID", MySqlDbType.Int32)
            };
            parameters[0].Value = ParamsID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(DiBo.Model.DVSBasicParamsUpdate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DVSBasicParamsUpdate(");
            strSql.Append("CommandCode,DeviceID,DMAID,TotalPoints,FrameNumber,AvgNumber,HighpassHz,Scanrate,PulseWidth,FiberLength1,FiberOffset1,Noiselevel1,FiberLength2,FiberOffset2,Noiselevel2,AlarmResolution,MinimumAlarmCnt,DurationCnt,DealingWind,WindWindow,WindMinimumCnt,CreateDate,UpdateDate)");
            strSql.Append(" values (");
            strSql.Append("@CommandCode,@DeviceID,@DMAID,@TotalPoints,@FrameNumber,@AvgNumber,@HighpassHz,@Scanrate,@PulseWidth,@FiberLength1,@FiberOffset1,@Noiselevel1,@FiberLength2,@FiberOffset2,@Noiselevel2,@AlarmResolution,@MinimumAlarmCnt,@DurationCnt,@DealingWind,@WindWindow,@WindMinimumCnt,@CreateDate,@UpdateDate)");
            strSql.Append(";SELECT LAST_INSERT_ID()");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CommandCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DeviceID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@DMAID", MySqlDbType.Int32),
                    new MySqlParameter("@TotalPoints", MySqlDbType.Int32),
                    new MySqlParameter("@FrameNumber", MySqlDbType.Int32),
                    new MySqlParameter("@AvgNumber", MySqlDbType.Int32),
                    new MySqlParameter("@HighpassHz", MySqlDbType.Float),
                    new MySqlParameter("@Scanrate", MySqlDbType.Float),
                    new MySqlParameter("@PulseWidth", MySqlDbType.Int32),
                    new MySqlParameter("@FiberLength1", MySqlDbType.Int32),
                    new MySqlParameter("@FiberOffset1", MySqlDbType.Int32),
                    new MySqlParameter("@Noiselevel1", MySqlDbType.Int32),
                    new MySqlParameter("@FiberLength2", MySqlDbType.Int32),
                    new MySqlParameter("@FiberOffset2", MySqlDbType.Int32),
                    new MySqlParameter("@Noiselevel2", MySqlDbType.Int32),
                    new MySqlParameter("@AlarmResolution", MySqlDbType.Int32),
                    new MySqlParameter("@MinimumAlarmCnt", MySqlDbType.Int32),
                    new MySqlParameter("@DurationCnt", MySqlDbType.Int32),
                    new MySqlParameter("@DealingWind", MySqlDbType.Int32),
                    new MySqlParameter("@WindWindow", MySqlDbType.Int32),
                    new MySqlParameter("@WindMinimumCnt", MySqlDbType.Int32),
                    new MySqlParameter("@CreateDate", MySqlDbType.DateTime),
                    new MySqlParameter("@UpdateDate", MySqlDbType.DateTime)};
            parameters[0].Value = model.CommandCode;
            parameters[1].Value = model.DeviceID;
            parameters[2].Value = model.DMAID;
            parameters[3].Value = model.TotalPoints;
            parameters[4].Value = model.FrameNumber;
            parameters[5].Value = model.AvgNumber;
            parameters[6].Value = model.HighpassHz;
            parameters[7].Value = model.Scanrate;
            parameters[8].Value = model.PulseWidth;
            parameters[9].Value = model.FiberLength1;
            parameters[10].Value = model.FiberOffset1;
            parameters[11].Value = model.Noiselevel1;
            parameters[12].Value = model.FiberLength2;
            parameters[13].Value = model.FiberOffset2;
            parameters[14].Value = model.Noiselevel2;
            parameters[15].Value = model.AlarmResolution;
            parameters[16].Value = model.MinimumAlarmCnt;
            parameters[17].Value = model.DurationCnt;
            parameters[18].Value = model.DealingWind;
            parameters[19].Value = model.WindWindow;
            parameters[20].Value = model.WindMinimumCnt;
            parameters[21].Value = model.CreateDate;
            parameters[22].Value = model.UpdateDate;

            var obj = DbHelperMySQL.ExecuteScalar(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(DiBo.Model.DVSBasicParamsUpdate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dvsbasicparamsupdate set ");
            strSql.Append("CommandCode=@CommandCode,");
            strSql.Append("DeviceID=@DeviceID,");
            strSql.Append("DMAID=@DMAID,");
            strSql.Append("TotalPoints=@TotalPoints,");
            strSql.Append("FrameNumber=@FrameNumber,");
            strSql.Append("AvgNumber=@AvgNumber,");
            strSql.Append("HighpassHz=@HighpassHz,");
            strSql.Append("Scanrate=@Scanrate,");
            strSql.Append("PulseWidth=@PulseWidth,");
            strSql.Append("FiberLength1=@FiberLength1,");
            strSql.Append("FiberOffset1=@FiberOffset1,");
            strSql.Append("Noiselevel1=@Noiselevel1,");
            strSql.Append("FiberLength2=@FiberLength2,");
            strSql.Append("FiberOffset2=@FiberOffset2,");
            strSql.Append("Noiselevel2=@Noiselevel2,");
            strSql.Append("AlarmResolution=@AlarmResolution,");
            strSql.Append("MinimumAlarmCnt=@MinimumAlarmCnt,");
            strSql.Append("DurationCnt=@DurationCnt,");
            strSql.Append("DealingWind=@DealingWind,");
            strSql.Append("WindWindow=@WindWindow,");
            strSql.Append("WindMinimumCnt=@WindMinimumCnt,");
            strSql.Append("UpdateDate=@UpdateDate");
            strSql.Append(" where ParamsID=@ParamsID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CommandCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DeviceID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@DMAID", MySqlDbType.Int32),
                    new MySqlParameter("@TotalPoints", MySqlDbType.Int32),
                    new MySqlParameter("@FrameNumber", MySqlDbType.Int32),
                    new MySqlParameter("@AvgNumber", MySqlDbType.Int32),
                    new MySqlParameter("@HighpassHz", MySqlDbType.Float),
                    new MySqlParameter("@Scanrate", MySqlDbType.Float),
                    new MySqlParameter("@PulseWidth", MySqlDbType.Int32),
                    new MySqlParameter("@FiberLength1", MySqlDbType.Int32),
                    new MySqlParameter("@FiberOffset1", MySqlDbType.Int32),
                    new MySqlParameter("@Noiselevel1", MySqlDbType.Int32),
                    new MySqlParameter("@FiberLength2", MySqlDbType.Int32),
                    new MySqlParameter("@FiberOffset2", MySqlDbType.Int32),
                    new MySqlParameter("@Noiselevel2", MySqlDbType.Int32),
                    new MySqlParameter("@AlarmResolution", MySqlDbType.Int32),
                    new MySqlParameter("@MinimumAlarmCnt", MySqlDbType.Int32),
                    new MySqlParameter("@DurationCnt", MySqlDbType.Int32),
                    new MySqlParameter("@DealingWind", MySqlDbType.Int32),
                    new MySqlParameter("@WindWindow", MySqlDbType.Int32),
                    new MySqlParameter("@WindMinimumCnt", MySqlDbType.Int32),
                    new MySqlParameter("@UpdateDate", MySqlDbType.DateTime),
                    new MySqlParameter("@ParamsID", MySqlDbType.Int32)};
            parameters[0].Value = model.CommandCode;
            parameters[1].Value = model.DeviceID;
            parameters[2].Value = model.DMAID;
            parameters[3].Value = model.TotalPoints;
            parameters[4].Value = model.FrameNumber;
            parameters[5].Value = model.AvgNumber;
            parameters[6].Value = model.HighpassHz;
            parameters[7].Value = model.Scanrate;
            parameters[8].Value = model.PulseWidth;
            parameters[9].Value = model.FiberLength1;
            parameters[10].Value = model.FiberOffset1;
            parameters[11].Value = model.Noiselevel1;
            parameters[12].Value = model.FiberLength2;
            parameters[13].Value = model.FiberOffset2;
            parameters[14].Value = model.Noiselevel2;
            parameters[15].Value = model.AlarmResolution;
            parameters[16].Value = model.MinimumAlarmCnt;
            parameters[17].Value = model.DurationCnt;
            parameters[18].Value = model.DealingWind;
            parameters[19].Value = model.WindWindow;
            parameters[20].Value = model.WindMinimumCnt;
            parameters[21].Value = model.UpdateDate;
            parameters[22].Value = model.ParamsID;

            int rows = DbHelperMySQL.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static DiBo.Model.DVSBasicParamsUpdate GetModel(string strWhere="")
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  ParamsID,CommandCode,DeviceID,DMAID,TotalPoints,FrameNumber,AvgNumber,HighpassHz,Scanrate,PulseWidth,FiberLength1,FiberOffset1,Noiselevel1,FiberLength2,FiberOffset2,Noiselevel2,AlarmResolution,MinimumAlarmCnt,DurationCnt,DealingWind,WindWindow,WindMinimumCnt,CreateDate,UpdateDate from DVSBasicParamsUpdate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ParamsID desc limit 0,1");

            DataTable dtbl = DbHelperMySQL.ExecuteDataTable(strSql.ToString());
            if (dtbl.Rows.Count > 0)
            {
                return DataRowToModel(dtbl.Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static DiBo.Model.DVSBasicParamsUpdate DataRowToModel(DataRow row)
        {
            DiBo.Model.DVSBasicParamsUpdate model = new DiBo.Model.DVSBasicParamsUpdate();
            if (row != null)
            {
                if (row["ParamsID"] != null && row["ParamsID"].ToString() != "")
                {
                    model.ParamsID = int.Parse(row["ParamsID"].ToString());
                }
                if (row["CommandCode"] != null)
                {
                    model.CommandCode = row["CommandCode"].ToString();
                }
                if (row["DeviceID"] != null)
                {
                    model.DeviceID = row["DeviceID"].ToString();
                }
                if (row["DMAID"] != null && row["DMAID"].ToString() != "")
                {
                    model.DMAID = int.Parse(row["DMAID"].ToString());
                }
                if (row["TotalPoints"] != null && row["TotalPoints"].ToString() != "")
                {
                    model.TotalPoints = int.Parse(row["TotalPoints"].ToString());
                }
                if (row["FrameNumber"] != null && row["FrameNumber"].ToString() != "")
                {
                    model.FrameNumber = int.Parse(row["FrameNumber"].ToString());
                }
                if (row["AvgNumber"] != null && row["AvgNumber"].ToString() != "")
                {
                    model.AvgNumber = int.Parse(row["AvgNumber"].ToString());
                }
                if (row["HighpassHz"] != null && row["HighpassHz"].ToString() != "")
                {
                    model.HighpassHz = float.Parse(row["HighpassHz"].ToString());
                }
                if (row["Scanrate"] != null && row["Scanrate"].ToString() != "")
                {
                    model.Scanrate = float.Parse(row["Scanrate"].ToString());
                }
                if (row["PulseWidth"] != null && row["PulseWidth"].ToString() != "")
                {
                    model.PulseWidth = int.Parse(row["PulseWidth"].ToString());
                }
                if (row["FiberLength1"] != null && row["FiberLength1"].ToString() != "")
                {
                    model.FiberLength1 = int.Parse(row["FiberLength1"].ToString());
                }
                if (row["FiberOffset1"] != null && row["FiberOffset1"].ToString() != "")
                {
                    model.FiberOffset1 = int.Parse(row["FiberOffset1"].ToString());
                }
                if (row["Noiselevel1"] != null && row["Noiselevel1"].ToString() != "")
                {
                    model.Noiselevel1 = int.Parse(row["Noiselevel1"].ToString());
                }
                if (row["FiberLength2"] != null && row["FiberLength2"].ToString() != "")
                {
                    model.FiberLength2 = int.Parse(row["FiberLength2"].ToString());
                }
                if (row["FiberOffset2"] != null && row["FiberOffset2"].ToString() != "")
                {
                    model.FiberOffset2 = int.Parse(row["FiberOffset2"].ToString());
                }
                if (row["Noiselevel2"] != null && row["Noiselevel2"].ToString() != "")
                {
                    model.Noiselevel2 = int.Parse(row["Noiselevel2"].ToString());
                }
                if (row["AlarmResolution"] != null && row["AlarmResolution"].ToString() != "")
                {
                    model.AlarmResolution = int.Parse(row["AlarmResolution"].ToString());
                }
                if (row["MinimumAlarmCnt"] != null && row["MinimumAlarmCnt"].ToString() != "")
                {
                    model.MinimumAlarmCnt = int.Parse(row["MinimumAlarmCnt"].ToString());
                }
                if (row["DurationCnt"] != null && row["DurationCnt"].ToString() != "")
                {
                    model.DurationCnt = int.Parse(row["DurationCnt"].ToString());
                }
                if (row["DealingWind"] != null && row["DealingWind"].ToString() != "")
                {
                    model.DealingWind = int.Parse(row["DealingWind"].ToString());
                }
                if (row["WindWindow"] != null && row["WindWindow"].ToString() != "")
                {
                    model.WindWindow = int.Parse(row["WindWindow"].ToString());
                }
                if (row["WindMinimumCnt"] != null && row["WindMinimumCnt"].ToString() != "")
                {
                    model.WindMinimumCnt = int.Parse(row["WindMinimumCnt"].ToString());
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if (row["UpdateDate"] != null && row["UpdateDate"].ToString() != "")
                {
                    model.UpdateDate = DateTime.Parse(row["UpdateDate"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ParamsID,CommandCode,DeviceID,DMAID,TotalPoints,FrameNumber,AvgNumber,HighpassHz,Scanrate,PulseWidth,FiberLength1,FiberOffset1,Noiselevel1,FiberLength2,FiberOffset2,Noiselevel2,AlarmResolution,MinimumAlarmCnt,DurationCnt,DealingWind,WindWindow,WindMinimumCnt,CreateDate,UpdateDate ");
            strSql.Append(" FROM DVSBasicParamsUpdate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.ExecuteDataTable(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public static DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            
            strSql.Append(" ParamsID,CommandCode,DeviceID,DMAID,TotalPoints,FrameNumber,AvgNumber,HighpassHz,Scanrate,PulseWidth,FiberLength1,FiberOffset1,Noiselevel1,FiberLength2,FiberOffset2,Noiselevel2,AlarmResolution,MinimumAlarmCnt,DurationCnt,DealingWind,WindWindow,WindMinimumCnt,CreateDate,UpdateDate ");
            strSql.Append(" FROM DVSBasicParamsUpdate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            if (Top > 0)
            {
                strSql.Append(" limit 0," + Top.ToString());
            }
            return DbHelperMySQL.ExecuteDataTable(strSql.ToString());
        }

        #endregion  BasicMethod
    }
}


