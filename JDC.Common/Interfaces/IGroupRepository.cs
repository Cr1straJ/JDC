using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.Common.Interfaces
{
    public interface IGroupRepository
    {
        Task<StudentGroup> GetById(int? id);

        IEnumerable<StudentGroup> GetAll();

        IEnumerable<StudentGroup> Find(Expression<Func<StudentGroup, bool>> expression);

        Task Add(StudentGroup entity);

        Task AddRange(IEnumerable<StudentGroup> entities);

        Task Remove(StudentGroup entity);

        Task RemoveRange(IEnumerable<StudentGroup> entities);

        Task Update(StudentGroup entity);
    }
}
