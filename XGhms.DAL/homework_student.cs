using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XGhms.Helper;

namespace XGhms.DAL
{
    /// <summary>
    /// 数据访问类：homework_student
    /// </summary>
    public partial class homework_student
    {
        #region 基本方法=============================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_homework_student");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据条件语句来判断是否存在该记录
        /// </summary>
        /// <param name="where">条件语句</param>
        /// <returns>true or false</returns>
        public bool Exists(string where)
        {
            string sql = "select count(1) from xg_homework_student where " + where;
            return SQLHelper.Exists(sql);
        }
        /// <summary>
        /// 根据ID来删除相应的数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from xg_homework_student ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            int rows = SQLHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">学生作业id</param>
        /// <returns>model对象</returns>
        public Model.homework_student GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            Model.homework_student model = new Model.homework_student();
            DataSet ds = SQLHelper.SelectSqlReturnDataSet("homework_student_SelectStuHomById", parameters, CommandType.StoredProcedure);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["homework_id"] != null && ds.Tables[0].Rows[0]["homework_id"].ToString() != "")
                {
                    model.homework_id = int.Parse(ds.Tables[0].Rows[0]["homework_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["student_id"] != null && ds.Tables[0].Rows[0]["student_id"].ToString() != "")
                {
                    model.student_id = int.Parse(ds.Tables[0].Rows[0]["student_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["submit_time"] != null && ds.Tables[0].Rows[0]["submit_time"].ToString() != "")
                {
                    model.submit_time = DateTime.Parse(ds.Tables[0].Rows[0]["submit_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["submit_file"] != null && ds.Tables[0].Rows[0]["submit_file"].ToString() != "")
                {
                    model.submit_file = ds.Tables[0].Rows[0]["submit_file"].ToString();
                }
                if (ds.Tables[0].Rows[0]["submit_content"] != null && ds.Tables[0].Rows[0]["submit_content"].ToString() != "")
                {
                    model.submit_content = ds.Tables[0].Rows[0]["submit_content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["homework_score"] != null && ds.Tables[0].Rows[0]["homework_score"].ToString() != "")
                {
                    model.homework_score = int.Parse(ds.Tables[0].Rows[0]["homework_score"].ToString());
                }
                if (ds.Tables[0].Rows[0]["homework_comment"] != null && ds.Tables[0].Rows[0]["homework_comment"].ToString() != "")
                {
                    model.homework_comment = ds.Tables[0].Rows[0]["homework_comment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["homework_status"] != null && ds.Tables[0].Rows[0]["homework_status"].ToString() != "")
                {
                    model.homework_status = int.Parse(ds.Tables[0].Rows[0]["homework_status"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据当前登录的用户ID来获取前几个id
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="showNum">前几条数据</param>
        /// <returns>返回id的集合</returns>
        public DataTable GetIDForUserID(int userID, int showNum)
        {
            string sql = "select top " + showNum + " id from xg_homework_student where student_id=" + userID + " order by id desc";
            using (DataSet ds = SQLHelper.Query(sql))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 根据的用户ID和相关的分页数据来获取作业
        /// </summary>
        /// <param name="userID">用户学生id</param>
        /// <param name="PageBeginNum">分页开始以多少</param>
        /// <param name="PageEndNum">分页结束以多少</param>
        /// <returns>返回id的DataTable</returns>
        public DataTable GetIDForUserID(int userID, int PageBeginNum, int PageEndNum)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                    new SqlParameter("@PageEndNum", SqlDbType.Int,6),
                    new SqlParameter("@stuId", SqlDbType.Int,6)};
            parameters[0].Value = PageBeginNum;
            parameters[1].Value = PageEndNum;
            parameters[2].Value = userID;
            using (DataSet ds = SQLHelper.SelectSqlReturnDataSet("homework_student_SelectHWByStuIdandPage", parameters, CommandType.StoredProcedure))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 根据用户名来获取该学生的作业总数
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <returns>作业总数</returns>
        public int GetTotalNumHWByUserId(int userID)
        {
            string sql = "select count(id) from dbo.xg_homework_student where student_id=" + userID;
            object ob = SQLHelper.GetSingle(sql);
            if (ob == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(ob);
            }
        }
        /// <summary>
        /// 根据课程ID和学生ID获取作业的列表总数
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="StuID">学生ID</param>
        /// <returns>总数</returns>
        public int GetHWListNumByCourseIdAndStuID(int courseID, int StuID, int status)
        {
            StringBuilder str = new StringBuilder();
            if (status == 5)
            {
                str.Append("select count(xg_homework_student.id) from xg_homework_student,xg_course_homework ");
                str.Append("where xg_homework_student.homework_id=xg_course_homework.id ");
                str.Append("and student_id=" + StuID + " and course_id=" + courseID);
            }
            else
            {
                str.Append("select count(xg_homework_student.id) from xg_homework_student,xg_course_homework ");
                str.Append("where xg_homework_student.homework_id=xg_course_homework.id ");
                str.Append("and student_id=" + StuID + " and course_id=" + courseID + "and homework_status=" + status);
            }
            object ob = SQLHelper.GetSingle(str.ToString());
            if (ob == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(ob);
            }
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
        public DataTable GetHWListByCourseIdAndStuID(int courseID, int StuID,int status, int PageBeginNum, int PageEndNum)
        {
            if (status == 5)
            {
                SqlParameter[] parameters = {
					new SqlParameter("@stuID", SqlDbType.Int),
                    new SqlParameter("@courseID", SqlDbType.Int),
                    new SqlParameter("@PageBeginNum", SqlDbType.Int),
                    new SqlParameter("@PageEndNum", SqlDbType.Int)};
                parameters[0].Value = StuID;
                parameters[1].Value = courseID;
                parameters[2].Value = PageBeginNum;
                parameters[3].Value = PageEndNum;
                using (DataSet ds = SQLHelper.SelectSqlReturnDataSet("homework_student_SelectStuHWListBycidandstuid", parameters, CommandType.StoredProcedure))
                {
                    return ds.Tables[0];
                }
            }
            else
            {
                SqlParameter[] parameters = {
					new SqlParameter("@stuID", SqlDbType.Int),
                    new SqlParameter("@courseID", SqlDbType.Int),
                    new SqlParameter("@status", SqlDbType.Int),
                    new SqlParameter("@PageBeginNum", SqlDbType.Int),
                    new SqlParameter("@PageEndNum", SqlDbType.Int)};
                parameters[0].Value = StuID;
                parameters[1].Value = courseID;
                parameters[2].Value = status;
                parameters[3].Value = PageBeginNum;
                parameters[4].Value = PageEndNum;
                using (DataSet ds = SQLHelper.SelectSqlReturnDataSet("homework_student_SelectStuHWListBycidandstuidandstatus", parameters, CommandType.StoredProcedure))
                {
                    return ds.Tables[0];
                }
            }
        }
        /// <summary>
        /// 根据当前登录的用户ID和作业ID来获该学生的作业id
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="hwID">作业ID</param>
        /// <returns>该作业的id</returns>
        public int GetIDOfUserIDandhwID(int userID, int hwID)
        {
            string sql = "select id from xg_homework_student where student_id=" + userID + " and homework_id=" + hwID;
            object ob = SQLHelper.GetSingle(sql);
            if (ob == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(ob);
            }
        }
        /// <summary>
        /// 查询前十条该学生的作业显示
        /// </summary>
        /// <param name="userID">学生ID</param>
        /// <returns>该学生的作业ID集合</returns>
        public DataTable GetTenIDForUserID(int userID)
        {
            string sql = "select top 10 id from xg_homework_student where student_id=" + userID + " and homework_status=4 order by id desc";
            using (DataSet ds = SQLHelper.Query(sql))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 根据作业的ID获取该作业的平均数
        /// </summary>
        /// <param name="hwID">作业的ID</param>
        /// <returns>平均数</returns>
        public int GetAllScoreByhwID(int hwID)
        {
            string sql = "select AVG(homework_score) from xg_homework_student where homework_status=4 and homework_id=" + hwID;
            object ob = SQLHelper.GetSingle(sql);
            if (ob == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(ob);
            }
        }
        /// <summary>
        /// 根据用户ID和作业ID来获取分数
        /// </summary>
        /// <param name="stuID">学生ID</param>
        /// <param name="hwID">作业ID</param>
        /// <returns>返回分数，没有分数返回0</returns>
        public int GetScoreByStuIDhwID(int stuID, int hwID)
        {
            string sql = "select homework_score from xg_homework_student where homework_id=" + hwID + " and student_id=" + stuID;
            object ob = SQLHelper.GetSingle(sql);
            if (ob == null || ob == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(ob);
            }
        }
        #endregion
        #region 扩展方法=============================================
        /// <summary>
        /// 插入新的数据
        /// </summary>
        /// <param name="hwID">作业ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>受影响的行数</returns>
        public int Insertstuhw(int hwID, int stuID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO [xg_homework_student]");
            str.Append("([homework_id],[student_id],[submit_time],[submit_file],[submit_content],[homework_score],[homework_comment],[homework_status])");
            str.Append(" VALUES(" + hwID + "," + stuID + ",NULL,NULL,NULL,NULL,NULL,0)");
            return SQLHelper.ExecuteSql(str.ToString());
        }
        /// <summary>
        /// 根据课程ID和作业状态获取分页的总数
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="status">作业状态</param>
        /// <returns>总数</returns>
        public int GetHomeWorkTotalNumByThreeCS(int courseID, int status)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select count(xg_homework_student.id) from xg_homework_student,xg_course_homework where ");
            if (status == 5)
            {
                str.Append(" homework_id=xg_course_homework.id and course_id=" + courseID);
            }
            else
            {
                str.Append(" homework_id=xg_course_homework.id and course_id=" + courseID);
                str.Append(" and homework_status=" + status);
            }
            object obj = SQLHelper.GetSingle(str.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
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
            if (status == 5)
            {
                SqlParameter[] parameters = {
					new SqlParameter("@CourseID", SqlDbType.Int),
                    new SqlParameter("@PageBeginNum", SqlDbType.Int),
                    new SqlParameter("@PageEndNum", SqlDbType.Int)};
                parameters[0].Value = courseID;
                parameters[1].Value = PageBeginNum;
                parameters[2].Value = PageEndNum;
                using (DataSet ds = SQLHelper.SelectSqlReturnDataSet("homework_student_terListByAllStatus", parameters, CommandType.StoredProcedure))
                {
                    return ds.Tables[0];
                }
            }
            else
            {
                SqlParameter[] parameters = {
					new SqlParameter("@CourseID", SqlDbType.Int),
                    new SqlParameter("@status",SqlDbType.Int),
                    new SqlParameter("@PageBeginNum", SqlDbType.Int),
                    new SqlParameter("@PageEndNum", SqlDbType.Int)};
                parameters[0].Value = courseID;
                parameters[1].Value = status;
                parameters[2].Value = PageBeginNum;
                parameters[3].Value = PageEndNum;
                using (DataSet ds = SQLHelper.SelectSqlReturnDataSet("homework_student_terListByStatus", parameters, CommandType.StoredProcedure))
                {
                    return ds.Tables[0];
                }
            }
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
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_homework_student]");
            str.Append(" SET [homework_score] =" + homework_score);
            str.Append(",[homework_comment] =@homework_comment");
            str.Append(", [homework_status]=4");
            str.Append(" where id=" + id);
            SqlParameter[] parameters = { new SqlParameter("@homework_comment", SqlDbType.NText) };
            parameters[0].Value = homework_comment;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 当老师正在查看作业的时候设置为正在审批
        /// </summary>
        /// <param name="id">学生作业ID</param>
        /// <returns>受影响的行数</returns>
        public int terLookingStuWork(int id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_homework_student]");
            str.Append(" SET [homework_status] =2");
            str.Append(" where id=" + id);
            return SQLHelper.ExecuteSql(str.ToString());
        }
        /// <summary>
        /// 老师让学生重新做作业
        /// </summary>
        /// <param name="id">学生作业ID</param>
        /// <returns>受影响的行数</returns>
        public int ReformWork(int id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_homework_student]");
            str.Append(" SET [homework_status] =3");
            str.Append(" where id=" + id);
            return SQLHelper.ExecuteSql(str.ToString());
        }
        /// <summary>
        /// 根据老师的ID获取没有审批的作业数目
        /// </summary>
        /// <param name="terID">老师ID</param>
        /// <returns>没有批阅的数目</returns>
        public int GetNoCheckWorkNum(int terID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select COUNT(xg_homework_student.id) from xg_homework_student,xg_course_homework,xg_course ");
            str.Append(" where xg_homework_student.homework_id=xg_course_homework.id and xg_course_homework.course_id=xg_course.id ");
            str.Append(" and xg_homework_student.homework_status=1 and xg_course.teacher='" + terID + "'");
            object obj = SQLHelper.GetSingle(str.ToString());
            if (obj==null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 根据老师的ID获取已经完成的批阅的数目
        /// </summary>
        /// <param name="terID">老师的ID</param>
        /// <returns>已经批阅的作业数目</returns>
        public int GetCheckWorkNum(int terID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select COUNT(xg_homework_student.id) from xg_homework_student,xg_course_homework,xg_course ");
            str.Append(" where xg_homework_student.homework_id=xg_course_homework.id and xg_course_homework.course_id=xg_course.id ");
            str.Append(" and xg_homework_student.homework_status=4 and xg_course.teacher='" + terID + "'");
            object obj = SQLHelper.GetSingle(str.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 获取首页需要的三条数据
        /// </summary>
        /// <param name="terID"></param>
        /// <returns></returns>
        public DataTable GetThreeNoChecikForDefault(int terID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select top 3 (xg_homework_student.id) from xg_homework_student,xg_course_homework,xg_course ");
            str.Append(" where xg_homework_student.homework_id=xg_course_homework.id and xg_course_homework.course_id=xg_course.id ");
            str.Append(" and xg_course.teacher='" + terID + "'");
            str.Append(" order by xg_homework_student.id desc");
            using (DataSet ds = SQLHelper.Query(str.ToString()))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 根据学生ID和作业ID获取作业状态
        /// </summary>
        /// <param name="hwID">作业ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>作业状态，5代表没有查到该数据</returns>
        public int GethwStatusByStuandhwID(int hwID,int stuID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select homework_status from xg_homework_student");
            str.Append(" where homework_id=" + hwID);
            str.Append(" and student_id=" + stuID);
            object obj = SQLHelper.GetSingle(str.ToString());
            if (obj==null)
            {
                return 5;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 学生提交作业文件时触发事件
        /// </summary>
        /// <param name="hwID">作业ID</param>
        /// <param name="stuID">学生ID</param>
        /// <param name="filePath">文件地址</param>
        /// <returns>受影响的行数</returns>
        public int UploadFile(int hwID, int stuID,string filePath)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_homework_student]");
            str.Append(" SET [submit_time] = GETDATE(),");
            str.Append("[submit_file] =@filePath" );
            str.Append(",[homework_status] =1");
            str.Append("  WHERE homework_id=" + hwID + " and student_id=" + stuID);
            SqlParameter[] parameters = { new SqlParameter("@filePath",SqlDbType.NText) };
            parameters[0].Value = filePath;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 根据学生ID和作业ID获取该学生上传的文件路径
        /// </summary>
        /// <param name="hwID">作业ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>文件路径</returns>
        public string GetFilePathByStuandhwID(int hwID, int stuID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select submit_file from xg_homework_student");
            str.Append(" where homework_id=" + hwID);
            str.Append(" and student_id=" + stuID);
            object obj = SQLHelper.GetSingle(str.ToString());
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString().Trim();
            }
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
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_homework_student]");
            str.Append(" SET [submit_time] = GETDATE(),");
            str.Append("[submit_content] =@strCon");
            str.Append(",[homework_status] =1");
            str.Append("  WHERE homework_id=" + hwID + " and student_id=" + stuID);
            SqlParameter[] parameters = { new SqlParameter("@strCon", SqlDbType.NText) };
            parameters[0].Value = strCon;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 根据作业ID和学生ID获取hwstu表的id
        /// </summary>
        /// <param name="hwID">作业ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>hwstu表的id</returns>
        public int GetIDByhwIDandStuID(int hwID, int stuID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select id from xg_homework_student");
            str.Append(" where homework_id=" + hwID);
            str.Append(" and student_id=" + stuID);
            object obj = SQLHelper.GetSingle(str.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 根据学生ID获取没有完成的作业总数
        /// </summary>
        /// <param name="stuID">学生ID</param>
        /// <returns>没有完成的数目</returns>
        public int GetStuNoWorkNum(int stuID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select count(id) from xg_homework_student");
            str.Append(" where homework_status=0");
            str.Append(" and student_id=" + stuID);
            object obj = SQLHelper.GetSingle(str.ToString());
            if (obj==null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 根据学生ID获取完成的作业总数
        /// </summary>
        /// <param name="stuID">学生ID</param>
        /// <returns>完成的数目</returns>
        public int GetStuWorkNum(int stuID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select count(id) from xg_homework_student");
            str.Append(" where homework_status=4");
            str.Append(" and student_id=" + stuID);
            object obj = SQLHelper.GetSingle(str.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion
    }
}
