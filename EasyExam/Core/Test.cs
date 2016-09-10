using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyExam.Core
{
    /// <summary>
    /// 测验模型
    /// </summary>
    public class Test
    {
        [Key]
        public int TestID { get; set; }

        public string Name { get; set; }
    }
}