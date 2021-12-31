using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.DataAccess.Interfaces;

namespace JDC.BusinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task Add(Message entity)
        {
            await this.messageRepository.Add(entity);
        }

        public async Task AddRange(IEnumerable<Message> entities)
        {
            await this.messageRepository.AddRange(entities);
        }

        public IEnumerable<Message> Find(Expression<Func<Message, bool>> expression)
        {
            return this.messageRepository.Find(expression);
        }

        public IEnumerable<Message> GetAll()
        {
            return this.messageRepository.GetAll();
        }

        public async Task<Message> GetById(int? id)
        {
            return await this.messageRepository.GetById(id);
        }

        public async Task Remove(Message entity)
        {
            await this.messageRepository.Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<Message> entities)
        {
            await this.messageRepository.RemoveRange(entities);
        }

        public async Task Update(Message entity)
        {
            await this.messageRepository.Update(entity);
        }
    }
}
