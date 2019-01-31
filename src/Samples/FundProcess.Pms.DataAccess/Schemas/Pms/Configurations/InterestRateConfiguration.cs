using FundProcess.Pms.DataAccess.Schemas.Pms.Tables;
using FundProcess.Pms.DataAccess.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundProcess.Pms.DataAccess.Schemas.Pms.Configurations
{
    public class InterestRateConfiguration : IEntityTypeConfiguration<InterestRate>
    {
        public void Configure(EntityTypeBuilder<InterestRate> builder)
        {
            builder.HasBaseType<Derivative>();
        }
    }
}
