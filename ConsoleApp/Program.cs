using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using NugetInspector.Packages;

namespace NugetInspector.ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            var packagesList = new List<Package>();

            using (var powerShellInstance = PowerShell.Create())
            {
                powerShellInstance.AddScript($@"dotnet list {args[0]} package");

                // begin invoke execution on the pipeline
                foreach (var o in powerShellInstance.Invoke())
                {
                    var trimmedPowerShellOutputLine = o.ToString().Trim();

                    if (trimmedPowerShellOutputLine.Contains('>'))
                    {
                        var package = new Package
                        {
                            Name = trimmedPowerShellOutputLine.Split(' ')[1],
                            Version = trimmedPowerShellOutputLine.Split(' ').Last()
                        };

                        packagesList.Add(package);
                    }
                }

                //remove duplicates
                packagesList = packagesList.Select(pkg => pkg).Distinct(new PackagesEqualityComparer()).ToList();

                Console.WriteLine("packagesList.Count: " + packagesList.Count);

                foreach (Package pkg in packagesList)
                {
                    Console.Write(pkg.Name);
                    Console.Write(" ");
                    Console.WriteLine(pkg.Version);
                }
            }
        }
    }
}
