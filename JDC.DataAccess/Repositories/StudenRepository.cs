﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.Common.Interfaces;
using JDC.DataAccess.Data;

namespace JDC.DataAccess.Repositories
{
    public class StudenRepository : ISudentRepository
    {
        private readonly ApplicationDbContext context;

        public StudenRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async void Add(Student entity)
        {
            await this.context.Students.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async void AddRange(IEnumerable<Student> entities)
        {
            await this.context.Students.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<Student> Find(Expression<Func<Student, bool>> expression)
        {
            return this.context.Students.Where(expression);
        }

        public IEnumerable<Student> GetAll()
        {
            return this.context.Students;
        }

        public async Task<Student> GetById(int? id)
        {
            return await this.context.Students.FindAsync(id);
        }

        public async void Remove(Student entity)
        {
            this.context.Students.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async void RemoveRange(IEnumerable<Student> entities)
        {
            this.context.Students.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async void Update(Student entity)
        {
            this.context.Students.Update(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
