using System;

namespace XGhms.Model
{
    public partial class role
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
        private string _role_name;
        /// <summary>
        /// 角色名称
        /// </summary>
        public string role_name
        {
            get { return _role_name; }
            set { _role_name = value; }
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
        private int _is_sys;
        /// <summary>
        /// 是否系统默认0否1是 
        /// </summary
        public int is_sys
        {
            get { return _is_sys; }
            set { _is_sys = value; }
        }
    }
}
