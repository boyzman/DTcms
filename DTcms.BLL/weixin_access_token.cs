using System;
using System.Data;
using System.Collections.Generic;

namespace DTcms.BLL
{
	/// <summary>
	/// 公众平台AccessToken值
	/// </summary>
	public partial class weixin_access_token
	{
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.weixin_access_token dal;
		public weixin_access_token()
		{
            dal = new DAL.weixin_access_token(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法===================================
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

        /// <summary>
        /// 快速增加一条数据
        /// </summary>
        public int Add(int account_id, string access_token)
        {
            Model.weixin_access_token model = new Model.weixin_access_token();
            model.account_id = account_id;
            model.access_token = access_token;
            model.count = 1;
            model.expires_in = 1200; //1200秒
            model.add_time = DateTime.Now;
            return dal.Add(model);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Model.weixin_access_token model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.weixin_access_token model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.weixin_access_token GetModel(int id)
		{
			return dal.GetModel(id);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		#endregion

        #region 扩展方法===================================
        /// <summary>
        /// 是否存在该公众账户记录
        /// </summary>
        public bool ExistsAccount(int account_id)
        {
            return dal.ExistsAccount(account_id);
        }

        /// <summary>
        /// 获取该公众账户的AccessToken实体
        /// </summary>
        public Model.weixin_access_token GetAccountModel(int account_id)
        {
            return dal.GetAccountModel(account_id);
        }
        #endregion
    }
}

