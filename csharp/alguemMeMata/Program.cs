using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace alguemMeMata
{
    class Program 
    {
        static void Main(string[] args)
        {
            var sut = new Allergies(255);
            foreach (var a in sut.List())
            {
                Console.WriteLine(a);
                
            }

            

           


            Console.ReadLine();
        }
    }
}