using Demo.DataAccess.Contexts;
using Demo.DataAccess.Repositories.Interface;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IEmployeeRepository> _EmployeeRepository;
        private readonly Lazy<IDepartmentRepository> _DepartmentRepository;
        private readonly AppDbContext _AppDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _EmployeeRepository = new Lazy<IEmployeeRepository>(()=>new EmployeeRepository(_AppDbContext));
            _DepartmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_AppDbContext));
            _AppDbContext = appDbContext;
        }

        public IEmployeeRepository EmployeeRepository => _EmployeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _DepartmentRepository.Value;

 

        public int SaveChanges()
        {
            return _AppDbContext.SaveChanges();
        }
    }
}
