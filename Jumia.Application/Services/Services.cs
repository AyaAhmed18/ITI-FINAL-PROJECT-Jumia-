using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services
{
    public class Services<TEntity,Tid,T> : IServices<TEntity,Tid,T> where TEntity : class where T : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public Services(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //public ResultView<TEntity> CreateAsync(TEntity entity)
        //{
        //    var Query = _unitOfWork.Repository<T, Tid>.GetAllAsync(); // se;ect * from product

        //    var OldProduct = Query.Where(p => p.Name == product.Name).FirstOrDefault();
        //    if (OldProduct != null)
        //    {
        //        return new ResultView<TEntity> { Entity = null, IsSuccess = false, Message = "Already Exist" };
        //    }
        //    else
        //    {
        //        var Prd = _mapper.Map<T>(entity);
        //        var NewPrd = await _productRepository.CreateAsync(Prd);
        //        await _productRepository.SaveChangesAsync();
        //        var PrdDto = _mapper.Map<CreateOrUpdateProductDTO>(NewPrd);
        //        return new ResultView<CreateOrUpdateProductDTO> { Entity = PrdDto, IsSuccess = true, Message = "Created Successfully" };
        //    }
        //}

        public Task<ResultDataForPagination<TEntity>> GetAllPagination(int items, int pagenumber)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetOne(Tid ID)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<TEntity>> HardDelete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        //public Task<ResultView<TEntity>> HardDelete(TEntity entity)
        //{
        //    _unitOfWork.Repository<TEntity,Tid>().DeleteAsync(entity);
        //}

        public Task<ResultView<TEntity>> SoftDelete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<TEntity>> UpdateAsync(TEntity book)
        {
            throw new NotImplementedException();
        }

        Task<ResultView<TEntity>> IServices<TEntity, Tid, T>.CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
