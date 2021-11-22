using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concessionária
{
    class Maintenance
    {
        private string garage;
        private DateTime when;
        private List<string> parts;


        public Maintenance(string garage, DateTime when, List<string> parts)
        {
            this.garage = garage;
            this.when = when;
            this.parts = parts;
        }

        public static List<Maintenance> AddMaintenances(List<Maintenance> list)
        {
            DateTime date = new DateTime();

            Console.WriteLine("Insira o nome da oficina que a manutenção foi executada");
            string garage = Console.ReadLine();

            Console.WriteLine("Insira a data da manutenção no formato DD-MM-YYYY");
            string maintenanceDate = Console.ReadLine();

            try
            {
                int[] splittedDate = maintenanceDate.Split('-').Select(info => Int32.Parse(info)).ToArray();
                date = new DateTime(splittedDate[2], splittedDate[1], splittedDate[0]);


                List<string> parts = new List<string>();
                bool exitPartsLoop = false;

                while (!exitPartsLoop)
                {
                    Console.WriteLine("Para inserir uma nova peça na manutenção, digite Y, se não, digite N");
                    string userPartsResponse = Console.ReadLine();

                    switch (userPartsResponse.ToUpper())
                    {
                        case "Y":
                            Console.WriteLine("Insira a peça que foi trocada: ");
                            parts.Add(Console.ReadLine());
                            break;

                        case "N":
                            exitPartsLoop = true;
                            break;

                        default:
                            Console.WriteLine("Opção Inválida");
                            break;
                    }
                }

                list.Add(new Maintenance(garage, date, parts));
            }
            catch
            {
                Console.WriteLine("Houve um erro. por favor, tente novamente");
            }

            return list;
        }

        public override string ToString()
        {
            string toString = "";
            toString += String.Format("Oficina: {0}\n", garage);
            toString += String.Format("Feita em: {0}\n", when.Date);
            toString += String.Format("Peças trocadas: {0}\n", String.Join(", ", parts.ToArray()));
            return toString;
        }

    }
}
