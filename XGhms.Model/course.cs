using System;

namespace XGhms.Model
{
    public partial class course
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
        private string _course_number;
        /// <summary>
        /// 课程号码（专业课程编码）
        /// </summary>
        public string course_number
        {
            get { return _course_number; }
            set { _course_number = value; }
        }
        private string _course_name;
        /// <summary>
        /// 课程名称
        /// </summary>
        public string course_name
        {
            get { return _course_name; }
            set { _course_name = value; }
        }
        private int _term_id;
        /// <summary>
        /// 学期ID
        /// </summary>
        public int term_id
        {
            get { return _term_id; }
            set { _term_id = value; }
        }
        private string _teacher;
        /// <summary>
        /// 课程老师（主教）
        /// </summary>
        public string teacher
        {
            get { return _teacher; }
            set { _teacher = value; }
        }
        private string _other_teacher;
        /// <summary>
        /// 助教
        /// </summary>
        public string other_teacher
        {
            get { return _other_teacher; }
            set { _other_teacher = value; }
        }
        private string _student_leader;
        /// <summary>
        /// 该课程学生负责人
        /// </summary>
        public string student_leader
        {
            get { return _student_leader; }
            set { _student_leader = value; }
        }
        private string _course_info;
        /// <summary>
        /// 该课程的信息
        /// </summary>
        public string course_info
        {
            get { return _course_info; }
            set { _course_info = value; }
        }
        private int _college_id;
        /// <summary>
        /// 学院ID
        /// </summary>
        public int college_id
        {
            get { return _college_id; }
            set { _college_id = value; }
        }
    }
}
