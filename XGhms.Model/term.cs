using System;

namespace XGhms.Model
{
    public partial class term
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
        private string _term_name;
        /// <summary>
        /// 学期名称
        /// </summary>
        public string term_name
        {
            get { return _term_name; }
            set { _term_name = value; }
        }
    }
}
