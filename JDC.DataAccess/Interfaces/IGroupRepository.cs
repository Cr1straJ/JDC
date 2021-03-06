using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    /// <summary>
    /// Group repository.
    /// </summary>
    public interface IGroupRepository
    {
        /// <summary>
        /// Gets group by Id from database.
        /// </summary>
        /// <param name="groupId">Group id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Group> GetById(int groupId);

        /// <summary>
        /// Creates group.
        /// </summary>
        /// <param name="group">Group request information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Add(Group group);

        /// <summary>
        /// Deletes a group from database.
        /// </summary>
        /// <param name="group">Group request information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Remove(Group group);

        /// <summary>
        /// Edits group information.
        /// </summary>
        /// <param name="group">Group request information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Update(Group group);

        /// <summary>
        /// Gets all groups in the institution from database.
        /// </summary>
        /// <param name="institutionId">Institution id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<List<Group>> GetInstitutionGroups(int institutionId);
    }
}
