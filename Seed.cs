using App.Models;
using App.Data;

namespace App
{
    public static class Seeder
    {
        public static void Seed(DataContext context)
        {
            context.Database.EnsureCreated();

            if (!context.branches.Any())
            {
                var branches = new Branch[]
                {
                    new Branch
                    {
                        Address = "Branch 1", PhoneNumber = "1234567890",
                        Email = "branch1@example.com", PostalCode = "12345"
                    },
                    new Branch
                    {
                        Address = "Branch 2", PhoneNumber = "0987654321",
                        Email = "branch2@example.com", PostalCode = "54321"
                    }
                };

                context.branches.AddRange(branches);
                context.SaveChanges();
            }

            if (!context.devices.Any())
            {
                var branches = context.branches.ToList();

                var devices = new Device[]
                {
                    new Device
                    {
                        BranchId = branches[0].Id,
                        Branch = branches[0],
                        Ip = "192.168.1.1",
                        Port = "8080",
                        Last_Reading_Time = DateTime.Now,
                        type = "Type1",
                        Name = "Device1",
                        Model = "Model1",
                        UserName = "User1",
                        PassWord = "Pass1",
                        Installation_Date = DateTime.Now.AddMonths(-3),
                        Life_Time = "2 years",
                        Maunfacturer = "Manufacturer1",
                        Made_In_Counry = "Country1",
                        State = 1
                    },
                    new Device
                    {
                        BranchId = branches[1].Id,
                        Branch = branches[1],
                        Ip = "192.168.1.2",
                        Port = "8081",
                        Last_Reading_Time = DateTime.Now,
                        type = "Type2",
                        Name = "Device2",
                        Model = "Model2",
                        UserName = "User2",
                        PassWord = "Pass2",
                        Installation_Date = DateTime.Now.AddMonths(-6),
                        Life_Time = "3 years",
                        Maunfacturer = "Manufacturer2",
                        Made_In_Counry = "Country2",
                        State = 2
                    }
                };

                context.devices.AddRange(devices);
                context.SaveChanges();
            }

            if (!context.readingLKPs.Any())
            {
                var readingLKPs = new ReadingLKP[]
                {
                    new ReadingLKP { Name = "Power", Unit = "Watt" },
                    new ReadingLKP { Name = "Voltage", Unit = "Volt" },
                    new ReadingLKP { Name = "Current", Unit = "Ampere" }
                };

                context.readingLKPs.AddRange(readingLKPs);
                context.SaveChanges();
            }

            if (!context.actualReadings.Any())
            {
                var devices = context.devices.ToList();
                var readingLKPs = context.readingLKPs.ToList();

                var actualReadings = new ActualReadings[]
                {
                    new ActualReadings
                    {
                        DeviceId = devices[0].Id, ReadingLKPId = readingLKPs[0].Id,
                        ReadingValue = 25.5m, TimeStamp = DateTime.Now,
                        device = devices[0], readingLKP = readingLKPs[0]
                    },
                    new ActualReadings
                    {
                        DeviceId = devices[1].Id, ReadingLKPId = readingLKPs[1].Id,
                        ReadingValue = 250m, TimeStamp = DateTime.Now,
                        device = devices[1], readingLKP = readingLKPs[1]
                    }
                };

                context.actualReadings.AddRange(actualReadings);
                context.SaveChanges();
            }
        }
    }
}