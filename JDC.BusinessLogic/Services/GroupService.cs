using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.Common.Interfaces;

namespace JDC.BusinessLogic.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;
        
        public GroupService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task Add(StudentGroup entity)
        {
            await this.groupRepository.Add(entity);
        }

        public async Task AddRange(IEnumerable<StudentGroup> entities)
        {
            await this.groupRepository.AddRange(entities);
        }

        public IEnumerable<StudentGroup> Find(Expression<Func<StudentGroup, bool>> expression)
        {
            return this.groupRepository.Find(expression);
        }

        public IEnumerable<StudentGroup> GetAll()
        {
            return this.groupRepository.GetAll();
        }

        public async Task<StudentGroup> GetById(int? id)
        {
            return await this.groupRepository.GetById(id);
        }

        public async Task Remove(StudentGroup entity)
        {
            await this.groupRepository.Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<StudentGroup> entities)
        {
            await this.groupRepository.RemoveRange(entities);
        }

        public async Task Update(StudentGroup entity)
        {
            await this.groupRepository.Update(entity);
        }
    }
}
