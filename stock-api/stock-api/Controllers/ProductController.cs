using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using stock_api.Models;

namespace stock_api.Controllers
{
    [EnableCors("STOCK")]
    public class ProductController : Controller
    {
        [HttpGet("api/Products")]
        public async Task<IActionResult> Products()
        {
            using (var db = new stockContext())
            {
                var result = db.products.ToList();
                return Ok(result);
            }
        }

        [HttpGet("api/Products/Detail")]
        public async Task<IActionResult> Detail(string code)
        {
            using (var db = new stockContext())
            {
                var result = db.products.Where(x => x.code == code).FirstOrDefault();
                return Ok(result);
            }
        }

        [HttpPost("api/Products/Detail")]
        public async Task<IActionResult> Save(string code, [FromBody] prod p)
        {
            try
            {
                using (var db = new stockContext())
                {
                    if (code == "new")
                    {
                        db.products.Add(new product
                        {
                            price = p.price,
                            name = p.name,
                            code = p.code
                        });
                        db.SaveChanges();
                    }
                    else
                    {
                        var pd = db.products.Where(x => x.code == code).FirstOrDefault();
                        pd.name = p.name;
                        pd.price = p.price;
                        db.SaveChanges();
                    }
                }
                return Ok(new { message = "successful." });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class prod
    {
        public new string code { get; set; }
        public new string name { get; set; } 
        public new double price { get; set; }
    }
}
