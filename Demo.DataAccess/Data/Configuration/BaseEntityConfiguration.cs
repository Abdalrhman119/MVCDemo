using Demo.DataAccess.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Data.Configuration
{
    public  class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
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
