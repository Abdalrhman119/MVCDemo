using Demo.DataAccess.Data.Configuration;
using Demo.DataAccess.Models.DepartmentModels;

namespace Demo.DataAccess.Data.Configurations
{
    public class DepartmentConfiguration : BaseEntityConfiguration<Department>, IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(dept => dept.Id).UseIdentityColumn(10, 10);
            builder.Property(dept => dept.Code).HasColumnType("varchar(20)");
            builder.Property(dept => dept.Name).HasColumnType("varchar(20)");
            builder.HasMany(dept => dept.Employees)
                .WithOne(emp => emp.Department)
                .HasForeignKey(emp => emp.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
            base.Configure(builder);

        }
    }
}
