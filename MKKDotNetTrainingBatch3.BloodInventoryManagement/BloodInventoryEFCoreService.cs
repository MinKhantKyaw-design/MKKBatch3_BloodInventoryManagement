using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKKDotNetTrainingBatch3.BloodInventoryManagement.Database.BloodInventroyDbContextModels;

namespace MKKDotNetTrainingBatch3.BloodInventoryManagement
{
    public class BloodInventoryEFCoreService
    {
        BloodInventroyDbContext bloodInventroyDbContext;
        public BloodInventoryEFCoreService() 
        { 
            bloodInventroyDbContext = new BloodInventroyDbContext();
        }

        #region Blood Donor
        public void BloodDonorList()
        {
            var lst = bloodInventroyDbContext.TblDonors.ToList();

            Console.WriteLine("-------------- Blood Donor List --------------");
            for (int i=1; i<=lst.Count; i++)
            {
                var item = lst[i-1];
                Console.WriteLine(i + ". Name:" + item.Name);
                Console.WriteLine("Age:" + item.Age);
                Console.WriteLine("Email:" + item.Email);
                Console.WriteLine("Phone Number:" + item.PhoneNo);
                Console.WriteLine("Blood Type:" + item.BloodType);
                Console.WriteLine("Donation Count:" + item.DonationCount);
                Console.WriteLine("---------------------------------");
            }
        }
        #endregion

        #region Blood Inventory
        public void InventoryList()
        {
            var lst = bloodInventroyDbContext.TblBloodInventories.ToList();

            Console.WriteLine("-------------- Inventory List --------------");
            foreach (var item in lst)
            {
                Console.WriteLine("Blood Type:" + item.BloodTypes);
                Console.WriteLine("Total:" + item.Count);
            }
        }
        #endregion

        #region Blood Donation History
        public void BloodDonationList()
        {
            var lst = bloodInventroyDbContext.TblBloodDonationHistories.ToList();

            Console.WriteLine("-------------- Blood Donation List --------------");
            for (int i = 1; i <= lst.Count; i++)
            {
                var item = lst[i-1];
                Console.WriteLine(i + ". DonarID:" + item.DonorId + ", Blood Type:" + item.BloodTypes + ", Count:" + item.Count + ", Date:" + item.Date);
            }
            Console.WriteLine();
        }

        public void BloodDonationProcess()
        {
            Console.WriteLine("Have you done Blood Donation from this System. Enter only false (or) true.");
            bool isDonate = Convert.ToBoolean(Console.ReadLine());

            if (isDonate) 
            {
                // Existing donor label
                Console.WriteLine("Proceeding with donation for an existing donor.");

                Console.WriteLine("Find Donor By Email:");
                string Email = Console.ReadLine();

                var findDonor = bloodInventroyDbContext.TblDonors.FirstOrDefault(x => x.Email == Email);
                if (findDonor is null)
                {
                    Console.WriteLine("Finding Donor Information not found in this system");
                    return;
                }

                // Create a new donation history record
                var newDonation = new TblBloodDonationHistory
                {
                    DonorId = findDonor.DonorId,
                    BloodTypes = findDonor.BloodType,
                    Count = 1,
                    Date = DateTime.Now
                };

                bloodInventroyDbContext.TblBloodDonationHistories.Add(newDonation);

                int savingDonationResult = bloodInventroyDbContext.SaveChanges();
                if (savingDonationResult == 0)
                {
                    Console.WriteLine("Insert Donor Donation History Failed.");
                    return;
                }

                // Update donor's donation
                findDonor.DonationCount += 1;
                findDonor.ModifiedDate = (DateTime)newDonation.Date;

                int savingDonorResult = bloodInventroyDbContext.SaveChanges();

                if (savingDonorResult == 0)
                {
                    Console.WriteLine("Update Donor Information Failed.");
                    return;
                }

                var findBloodInventory = bloodInventroyDbContext.TblBloodInventories.FirstOrDefault(x => x.BloodTypes == findDonor.BloodType);
                findBloodInventory.Count += 1;

                int savingBloodInventory = bloodInventroyDbContext.SaveChanges();

                string message = savingBloodInventory > 0 ? "Saving Successful." : "Saving Failed in Blood Inventory List";
                Console.WriteLine(message);
            }
            else
            {
                // Existing donor label
                Console.WriteLine("Proceeding with donation for a new donor.");

                Console.WriteLine("Enter Name:");
                string Name = Console.ReadLine();

                Console.WriteLine("Enter Age:");
                int Age = Convert.ToInt32(Console.ReadLine());

                if (Age < 17 || Age > 65)
                {
                    Console.WriteLine("You cannot donate your blood in your age.");
                    return;
                }

                Console.WriteLine("Enter Email:");
                string Email = Console.ReadLine();

                Console.WriteLine("Enter Phone Number:");
                string PhoneNo = Console.ReadLine();

                Console.WriteLine("Enter Blood Type (A+,A-,B+,B-,AB+,AB-,O+,O-):");
                string BloodType = Console.ReadLine();

                

                Console.WriteLine("Do you have HIV/AIDS. Are you feel illness. Enter only false (or) true.");
                bool haveDisease = Convert.ToBoolean(Console.ReadLine());

                if (haveDisease) 
                {
                    Console.WriteLine("You cannot donate!");
                    return;
                }

                // Create a new donor object
                var newDonor = new TblDonor
                {
                    Name = Name,
                    Age = Age,
                    Email = Email,
                    PhoneNo = PhoneNo,
                    BloodType = BloodType,
                    DonationCount = 1,
                    CreatedDate = DateTime.Now
                };

                // Add to context
                bloodInventroyDbContext.TblDonors.Add(newDonor);

                int savingDonorResult = bloodInventroyDbContext.SaveChanges();

                if (savingDonorResult == 0) {
                    Console.WriteLine("Saving new blood donor failed.");
                    return;
                }

                bloodInventroyDbContext.TblBloodDonationHistories.Add(new TblBloodDonationHistory
                {
                    DonorId = newDonor.DonorId,
                    BloodTypes = BloodType,
                    Count = 1,
                    Date = (DateTime)newDonor.CreatedDate
                });

                int savingDonationResult = bloodInventroyDbContext.SaveChanges();

                if (savingDonationResult == 0)
                {
                    Console.WriteLine("Insert Donor Donation History Failed.");
                    return;
                }

                var findBloodInventory = bloodInventroyDbContext.TblBloodInventories.FirstOrDefault(x => x.BloodTypes == BloodType);
                findBloodInventory.Count += 1;

                int savingBloodInventory = bloodInventroyDbContext.SaveChanges();

                string message = savingBloodInventory > 0 ? "Saving Successful." : "Saving Failed in Blood Inventory List";
                Console.WriteLine(message);
            }
        }
        #endregion

        #region Blood Usage History
        public void BloodUsageList()
        {
            var lst = bloodInventroyDbContext.TblBloodUsageHistories.ToList();

            Console.WriteLine("-------------- Blood Usage List --------------");
            for (int i = 1; i <= lst.Count; i++)
            {
                var item = lst[i-1];
                Console.WriteLine(item.UsingId + ", Blood Type:" + item.BloodTypes + ", Count:" + item.Count + ", Date:" + item.Date);
            }
            Console.WriteLine();
        }

        public void BloodUsageProcess()
        {
            Console.Write("Enter the Blood Type you Need (A+,A-,B+,B-,AB+,AB-,O+,O-): ");
            string BloodType = Console.ReadLine();

            Console.Write("Enter the number: ");
            int Count = Convert.ToInt32(Console.ReadLine());

            var findBloodInventory = bloodInventroyDbContext.TblBloodInventories.FirstOrDefault(x => x.BloodTypes == BloodType);

            if(findBloodInventory.Count< Count || findBloodInventory.Count==0)
            {
                Console.WriteLine("Insufficient inventory available.");
                return;
            }
            findBloodInventory.Count -= Count;

            int savingBloodInventory = bloodInventroyDbContext.SaveChanges();
            if (savingBloodInventory == 0)
            {
                Console.WriteLine("Update Blood Inventory List Failed.");
                return;
            }

            bloodInventroyDbContext.TblBloodUsageHistories.Add(new TblBloodUsageHistory
            {
                BloodTypes = BloodType,
                Count = Count,
                Date = DateTime.Now
            });

            int savingUsageResult = bloodInventroyDbContext.SaveChanges();
            string message = savingBloodInventory > 0 ? "Saving Successful." : "Saving Failed in Blood Usage History";
            Console.WriteLine(message);
        }
        #endregion

        
    }
}
