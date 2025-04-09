using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DTOs
{
    public class UpdateDepartmentDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Code { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfCreation { get; set; }




    }
}
