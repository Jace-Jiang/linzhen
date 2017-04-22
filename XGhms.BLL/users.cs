using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XGhms.BLL
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public partial class users
    {
        DAL.users usersDal = new DAL.users();
        DAL.role roleDal = new DAL.role();
        DAL.users_info userinfoDal = new DAL.users_info();
        /// <summary>
        /// 简单的获取用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>简单的用户信息</returns>
        public DataTable GetUsersInfo(string userName)
        {
            DataTable dt=new DataTable();
            //dt = null;
            //根据用户名来检查是否存在该用户
            if (usersDal.ExistsByUserName(userName))
            {
                Model.users usersModel = new Model.users();
                Model.role roleModel = new Model.role();
                usersModel = usersDal.GetModelByUserName(userName);
                roleModel = roleDal.GetModel(usersModel.role_id);
                //按照以下的方式输出DataTable，以后如果需要更多的数据，可以直接从这里添加
                //用户ID，用户名，用户密码，用户角色，用户编号（学号/工号），是否锁定
                dt.Columns.Add("id",typeof(Int32));
                dt.Columns.Add("user_name", typeof(String));
                dt.Columns.Add("password", typeof(String));
                dt.Columns.Add("role_name", typeof(String));
                dt.Columns.Add("user_number", typeof(String));
                dt.Columns.Add("is_lock", typeof(Int32));
                //添加数据
                dt.Rows.Add(new Object[] { usersModel.id, usersModel.user_name, usersModel.password, roleModel.role_name, usersModel.user_number, usersModel.is_lock});
                return dt;
            }
            else //用户不存在则输出null
            {
                return dt;
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Exists(int id)
        {
            return usersDal.Exists(id);
        }
        /// <summary>
        /// 是否存在该编号的用户
        /// </summary>
        /// <param name="id">编号（学号或者教工号）</param>
        /// <returns>true or false</returns>
        public bool Exists(string usernum)
        {
            return usersDal.Exists(usernum);
        }
        /// <summary>
        /// 根据用户名和密码检查用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>true or false</returns>
        public bool Exists(string userName,string password)
        {
            return Exists(userName, password);
        }
        /// <summary>
        /// 根据用户ID获取用户的学号/教工号
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns>学号/教工号</returns>
        public string GetUserNumByUserID(int UserID)
        {
            return usersDal.GetUserNumByUserID(UserID);
        }
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>model对象</returns>
        public Model.users GetModel(int id)
        {
            return usersDal.GetModel(id);
        }
        /// <summary>
        /// 根据ID来删除相应的数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            if (userinfoDal.DeleteByUserID(id) == 1)
            {
                if (usersDal.Delete(id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="pwd">新的密码</param>
        /// <returns>返回受影响的行数</returns>
        public int UpdateUserPwd(int UserID, string pwd)
        {
            return usersDal.UpdateUserPwd(UserID, pwd);
        }

        /// <summary>
        /// 根据用户角色返回所有该角色的用户信息
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>返回该角色的所有用户</returns>
        public DataTable GetUserIDandNumByRoleID(int roleID)
        {
            return usersDal.GetUserIDandNumByRoleID(roleID);
        }
        /// <summary>
        /// 根据角色和学院ID获取用户列表和真实姓名
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>用户列表和真实姓名</returns>
        public DataTable GetHeadTeacherListByCollegeID(int roleID, int collegeID)
        {
            return usersDal.GetHeadTeacherListByCollegeID(roleID, collegeID);
        }
        /// <summary>
        /// 根据学号/工号获取用户的ID
        /// </summary>
        /// <param name="userNum">学号/工号</param>
        /// <returns>用户ID</returns>
        public int GetUserIDByUserNum(string userNum)
        {
            return usersDal.GetUserIDByUserNum(userNum);
        }
        /// <summary>
        /// 插入新的用户
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="userNum">学号/工号</param>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="isLock">是否锁定</param>
        /// <returns>产生的用户名</returns>
        public int InsertUserReturnID(int roleID, string userNum, string userName, string pwd, int isLock)
        {
            return usersDal.InsertUserReturnID(roleID, userNum, userName, pwd, isLock);
        }
        /// <summary>
        /// 根据用户ID更新用户的角色和锁定状态
        /// </summary>
        /// <param name="UserID">用户id</param>
        /// <param name="roleID">角色ID</param>
        /// <param name="isLock">锁定状态</param>
        /// <returns>受影响的行数</returns>
        public int UpdateUserByIDForAdmin(int UserID, int roleID, int isLock)
        {
            int ri= userinfoDal.UpdateUserByIDForAdmin(UserID, roleID);
            if (ri==1)
            {
                return usersDal.UpdateUserByIDForAdmin(UserID, roleID, isLock);
            }
            else
            {
                return 0;
            }
        }
    }
}
