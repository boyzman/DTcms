using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
	/// <summary>
	/// 数据访问类:公众平台账户
	/// </summary>
	public partial class weixin_account
	{
        private string databaseprefix; //数据库表名前缀
        public weixin_account(string _databaseprefix)
		{
            databaseprefix = _databaseprefix;
        }

        #region 基本方法================================
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "weixin_account");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.weixin_account model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "weixin_account(");
            strSql.Append("name,originalid,wxcode,token,appid,appsecret,is_push,sort_id,add_time)");
            strSql.Append(" values (");
            strSql.Append("@name,@originalid,@wxcode,@token,@appid,@appsecret,@is_push,@sort_id,@add_time)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,100),
					new SqlParameter("@originalid", SqlDbType.NVarChar,50),
					new SqlParameter("@wxcode", SqlDbType.NVarChar,50),
					new SqlParameter("@token", SqlDbType.NVarChar,300),
					new SqlParameter("@appid", SqlDbType.NVarChar,100),
					new SqlParameter("@appsecret", SqlDbType.NVarChar,150),
					new SqlParameter("@is_push", SqlDbType.TinyInt,1),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.originalid;
            parameters[2].Value = model.wxcode;
            parameters[3].Value = model.token;
            parameters[4].Value = model.appid;
            parameters[5].Value = model.appsecret;
            parameters[6].Value = model.is_push;
            parameters[7].Value = model.sort_id;
            parameters[8].Value = model.add_time;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
		public bool Update(Model.weixin_account model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "weixin_account set ");
            strSql.Append("name=@name,");
            strSql.Append("originalid=@originalid,");
            strSql.Append("wxcode=@wxcode,");
            strSql.Append("token=@token,");
            strSql.Append("appid=@appid,");
            strSql.Append("appsecret=@appsecret,");
            strSql.Append("is_push=@is_push,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,100),
					new SqlParameter("@originalid", SqlDbType.NVarChar,50),
					new SqlParameter("@wxcode", SqlDbType.NVarChar,50),
					new SqlParameter("@token", SqlDbType.NVarChar,300),
					new SqlParameter("@appid", SqlDbType.NVarChar,100),
					new SqlParameter("@appsecret", SqlDbType.NVarChar,150),
					new SqlParameter("@is_push", SqlDbType.TinyInt,1),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.originalid;
            parameters[2].Value = model.wxcode;
            parameters[3].Value = model.token;
            parameters[4].Value = model.appid;
            parameters[5].Value = model.appsecret;
            parameters[6].Value = model.is_push;
            parameters[7].Value = model.sort_id;
            parameters[8].Value = model.add_time;
            parameters[9].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql7 = new StringBuilder();
            strSql7.Append("delete " + databaseprefix + "weixin_request_content ");
            strSql7.Append(" where account_id=@raccount_id");
            SqlParameter[] parameters7 = {
					new SqlParameter("@account_id", SqlDbType.Int,4)};
            parameters7[0].Value = id;
            CommandInfo cmd = new CommandInfo(strSql7.ToString(), parameters7);
            sqllist.Add(cmd);

            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("delete " + databaseprefix + "weixin_request_rule ");
            strSql6.Append(" where account_id=@raccount_id");
            SqlParameter[] parameters6 = {
					new SqlParameter("@account_id", SqlDbType.Int,4)};
            parameters6[0].Value = id;
            cmd = new CommandInfo(strSql6.ToString(), parameters6);
            sqllist.Add(cmd);

            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete from " + databaseprefix + "weixin_response_content");
            strSql5.Append(" where account_id=@account_id");
            SqlParameter[] parameters5 = {
					new SqlParameter("@account_id", SqlDbType.Int,4)};
            parameters5[0].Value = id;
            cmd = new CommandInfo(strSql5.ToString(), parameters5);
            sqllist.Add(cmd);

            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete from " + databaseprefix + "weixin_lbs_shop");
            strSql4.Append(" where account_id=@account_id");
            SqlParameter[] parameters4 = {
					new SqlParameter("@account_id", SqlDbType.Int,4)};
            parameters4[0].Value = id;
            cmd = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd);

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete from " + databaseprefix + "weixin_lbs_setting");
            strSql3.Append(" where account_id=@account_id");
            SqlParameter[] parameters3 = {
					new SqlParameter("@account_id", SqlDbType.Int,4)};
            parameters3[0].Value = id;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "weixin_access_token");
            strSql2.Append(" where account_id=@account_id");
            SqlParameter[] parameters2 = {
					new SqlParameter("@account_id", SqlDbType.Int,4)};
            parameters2[0].Value = id;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "weixin_account");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;
            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
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
		public Model.weixin_account GetModel(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select top 1 id,name,originalid,wxcode,token,appid,appsecret,is_push,sort_id,add_time");
            strSql.Append(" from " + databaseprefix + "weixin_account ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select id,name,originalid,wxcode,token,appid,appsecret,is_push,sort_id,add_time");
            strSql.Append(" FROM " + databaseprefix + "weixin_account ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by sort_id asc,add_time desc");
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" id,name,originalid,wxcode,token,appid,appsecret,is_push,sort_id,add_time ");
            strSql.Append(" FROM " + databaseprefix + "weixin_account");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "weixin_account");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
		#endregion

        #region 扩展方法================================
        /// <summary>
        /// 返回账户名称
        /// </summary>
        public string GetName(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 name from " + databaseprefix + "weixin_account");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }
            return title;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.weixin_account DataRowToModel(DataRow row)
        {
            Model.weixin_account model = new Model.weixin_account();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["originalid"] != null)
                {
                    model.originalid = row["originalid"].ToString();
                }
                if (row["wxcode"] != null)
                {
                    model.wxcode = row["wxcode"].ToString();
                }
                if (row["token"] != null)
                {
                    model.token = row["token"].ToString();
                }
                if (row["appid"] != null)
                {
                    model.appid = row["appid"].ToString();
                }
                if (row["appsecret"] != null)
                {
                    model.appsecret = row["appsecret"].ToString();
                }
                if (row["is_push"] != null && row["is_push"].ToString() != "")
                {
                    model.is_push = int.Parse(row["is_push"].ToString());
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "weixin_account set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public string GetToken(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 token from " + databaseprefix + "weixin_account");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 公众账户和原始ID是否对应
        /// </summary>
        public bool ExistsOriginalId(int id, string originalid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "weixin_account");
            strSql.Append(" where id=@id and originalid=@originalid");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@originalid", SqlDbType.NVarChar,50)};
            parameters[0].Value = id;
            parameters[1].Value = originalid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        #endregion
    }
}

