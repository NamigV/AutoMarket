using AutoMarket.DAL.Interfaces;
using AutoMarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _db;

        public CarRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Car entity)
        {
            _db.Car.Add(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Delete(Car entity)
        {
            _db.Car.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public Car Get(int id)
        {
            return _db.Car.FirstOrDefault(x => x.Id == id);
        }

        public Car GetByName(string name)
        {
            return _db.Car.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Car> Select()
        {
            return _db.Car.ToList();
        }

        public Car Update(Car entity)
        {
            _db.Car.Update(entity);
            _db.SaveChanges();

            return entity;
        }
    }
}
