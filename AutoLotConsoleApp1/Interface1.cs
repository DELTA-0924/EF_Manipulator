using AutoLotConsoleApp1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotConsoleApp1
{
    public  interface IFunctionForAddandPrint
    {
        Task<int> AddNewRecord();       
        Task AddNewRecords(IEnumerable<Car> carList);
        Task PrintAllInventoryV1();
        Task PrintAllInventoryV2();
        Task PrintAllInventoryV3();
        Task PrintAllInventoryV4();
    }
    public interface IFunctionLoadfromDataBase
    {
        Task LazyLoad();
        Task TrueLoad();
       Task EnergyLoad();
    }
    public interface IFunctionDeleteFromDataBase
    {
        Task RemoveRecord(int CarId);
        Task RemoveRangeRecors(IEnumerable<Car> records);
        Task RemoveRecordsUsingEntityState(int CarId);
    }
    public interface IFunctionUpdate
    {
        Task Updaterecord(int CarID);
    }
}
