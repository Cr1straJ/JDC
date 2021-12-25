using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.Common.Interfaces
{
    public interface IChatRepository
    {
        IEnumerable<ChatGroup> GetAll();

        ChatGroup GetById(int? id);
    }
}
