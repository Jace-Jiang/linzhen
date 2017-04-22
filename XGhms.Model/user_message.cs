using System;

namespace XGhms.Model
{
    public partial class user_message
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
        private int _type = 2;
        /// <summary>
        /// 短信息类型：  0系统消息  1学生消息  2老师消息  3管理员消息
        /// </summary>
        public int type
        {
            get { return _type; }
            set { _type = value; }
        }
        private int _sender;
        /// <summary>
        /// 发送者
        /// </summary>
        public int sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
        private int _receiver;
        /// <summary>
        /// 接收者
        /// </summary>
        public int receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }
        private string _content;
        /// <summary>
        /// 站内信内容
        /// </summary>
        public string content
        {
            get { return _content; }
            set { _content = value; }
        }
        private int _is_read=0;
        /// <summary>
        /// 是否查看0未阅1已阅
        /// </summary>
        public int is_read
        {
            get { return _is_read; }
            set { _is_read = value; }
        }
        private DateTime _send_time=DateTime.Now;
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime send_time
        {
            get { return _send_time; }
            set { _send_time = value; }
        }
        private DateTime? _read_time;
        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime? read_time
        {
            get { return _read_time; }
            set { _read_time = value; }
        }
        private int _last_id;
        /// <summary>
        /// 根据上个ID回复的信息
        /// </summary>
        public int last_id
        {
            get { return _last_id; }
            set { _last_id = value; }
        }
    }
}
