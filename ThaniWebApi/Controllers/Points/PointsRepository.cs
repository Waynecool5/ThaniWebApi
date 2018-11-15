using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ThaniWebApi.Controllers.Points
{
    public interface IPointsRepository
    {
        Task<IEnumerable<Comp>> GetSomeJsonAsync();

        Task<IEnumerable<Point>> GetPointsAsync();

        Task<MassyRespEarn> DoPointsAsync(string apiType, Point Points);

        Task<MassyRespProfile> GetCustProfile(string apiType, Point Points);

        Task<MassyRespEarn> GetRedeem(string apiType, Point Points);

        Task<MassyRespEarn> GetRefund(string apiType, Point Points);

        Task<MassyRespEarn> GetVoid(string apiType, Point Points);
        //Task<MassyPoints> InsertPointsAsync(Point Points);
        //Task<dynamic> MassyController.InsertMassyApiPoints(MassyPoints mPts);

        //Task<bool> UpdatePointsAsync(Point Points);


        //IEnumerable<Product> GetAll();
        //Product Get(int id);
        //Product Add(Product item);
        //void Remove(int id);
        //bool Update(Product item);
    }
}
