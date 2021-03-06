﻿
using System.Data.Entity.ModelConfiguration;
using DataBase.DataModel;

namespace DataBase.DataConfiguration
{
    /// <summary>
    /// Структура таблицы рубрики
    /// </summary>
    class HeadingConfiguration : EntityTypeConfiguration<Heading>
    {
        public HeadingConfiguration()
        {
            HasKey(x => x.ID);
            Property(x => x.Name).HasMaxLength(100).IsRequired();
            Property(x => x.PathLink).HasMaxLength(100).IsRequired();
        }
    }
}
