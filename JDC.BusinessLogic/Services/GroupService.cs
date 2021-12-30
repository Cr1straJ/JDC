using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.DataAccess.Interfaces;

namespace JDC.BusinessLogic.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task Add(Group entity)
        {
            await this.groupRepository.Add(entity);
        }

        public async Task AddRange(IEnumerable<Group> entities)
        {
            await this.groupRepository.AddRange(entities);
        }

        public IEnumerable<Group> Find(Expression<Func<Group, bool>> expression)
        {
            return this.groupRepository.Find(expression);
        }

        public IEnumerable<Group> GetAll()
        {
            return this.groupRepository.GetAll();
        }

        public async Task Remove(Group entity)
        {
            await this.groupRepository.Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<Group> entities)
        {
            await this.groupRepository.RemoveRange(entities);
        }

        public async Task<Group> GetById(int? id)
        {
            return id.HasValue ? await this.groupRepository.GetById(id.Value) : null;
        }

        public async Task<List<Group>> GetInstitutionGroups(int? id)
        {
            return id.HasValue ? await this.groupRepository.GetInstitutionGroups(id.Value) : null;
        }

        public async Task Update(Group entity)
        {
            await this.groupRepository.Update(entity);
        }
    }
}
