using System;
using System.Linq;
using System.Reflection;

namespace BinCalcResearch
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFrom("BinCalc.dll");

            Type[] assemblyTypes = assembly.GetTypes();
            Console.WriteLine("Типы:");
            assemblyTypes.ToList().ForEach((i) => { Console.WriteLine("\t" + i.Name + " " + i.FullName); });

            FieldInfo[] ts = assemblyTypes[0].GetFields();
            Console.WriteLine("Публичные поля (" + ts.Length + " штук) " + assemblyTypes[0].Name + ":");
            ts.ToList().ForEach((i) => { Console.WriteLine("\t" + i.FieldType.Name + " " + i.Name); });

            MethodInfo[] ms = assemblyTypes[0].GetMethods();
            Console.WriteLine("Методы " + assemblyTypes[0].Name + ":");
            ms.ToList().ForEach((i) => { Console.WriteLine("\t" + i.DeclaringType + "::" + i.Name); });

            MethodInfo method = assemblyTypes[0].GetMethods()[0];
            Console.WriteLine("Сигнатура " + assemblyTypes[0].Name + "::" + method.Name + ":");
            string parameters = string.Join("", method.GetParameters().Select(i => i.Name + ",  "));
            parameters = parameters.Remove(parameters.Length - 3);
            Console.WriteLine("\t" + method.ReturnType.Name + " " + method.Name + " (" + parameters + ")");

            Console.ReadKey();
        }
    }
}
