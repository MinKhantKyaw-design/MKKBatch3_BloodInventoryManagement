using MKKDotNetTrainingBatch3.BloodInventoryManagement;

BloodInventoryEFCoreService bloodInventoryEFCoreService = new BloodInventoryEFCoreService();

while (true)
{
    Console.WriteLine("\nBlood Donation Inventory");
    Console.WriteLine("---------------------------------------");
    Console.WriteLine("*Choose One of Them* \n" +
                      "1. Blood Inventory List \n" +
                      "2. Blood Donor Information List \n" +
                      "3. Blood Donation History \n" +
                      "4. Do Blood Donation Process \n" +
                      "5. Blood Usage History \n" +
                      "6. Do Blood Usage Process \n" +
                      "0. Exit \n");

    Console.Write("Enter your choice: ");
    string? processString = Console.ReadLine();

    if (int.TryParse(processString, out int processInt))
    {
        switch (processInt)
        {
            case 1:
                bloodInventoryEFCoreService.InventoryList();
                break;
            case 2:
                bloodInventoryEFCoreService.BloodDonorList();
                break;
            case 3:
                bloodInventoryEFCoreService.BloodDonationList();
                break;
            case 4:
                bloodInventoryEFCoreService.BloodDonationProcess();
                break;
            case 5:
                bloodInventoryEFCoreService.BloodUsageList();
                break;
            case 6:
                bloodInventoryEFCoreService.BloodUsageProcess();
                break;
            case 0:
                Console.WriteLine("Exiting the program. Goodbye!");
                return;
            default:
                Console.WriteLine("Invalid choice. Please enter a number between 0 and 6.");
                break;
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a numeric value.");
    }
}

static void BloodUsageProcess()
{
    Console.WriteLine("Blood usage process logic goes here...");
}