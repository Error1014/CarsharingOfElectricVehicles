using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TransactionsSystem.Repository.Entities;
using TransactionsSystem.Repository.Interfaces;
using TransactionsSystem.Service.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TransactionsSystem.Service.Service
{
    public class TypeTransactionService : ITypeTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TypeTransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddTypeTransaction(string typeTransaction)
        {
            var type = new TypeTransaction(typeTransaction);
            await _unitOfWork.TypeTransactions.AddEntities(type);
            await _unitOfWork.TypeTransactions.SaveChanges();
            return type.Id;
        }

        public async Task<string> GetTypeTransaction(int transactionId)
        {
            var type = await _unitOfWork.TypeTransactions.GetEntity(transactionId);
            if (type==null)
            {
                throw new NotFoundException("Тип транзацки не найден");
            }
            return type.Name;
        }
        public async Task<Dictionary<int, string>> GetTypeTransactions()
        {
            var list = await _unitOfWork.TypeTransactions.GetAll();
            Dictionary<int,string> result = new Dictionary<int,string>();
            foreach (var transaction in list)
            {
                result.Add(transaction.Id, transaction.Name);
            }
            return result;
        }
        public async Task RemoveTypeTransiction(int transactionId)
        {
            var type = await _unitOfWork.TypeTransactions.GetEntity(transactionId);
            if (type == null)
            {
                throw new NotFoundException("Тип транзацки не найден");
            }
            _unitOfWork.TypeTransactions.RemoveEntities(type);
            await _unitOfWork.TypeTransactions.SaveChanges();
        }

        public async Task UpdateTypeTransaction(int id, string value)
        {
            var type = await _unitOfWork.TypeTransactions.GetEntity(id);
            if (type == null)
            {
                throw new NotFoundException("Тип транзацки не найден");
            }
            _unitOfWork.TypeTransactions.UpdateEntities(type);
            await _unitOfWork.TypeTransactions.SaveChanges();
        }
    }
}
