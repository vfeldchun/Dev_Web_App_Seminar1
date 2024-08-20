using DbWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpPost(template: "addgroup")]
        public ActionResult AddGroup(string name, string description)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.ProductGroups.Count(x => x.Name!.ToLower() == name.ToLower()) > 0)
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        ctx.ProductGroups.Add(new ProductGroup { Name = name, Description = description });
                        ctx.SaveChanges();
                    }
                }

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "getgroups")]
        public ActionResult<IEnumerable<ProductGroupModel>> GetGroups()
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var list = ctx.ProductGroups.Select(x => new ProductGroupModel { Id = x.Id, Name = x.Name!, Description = x.Description! }).ToList();
                    return list;
                }
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpDelete(template: "deletegroup")]
        public ActionResult DeleteGroup(int id)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.ProductGroups.Count(x => x.Id == id) > 0)
                    {
                        var deleteRec = ctx.ProductGroups.FirstOrDefault(x => x.Id == id);
                        ctx.ProductGroups.Remove(deleteRec!);
                        ctx.SaveChanges();

                        return Ok();
                    }
                    
                    return StatusCode(404);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost(template: "addproduct")]
        public ActionResult AddProduct(string name, string description, int groupId)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.Products.Count(x => x.Name!.ToLower() == name.ToLower()) > 0)
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        ctx.Products.Add(new Product { Name = name, Description = description, ProductGroupId = groupId });
                        ctx.SaveChanges();
                    }
                }

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost(template: "addprice")]
        public ActionResult AddPrice(long price, int productId)
        {
            if (price < 0) return StatusCode(500);
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.Products.Count(x => x.Id == productId) == 0)
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        var modifiedProduct = ctx.Products.FirstOrDefault(x => x.Id == productId);
                        modifiedProduct!.Price = price;                        
                        ctx.SaveChanges();
                    }
                }

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "getproducts")]
        public ActionResult<IEnumerable<ProductModel>> GetProducts()
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var list = ctx.Products.Select(x => new ProductModel { Id = x.Id, Name = x.Name!, Description = x.Description!, GroupName = x.ProductGroup!.Name!, Price = x.Price ?? 0 }).ToList();

                    return list;
                }
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpDelete(template: "deleteproduct")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.Products.Count(x => x.Id == id) > 0)
                    {
                        var deleteRec = ctx.Products.FirstOrDefault(x => x.Id == id);
                        ctx.Products.Remove(deleteRec!);
                        ctx.SaveChanges();

                        return Ok();
                    }

                    return StatusCode(404);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }  
}
