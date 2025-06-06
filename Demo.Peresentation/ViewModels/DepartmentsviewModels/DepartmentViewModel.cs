﻿using System.ComponentModel.DataAnnotations;

namespace Demo.Peresentation.ViewModels.DepartmentsviewModels
{
    public class DepartmentViewModel
    {

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code Is Required")]
        public int Code { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
