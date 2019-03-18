using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSDataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLSDataBase.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(p => p.PhoneNumber)
                .HasColumnType("char(10)");
        }
    }
}
