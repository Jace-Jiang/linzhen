using System;

namespace XGhms.Model
{
    public partial class users_info
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
        private int _user_id;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        private int _role_id;
        /// <summary>
        /// 角色ID
        /// </summary>
        public int role_id
        {
            get { return _role_id; }
            set { _role_id = value; }
        }
        private string _real_name;
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string real_name
        {
            get { return _real_name; }
            set { _real_name = value; }
        }
        private int _sex;
        /// <summary>
        /// 性别
        /// </summary>
        public int sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        private DateTime _birthday;
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        private string _telephone;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }
        private string _email;
        /// <summary>
        /// E-mail
        /// </summary>
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        private int _college_id;
        /// <summary>
        /// 学院（部门）ID
        /// </summary>
        public int college_id
        {
            get { return _college_id; }
            set { _college_id = value; }
        }
        private int _class_id;
        /// <summary>
        /// 班级ID
        /// </summary>
        public int class_id
        {
            get { return _class_id; }
            set { _class_id = value; }
        }
        private string _major;
        /// <summary>
        /// 专业
        /// </summary>
        public string major
        {
            get { return _major; }
            set { _major = value; }
        }
        private string _address;
        /// <summary>
        /// 联系地址
        /// </summary>
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }
        private string _explain;
        /// <summary>
        /// 简介
        /// </summary>
        public string explain
        {
            get { return _explain; }
            set { _explain = value; }
        }
    }
}
