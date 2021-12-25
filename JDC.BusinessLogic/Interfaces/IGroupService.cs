using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IGroupService
    {
        Task<List<Group>> GetInstitutionGroups(int? id);

        Task<Group> GetById(int? id);

        Task Update(Group group);

        Task Add(Group group);
    }
}
