using System;

namespace XGhms.Model
{
    public partial class homework_student
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
        private int _homework_id;
        /// <summary>
        /// 作业ID
        /// </summary>
        public int homework_id
        {
            get { return _homework_id; }
            set { _homework_id = value; }
        }
        private int _student_id;
        /// <summary>
        /// 学生ID
        /// </summary>
        public int student_id
        {
            get { return _student_id; }
            set { _student_id = value; }
        }
        private DateTime _submit_time;
        /// <summary>
        /// 作业提交时间（最近）
        /// </summary>
        public DateTime submit_time
        {
            get { return _submit_time; }
            set { _submit_time = value; }
        }
        private string _submit_file;
        /// <summary>
        /// 提交的作业文件
        /// </summary>
        public string submit_file
        {
            get { return _submit_file; }
            set { _submit_file = value; }
        }
        private string _submit_content;
        /// <summary>
        /// 作业提交的内容
        /// </summary>
        public string submit_content
        {
            get { return _submit_content; }
            set { _submit_content = value; }
        }
        private int _homework_score;
        /// <summary>
        /// 作业分数
        /// </summary>
        public int homework_score
        {
            get { return _homework_score; }
            set { _homework_score = value; }
        }
        private string _homework_comment;
        /// <summary>
        /// 老师评价
        /// </summary>
        public string homework_comment
        {
            get { return _homework_comment; }
            set { _homework_comment = value; }
        }
        private int _homework_status=0;
        /// <summary>
        /// 作业的状态，（0：未完成（默认）1：送交批阅中，2：正在批阅，3：重做，4：完成）
        /// </summary>
        public int homework_status
        {
            get { return _homework_status; }
            set { _homework_status = value; }
        }
    }
}
