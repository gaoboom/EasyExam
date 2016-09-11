using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyExam.Core
{
    /// <summary>
    /// 试卷类
    /// <remarks>
    /// Created at 2016.09.12
    /// </remarks>
    /// </summary>
    public class Paper
    {
        [Key]
        public int PaperID { get; set; }

        /// <summary>
        /// 试题列表
        /// </summary>
        [Display(Name = "试题列表")]
        public string QuestionList { get; set; }

        /// <summary>
        /// 答案列表
        /// </summary>
        [Display(Name = "答案列表")]
        public string AnswerList { get; set; }

        /// <summary>
        /// 试卷状态
        /// 0-未开始
        /// 1-正在进行
        /// 2-已完成
        /// </summary>
        public int PaperStatus { get; set; }

        /// <summary>
        /// 试卷所属人
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 试卷所属考试
        /// </summary>
        public virtual Test Test { get; set; }
    }
}