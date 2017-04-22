using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGhms.BLL
{
    /// <summary>
    /// 班级管理
    /// </summary>
    public partial class classes
    {
        DAL.classes classDal = new DAL.classes();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Exists(int id)
        {
            return classDal.Exists(id);
        }
        /// <summary>
        /// 根据班级名称检查是否存在该班级
        /// </summary>
        /// <param name="id">班级名称</param>
        /// <returns>true or false</returns>
        public bool Exists(string className)
        {
            return classDal.Exists(className);
        }
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">班级id</param>
        /// <returns>model对象</returns>
        public Model.classes GetModel(int id)
        {
            return classDal.GetModel(id);
        }
        /// <summary>
        /// 插入新的班级（系统管理员插入）
        /// </summary>
        /// <param name="className">班级名称</param>
        /// <param name="colegeID">学院ID</param>
        /// <param name="hterID">辅导员ID</param>
        /// <returns>返回受影响的行数</returns>
        public int InsertNewClass(string className, int colegeID, int hterID)
        {
            return classDal.InsertNewClass(className, colegeID, hterID);
        }
        /// <summary>
        /// 更新班级信息（管理员更新）
        /// </summary>
        /// <param name="className">班级名称</param>
        /// <param name="colegeID">学院ID</param>
        /// <param name="hterID">辅导员ID</param>
        /// <param name="classID">班级ID</param>
        /// <returns>返回受影响的行数</returns>
        public int UpdateClassByID(string className, int colegeID, int hterID, int classID)
        {
            return classDal.UpdateClassByID(className, colegeID, hterID, classID);
        }
        /// <summary>
        /// 根据班级名称获取班级ID
        /// </summary>
        /// <param name="className">班级名称</param>
        /// <returns>班级ID</returns>
        public int GetClassIDByClassName(string className)
        {
            return classDal.GetClassIDByClassName(className);
        }
        /// <summary>
        /// 根据ID来删除相应的数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            return classDal.Delete(id);
        }
        /// <summary>
        /// 根据学院ID获取班级列表
        /// </summary>
        /// <param name="collegeID">学院ID</param>
        /// <returns>班级的列表</returns>
        public IEnumerable<Model.classes> GetClassListByCollege(int collegeID)
        {
            return classDal.GetClassListByCollege(collegeID);
        }
        /// <summary>
        /// 获取该辅导员是否有班级
        /// </summary>
        /// <param name="headTeacherID">辅导员ID</param>
        /// <returns>是否有班级</returns>
        public bool ExistsHeadTea(int headTeacherID)
        {
            return classDal.ExistsHeadTea(headTeacherID);
        }
        /// <summary>
        /// 根据班级和辅导员ID查看该班级是否存在
        /// </summary>
        /// <param name="clsID">班级ID</param>
        /// <param name="terID">辅导员ID</param>
        /// <returns>是否存在</returns>
        public bool Exists(int clsID, int terID)
        {
            return classDal.Exists(clsID, terID);
        }
        /// <summary>
        /// 辅导员更新班级信息
        /// </summary>
        /// <param name="id">班级id</param>
        /// <param name="classLeader">班长ID</param>
        /// <param name="squad_leader">副班长ID</param>
        /// <param name="class_group_secretary">团支书ID</param>
        /// <param name="study_secretary">学习委员ID</param>
        /// <param name="life_secretary">生活委员ID</param>
        /// <returns>受影响的行数</returns>
        public int UpdateClassByTer(int id, int classLeader, int squad_leader, int class_group_secretary, int study_secretary, int life_secretary)
        {
            return classDal.UpdateClassByTer(id,  classLeader,  squad_leader,  class_group_secretary,  study_secretary,  life_secretary);
        }
    }
}
