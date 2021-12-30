using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.Common.Interfaces;

namespace JDC.BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public async Task Add(ChatGroup entity)
        {
            await this.chatRepository.Add(entity);
        }

        public async Task AddRange(IEnumerable<ChatGroup> entities)
        {
            await this.chatRepository.AddRange(entities);
        }

        public IEnumerable<ChatGroup> Find(Expression<Func<ChatGroup, bool>> expression)
        {
            return this.chatRepository.Find(expression);
        }

        public IEnumerable<ChatGroup> GetAll()
        {
            return this.chatRepository.GetAll();
        }

        public async Task<ChatGroup> GetById(int? id)
        {
            return await this.chatRepository.GetById(id);
        }

        public async Task Remove(ChatGroup entity)
        {
            await this.chatRepository.Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<ChatGroup> entities)
        {
            await this.chatRepository.RemoveRange(entities);
        }

        public async Task Update(ChatGroup entity)
        {
            await this.chatRepository.Update(entity);
        }
    }
}
