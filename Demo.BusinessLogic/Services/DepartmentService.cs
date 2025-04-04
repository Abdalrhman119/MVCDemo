using Demo.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services
{
    internal class DepartmentService
    {
        private readonly IDepartmentRepository _DepartmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _DepartmentRepository = departmentRepository;
        }


    }
}
