using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XGhms.BLL
{
    /// <summary>
    /// 学生作业的管理
    /// </summary>
    public partial class homework_student
    {
        DAL.homework_student hwstuDal = new DAL.homework_student(); //作业_学生
        DAL.course_homework courhwDal = new DAL.course_homework(); //课程_作业
        DAL.course_student courstuDal = new DAL.course_student(); //课程_学生
        DAL.course courseDal = new DAL.course(); //课程表
        /// <summary>
        /// 根据条件语句来查询是否存在
        /// </summary>
        /// <param name="where">条件语句</param>
        /// <returns>true or false</returns>
        public bool Exists(string where)
        {
            return hwstuDal.Exists(where);
        }
        /// <summary>
        /// 根据用户的ID和显示的条目数来获取该学生的作业的条目
        /// </summary>
        /// <param name="userID">学生作业ID</param>
        /// <param name="showNum">显示多少条数目</param>
        /// <returns>返回该学生的showNum条作业</returns>
        public DataTable GetStuHomeWorkForDefault(string userID,int showNum)
        { 
            //根据用户ID来确定作业，然后根据作业ID来确定课程和老师
            DataTable dt_hwstu= hwstuDal.GetIDForUserID(Convert.ToInt32(userID), showNum);
            //需要显示的为：该学生作业的ID，作业ID，课程名称，作业名称，作业开始时间，该课程老师，该学生作业状态
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("howstuId", typeof(Int32));
            dt.Columns.Add("course_name", typeof(String));
            dt.Columns.Add("homework_name", typeof(String));
            dt.Columns.Add("hw_begtime", typeof(DateTime));
            dt.Columns.Add("course_teacher", typeof(String));
            dt.Columns.Add("hw_status", typeof(String));
            for (int i = 0; i < dt_hwstu.Rows.Count; i++)
            {
                Model.homework_student hwstuModel = hwstuDal.GetModel(Convert.ToInt32(dt_hwstu.Rows[i][0]));//作业-学生表
                Model.course_homework courhwModel = courhwDal.GetModel(hwstuModel.homework_id); //课程-作业表
                Model.course courseModel = courseDal.GetModel(courhwModel.course_id); //课程表
                dt.Rows.Add(new Object[] { 
                    hwstuModel.id,
                    hwstuModel.homework_id, 
                    courseModel.course_name,
                    courhwModel.homework_name,
                    courhwModel.homework_beginTime,
                    courseModel.teacher,
                    hwstuModel.homework_status
                });
            }
            return dt;
        }
        /// <summary>
        /// 以分页的形式展现学生的作业
        /// </summary>
        /// <param name="userID">学生的用户ID</param>
        /// <param name="PageBeginNum">分页开始的数字</param>
        /// <param name="PageEndNum">分页结束的数字</param>
        /// <returns>返回分页信息</returns>
        public DataTable GetStuHomeWorkForPageList(int userID, int PageBeginNum, int PageEndNum)
        {
            //根据用户ID来确定作业，然后根据作业ID来确定课程和老师
            DataTable dt_hwstu = hwstuDal.GetIDForUserID(userID, PageBeginNum, PageEndNum);
            //需要显示的为：该学生作业的ID，作业ID，课程名称，作业名称，作业开始时间，该课程老师，该学生作业状态
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("howstuId", typeof(Int32));
            dt.Columns.Add("course_name", typeof(String));
            dt.Columns.Add("homework_name", typeof(String));
            dt.Columns.Add("hw_begtime", typeof(DateTime));
            dt.Columns.Add("course_teacher", typeof(String));
            dt.Columns.Add("hw_status", typeof(String));
            for (int i = 0; i < dt_hwstu.Rows.Count; i++)
            {
                Model.homework_student hwstuModel = hwstuDal.GetModel(Convert.ToInt32(dt_hwstu.Rows[i][0]));//作业-学生表
                Model.course_homework courhwModel = courhwDal.GetModel(hwstuModel.homework_id); //课程-作业表
                Model.course courseModel = courseDal.GetModel(courhwModel.course_id); //课程表
                dt.Rows.Add(new Object[] { 
                    hwstuModel.id,
                    hwstuModel.homework_id, 
                    courseModel.course_name,
                    courhwModel.homework_name,
                    courhwModel.homework_beginTime,
                    courseModel.teacher,
                    hwstuModel.homework_status
                });
            }
            return dt;
        }
        /// <summary>
        /// 根据用户学生的ID和作业的ID来查询作业的相关信息
        /// </summary>
        /// <param name="userID">学生用户的ID</param>
        /// <param name="hwID">作业的ID</param>
        /// <returns>该作业的信息</returns>
        public DataTable GetStuHomeWorkInfoFormyhw(int userID,int hwID)
        {
            Model.course_homework courhwModel = courhwDal.GetModel(hwID);  //获取该作业的信息
            Model.homework_student hwstuModel = hwstuDal.GetModel(hwstuDal.GetIDOfUserIDandhwID(userID, hwID)); //获取该作业的学生完成情况
            Model.course courseModel = courseDal.GetModel(courhwModel.course_id); //获取该作业的课程信息
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("hwId", typeof(Int32));
            dt.Columns.Add("homework_name", typeof(String));
            dt.Columns.Add("courseID",typeof(Int32));
            dt.Columns.Add("course_name", typeof(String));
            dt.Columns.Add("hw_begtime", typeof(DateTime));
            dt.Columns.Add("hw_endtime", typeof(DateTime));
            dt.Columns.Add("course_teacher", typeof(String));
            dt.Columns.Add("hw_status", typeof(String));
            dt.Rows.Add(new Object[] {
                hwstuModel.id,
                courhwModel.id,
                courhwModel.homework_name,
                courseModel.id,
                courseModel.course_name,
                courhwModel.homework_beginTime,
                courhwModel.homework_endTime,
                courseModel.teacher,
                hwstuModel.homework_status
            });
            return dt;
        }
        /// <summary>
        /// 根据用户名来获取该学生的作业总数
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <returns>作业总数</returns>
        public int GetTotalNumHWByUserId(int userID)
        {
            return hwstuDal.GetTotalNumHWByUserId(userID);
        }
        /// <summary>
        /// 根据课程ID和学生ID获取作业的列表总数
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="StuID">学生ID</param>
        /// <returns>总数</returns>
        public int GetHWListNumByCourseIdAndStuID(int courseID, int StuID, int status)
        {
            return hwstuDal.GetHWListNumByCourseIdAndStuID(courseID, StuID, status);
        }
        /// <summary>
        /// 根据不同的状态和课程ID还有学生ID获取作业的列表
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="StuID">学生ID</param>
        /// <param name="status">状态</param>
        /// <param name="PageBeginNum">开始数</param>
        /// <param name="PageEndNum">结束数</param>
        /// <returns>作业的ID列表</returns>
        public DataTable GetHWListByCourseIdAndStuID(int courseID, int StuID, int status, int PageBeginNum, int PageEndNum)
        {
            return hwstuDal.GetHWListByCourseIdAndStuID(courseID, StuID, status, PageBeginNum, PageEndNum);
        }
        /// <summary>
        /// 获取前十个作业的，来展示在页面
        /// </summary>
        /// <param name="userID">学生ID</param>
        /// <returns>返回DataTable数据</returns>
        public DataTable GetTenScoreOfStuID(int userID)
        {
            DataTable hwstu = hwstuDal.GetTenIDForUserID(userID);
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(Int32)); //该学生作业id
            dt.Columns.Add("howstuId", typeof(Int32));  //该作业的编号
            dt.Columns.Add("hw_score", typeof(Int32)); //该学生的作业分数
            for (int i = 0; i < hwstu.Rows.Count; i++)
            {
                Model.homework_student hwstuModel = hwstuDal.GetModel(Convert.ToInt32(hwstu.Rows[i][0]));//作业-学生表
                dt.Rows.Add(new Object[] { 
                    hwstuModel.id,
                    hwstuModel.homework_id, 
                    hwstuModel.homework_score
                });
            }
            return dt;
        }
        /// <summary>
        /// 根据用户ID和作业ID来获取分数
        /// </summary>
        /// <param name="stuID">学生ID</param>
        /// <param name="hwID">作业ID</param>
        /// <returns>返回分数，没有分数返回0</returns>
        public int GetScoreByStuIDhwID(int stuID, int hwID)
        {
            return hwstuDal.GetScoreByStuIDhwID(stuID, hwID);
        }
        /// <summary>
        /// 插入新的数据
        /// </summary>
        /// <param name="hwID">作业ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>受影响的行数</returns>
        public int Insertstuhw(int hwID, int stuID)
        {
            return hwstuDal.Insertstuhw(hwID, stuID);
        }
        /// <summary>
        /// 根据课程ID和作业状态获取分页的总数
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="status">作业状态</param>
        /// <returns>总数</returns>
        public int GetHomeWorkTotalNumByThreeCS(int courseID, int status)
        {
            return hwstuDal.GetHomeWorkTotalNumByThreeCS(courseID, status);
        }
        /// <summary>
        /// 分页专用，老师分页专用，基于课程ID和作业状态
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="status">作业状态</param>
        /// <param name="PageBeginNum">开始数目</param>
        /// <param name="PageEndNum">结束数目</param>
        /// <returns>id的集合</returns>
        public DataTable GetHomeWorkListByByThreeCSOfPage(int courseID, int status, int PageBeginNum, int PageEndNum)
        {
            return hwstuDal.GetHomeWorkListByByThreeCSOfPage(courseID,  status,  PageBeginNum,  PageEndNum);
        }
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">学生作业id</param>
        /// <returns>model对象</returns>
        public Model.homework_student GetModel(int id)
        {
            return hwstuDal.GetModel(id);
        }
        /// <summary>
        /// 根据ID来删除相应的数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            return hwstuDal.Delete(id);
        }
        /// <summary>
        /// 老师批阅作业的方法
        /// </summary>
        /// <param name="id">学生作业ID</param>
        /// <param name="homework_comment">老师批阅的内容</param>
        /// <param name="homework_score">老师修改的分数</param>
        /// <returns>受影响的行数</returns>
        public int terCheckStuHomeWork(int id, string homework_comment, int homework_score)
        {
            return hwstuDal.terCheckStuHomeWork(id, homework_comment, homework_score);
        }
        /// <summary>
        /// 当老师正在查看作业的时候设置为正在审批
        /// </summary>
        /// <param name="id">学生作业ID</param>
        /// <returns>受影响的行数</returns>
        public int terLookingStuWork(int id)
        {
            return hwstuDal.terLookingStuWork(id);
        }
        /// <summary>
        /// 老师让学生重新做作业
        /// </summary>
        /// <param name="id">学生作业ID</param>
        /// <returns>受影响的行数</returns>
        public int ReformWork(int id)
        {
            return hwstuDal.ReformWork(id);
        }
        /// <summary>
        /// 根据老师的ID获取没有审批的作业数目
        /// </summary>
        /// <param name="terID">老师ID</param>
        /// <returns>没有批阅的数目</returns>
        public int GetNoCheckWorkNum(int terID)
        {
            return hwstuDal.GetNoCheckWorkNum(terID);
        }
        /// <summary>
        /// 根据老师的ID获取已经完成的批阅的数目
        /// </summary>
        /// <param name="terID">老师的ID</param>
        /// <returns>已经批阅的作业数目</returns>
        public int GetCheckWorkNum(int terID)
        {
            return hwstuDal.GetCheckWorkNum(terID);
        }
        /// <summary>
        /// 获取首页需要的三条数据
        /// </summary>
        /// <param name="terID"></param>
        /// <returns></returns>
        public DataTable GetThreeNoChecikForDefault(int terID)
        {
            return hwstuDal.GetThreeNoChecikForDefault(terID);
        }
        /// <summary>
        /// 根据学生ID和作业ID获取作业状态
        /// </summary>
        /// <param name="hwID">作业ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>作业状态，5代表没有查到该数据</returns>
        public int GethwStatusByStuandhwID(int hwID, int stuID)
        {
            return hwstuDal.GethwStatusByStuandhwID(hwID, stuID);
        }
        /// <summary>
        /// 学生提交作业文件时触发事件
        /// </summary>
        /// <param name="hwID">作业ID</param>
        /// <param name="stuID">学生ID</param>
        /// <param name="filePath">文件地址</param>
        /// <returns>受影响的行数</returns>
        public int UploadFile(int hwID, int stuID, string filePath)
        {
            return hwstuDal.UploadFile(hwID, stuID, filePath);
        }
        /// <summary>
        /// 根据学生ID和作业ID获取该学生上传的文件路径
        /// </summary>
        /// <param name="hwID">作业ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>文件路径</returns>
        public string GetFilePathByStuandhwID(int hwID, int stuID)
        {
            return hwstuDal.GetFilePathByStuandhwID(hwID, stuID);
        }
        /// <summary>
        /// 学生提交作业
        /// </summary>
        /// <param name="hwID">作业ID</param>
        /// <param name="stuID">学生ID</param>
        /// <param name="strCon">提交的内容</param>
        /// <returns>受影响的行数</returns>
        public int StuSubmitHomeWork(int hwID, int stuID, string strCon)
        {
            return hwstuDal.StuSubmitHomeWork(hwID, stuID, strCon);
        }
        /// <summary>
        /// 根据作业ID和学生ID获取hwstu表的id
        /// </summary>
        /// <param name="hwID">作业ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>hwstu表的id</returns>
        public int GetIDByhwIDandStuID(int hwID, int stuID)
        {
            return hwstuDal.GetIDByhwIDandStuID(hwID, stuID);
        }
        /// <summary>
        /// 根据学生ID获取没有完成的作业总数
        /// </summary>
        /// <param name="stuID">学生ID</param>
        /// <returns>没有完成的数目</returns>
        public int GetStuNoWorkNum(int stuID)
        {
            return hwstuDal.GetStuNoWorkNum(stuID);
        }
        /// <summary>
        /// 根据学生ID获取完成的作业总数
        /// </summary>
        /// <param name="stuID">学生ID</param>
        /// <returns>完成的数目</returns>
        public int GetStuWorkNum(int stuID)
        {
            return hwstuDal.GetStuWorkNum(stuID);
        }
    }
}
