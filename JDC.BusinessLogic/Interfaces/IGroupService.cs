using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IGroupService
    {
        Task<List<Group>> GetInstitutionGroups(int? id);
    }
}
