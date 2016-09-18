using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.weixin
{
    public partial class account_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.weixin_account().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("weixin_account_manage", DTEnums.ActionEnum.View.ToString()); //检查权限
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.weixin_account bll = new BLL.weixin_account();
            Model.weixin_account model = bll.GetModel(_id);

            txtName.Text = model.name;
            txtOriginalId.Text = model.originalid;
            txtWxCode.Text = model.wxcode;
            txtToKen.Text = model.token;
            txtAppId.Text = model.appid;
            txtAppSecret.Text = model.appsecret;
            if (model.is_push== 1)
            {
                cbIsPush.Checked = true;
            }
            else
            {
                cbIsPush.Checked = false;
            }
            txtSortId.Text = model.sort_id.ToString();
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.weixin_account model = new Model.weixin_account();
            BLL.weixin_account bll = new BLL.weixin_account();

            model.name = txtName.Text.Trim();
            model.originalid = txtOriginalId.Text.Trim();
            model.wxcode = txtWxCode.Text.Trim();
            model.token = txtToKen.Text.Trim();
            model.appid = txtAppId.Text.Trim();
            model.appsecret = txtAppSecret.Text.Trim();
            if (cbIsPush.Checked == true)
            {
                model.is_push = 1;
            }
            else
            {
                model.is_push = 0;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);

            if (bll.Add(model) > 0)
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "增加公众账户:" + model.name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.weixin_account bll = new BLL.weixin_account();
            Model.weixin_account model = bll.GetModel(_id);

            model.name = txtName.Text.Trim();
            model.originalid = txtOriginalId.Text.Trim();
            model.wxcode = txtWxCode.Text.Trim();
            model.token = txtToKen.Text.Trim();
            model.appid = txtAppId.Text.Trim();
            model.appsecret = txtAppSecret.Text.Trim();
            if (cbIsPush.Checked == true)
            {
                model.is_push = 1;
            }
            else
            {
                model.is_push = 0;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);

            if (bll.Update(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改公众账户:" + model.name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("weixin_account_manage", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("修改公众账户成功！", "account_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("weixin_account_manage", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("添加公众账户成功！", "account_list.aspx");
            }
        }

    }
}