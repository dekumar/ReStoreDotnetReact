using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: ControllerBase
    {

        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            var p = await _context.Products.ToListAsync();
            return Ok(p);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id){
            return Ok(await _context.Products.FindAsync(id));
        }
    }
}