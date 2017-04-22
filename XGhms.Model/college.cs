using System;

namespace XGhms.Model
{
    public partial class college
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
        private string _college_name;
        /// <summary>
        /// 学院（部门）名称
        /// </summary>
        public string college_name
        {
            get { return _college_name; }
            set { _college_name = value; }
        }
        private string _college_admin;
        /// <summary>
        /// 学院（部门）管理员
        /// </summary>
        public string college_admin
        {
            get { return _college_admin; }
            set { _college_admin = value; }
        }
    }
}
