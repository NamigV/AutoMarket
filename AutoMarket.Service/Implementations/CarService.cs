using AutoMarket.DAL.Interfaces;
using AutoMarket.Domain.Entity;
using AutoMarket.Domain.Enum;
using AutoMarket.Domain.Response;
using AutoMarket.Domain.ViewModels.Car;
using AutoMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IBaseResponse<Car>> GetCar(int id)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var car = _carRepository.Get(id);
                if(car == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = car;
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[GetCar] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateCar(CarViewModel carViewModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var car = new Car()
                {
                    Description = carViewModel.Description,
                    DateCreate = DateTime.Now,
                    Speed = carViewModel.Speed,
                    Model = carViewModel.Model,
                    Price = carViewModel.Price,
                    Name = carViewModel.Name,
                    TypeCar = (TypeCar)Convert.ToInt32(carViewModel.TypeCar)
                };

                _carRepository.Create(car);
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteCar(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };
            try
            {
                var car = _carRepository.Get(id);
                if(car == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    baseResponse.Data = false;

                    return baseResponse;
                }

                _carRepository.Delete(car);

                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Car>> GetCarByName(string name)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var car = _carRepository.GetByName(name);
                if (car == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = car;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[GetCarByName] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Car>> Edit(int id, CarViewModel model)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var car = _carRepository.Get(id);
                if(car==null)
                {
                    baseResponse.StatusCode=StatusCode.CarNotFound;
                    baseResponse.Description = "Car not found";

                    return baseResponse;
                }

                car.Description = model.Description;
                car.Model = model.Model;
                car.Price = model.Price;
                car.Speed = model.Speed;
                car.DateCreate = model.DateCreate;
                car.Name = model.Name;

                _carRepository.Update(car);

                return baseResponse;

                // TypeCar
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
        {
            var baseResponse = new BaseResponse<IEnumerable<Car>>();
            try
            {
                var cars = _carRepository.Select();
                if(cars.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = cars;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<Car>>()
                {
                    Description = $"[GetCars] : {ex.Message}"
                };
            }
        }
    }
}
