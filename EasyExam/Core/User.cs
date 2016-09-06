using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyExam.Core
{
    /// <summary>
    /// 用户模型
    /// <remarks>Created at 2016.09.02</remarks>
    /// </summary>
    public class User
    {
        [Key]
        public int UserID { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "登录名")]
        public string Username { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [DataType(DataType.Password)]
        [StringLength(256, ErrorMessage = "{0}长度少于{1}个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 名称【可做昵称、真实姓名等】
        /// </summary>
        [StringLength(20, ErrorMessage = "{0}必须少于{1}个字符")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 分公司
        /// </summary>
        [StringLength(20, ErrorMessage = "{0}必须少于{1}个字符")]
        [Display(Name = "分公司")]
        public string Company { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [StringLength(20, ErrorMessage = "{0}必须少于{1}个字符")]
        [Display(Name = "所属部门")]
        public string Department { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "最后登录时间")]
        public Nullable<DateTime> LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        [Display(Name = "最后登录IP")]
        public string LastLoginIP { get; set; }
    }
}