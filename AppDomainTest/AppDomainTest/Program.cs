using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using Evil;

namespace AppDomainTest
{
    class Program : MarshalByRefObject
    {
        static void Main(string[] args)
        {
            // Pfad, muss absolut sein für FileIOPermission
            string path = @"C:\Users\philipp\Dropbox\Dokumente\Schule\PR\src\dot-net-security-and-encryption\AppDomainTest\Evil\bin\Debug";

            // PermissionSet ohne Berechtigungen Erstellen
            PermissionSet set = new PermissionSet(PermissionState.None);

            // Berechtigen zum Ausführen des Programmes hinzufügen
            set.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

            // Berechtigungen zum Lesen des Pfades hinzufügen
            set.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read |
                                                   FileIOPermissionAccess.PathDiscovery,
                                                   path));

            // Info, wird benötigt um Domain zu erstellen
            var info = new AppDomainSetup { ApplicationBase = path };

            // Erstellen der Domain
            AppDomain domain = AppDomain.CreateDomain("Sandbox", null, info, set, null);

            // Laden der Instance
            Example evil = (Example) domain.CreateInstanceFromAndUnwrap(path + @"\Evil.dll", "Evil.Example");

            // Ausführen der (bösen) Methode
            evil.DoSomethingEvil();

            Console.ReadKey();
        }
    }
}