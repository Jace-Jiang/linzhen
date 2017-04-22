using System;

namespace XGhms.Model
{
    public partial class course_student
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
        private int _student_id;
        /// <summary>
        /// 学生ID
        /// </summary>
        public int student_id
        {
            get { return _student_id; }
            set { _student_id = value; }
        }
    }
}
