using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyExam.Core
{
    /// <summary>
    /// 试题类
    /// <remarks>
    /// Created at 2016.09.05
    /// </remarks>
    /// </summary>
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }

        /// <summary>
        /// 考题类型
        /// 单选/多选/判断
        /// </summary>
        [Required]
        [Display(Name ="考题类型")]
        public string QuestionType { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        [StringLength(200,MinimumLength =10,ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name ="题目")]
        public string QuestionTitle { get; set; }

        /// <summary>
        /// 选项A
        /// </summary>
        [Display(Name = "选项A")]
        public string OptionA { get; set; }

        /// <summary>
        /// 选项B
        /// </summary>
        [Display(Name = "选项B")]
        public string OptionB { get; set; }

        /// <summary>
        /// 选项C
        /// </summary>
        [Display(Name = "选项C")]
        public string OptionC { get; set; }

        /// <summary>
        /// 选项D
        /// </summary>
        [Display(Name = "选项D")]
        public string OptionD { get; set; }

        /// <summary>
        /// 正确答案，英文逗号分隔，如（A,B,C）
        /// </summary>
        [Display(Name = "正确答案")]
        public string RightAnswer { get; set; }

        /// <summary>
        /// 解析
        /// </summary>
        [Display(Name = "解析")]
        public string Note { get; set; }

        /// <summary>
        /// 所属栏目
        /// </summary>
        public virtual Category Category { get; set; }
    }
}