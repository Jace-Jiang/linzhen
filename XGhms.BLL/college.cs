using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGhms.BLL
{
    /// <summary>
    /// 学院（部门）管理
    /// </summary>
    public partial class college
    {
        DAL.college collegeDal = new DAL.college();

        /// <summary>
        /// 根据学院名称检查是否存在该记录
        /// </summary>
        /// <param name="id">学院名称</param>
        /// <returns>true or false</returns>
        public bool Exists(string collegeName)
        {
            return collegeDal.Exists(collegeName);
        }
        /// <summary>
        /// 根据ID来删除相应的数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            return collegeDal.Delete(id);
        }
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">学院id</param>
        /// <returns>model对象</returns>
        public Model.college GetModel(int id)
        {
            return collegeDal.GetModel(id);
        }
        /// <summary>
        /// 插入新的数据
        /// </summary>
        /// <param name="collegeName">学院名称</param>
        /// <param name="usersList">用户列表</param>
        /// <returns>受影响的行数</returns>
        public int InsertNewCollege(string collegeName, string usersList)
        {
            return collegeDal.InsertNewCollege(collegeName, usersList);
        }
        /// <summary>
        /// 根据学院ID来更新学院信息
        /// </summary>
        /// <param name="collegeName">学院名称</param>
        /// <param name="usersList">用户列表</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>受影响的行数</returns>
        public int UpdateCollegeByID(string collegeName, string usersList,int collegeID)
        {
            return collegeDal.UpdateCollegeByID(collegeName, usersList, collegeID);
        }

        /// <summary>
        /// 获取所有的学院部门列表
        /// </summary>
        /// <returns>model列表</returns>
        public IEnumerable<Model.college> GetAllCollegeList()
        {
            return collegeDal.GetAllCollegeList();
        }
        /// <summary>
        /// 根据学院名称获取学院ID
        /// </summary>
        /// <param name="collegeName">学院名称</param>
        /// <returns>学院ID</returns>
        public int GetCollegeIDByCollegeName(string collegeName)
        {
            return collegeDal.GetCollegeIDByCollegeName(collegeName);
        }
    }
}
