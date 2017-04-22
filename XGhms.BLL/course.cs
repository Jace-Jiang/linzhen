using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XGhms.BLL
{
    /// <summary>
    /// 课程管理
    /// </summary>
    public partial class course
    {
        DAL.course_student courstuDal = new DAL.course_student();
        DAL.course coursDal = new DAL.course();
        /// <summary>
        /// 根据课程ID和老师ID检查是否存在该课程
        /// </summary>
        /// <param name="id">课程ID</param>
        /// <param name="teacherID">老师ID</param>
        /// <returns>是否存在</returns>
        public bool Exists(int id, int teacherID)
        {
            return coursDal.Exists(id, teacherID);
        }
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">课程id</param>
        /// <returns>model对象</returns>
        public Model.course GetModel(int id)
        {
            return coursDal.GetModel(id);
        }
        public List<Model.course> GetCourListOfstuID(int stuID,int pageBeginNum,int pageEndNum)
        {
            DataTable courListDT = courstuDal.GetListOfStuID(stuID, pageBeginNum, pageEndNum);
            List<Model.course> listModel=new List<Model.course>();
            for (int i = 0; i < courListDT.Rows.Count; i++)
            {
                Model.course courModel=coursDal.GetModel(Convert.ToInt32(courListDT.Rows[i][0]));
                listModel.Add(courModel);
            }
            return listModel;
        }
        /// <summary>
        /// 根据ID来删除相应的数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            return coursDal.Delete(id);
        }
        public bool Exists(int id)
        {
            return coursDal.Exists(id);
        }
        /// <summary>
        /// 插入新的课程（管理员插入）
        /// </summary>
        /// <param name="course_number">课程编号</param>
        /// <param name="course_name">课程名</param>
        /// <param name="tremID">学期ID</param>
        /// <param name="terID">老师ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>受影响的行数</returns>
        public int Insert(string course_number, string course_name, int tremID, int terID, int collegeID)
        {
            return coursDal.Insert(course_number, course_name, tremID, terID, collegeID);
        }
        /// <summary>
        /// 根据课程的ID更新相应的课程
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="course_number">课程编号</param>
        /// <param name="course_name">课程名</param>
        /// <param name="tremID">学期ID</param>
        /// <param name="terID">老师ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>受影响的行数</returns>
        public int UpdateOldCourse(int courseID, string course_number, string course_name, int tremID, int terID, int collegeID)
        {
            return coursDal.UpdateOldCourse( courseID,  course_number,  course_name,  tremID,  terID,  collegeID);
        }
        /// <summary>
        /// 根据学期ID和学院ID获取课程数目
        /// </summary>
        /// <param name="tremID">课程ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>课程数目</returns>
        public int GetNumTotalByTremIdandCollegeID(int tremID, int collegeID)
        {
            return coursDal.GetNumTotalByTremIdandCollegeID(tremID, collegeID);
        }
        /// <summary>
        /// 根据学期和学院来分页显示课程
        /// </summary>
        /// <param name="tremID">学期ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <param name="PageBeginNum">开始的数目</param>
        /// <param name="PageEndNum">结束的数目</param>
        /// <returns>DataTable</returns>
        public DataTable GetSelectByTremandCollegeandPage(int tremID, int collegeID, int PageBeginNum, int PageEndNum)
        {
            return coursDal.GetSelectByTremandCollegeandPage( tremID,  collegeID,  PageBeginNum,  PageEndNum);
        }
        /// <summary>
        /// 根据学期和老师来查询课程
        /// </summary>
        /// <param name="terID">老师ID</param>
        /// <param name="tremID">学期ID</param>
        /// <returns>课程列表</returns>
        public DataTable GetCourseListByTerTrem(int terID, int tremID)
        {
            return coursDal.GetCourseListByTerTrem(terID, tremID);
        }
        /// <summary>
        /// 老师更新课程信息
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="oTerID">助教ID</param>
        /// <param name="stuID">学生ID</param>
        /// <param name="courInfo">课程信息</param>
        /// <returns>受影响的行数</returns>
        public int Update(int courseID, string oTerID, string stuID, string courInfo)
        {
            return coursDal.Update(courseID, oTerID, stuID, courInfo);
        }
        /// <summary>
        /// 根据学期和学生ID来获取课程列表
        /// </summary>
        /// <param name="stuID">学生ID</param>
        /// <param name="tremID">学期ID</param>
        /// <returns>课程列表</returns>
        public DataTable GetCourseListByStuTrem(int stuID, int tremID)
        {
            return coursDal.GetCourseListByStuTrem(stuID, tremID);
        }
    }
}
