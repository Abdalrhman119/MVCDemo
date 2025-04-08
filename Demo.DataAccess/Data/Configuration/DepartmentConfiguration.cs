namespace Demo.DataAccess.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(dept => dept.Id).UseIdentityColumn(10, 10);
            builder.Property(dept => dept.Code).HasColumnType("varchar(20)");
            builder.Property(dept => dept.Name).HasColumnType("varchar(20)");

            builder.Property(dept => dept.CreatedOn).HasDefaultValueSql("GETDATE()");
            //If Row Is Inserted Without Value , The Default Value Will Be Used
            //On Insert
            //Can Reference Other Columns
            //Can Be Overridden
            builder.Property(dept => dept.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            //Value Calculated Every Time The Record Changed
            //On Update
            //Can Reference Other Columns
        }
    }
}
