using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IInstitutionService
    {
        Task<Institution> GetById(int id);

        Task Add(Institution entity);

        Task Remove(Institution entity);

        Task Update(Institution entity);
    }
}
