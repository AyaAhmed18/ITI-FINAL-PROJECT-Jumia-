using Jumia.Application.Contract;
using Jumia.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly JumiaContext _jumiaContext;
        //private readonly ILogger _logger;
        //private Hashtable _repositories;
        public ICategoryRepository CategoryRepository { get; private set; }
        public IOrderItemsRepository OrderItemsRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IPaymentRepository PaymentRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IReviewRepository ReviewRepository { get; private set; }
        public IShippmentRepository ShippmentRepository { get; private set; }
        public ISubCategoryRepository SubCategoryRepository { get; private set; }

        public UnitOfWork(JumiaContext jumiaContext
            //,ILoggerFactory loggerFactory
            )
        {
            _jumiaContext = jumiaContext;
            //_logger = loggerFactory.CreateLogger("logs");
            CategoryRepository = new CategoryRepository(_jumiaContext);
            OrderItemsRepository = new OrderItemRepository(_jumiaContext);
            OrderRepository = new OrderRepository(_jumiaContext);
            PaymentRepository = new PaymentRepository(_jumiaContext);
            ProductRepository = new ProductRepository(_jumiaContext);
            ReviewRepository = new ReviewRepository(_jumiaContext);
            ShippmentRepository = new ShippmentRepository(_jumiaContext);
            SubCategoryRepository = new SubCategoryRepository(_jumiaContext);
        }

        public async Task SaveChangesAsync()
        {
            await _jumiaContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _jumiaContext?.Dispose();
        }
        //public IRepository<TEntity, Tid> Repository<TEntity, Tid>() where TEntity : class
        //{
        //    if (_repositories == null) _repositories = new Hashtable();

        //    var type = typeof(TEntity).Name;

        //    if (!_repositories.ContainsKey(type))
        //    {
        //        var repositoryType = typeof(Repository<TEntity,Tid>);
        //        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _jumiaContext);

        //        _repositories.Add(type, repositoryInstance);
        //    }

        //    return (IRepository<TEntity,Tid>)_repositories[type];

        //}
    }
}
