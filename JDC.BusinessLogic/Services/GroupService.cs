using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.DataAccess.Interfaces;

namespace JDC.BusinessLogic.Services
{
    /// <inheritdoc />
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupService"/> class.
        /// </summary>
        /// <param name="groupRepository">Group repository.</param>
        public GroupService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        /// <inheritdoc />
        public async Task Add(Group group)
        {
            await groupRepository.Add(group);
        }

        /// <inheritdoc />
        public async Task Remove(Group group)
        {
            await groupRepository.Remove(group);
        }

        /// <inheritdoc />
        public async Task<Group> GetById(int groupId)
        {
            return await groupRepository.GetById(groupId);
        }

        /// <inheritdoc />
        public async Task<List<Group>> GetInstitutionGroups(int institutionId)
        {
            return await groupRepository.GetInstitutionGroups(institutionId);
        }

        /// <inheritdoc />
        public async Task Update(Group group)
        {
            await groupRepository.Update(group);
        }
    }
}
