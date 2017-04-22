using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGhms.BLL
{
    /// <summary>
    /// 学期管理
    /// </summary>
    public partial class term
    {
        DAL.term termDal = new DAL.term();
        public Model.term GetModel(int id)
        {
            return termDal.GetModel(id);
        }
        /// <summary>
        /// 获取所有的学期（管理员专用）
        /// </summary>
        /// <returns>modellist</returns>
        public IEnumerable<Model.term> GetAllTrem()
        {
            return termDal.GetAllTrem();
        }
        /// <summary>
        /// 根据学期名获取学期ID
        /// </summary>
        /// <param name="tremName">学期名</param>
        /// <returns>学期ID</returns>
        public int GetTremIDByTremName(string tremName)
        {
            return termDal.GetTremIDByTremName(tremName);
        }
    }
}
