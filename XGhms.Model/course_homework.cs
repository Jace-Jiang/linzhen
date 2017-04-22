using System;

namespace XGhms.Model
{
    public partial class course_homework
    {
        private int _id;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _course_id;
        /// <summary>
        /// 课程ID
        /// </summary>
        public int course_id
        {
            get { return _course_id; }
            set { _course_id = value; }
        }
        private string _homework_name;
        /// <summary>
        /// 作业名称
        /// </summary>
        public string homework_name
        {
            get { return _homework_name; }
            set { _homework_name = value; }
        }
        private string _homework_info;
        /// <summary>
        /// 作业信息
        /// </summary>
        public string homework_info
        {
            get { return _homework_info; }
            set { _homework_info = value; }
        }
        private DateTime _homework_beginTime;
        /// <summary>
        /// 作业开始时间
        /// </summary>
        public DateTime homework_beginTime
        {
            get { return _homework_beginTime; }
            set { _homework_beginTime = value; }
        }
        private DateTime _homework_endTime;
        /// <summary>
        /// 作业结束时间
        /// </summary>
        public DateTime homework_endTime
        {
            get { return _homework_endTime; }
            set { _homework_endTime = value; }
        }
    }
}
