using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stock_api.Models;

namespace stock_api.Controllers
{
    [EnableCors("STOCK")]
    public class ShoppingController : Controller
    {
        [HttpGet("api/Shopping")]
        public IActionResult Index()
        {
            using (var db = new stockContext())
            {
                var result = db.products.Select(x => new
                {
                    x.code,
                    x.name,
                    x.price,
                    stock = x.stock == null ? 0 : x.stock.qty
                }).ToList();
                return Ok(result);
            }
        }

        [HttpPost("api/Shopping")]
        public IActionResult Checkout([FromBody] List<cart> carts)
        {
            using (var db = new stockContext())
            {
                var dateNow = DateTime.Now;
                var tran = new transaction
                {
                    transactionDate = dateNow,
                };
                db.transactions.Add(tran);
                foreach (var cart in carts)
                {
                    tran.transactionDetails.Add(new transactionDetail
                    {
                        code = cart.code,
                        qty = cart.qty,
                    });

                    var stock = db.stocks.Where(x => x.code == cart.code).FirstOrDefault();
                    if (stock != null)
                    {
                        stock.qty = stock.qty - cart.qty;
                    }
                }
                db.SaveChanges();
                return Ok(new { message = "successful." });
            }
        }

        public class cart
        {
            public string code { get; set; }
            public int qty { get; set; }
        }
    }
}


//Scaffold-DbContext "Server=.\SQLEXPRESS;Database=stock;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –force -UseDatabaseNames