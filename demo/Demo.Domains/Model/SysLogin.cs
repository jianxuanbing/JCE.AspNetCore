using JCE.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domains.Model
{
    public class SysLogin: AggregateRoot<SysLogin>
    {
        #region Property(属性)

        /// <summary>
        /// ID
        /// </summary>
        
        public Guid LoginID { get; set; }

        /// <summary>
        /// 对应的商家。
        /// </summary>

        public Guid MerchantID { get; set; }

        /// <summary>
        /// 帐号，建议前带商家前缀
        /// </summary>

        public string Login { get; set; }

        /// <summary>
        /// 名字
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// 密码。加密的密码。
        /// </summary>

        public string Password { get; set; }

        /// <summary>
        /// 手势密码。加密的密码。
        /// </summary>

        public string HandPassword { get; set; }

        /// <summary>
        /// 状态，1在用，其它停用
        /// </summary>

        public int Status { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>

        public int IsOnline { get; set; }

        /// <summary>
        /// 联系电话号码
        /// </summary>

        public string Tele { get; set; }

        /// <summary>
        /// 在线时间
        /// </summary>

        public DateTime? OnlineTime { get; set; }

        /// <summary>
        /// 是否默认帐号。新建商家时产生的帐号为默认帐号1是，0否。
        /// </summary>

        public int IsDefault { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>

        public string Creater { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 编辑人
        /// </summary>

        public string Editor { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>

        public DateTime EditTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>

        public string Note { get; set; }

        #endregion

        public SysLogin() : this(Guid.Empty) { }

        public SysLogin(Guid id) : base(id)
        {
        }
    }
}
