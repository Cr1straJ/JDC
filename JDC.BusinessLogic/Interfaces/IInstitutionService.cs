using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IInstitutionService
    {
        Task<Institution> GetById(int id);

        Task<IEnumerable<Institution>> GetAll();

        Task Add(Institution entity);

        Task Remove(Institution entity);

        Task Update(Institution entity);
    }
}
