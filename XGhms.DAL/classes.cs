using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XGhms.Helper;
using System.Collections.Generic;

namespace XGhms.DAL
{
    /// <summary>
    /// 数据访问类:classes
    /// </summary>
    public partial class classes
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
            strSql.Append("select count(1) from xg_classes");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据班级名称检查是否存在该班级
        /// </summary>
        /// <param name="id">班级名称</param>
        /// <returns>true or false</returns>
        public bool Exists(string className)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_classes");
            strSql.Append(" where class_name=@class_name ");
            SqlParameter[] parameters = {
					new SqlParameter("@class_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = className;

            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据班级和辅导员ID查看该班级是否存在
        /// </summary>
        /// <param name="clsID">班级ID</param>
        /// <param name="terID">辅导员ID</param>
        /// <returns>是否存在</returns>
        public bool Exists(int clsID, int terID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_classes");
            strSql.Append(" where id=" + clsID);
            strSql.Append(" and head_teacher='" + terID + "'");
            return SQLHelper.Exists(strSql.ToString());
        }
        /// <summary>
        /// 根据ID来删除相应的数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from xg_classes ");
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
        /// 插入新的班级（系统管理员插入）
        /// </summary>
        /// <param name="className">班级名称</param>
        /// <param name="colegeID">学院ID</param>
        /// <param name="hterID">辅导员ID</param>
        /// <returns>返回受影响的行数</returns>
        public int InsertNewClass(string className,int colegeID,int hterID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO [xg_classes]([class_name],[college_id],[head_teacher])");
            str.Append(" VALUES(@className," + colegeID + ",'" + hterID + "')");
            SqlParameter[] parameters = {
					new SqlParameter("@className", SqlDbType.NVarChar,50)};
            parameters[0].Value = className;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 更新班级信息（管理员更新）
        /// </summary>
        /// <param name="className">班级名称</param>
        /// <param name="colegeID">学院ID</param>
        /// <param name="hterID">辅导员ID</param>
        /// <param name="classID">班级ID</param>
        /// <returns>返回受影响的行数</returns>
        public int UpdateClassByID(string className, int colegeID, int hterID,int classID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_classes] ");
            str.Append("SET [class_name] = @className");
            str.Append(",[college_id] = " + colegeID);
            str.Append(",[head_teacher] = '" + hterID + "'");
            str.Append(" WHERE id=" + classID);
            SqlParameter[] parameters = {
					new SqlParameter("@className", SqlDbType.NVarChar,50)};
            parameters[0].Value = className;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">班级id</param>
        /// <returns>model对象</returns>
        public Model.classes GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            Model.classes model = new Model.classes();
            DataSet ds = SQLHelper.SelectSqlReturnDataSet("classes_SelectClassById", parameters, CommandType.StoredProcedure);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["class_name"] != null && ds.Tables[0].Rows[0]["class_name"].ToString() != "")
                {
                    model.class_name = ds.Tables[0].Rows[0]["class_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["college_id"] != null && ds.Tables[0].Rows[0]["college_id"].ToString() != "")
                {
                    model.college_id = int.Parse(ds.Tables[0].Rows[0]["college_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["head_teacher"] != null && ds.Tables[0].Rows[0]["head_teacher"].ToString() != "")
                {
                    model.head_teacher = ds.Tables[0].Rows[0]["head_teacher"].ToString();
                }
                if (ds.Tables[0].Rows[0]["class_leader"] != null && ds.Tables[0].Rows[0]["class_leader"].ToString() != "")
                {
                    model.class_leader = ds.Tables[0].Rows[0]["class_leader"].ToString();
                }
                if (ds.Tables[0].Rows[0]["squad_leader"] != null && ds.Tables[0].Rows[0]["squad_leader"].ToString() != "")
                {
                    model.squad_leader = ds.Tables[0].Rows[0]["squad_leader"].ToString();
                }
                if (ds.Tables[0].Rows[0]["class_group_secretary"] != null && ds.Tables[0].Rows[0]["class_group_secretary"].ToString() != "")
                {
                    model.class_group_secretary = ds.Tables[0].Rows[0]["class_group_secretary"].ToString();
                }
                if (ds.Tables[0].Rows[0]["study_secretary"] != null && ds.Tables[0].Rows[0]["study_secretary"].ToString() != "")
                {
                    model.study_secretary = ds.Tables[0].Rows[0]["study_secretary"].ToString();
                }
                if (ds.Tables[0].Rows[0]["life_secretary"] != null && ds.Tables[0].Rows[0]["life_secretary"].ToString() != "")
                {
                    model.life_secretary = ds.Tables[0].Rows[0]["life_secretary"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据班级名称获取班级ID
        /// </summary>
        /// <param name="className">班级名称</param>
        /// <returns>班级ID</returns>
        public int GetClassIDByClassName(string className)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from xg_classes ");
            strSql.Append(" where class_name=@class_name");
            SqlParameter[] parameters = {
					new SqlParameter("@class_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = className;
            return Convert.ToInt32(SQLHelper.GetSingle(strSql.ToString(), parameters));
        }
        #endregion

        #region 扩展方法=============================================
        /// <summary>
        /// 根据学院ID获取班级列表
        /// </summary>
        /// <param name="collegeID">学院ID</param>
        /// <returns>班级的列表</returns>
        public IEnumerable<Model.classes> GetClassListByCollege(int collegeID)
        {
            string sql = "SELECT [id],[class_name],[college_id],[head_teacher],[class_leader],[squad_leader],[class_group_secretary],[study_secretary],[life_secretary] FROM [xg_classes] where college_id=" + collegeID;
            var modelList = new List<Model.classes>();
            using (DataSet ds = SQLHelper.Query(sql))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["id"])));
                }
                return modelList;
            }
        }
        /// <summary>
        /// 获取该辅导员是否有班级
        /// </summary>
        /// <param name="headTeacherID">辅导员ID</param>
        /// <returns>是否有班级</returns>
        public bool ExistsHeadTea(int headTeacherID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select count(1) from xg_classes");
            str.Append(" where head_teacher='" + headTeacherID+"'");
            return SQLHelper.Exists(str.ToString());
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
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_classes] ");
            str.Append("SET [class_leader] ='"+classLeader+"'");
            str.Append(",[squad_leader] = '" + squad_leader+"'");
            str.Append(",[class_group_secretary] = '" + class_group_secretary + "'");
            str.Append(",[study_secretary] = '" + study_secretary + "'");
            str.Append(",[life_secretary] = '" + life_secretary + "'");
            str.Append(" WHERE id=" + id);
            return SQLHelper.ExecuteSql(str.ToString());
        }
        #endregion
    }
}
