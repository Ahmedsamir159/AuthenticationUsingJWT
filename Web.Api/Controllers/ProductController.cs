using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Infrastructure.Data;

namespace Web.Api.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET api/Product/home
        [HttpGet]
        public IActionResult Home()
        {
            return new OkObjectResult(new { result = true });
        }

        AppDbContext context;
        public ProductController(AppDbContext _db)
        {
            context = _db;
        }
        [HttpGet]
        [Route("gett-all-products")]
        public List<Product>  GetProducts()
        {
            if (context != null)
            {
                return  context.Products.ToList();
                
            }

            return null;
        }
        [HttpGet]
        [Route("gett-product")]

        public Product GetProduct(int id)
        {
            if (context != null)
            {
                return context.Products.Where(I=>I.Id== id).FirstOrDefault();
            }

            return null;
        }
        [HttpPost]
        [Route("add-new-product")]
        public IActionResult  AddPost(Product Product)
        {
            if (context != null)
            {
                context.Products.Add(Product);
                context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
        [HttpDelete]
        [Route("delete-product")]
        public IActionResult DeletePost(int? id)
        {
            if (context != null)
            {
                //Find the post for specific post id
                var post =  context.Products.FirstOrDefault(x => x.Id == id);

                if (post != null)
                {
                    //Delete that post
                    context.Products.Remove(post);

                    //Commit the transaction
                    context.SaveChanges();
                }
                return Ok();
            }

            return BadRequest();
        }
        [HttpPut]
        [Route("update-product")]
        public IActionResult UpdatePost(Product Product)
        {
            if (context != null)
            {
                //Delete that post
                context.Products.Update(Product);

                //Commit the transaction
                 context.SaveChanges();
                return Ok();
            }
            return BadRequest();

        }
    }
}
