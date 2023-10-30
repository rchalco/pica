using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runtime.Receptor.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Receptor");
            ReceptorServiceTest receptorServiceTest = new ReceptorServiceTest();
            receptorServiceTest.ActivarReceptorTest();
            Console.ReadLine();
        }
    }
}
