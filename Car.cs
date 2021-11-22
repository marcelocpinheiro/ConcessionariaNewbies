using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Carro
// - Modelo
// - Marca
// Kms Rodados
// Cor
// Manutenções
// Status (Em Estoque, Vendido)
// Vendido para: Cliente


namespace Concessionária
{

    enum CarFilterProperties
    {
        Model,
        Brand,
        Color
    }

    class Car
    {
        private int id;
        private string model;
        public string brand;
        private int km;
        private string color;
        private List<Maintenance> maintenances;
        private string status;
        private Customer customer;

        public Car(int id, string model, string brand, int km, string color, List<Maintenance> maintenances, string status, Customer customer)
        {
            this.id = id;
            this.model = model;
            this.brand = brand;
            this.km = km;
            this.color = color;
            this.maintenances = maintenances;
            this.status = status;
            this.customer = customer;
        }

        public Car(int id, string model, string brand, int km, string color, List<Maintenance> maintenances)
        {
            this.id = id;
            this.model = model;
            this.brand = brand;
            this.km = km;
            this.color = color;
            this.maintenances = maintenances;
            status = "Em Estoque";
            customer = null;
        }

        public string getBrand()
        {
            return this.brand;
        }

        public string getModel()
        {
            return this.model;
        }

        public string getColor()
        {
            return this.color;
        }

        public int getKM()
        {
            return this.km;
        }

        public int getID()
        {
            return this.id;
        }

        public string getStatus()
        {
            return this.status;
        }

        public void setCustomer(Customer customer)
        {
            this.customer = customer;
        }

        public void setStatus(string status)
        {
            this.status = status;
        }
        public static void FilterByStatus(List<Car> cars)
        {
            Console.WriteLine("Qual o status que deseja filtrar?");
            Console.WriteLine("1 - Em Estoque");
            Console.WriteLine("2 - Vendidos");

            string response = Console.ReadLine();
            switch(response)
            {
                case "1":
                    ListCars(cars.FindAll(car => car.getStatus().Equals("Em Estoque")));
                    break;

                case "2":
                    ListCars(cars.FindAll(car => car.getStatus().Equals("Vendido")));
                    break;

                default:
                    Console.WriteLine("Status inválido");
                    break;
            }


        }

        public static List<Car> SellCar(List<Car> cars)
        {
            Console.WriteLine("Qual o ID do carro que deseja vender?");
            string id = Console.ReadLine();

            try
            {
                var parsedID = Int32.Parse(id);
                Car car = cars.Find(car => car.getID().Equals(parsedID));
                if(null != car)
                {
                    Console.WriteLine("Qual o nome do cliente que comprou o carro?");
                    string name = Console.ReadLine();

                    Console.WriteLine("Qual o documento do cliente que comprou o carro?");
                    string document = Console.ReadLine();

                    car.setCustomer(new Customer(name, document));
                    car.setStatus("Vendido");

                    List<Car> newList = cars.FindAll(car => !car.getID().Equals(parsedID));
                    newList.Add(car);
                    return newList;
                } else
                {
                    Console.WriteLine("Carro não encontrado.");
                }
            }
            catch
            {
                Console.WriteLine("Valor inválido.");
                return cars;
            }

            return cars;
        }

        public static List<Car> AddCar(List<Car> myCars, int id)
        {
            Console.Clear();

            Console.WriteLine("Quantos kilometros este carro já rodou? ");
            string km = Console.ReadLine();
            int parsedKm = 0;

            try
            {
                parsedKm = Int32.Parse(km);
            }
            catch
            {
                Console.WriteLine("O valor de kilometros rodados precisa ser numérico. Por favor, tente novamente.");
                return myCars;
            }


            Console.WriteLine("Digite o modelo do carro que deseja adicionar: ");
            string model = Console.ReadLine();

            Console.WriteLine("Digite a marca do carro que deseja adicionar: ");
            string brand = Console.ReadLine();

            Console.WriteLine("Qual a cor do carro? ");
            string color = Console.ReadLine();

            List<Maintenance> maintenances = new List<Maintenance>();
            bool exitMaintenanceLoop = false;

            while (!exitMaintenanceLoop)
            {
                Console.WriteLine("Para inserir uma nova manutenção, digite 1, se não, digite 0");
                string userMaintenanceResponse = Console.ReadLine();

                switch (userMaintenanceResponse)
                {
                    case "1":
                        maintenances = Maintenance.AddMaintenances(maintenances);
                        break;

                    case "0":
                        exitMaintenanceLoop = true;
                        break;

                    default:
                        Console.WriteLine("Opção Inválida");
                        break;
                }
            }


            myCars.Add(new Car(id, model, brand, parsedKm, color, maintenances));

            return myCars;
        }

        public static void ListCars(List<Car> cars)
        {
            Console.WriteLine("******* Carros Cadastrados *******");
            Console.WriteLine(String.Join("\n\n", cars.Select(car => car.ToString()).ToArray()));
        }

        public static void SearchCars(List<Car> cars)
        {
            Console.Clear();
            Console.WriteLine("******* Encontrar Veículo ********");
            Console.WriteLine("Escolha o parametro que deseja buscar o carro");
            Console.WriteLine("1 - Modelo");
            Console.WriteLine("2 - Marca");
            Console.WriteLine("3 - Cor");

            string resposta = Console.ReadLine();
            try
            {
                findBy(cars, (CarFilterProperties)(int.Parse(resposta) - 1));
            } catch
            {
                Console.WriteLine("Opção inválida");
            }
            
        }

        public static void FilterCarsByKM(List<Car> cars)
        {
            Console.Clear();
            Console.WriteLine("Digite o valor máximo de kilometros rodados: ");
            string maxValue = Console.ReadLine();

            int maxValueParsed = 0;

            try
            {
                maxValueParsed = int.Parse(maxValue);
                ListCars(cars.FindAll(car => car.getKM() < maxValueParsed));
            } catch
            {
                Console.WriteLine("Valor inválido");
            }
        }

        public static void findBy(List<Car> cars, CarFilterProperties property)
        {
            Dictionary<CarFilterProperties, string> translations = new Dictionary<CarFilterProperties, string>();
            Dictionary<CarFilterProperties, string> properties = new Dictionary<CarFilterProperties, string>();
            translations.Add(CarFilterProperties.Brand, "marca");
            translations.Add(CarFilterProperties.Model, "modelo");
            translations.Add(CarFilterProperties.Color, "cor");

            properties.Add(CarFilterProperties.Brand, "brand");
            properties.Add(CarFilterProperties.Model, "model");
            properties.Add(CarFilterProperties.Color, "color");


            Console.WriteLine("Defina o valor do parametro {0} que deseja filtrar:", translations[property]);
            string valueToFilter = Console.ReadLine();
            List<Car> carArray = null;
            switch (property)
            {
                case CarFilterProperties.Brand:
                    carArray = cars.FindAll(car => car.getBrand().Equals(valueToFilter));
                    break;

                case CarFilterProperties.Model:
                    carArray = cars.FindAll(car => car.getModel().Equals(valueToFilter));
                    break;

                case CarFilterProperties.Color:
                    carArray = cars.FindAll(car => car.getColor().Equals(valueToFilter));
                    break;

                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }

            ListCars(carArray);
        }

        public override string ToString()
        {
            string toString = "#########################\n";
            toString += String.Format("ID: {0}\n", id);
            toString += String.Format("Modelo: {0}\n", model);
            toString += String.Format("Marca: {0}\n", brand);
            toString += String.Format("KM Rodados: {0}\n", km);
            toString += String.Format("Cor: {0}\n", color);
            toString += String.Format("Status: {0}\n", status);
            toString += String.Format("Cliente: {0}\n", customer);
            toString += "***** MANUTENÇÕES ****\n";
            toString += String.Join("\n\n", maintenances.Select(maintenance => maintenance.ToString()).ToArray());

            return toString;
        }
    }
}
