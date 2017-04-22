using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace XGhms.BLL
{
    /// <summary>
    /// 用户信息管理
    /// </summary>
    public partial class users_info
    {
        DAL.users_info userinfoDal = new DAL.users_info();
        DAL.users userDal = new DAL.users();
        DAL.college collegeDal = new DAL.college();
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">用户信息id</param>
        /// <returns>model对象</returns>
        public Model.users_info GetModel(int id)
        {
            return userinfoDal.GetModel(id);
        }
        /// <summary>
        /// 根据用户ID获取用户的真是姓名
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns>用户姓名</returns>
        public string GetUserRealNameForID(int UserID)
        {
            return userinfoDal.GetUserRealNameForID(UserID);
        }
        public Model.users_info GetModelByUserID(int UserID)
        {
            return userinfoDal.GetModelByUserID(UserID);
        }
        /// <summary>
        /// 搜索专用方法，用于搜索用户列表
        /// </summary>
        /// <param name="str">搜索关键词</param>
        /// <returns>用户的列表</returns>
        public DataTable ExistsUser(string str)
        {
            //首先判断是否存在该用户（根据编号或者姓名来查询）
            if (userDal.Exists(str))
            {
                //如果存在该编号的用户
                Model.users userModel = userDal.GetModelByUserNum(str); //获取用户表里面的model
                Model.users_info userInfoModel = userinfoDal.GetModelByUserID(userModel.id); //获取用户信息表里面的model
                Model.college collegeModel = collegeDal.GetModel(userInfoModel.college_id); //获取学院表里面的model
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(Int32));
                dt.Columns.Add("user_number", typeof(String));
                dt.Columns.Add("role_id", typeof(Int32));
                dt.Columns.Add("real_name", typeof(String));
                dt.Columns.Add("college_id", typeof(Int32));
                dt.Columns.Add("college_name", typeof(String));
                dt.Rows.Add(new Object[] { 
                    userModel.id,
                    userModel.user_number, 
                    userModel.role_id,
                    userInfoModel.real_name,
                    userInfoModel.college_id,
                    collegeModel.college_name
                });
                return dt;
            }
            else
            {
                if (userinfoDal.Exists(str))
                {
                    //如果存在该姓名的用户
                    IEnumerable<Model.users_info> modelList = userinfoDal.GetModelListByName(str);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id", typeof(Int32));
                    dt.Columns.Add("user_number", typeof(String));
                    dt.Columns.Add("role_id", typeof(Int32));
                    dt.Columns.Add("real_name", typeof(String));
                    dt.Columns.Add("college_id", typeof(Int32));
                    dt.Columns.Add("college_name", typeof(String));
                    foreach (Model.users_info model in modelList)
                    {
                        Model.users userModel = userDal.GetModel(model.user_id); //获取用户表里面的model
                        Model.college collegeModel = collegeDal.GetModel(model.college_id); //获取学院表里面的model
                        dt.Rows.Add(new Object[] { 
                            userModel.id,
                            userModel.user_number, 
                            userModel.role_id,
                            model.real_name,
                            model.college_id,
                            collegeModel.college_name
                        });
                    }
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 设置学生的班级id
        /// </summary>
        /// <param name="userID">学生ID</param>
        /// <param name="classID">班级ID</param>
        /// <returns>受影响的行数</returns>
        public int UpdateClassByUserID(int userID, int classID)
        {
            return userinfoDal.UpdateClassByUserID(userID, classID);
        }
        /// <summary>
        /// 更新学生的学院和本机
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <param name="classID">班级ID</param>
        /// <returns>受影响的行数</returns>
        public int UpdateClassByUserID(int userID, string realName, int sex, int collegeID, int classID, string major)
        {
            return userinfoDal.UpdateClassByUserID(userID, realName, sex, collegeID, classID,major);
        }
        /// <summary>
        /// 根据班级ID获取班上的所有人的名单，用于导出Excel表
        /// </summary>
        /// <param name="classID">班级ID</param>
        /// <returns>DataSet</returns>
        public DataSet GetAllStudentByClassID(int classID)
        {
            return userinfoDal.GetAllStudentByClassID(classID);
        }
        /// <summary>
        /// 根据学生角色的用户列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <param name="classID">班级ID</param>
        /// <returns>用户列表</returns>
        public int GetTotalNumOFStuandClsandRol(int roleID, int collegeID, int classID)
        {
            return userinfoDal.GetTotalNumOFStuandClsandRol(roleID, collegeID, classID);
        }
        /// <summary>
        /// 根据老师角色的用户列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>用户列表数目</returns>
        public int GetTotalNumOfTerandCls(int roleID, int collegeID)
        {
            return userinfoDal.GetTotalNumOfTerandCls(roleID, collegeID);
        }
        /// <summary>
        /// 根据管理员角色获取用户列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>用户列表数目</returns>
        public int GetTotalNumOfAdm(int roleID)
        {
            return userinfoDal.GetTotalNumOfAdm(roleID);
        }
        /// <summary>
        /// 执行存储过程来显示分页
        /// </summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>显示xg_users_info表里面的id列表</returns>
        public DataTable GetPageOfRoleForUserList(string ProcedureName, SqlParameter[] parameters)
        {
            return userinfoDal.GetPageOfRoleForUserList(ProcedureName, parameters);
        }
        /// <summary>
        /// 根据班级获取信息
        /// </summary>
        /// <param name="classID">班级ID</param>
        /// <returns>用户列表</returns>
        public IEnumerable<Model.users_info> GetUserListByClassID(int classID, int role_id)
        {
            return userinfoDal.GetUserListByClassID(classID, role_id);
        }
        /// <summary>
        /// 根据学院ID获取所有的老师角色列表
        /// </summary>
        /// <param name="collegeID">学院ID</param>
        /// <param name="role_id">角色ID</param>
        /// <returns>老师列表</returns>
        public IEnumerable<Model.users_info> GetTerListByCollegeID(int collegeID, int role_id)
        {
            return userinfoDal.GetTerListByCollegeID(collegeID, role_id);
        }
        /// <summary>
        /// 获取所有的管理员
        /// </summary>
        /// <param name="roleID">管理员ID列表</param>
        /// <returns>管理员列表</returns>
        public IEnumerable<Model.users_info> GetAdminList(int[] roleID)
        {
            return userinfoDal.GetAdminList(roleID);
        }
        /// <summary>
        /// 如果用户查看自己的信息，不存在时插入
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="roleID">角色ID</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="birthday">生日</param>
        /// <param name="telPhone">联系电话</param>
        /// <param name="email">电子邮件</param>
        /// <param name="address">地址</param>
        /// <param name="explain">简介</param>
        /// <returns>受影响的行数</returns>
        public int InsertUserInfoOfUserID(int userID, int roleID, string realName, string birthday, string telPhone, string email, string address, string explain)
        {
            return userinfoDal.InsertUserInfoOfUserID( userID,  roleID,  realName,  birthday,  telPhone,  email,  address,  explain);
        }
        /// <summary>
        /// 用户查看自己的信息，保存自己的信息
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="birthday">生日</param>
        /// <param name="telPhone">联系电话</param>
        /// <param name="email">Email</param>
        /// <param name="address">联系地址</param>
        /// <param name="explain">个人简介</param>
        /// <returns>受影响的行数</returns>
        public int UpdateUserInfoOfUserID(int id, string realName, string birthday, string telPhone, string email, string address, string explain)
        {
            return userinfoDal.UpdateUserInfoOfUserID(id, realName, birthday, telPhone, email, address, explain);
        }
    }
}
