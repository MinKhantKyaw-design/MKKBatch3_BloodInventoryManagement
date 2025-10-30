using System;
using System.Collections.Generic;

namespace MKKDotNetTrainingBatch3.BloodInventoryManagement.Database.BloodInventroyDbContextModels;

public partial class TblDonor
{
    public int DonorId { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string BloodType { get; set; } = null!;

    public int? DonationCount { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
