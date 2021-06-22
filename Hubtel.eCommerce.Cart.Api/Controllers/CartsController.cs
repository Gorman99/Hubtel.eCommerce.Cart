using Hubtel.eCommerce.Cart.Api.Context;
using Hubtel.eCommerce.Cart.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {

        //    private List<Cart> cartList = new List<Cart>();
        private readonly CartDbContext _context;

        public CartsController(CartDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var carts = await _context.Carts.ToListAsync();
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public IActionResult GetCart(int Id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.ItemID == Id);
            if (cart == null)
                return NotFound();
            return Ok(cart);
        }

        [HttpGet("CartByPhone/{phoneNumber}")]
        public IActionResult GetCart([FromRoute] string phoneNumber)
        {
            var cart = _context.Carts.Where(c => c.PhoneNumber == phoneNumber).ToList();
            if (cart == null)
                return NotFound();
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> PostCart(Carts cart)
        {

            if (cart == null)
                return BadRequest();

            var cartExist = await _context.Carts.FirstOrDefaultAsync(C => C.ItemID == cart.ItemID && C.PhoneNumber == cart.PhoneNumber);
            if (cartExist == null)
            {
                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();
                return Ok(cart);
            }

            cartExist.Quantity += 1;
            _context.Entry(cartExist).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(cartExist);
        }


        [HttpPut]
        public async Task<IActionResult> PutCart(Carts cart)
        {
            var cartExist = await _context.Carts.FirstOrDefaultAsync(C => C.ItemID == cart.ItemID);
            if (cartExist == null)
                return NotFound();
            cartExist.Quantity = cart.Quantity;
            cartExist.Price = cart.Price;
            cartExist.PhoneNumber = cart.PhoneNumber;
            cartExist.ItemName = cart.ItemName;
            _context.Entry(cartExist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart(int Id)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.ItemID == Id);
            if (cart == null)
                return NotFound();
            _context.Set<Carts>().Remove(cart);
            await _context.SaveChangesAsync();
            return Ok(cart);
        }
       

    }
}
