using System;
using System.Collections.Generic;

namespace Northwind.Models;

public partial class UpdateProductDto
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? QuantityPerUnit { get; set; }

    public decimal? UnitPrice { get; set; }

    public int? UnitsInStock { get; set; }

    public int? UnitsOnOrder { get; set; }

    public int? ReorderLevel { get; set; }

    public bool? Discontinued { get; set; }

}
