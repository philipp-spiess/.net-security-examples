using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Security.Permissions;

namespace Authentifizierung
{
    class Program
    {
        static void Main(string[] args)
        {
          
            Console.WriteLine(
                new WindowsPrincipal(WindowsIdentity.GetCurrent())
                .IsInRole(WindowsBuiltInRole.Administrator) 
                ? "Yup" : "Nope");
            Console.ReadKey();
        }
    }
}
