using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyExam.ViewModel
{
    /// <summary>
    /// 管理员登录模型
    /// <remarks>
    /// Created at 2016.09.18
    /// </remarks>
    /// </summary>

    public class AdminLoginViewModel
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "登录名")]
        public string Accounts { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [DataType(DataType.Password)]
        [StringLength(256, ErrorMessage = "{0}长度少于{1}个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }

    }
}