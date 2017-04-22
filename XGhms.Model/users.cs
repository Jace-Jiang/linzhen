using System;

namespace XGhms.Model
{
    public partial class users
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
        private int _role_id;
        /// <summary>
        /// 角色ID
        /// </summary>
        public int role_id
        {
            get { return _role_id; }
            set { _role_id = value; }
        }
        private int _role_type;
        /// <summary>
        /// 角色类型
        /// </summary>
        public int role_type
        {
            get { return _role_type; }
            set { _role_type = value; }
        }
        private string _user_number;
        /// <summary>
        /// 学号/教工号
        /// </summary>
        public string user_number
        {
            get { return _user_number; }
            set { _user_number = value; }
        }
        private string _user_name;
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name
        {
            get { return _user_name; }
            set { _user_name = value; }
        }
        private string _password;
        /// <summary>
        /// 密码
        /// </summary>
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
        private int _is_lock;
        /// <summary>
        /// 是否锁定
        /// </summary>
        public int is_lock
        {
            get { return _is_lock; }
            set { _is_lock = value; }
        }
        private DateTime _add_time=DateTime.Now;
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time
        {
            get { return _add_time; }
            set { _add_time = value; }
        }
    }
}
