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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext context;

        public TeacherRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async void Add(Teacher entity)
        {
            await this.context.Teachers.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async void AddRange(IEnumerable<Teacher> entities)
        {
            await this.context.Teachers.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<Teacher> Find(Expression<Func<Teacher, bool>> expression)
        {
            return this.context.Teachers.Where(expression);
        }

        public IEnumerable<Teacher> GetAll()
        {
            return this.context.Teachers;
        }

        public async Task<Teacher> GetById(int? id)
        {
            return await this.context.Teachers.FindAsync(id);
        }

        public async void Remove(Teacher entity)
        {
            this.context.Teachers.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async void RemoveRange(IEnumerable<Teacher> entities)
        {
            this.context.Teachers.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async void Update(Teacher entity)
        {
            this.context.Teachers.Update(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
