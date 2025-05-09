﻿using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class ProductWarehouse : IEntityId<long>
{
    public long Id { get; set; }
    
    public long ProductId { get; set; }
    
    public long WarehouseId { get; set; }
    
    public int Amount { get; set; }
}