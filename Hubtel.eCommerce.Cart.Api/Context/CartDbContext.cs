using Hubtel.eCommerce.Cart.Api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Context
{
    public class CartDbContext : DbContext 
    {
        public CartDbContext(DbContextOptions<CartDbContext> options):base(options)
        {

        }

        public DbSet<Carts> Carts { get; set; }
    }
}
