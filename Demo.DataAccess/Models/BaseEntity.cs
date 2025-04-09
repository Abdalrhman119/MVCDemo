namespace Demo.DataAccess.Models
{
    public class BaseEntity
    {
        public int Id { get; set; } //PK
        public int CreatedBy { get; set; } //User Id
        public DateTime CreatedOn { get; set; }

        public int LastModifiedBy { get; set; } //User Id
        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; } //Flag for Soft Delete



    }
}
