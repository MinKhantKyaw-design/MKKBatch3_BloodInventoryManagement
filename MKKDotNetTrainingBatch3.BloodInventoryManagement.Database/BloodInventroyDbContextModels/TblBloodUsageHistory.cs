using System;
using System.Collections.Generic;

namespace MKKDotNetTrainingBatch3.BloodInventoryManagement.Database.BloodInventroyDbContextModels;

public partial class TblBloodUsageHistory
{
    public int UsingId { get; set; }

    public string BloodTypes { get; set; } = null!;

    public int Count { get; set; }

    public DateTime Date { get; set; }
}
