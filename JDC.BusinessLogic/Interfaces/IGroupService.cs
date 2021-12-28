using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IGroupService
    {
        Task<StudentGroup> GetById(int? id);

        IEnumerable<StudentGroup> GetAll();

        IEnumerable<StudentGroup> Find(Expression<Func<StudentGroup, bool>> expression);

        void Add(StudentGroup entity);

        void AddRange(IEnumerable<StudentGroup> entities);

        void Remove(StudentGroup entity);

        void RemoveRange(IEnumerable<StudentGroup> entities);

        void Update(StudentGroup entity);
    }
}
