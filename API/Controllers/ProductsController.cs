using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using API.DTOs;
using System;
using Core.Specification;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo,
        IMapper mapper)
        {
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _mapper = mapper;
            _productRepo = productRepo;

        }

        [HttpGet]
        public async Task<ActionResult <IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecPrams specPrams)
        {
            var spec = new PrductWithTypeAndBrand(specPrams);
            var product = await _productRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(product);
             return Ok(data);


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new PrductWithTypeAndBrand(id);

            var product = await _productRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Product, ProductToReturnDto>(product);







        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _brandRepo.ListAllAsync());



        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _typeRepo.ListAllAsync());



        }






    }
}