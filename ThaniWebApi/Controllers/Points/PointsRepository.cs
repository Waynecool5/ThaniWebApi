using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaniWebApi.Controllers.Points
{
    public interface IPointsRepository
    {

        Task<IEnumerable<Point>> GetPointsAsync();
        Task<IEnumerable<Comp>> GetSomeJsonAsync();

        Task<bool> InsertPointsAsync(Point Points);

        //Task<bool> UpdatePointsAsync(Point Points);


        //IEnumerable<Product> GetAll();
        //Product Get(int id);
        //Product Add(Product item);
        //void Remove(int id);
        //bool Update(Product item);
    }
}
