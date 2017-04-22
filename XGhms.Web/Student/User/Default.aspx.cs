using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XGhms.Web.Student.User
{
    public partial class Default : Common.StuPageBase
    {
        BLL.users_info userinfoBll = new BLL.users_info();
        BLL.users userBll = new BLL.users();
        BLL.role roleBll = new BLL.role();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = (DataTable)Session["UserInfo"]; //获取Session的值
                int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取用户ID
                Model.users userModel = userBll.GetModel(userID);
                Model.users_info userinfoModel = userinfoBll.GetModelByUserID(userID);
                tb_UserName.Text = userModel.user_name;
                tb_UserNum.Text = userModel.user_number;
                if (userinfoModel != null)
                {
                    tb_RealName.Text = userinfoModel.real_name;
                    tb_UserBir.Text = userinfoModel.birthday.ToString("yyyy-MM-dd");
                    tb_UserEmail.Text = userinfoModel.email;
                    tb_UserPhone.Text = userinfoModel.telephone;
                    tb_UserAddress.Text = userinfoModel.address;
                    tb_UserIntro.Text = userinfoModel.explain;
                }
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取用户ID
            Model.users userModel = userBll.GetModel(userID);
            Model.users_info userinfoModel = userinfoBll.GetModelByUserID(userID);
            string bir = null;
            if (tb_UserBir.Text == "" || tb_UserBir.Text == null || tb_UserBir.Text == "0001-01-01")
            {
                bir = "1900-1-1";
            }
            else
            {
                bir = tb_UserBir.Text;
            }
            if (userinfoModel == null)
            {
                //执行插入
                int Ins = userinfoBll.InsertUserInfoOfUserID(userID, userModel.role_id, tb_RealName.Text, bir, tb_UserPhone.Text, tb_UserEmail.Text, tb_UserAddress.Text, tb_UserIntro.Text);
                if (Ins == 1)
                {
                    lab_infoMsg.Text = "保存成功!";
                }
                else
                {
                    lab_infoMsg.Text = "保存失败!";
                }
            }
            else
            {
                //执行更新
                int Upd = userinfoBll.UpdateUserInfoOfUserID(userinfoModel.id, tb_RealName.Text, bir, tb_UserPhone.Text, tb_UserEmail.Text, tb_UserAddress.Text, tb_UserIntro.Text);
                if (Upd == 1)
                {
                    lab_infoMsg.Text = "保存成功!";
                }
                else
                {

                    lab_infoMsg.Text = "保存失败!";
                }
            }
        }
    }
}