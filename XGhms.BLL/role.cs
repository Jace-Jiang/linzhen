using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGhms.BLL
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public partial class role
    {
        DAL.role roleDal = new DAL.role();
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns>model对象</returns>
        public Model.role GetModel(int id)
        {
            return roleDal.GetModel(id);
        }
        /// <summary>
        /// 根据角色名称返回角色ID
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色ID</returns>
        public int GetRoleIDByRoleName(string roleName)
        {
            return roleDal.GetRoleIDByRoleName(roleName);
        }
    }
}
