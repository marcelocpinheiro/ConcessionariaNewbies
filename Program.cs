using System;
using System.Collections.Generic;

namespace Concessionária
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            int id = 4;
            List<Car> myCars = CarList();

            while(!exit)
            {
                Console.WriteLine("Bem vindo a concessionária HDN Newbies");
                Console.WriteLine("Escolha uma das opções abaixo");

                Console.WriteLine("1 - Adicionar um veículo");
                Console.WriteLine("2 - Listar todos os veículos");
                Console.WriteLine("3 - Buscar Veículos");
                Console.WriteLine("4 - Filtrar veículos por KM rodados");
                Console.WriteLine("5 - Vender um Carro");
                Console.WriteLine("6 - Filtrar por status");
                Console.WriteLine("0 - Sair");

                string userMenuInput = Console.ReadLine();

                switch(userMenuInput)
                {
                    case "1":
                        myCars = Car.AddCar(myCars, id++);
                        break;

                    case "2":
                        Car.ListCars(myCars);
                        break;

                    case "3":
                        Car.SearchCars(myCars);
                        break;

                    case "4":
                        Car.FilterCarsByKM(myCars);
                        break;

                    case "5":
                        myCars = Car.SellCar(myCars);
                        break;

                    case "6":
                        Car.FilterByStatus(myCars);
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("A opção selecionada é inválida");
                        break;
                }
            }
        }

        static List<Car> CarList()
        {
            List<Car> cars = new List<Car>();
            cars.Add(new Car(0, "Megane", "Renault", 0, "Preto", new List<Maintenance>()));
            cars.Add(new Car(1, "Logan", "Renault", 0, "Prata", new List<Maintenance>()));
            cars.Add(new Car(2, "Gol", "Volkswagem", 0, "Preto", new List<Maintenance>()));
            cars.Add(new Car(3, "Golf", "Volkswagem", 0, "Branco", new List<Maintenance>()));
            return cars;
        }
    }
}
