using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concessionária
{
    class Customer
    {
        private string name;
        private string document;

        public Customer(string name, string document)
        {
            this.name = name;
            this.document = document;
        }

        public override string ToString()
        {
            return String.Format("Nome: {0}, Documento: {1}", this.name, this.document);
        }
    }
}
