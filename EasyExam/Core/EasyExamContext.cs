using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EasyExam.Core
{
    /// <summary>
    /// 数据上下文类
    /// <remarks>
    /// Created at 2016.09.06
    /// </remarks>
    /// </summary>
    public class EasyExamContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<User> Users { get; set; }

        public EasyExamContext():base("EasyExamContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EasyExamContext>());
        }

    }
}