using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetInstitutionGroups(int id);
    }
}
