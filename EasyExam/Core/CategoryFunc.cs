﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyExam.Core
{
    /// <summary>
    /// 栏目操作功能类
    /// <remarks>
    /// Created at 2016.09.26
    /// </remarks>
    /// </summary>
    public class CategoryFunc
    {
        private EasyExamContext dbContext = new EasyExamContext();

        /// <summary>
        /// 根据栏目名称获取已有的栏目实例
        /// </summary>
        /// <param name="cateName">栏目名称</param>
        /// <returns>栏目实例</returns>
        public Category Find(string cateName)
        {
            var _cate = dbContext.Categories.First(c => c.Name == cateName);
            return _cate;
        }

        /// <summary>
        /// 根据栏目ID获取已有栏目实例
        /// </summary>
        /// <param name="cateID">栏目ID</param>
        /// <returns>栏目实例</returns>
        public Category Find(int cateID)
        {
            var _cate = dbContext.Categories.Find(cateID);
            return _cate;
        }
    }
}