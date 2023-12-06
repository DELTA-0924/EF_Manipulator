using AutoLotConsoleApp1.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace AutoLotConsoleApp1
{
    public class Functions : IFunctionForAddandPrint, IFunctionLoadfromDataBase,IFunctionDeleteFromDataBase
    {
        public static void Print()
        {
            Console.WriteLine("Hello");
        }
        public  async Task<int> AddNewRecord()
        {
            using (var context = new AutoLotEntities())
            {
                try
                {
                    var car = new Car() { Model = "Frog", Color = "Red", CarNickName = "Lotus" };
                    context.Cars.Add(car);
                    await context.SaveChangesAsync();
                    return car.CarId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex?.InnerException.Message);
                }
                return 0;
            }
        }
        public async Task AddNewRecords(IEnumerable<Car> carList)
        {
            using (AutoLotEntities context = new AutoLotEntities())
            {
                try
                {
                    context.Cars.AddRange(carList);
                    await context.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex?.InnerException.Message);
                }
            }
        }

        public  async Task PrintAllInventoryV1()
        {
            using (var context = new AutoLotEntities())
            {
                foreach (Car car in await context.Cars.ToListAsync())
                    Console.WriteLine(car);
            }
        }
        public  async Task PrintAllInventoryV2()
        {
            using (var context = new AutoLotEntities())
            {
                foreach (Car car in await context.Cars.SqlQuery("Select CarId,Model,Color,PetName as CarNickName from Inventory").ToListAsync())
                {
                    Console.WriteLine(car);
                }
            }
        }
        public  async Task PrintAllInventoryV3()
        {
            using (var context = new AutoLotEntities())
            {
                var Cars = await context.Cars.Select(car => new { car.Color, car.Model, car.CarId, car.CarNickName }).ToListAsync();
                foreach (var car in Cars)
                {
                    Console.WriteLine(car);
                }
            }
        }
        public  async Task PrintAllInventoryV4()
        {
            using (var context = new AutoLotEntities())
            {
                foreach (var car in await context.Database.SqlQuery(typeof(ShortCar), "Select CarID,Model as CarModel from Inventory").ToListAsync())
                {
                    Console.WriteLine(car);
                }
            }
        }

       public async Task LazyLoad()
        {
       
            using (var context=new AutoLotEntities())
            {
                context.Configuration.LazyLoadingEnabled = true;
                foreach (Car car in  await context.Cars.ToListAsync())
                {
                    Console.WriteLine(car);
                    foreach (Order order in  car.Orders)
                    {
                        Console.WriteLine(order);
                      
                    }

                }
            }
        }
        public async Task EnergyLoad()
        {
            using (var context =new AutoLotEntities())
            {
                var carsWithOrders = await context.Cars.Include(c => c.Orders).ToListAsync();                                
                foreach (var car in carsWithOrders)
                {
                    WriteLine(car);
                    if (car.Orders.Count() > 0)
                        foreach (var order in car.Orders)
                        {

                            Console.WriteLine(order.OrderId);
                        }
                    else WriteLine("Empty");
                }
            }
        }
   
       virtual public async Task TrueLoad()
        {
            using (var context=new AutoLotEntities())
            {
 
                foreach (var c in await context.Cars.ToListAsync())
                {
                    context.Entry(c).Collection(x => x.Orders).Load();
                    foreach (var order in c.Orders) {
                        Console.WriteLine(order.OrderId);
                    }


                }
            }
        }
        public async Task RemoveRecord(int CarID)
        {
            using (var context =new AutoLotEntities())
            {
                var recordToDelete = context.Cars.Find(CarID);
                if (recordToDelete != null)
                {
                    context.Cars.Remove(recordToDelete);
                    if (context.Entry(recordToDelete).State != EntityState.Deleted)
                    {
                        throw new Exception("Unable to delete the record");
                    }
                    await context.SaveChangesAsync ();
                }  
            }
        }
        public async Task RemoveRangeRecors(IEnumerable<Car> records)
        {
            using (var context =new AutoLotEntities())
            {
                foreach(var record in records)
                {
                    if (context.Entry(record).State != EntityState.Detached)
                    {
                        context.Cars.Attach(record);
                    }
                }
                context.Cars.RemoveRange(records);
                await context.SaveChangesAsync();
            }
        }
        public async Task RemoveRecordsUsingEntityState(int CarId)
        {
            using(var context =new AutoLotEntities())
            {
                Car cartoDelete = new Car { CarId = CarId };
                context.Entry(cartoDelete).State=EntityState.Deleted;
                try
                {
                    await context.SaveChangesAsync();
                }catch(DbUpdateConcurrencyException ex){
                    Console.WriteLine(ex.Message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public async Task Updaterecord(int CarID)
        {
            using (var context =new AutoLotEntities())
            {
                Car carToDelete = context.Cars.Find();
                if (carToDelete != null)
                {
                    WriteLine(context.Entry(carToDelete).State);
                    carToDelete.Color = "Blue";
                    WriteLine(context.Entry(carToDelete).State);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
    public class ShortCar
    {
        public int CarId { get; set; }
        public string CarModel { get; set; }
        public override string ToString()
        {
            return $"Car id=>{this.CarId}\n Car Model->{this.CarModel}";
        }
    }
}
