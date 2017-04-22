using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XGhms.BLL
{
    /// <summary>
    /// 每个课程对于的学生管理
    /// </summary>
    public partial class course_student
    {
        DAL.course_student courstuDal = new DAL.course_student();
        /// <summary>
        /// 根据课程ID和学生的ID来检查是否存在
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>是否存在</returns>
        public bool Exists(int courseID, int stuID)
        {
            return courstuDal.Exists(courseID, stuID);
        }
        /// <summary>
        /// 执行插入
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>受影响的行数</returns>
        public int Insert(int courseID, int stuID)
        {
            return courstuDal.Insert(courseID, stuID);
        }
        /// <summary>
        /// 根据课程ID和学生ID来删除
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="StuID">学生ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int courseID, int StuID)
        {
            return courstuDal.Delete(courseID, StuID);
        }
        /// <summary>
        /// 查询某课程的学生列表，用于导出Excel表
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns>学生列表</returns>
        public DataSet SelectCourseStudentForExcel(int courseID)
        {
            return courstuDal.SelectCourseStudentForExcel(courseID);
        }
        /// <summary>
        /// 根据课程id获取学生列表
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns>学生ID和学生姓名</returns>
        public DataTable SelectCourseStudentList(int courseID)
        {
            return courstuDal.SelectCourseStudentList(courseID);
        }
    }
}
