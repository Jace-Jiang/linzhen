using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XGhms.BLL
{
    /// <summary>
    /// 课程-作业的管理
    /// </summary>
    public partial class course_homework
    {
        DAL.course_homework courhwDal = new DAL.course_homework();
        DAL.homework_student hwstuDal = new DAL.homework_student();
        /// <summary>
        /// 根据作业的ID来获取作业的说明信息
        /// </summary>
        /// <param name="hwID">作业的ID</param>
        /// <returns>返回作业的html说明</returns>
        public string GetHWinfosByID(int hwID)
        {
            return courhwDal.GetHWinfosByID(hwID);
        }
        /// <summary>
        /// 根据作业的ID来获取该作业的平均数
        /// </summary>
        /// <param name="hwID">作业的id</param>
        /// <returns>该作业平均数</returns>
        public int GetAVGByhwID(int hwID)
        {
            return hwstuDal.GetAllScoreByhwID(hwID);
        }
        /// <summary>
        /// 根据获取model
        /// </summary>
        /// <param name="id">作业id</param>
        /// <returns>model</returns>
        public Model.course_homework GetModel(int id)
        {
            return courhwDal.GetModel(id);
        }
        /// <summary>
        /// 根据课程的ID获取前20条作业
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns>返回作业id，作业名称，作业开始时间</returns>
        public DataTable GetTwentyHWByCourID(int courseID)
        {
            return courhwDal.GetTwentyHWByCourID(courseID);
        }
        /// <summary>
        /// 根据课程ID获取作业的总数，用于分页
        /// </summary>
        /// <param name="CourseID">课程ID</param>
        /// <returns>总数</returns>
        public int GetPageNumOfCourseID(int CourseID)
        {
            return courhwDal.GetPageNumOfCourseID(CourseID);
        }
        /// <summary>
        /// 分页，用于查询课程作业id
        /// </summary>
        /// <param name="CourseID">课程ID</param>
        /// <param name="PageBeginNum">开始数目</param>
        /// <param name="PageEndNum">结束数目</param>
        /// <returns>id的table</returns>
        public DataTable GetPageOfCourseID(int CourseID, int PageBeginNum, int PageEndNum)
        {
            return courhwDal.GetPageOfCourseID(CourseID,  PageBeginNum,  PageEndNum);
        }
        /// <summary>
        /// 根据ID来删除相应的数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            return courhwDal.Delete(id);
        }
        /// <summary>
        /// 插入新的作业
        /// </summary>
        /// <param name="cid">课程ID</param>
        /// <param name="hwName">作业名称</param>
        /// <param name="hwInfo">作业信息</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>该作业的ID</returns>
        public int InsertNewHWGethwID(int cid, string hwName, string hwInfo, string beginTime, string endTime)
        {
            return courhwDal.InsertNewHWGethwID(cid,  hwName,  hwInfo,  beginTime,  endTime);
        }
        /// <summary>
        /// 更新修改作业
        /// </summary>
        /// <param name="id">作业ID</param>
        /// <param name="hwName">作业名称</param>
        /// <param name="hwInfo">作业信息</param>
        /// <param name="beginTime">作业开始时间</param>
        /// <param name="endTime">作业结束时间</param>
        /// <returns>受影响的行数</returns>
        public int UpdateHW(int id, string hwName, string hwInfo, string beginTime, string endTime)
        {
            return courhwDal.UpdateHW(id, hwName, hwInfo, beginTime, endTime);
        }
    }
}
