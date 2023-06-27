using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsSystem.Repository.Entities;
using TransactionsSystem.Repository.Interfaces;
using TransactionsSystem.Service.Interfaces;

namespace TransactionsSystem.Service.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly IUserSessionGetter _userSessionGetter;
        public TransactionService(IUnitOfWork unitOfWork, IMapper map, IUserSessionGetter userSessionGetter)
        {
            _unitOfWork = unitOfWork;
            _map = map;
            _userSessionGetter = userSessionGetter;
        }

        public async Task<Guid> AddTransaction(TransactionAddDTO transactionItemDTO)
        {
            var transaction = _map.Map<TransactionItem>(transactionItemDTO);
            transaction.DateTime = DateTime.Now;
            await _unitOfWork.Transactions.AddEntities(transaction);
            await _unitOfWork.Transactions.SaveChanges();
            return transaction.Id;
        }

        public async Task<TransactionItemDTO> GetTransaction(Guid id)
        {
            var transaction = await _unitOfWork.Transactions.GetEntity(id);
            if (transaction==null)
            {
                throw new NotFoundException("Транзакция не найдена");
            }
            var result = _map.Map<TransactionItemDTO>(transaction);
            return result;
        }

        public async Task<Dictionary<Guid, TransactionItemDTO>> GetTransactions(TransactionFilter filter)
        {
            if (_userSessionGetter.Role=="Client")
            {
                filter.ClientId = _userSessionGetter.UserId;
            }
            var list = await _unitOfWork.Transactions.GetTransactions(filter);
            Dictionary<Guid, TransactionItemDTO> result = new Dictionary<Guid, TransactionItemDTO>();
            foreach (var item in list)
            {
                result.Add(item.Id, _map.Map<TransactionItemDTO>(item));
            }
            return result;
        }

        public async Task UpdateTransactionItem(Guid id, TransactionAddDTO transactionItemDTO)
        {
            var transaction = await _unitOfWork.Transactions.GetEntity(id);
            if (transaction == null)
            {
                throw new NotFoundException("Транзакция не найдена");
            }
            transaction = _map.Map<TransactionItem>(transactionItemDTO);
            _unitOfWork.Transactions.UpdateEntities(transaction);
            await _unitOfWork.Transactions.SaveChanges();
        }
        public async Task RemoveTransaction(Guid id)
        {
            var transaction = await _unitOfWork.Transactions.GetEntity(id);
            if (transaction == null)
            {
                throw new NotFoundException("Транзакция не найдена");
            }
            _unitOfWork.Transactions.RemoveEntities(transaction);
            await _unitOfWork.Transactions.SaveChanges();
        }

        public async Task<decimal> GetBalance()
        {
            var transaction = await _unitOfWork.Transactions.GetTransactionByClient(_userSessionGetter.UserId);
            var balance = transaction.Sum(x => x.Summ);
            return balance;

        }
        public async Task<decimal> GetBalance(Guid id)
        {
            var transaction = await _unitOfWork.Transactions.GetTransactionByClient(id);
            var balance = transaction.Sum(x => x.Summ);
            return balance;

        }
    }
}
