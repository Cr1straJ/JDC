using System.Collections.Generic;
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

        public async Task Add(Group group)
        {
            await this.groupRepository.Add(group);
        }

        public async Task<Group> GetById(int? id)
        {
            return id.HasValue ? await this.groupRepository.GetById(id.Value) : null;
        }

        public async Task<List<Group>> GetInstitutionGroups(int? id)
        {
            return id.HasValue ? await this.groupRepository.GetInstitutionGroups(id.Value) : null;
        }

        public async Task Update(Group group)
        {
            await this.groupRepository.Update(group);
        }
    }
}
