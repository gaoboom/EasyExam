using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyExam.Core
{
    /// <summary>
    /// 测验模型
    /// <remarks>
    /// Created at 2016.9.11
    /// </remarks>
    /// </summary>
    public class Test
    {
        [Key]
        public int TestID { get; set; }

        /// <summary>
        /// 测试名称
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "测试名称")]
        public string Name { get; set; }

        /// <summary>
        /// 测试说明
        /// </summary>
        [Display(Name = "测试说明")]
        public string Description { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 试题类型
        /// 1-统一试题
        /// 2-随机抽题，统一试题
        /// 3-个人随机抽题
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [Display(Name = "试题类型")]
        public int Mode { get; set; }

        /// <summary>
        /// 试题列表
        /// 试题统一时有内容，否则为空
        /// </summary>
        [Display(Name = "试题列表")]
        public string QuestionList { get; set; }

        /// <summary>
        /// 测试状态
        /// 1-正常测验中
        /// 2-测验完，已下线
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [Display(Name = "测试状态")]
        public int Status { get; set; }
    }
}