using System;
using System.Collections.Generic;

namespace MKKDotNetTrainingBatch3.BloodInventoryManagement.Database.BloodInventroyDbContextModels;

public partial class TblBloodInventory
{
    public string BloodTypes { get; set; } = null!;

    public int? Count { get; set; }
}
