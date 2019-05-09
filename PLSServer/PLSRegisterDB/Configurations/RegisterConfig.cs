using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSRegisterDB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLSMobileRegisterDB.Configurations
{
    public class RegisterConfig : IEntityTypeConfiguration<MobileUserRegisterQueue>
    {
        public void Configure(EntityTypeBuilder<MobileUserRegisterQueue> builder)
        {
            
        }
    }
}

