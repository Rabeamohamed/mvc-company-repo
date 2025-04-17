﻿using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositrories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MvcAppDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public UnitOfWork(MvcAppDbContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> CompeleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose(); 
        }
    }
}
