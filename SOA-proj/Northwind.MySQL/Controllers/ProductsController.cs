using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Northwind.Data;
using Northwind.Models;

namespace Northwind.MySQL.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly NorthwindContext _context;
    private readonly IMapper _mapper;

    public ProductsController(NorthwindContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var productDtos = await _context.Products
            .Select(product => new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
                Category = new CategoryDto
                {
                    CategoryId = product.Category.CategoryId,
                    CategoryName = product.Category.CategoryName,
                },
                Supplier = new SupplierDto
                {
                    SupplierId = product.Supplier.SupplierId,
                    CompanyName = product.Supplier.CompanyName,
                }
            })
            .ToListAsync();

        return Ok(productDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Orderdetails)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> EditProduct(int id, [FromBody] UpdateProductDto updatedProductDto)
    {
        if (id != updatedProductDto.ProductId)
        {
            return BadRequest();
        }

        var existingProduct = await _context.Products.FindAsync(id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        // Use AutoMapper to map properties from updatedProductDto to existingProduct
        _mapper.Map(updatedProductDto, existingProduct);

        try
        {
            await _context.SaveChangesAsync();
            return Ok(updatedProductDto);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
    }


    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto productDto)
    {
        // Check if the Category with the given CategoryId exists in the database
        Category category = await _context.Categories.FindAsync(productDto.Category.CategoryId);

        // Check if the Supplier with the given SupplierId exists in the database
        Supplier supplier = await _context.Suppliers.FindAsync(productDto.Supplier.SupplierId);

        if (category == null)
        {
            // The Category does not exist, so create a new Category
            category = new Category
            {
                CategoryId = productDto.Category.CategoryId
            };

            // Add the new Category to the context and save it
            _context.Categories.Add(category);
        }

        if (supplier == null)
        {
            // The Supplier does not exist, so create a new Supplier
            supplier = new Supplier
            {
                SupplierId = productDto.Supplier.SupplierId
            };

            // Add the new Supplier to the context and save it
            _context.Suppliers.Add(supplier);
        }

        // Now, create the new Product using the Category and Supplier (existing or newly created)
        Product product = new Product
        {
            // Map other properties from the DTO
            ProductName = productDto.ProductName,
            // Map other properties as needed
            Category = category,
            Supplier = supplier
        };

        // Add the new Product to the context and save it
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Map the created Product entity back to a DTO and return it
        var createdProductDto = _mapper.Map<ProductDto>(product);

        return CreatedAtAction("GetProduct", new { id = createdProductDto.ProductId }, createdProductDto);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        // Find and delete associated order details
        var orderDetails = await _context.Orderdetails
            .Where(od => od.ProductId == id)
            .ToListAsync();

        _context.Orderdetails.RemoveRange(orderDetails);

        // Delete the product
        _context.Products.Remove(product);

        try
        {
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.ProductId == id);
    }

}