using Demo.DataAccess.Models.DepartmentModels;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Models.SharedModels;

namespace Demo.DataAccess.Data.Configuration
{
    class EmployeeConfiguration : BaseEntityConfiguration<Employee>, IEntityTypeConfiguration<Employee>
    {
        public new void  Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)");
            builder.Property(e => e.Address).HasColumnType("varchar(150)");
            builder.Property(e => e.Salary).HasColumnType("decimal(10,2)");

            builder.Property(e => e.Email).HasColumnType("varchar(30)");
            builder.Property(e => e.PhoneNumber).HasColumnType("varchar(11)");

            builder.Property(e => e.Gender).HasConversion(
    convertToProviderExpression: valueToAddInDb => valueToAddInDb.ToString(),
    convertFromProviderExpression: valueToReadInDb => (Gender)Enum.Parse(typeof(Gender), valueToReadInDb)).HasColumnType("varchar(6)");

            builder.Property(e => e.EmployeeType).HasConversion(
                 valueToAddInDb => valueToAddInDb.ToString(),
                 valueToReadInDb => (EmployeeType)Enum.Parse(typeof(EmployeeType), valueToReadInDb)).HasColumnType("varchar(8)");
            base.Configure(builder);
        }

    }
}
