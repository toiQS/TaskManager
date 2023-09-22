using Data;
using ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ModelRequest.ImageModel;
using TaskManager.Models.ModelResponse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ImageController> _logger;
        public ImageController(ApplicationDbContext context, ILogger<ImageController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/<ImageController>
        [HttpGet]
        public async Task<ActionResult<ICollection<ImageIndexRequest>>> GetImageByProductId(string productId)
        {
            if (_context.Images == null)
            {
                return Problem();
            }
            if (!string.IsNullOrEmpty(productId))
            {
                var images = await _context.Images.Where(i => i.ProductId == productId).ToListAsync();
                if (images != null)
                {
                    var result = images.Select(i => new ImageIndexRequest
                    {
                        ImageId = i.ImageId,
                        ImageName = i.ImageName,
                    }).ToList();
                    return Ok(result);
                }
                return NotFound();
            }
            return BadRequest();
        }

        // GET api/<ImageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ICollection<ImageDetailRequest>>> GetImageByImageId(string imageId)
        {
            if (_context.Images == null)
                return Problem();
            if (!string.IsNullOrEmpty(imageId))
            {
                var image = await _context.Images.Where(i => i.ImageId == imageId).FirstOrDefaultAsync();
                if (image != null)
                {
                    return Ok(image);
                }
                return NotFound();
            }
            return BadRequest();
        }

        // POST api/<ImageController>
        [HttpPost]
        public async Task<IActionResult> CreateImageAndAddToProductAsync(ImageResponse newimage)
        {
            if (ModelState.IsValid)
            {
                // Thêm ảnh vào cơ sở dữ liệu
                var image = new Image
                {
                    ImageId = newimage.ImageId,
                    ImageName = newimage.ImageName,
                    ImageUrl = newimage.ImageUrl,
                    ProductId = newimage.ProductId,
                };
                _context.Images.Add(image);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }

                //return CreatedAtAction(nameof(GetImageByImageId), image);
                return Ok();
            }

            return BadRequest();
        }


        // PUT api/<ImageController>/5
        [HttpPut("{imageId}")]
        public async Task<IActionResult> EditImageByImageId(string imageId, ImageResponse newimage)
        {
            if (_context.Images == null)
            {
                return Problem();
            }
            if (ModelState.IsValid && !string.IsNullOrEmpty(imageId))
            {
                var image = await _context.Images.Where(i => i.ImageId == imageId).FirstOrDefaultAsync();
                if (image != null)
                {
                    image.ImageUrl = newimage.ImageUrl;
                    image.ProductId = newimage.ProductId;
                    image.ImageName = newimage.ImageName;
                    _context.Images.Update(image);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return Problem(ex.Message);
                    }
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest(ModelState);

        }

        // DELETE api/<ImageController>/5
        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteImageAsync(string imageId)
        {
            if (_context.Images == null)
            {
                return BadRequest();
            }
            if (!string.IsNullOrEmpty(imageId))
            {
                var image = await _context.Images
                    .Where(i => i.ImageId == imageId)

                    .FirstOrDefaultAsync();
                if (image != null)
                {
                    _context.Images.Remove(image);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return Problem(ex.Message);
                    }
                }
                return NoContent();
            }
            return BadRequest(string.Empty);
        }
    }
}
