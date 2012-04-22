using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;

namespace Evil
{
    public class Example : MarshalByRefObject
    {
        public void DoSomethingEvil()
        {
            Console.WriteLine("[Evil] Trying to do something Evil!");

            try
            {
                // Lesen der hosts Datei, weil wir so böse sind!
                TextReader tr = new StreamReader(@"C:\Windows\System32\Drivers\etc\hosts");
                Console.WriteLine(tr.ReadLine());
                tr.Close();
            }
            catch (SecurityException e)
            {
                Console.WriteLine("[Evil] Whops, they catched me!");
            }
        }
    }
}
