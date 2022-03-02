﻿using System;
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
    public class ChatService : IChatService
    {
        private readonly IChatRepository chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public async Task Add(Chat entity)
        {
            await this.chatRepository.Add(entity);
        }

        public async Task AddRange(IEnumerable<Chat> entities)
        {
            await this.chatRepository.AddRange(entities);
        }

        public IEnumerable<Chat> Find(Expression<Func<Chat, bool>> expression)
        {
            return this.chatRepository.Find(expression);
        }

        public IEnumerable<Chat> GetAll()
        {
            return this.chatRepository.GetAll();
        }

        public async Task<Chat> GetById(int? id)
        {
            return await this.chatRepository.GetById(id);
        }

        public async Task Remove(Chat entity)
        {
            await this.chatRepository.Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<Chat> entities)
        {
            await this.chatRepository.RemoveRange(entities);
        }

        public async Task Update(Chat entity)
        {
            await this.chatRepository.Update(entity);
        }
    }
}
