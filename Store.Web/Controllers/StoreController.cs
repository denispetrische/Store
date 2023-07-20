﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Abstractions.Data;
using Store.Web.Dtos.Product;
using Store.Web.Models;

namespace Store.Web.Controllers
{
    [Authorize(Roles ="Manager, Admin")]
    public class StoreController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo _repo;

        public StoreController(IMapper mapper, IProductRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IActionResult> MainView()
        {
            var products = await _repo.GetProducts();
            return View(_mapper.Map<List<Product>,List<ProductStoreViewDto>>(products));
        }
    }
}
