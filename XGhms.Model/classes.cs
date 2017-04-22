using System;

namespace XGhms.Model
{
    public partial class classes
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
        private string _class_name;
        /// <summary>
        /// 班级名称
        /// </summary>
        public string class_name
        {
            get { return _class_name; }
            set { _class_name = value; }
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
        private string _head_teacher;
        /// <summary>
        /// 辅导员（班主任）
        /// </summary>
        public string head_teacher
        {
            get { return _head_teacher; }
            set { _head_teacher = value; }
        }
        private string _class_leader;
        /// <summary>
        /// 班长
        /// </summary>
        public string class_leader
        {
            get { return _class_leader; }
            set { _class_leader = value; }
        }
        private string _squad_leader;
        /// <summary>
        /// 副班长
        /// </summary>
        public string squad_leader
        {
            get { return _squad_leader; }
            set { _squad_leader = value; }
        }
        private string _class_group_secretary;
        /// <summary>
        /// 团支书
        /// </summary>
        public string class_group_secretary
        {
            get { return _class_group_secretary; }
            set { _class_group_secretary = value; }
        }
        private string _study_secretary;
        /// <summary>
        /// 学习委员
        /// </summary>
        public string study_secretary
        {
            get { return _study_secretary; }
            set { _study_secretary = value; }
        }
        private string _life_secretary;
        /// <summary>
        /// 生活委员
        /// </summary>
        public string life_secretary
        {
            get { return _life_secretary; }
            set { _life_secretary = value; }
        }
    }
}
