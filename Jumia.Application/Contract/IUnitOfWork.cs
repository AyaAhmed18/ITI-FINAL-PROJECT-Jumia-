using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Contract
{
    public interface IUnitOfWork 
    {
        //ICategoryRepository? CategoryRepository { get; set; }
        //IOrderItemsRepository? OrderItemsRepository { get; set; }
        //IOrderRepository? OrderRepository { get; set; }
        //IPaymentRepository? PaymentRepository { get; set; }
        //IProductRepository? ProductRepository { get; set; }
        //IReviewRepository? ReviewRepository { get; set; }
        //IShippmentRepository? ShippmentRepository { get; set; }
        //ISubCategoryRepository? SubCategoryRepository { get; set; }
        IRepository<TEntity,Tid> Repository<TEntity,Tid>() where TEntity : class;
        Task SaveChangesAsync();
    }
}
