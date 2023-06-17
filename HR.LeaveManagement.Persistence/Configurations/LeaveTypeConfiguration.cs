using HR.LeaveManagement.Domain.Entities;

namespace HR.LeaveManagement.Persistence.Configurations
{
    public class LeaveTypeConfiguration : BaseConfiguration<Domain.Entities.LeaveType>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.LeaveType> builder)
        {
            base.Configure(builder);

            builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    Name = "Vacation",
                    DefaultDays = 10,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }
            );

            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
