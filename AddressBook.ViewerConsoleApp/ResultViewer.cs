using AddressBook.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AddressBook.ViewerConsoleApp
{
    internal class ResultViewer
    {
        private string _input; //cesta k input suboru
        private string? _name;
        private string? _mainWorkplace;
        private string? _position;
        private string? _output; //cesta k output suboru
   

        public ResultViewer(string input, string? name, string? mainWorkplace, string? position, string? output) 
        {
            _input = input;
            _name = name;
            _mainWorkplace = mainWorkplace;
            _position = position;
            _output = output;
        }

        public void viewResult()
        {
            //zakomentovany kod: ak by LoadFromJson hadzalo vynimky

            //try
            //{
            //    EmployeeList employeeList = EmployeeList.LoadFromJson(new FileInfo(_input));
            //}
            //catch (JsonException ex)
            //{
            //    Console.WriteLine("Error occurred during JSON deserialization: " + ex.Message);
            //}
            //catch (IOException ex)
            //{
            //    Console.WriteLine("Error occurred while reading the JSON file: " + ex.Message);
            //}

            EmployeeList employeeList = EmployeeList.LoadFromJson(new FileInfo(_input));

            SearchResult result = employeeList.Search(_mainWorkplace, _position, _name);
            if (_output != null) //ak mame output subor, tak tam hodime output
            {
                result.SaveToCsv(new FileInfo(_output));
            }

            int index = 1;
            foreach (Employee employee in result.Employees)
            {
                Console.WriteLine($"[{index}] {employee.Name}");
                Console.WriteLine($"Pracovisko: {employee.Workplace}");
                Console.WriteLine($"Miestnosť: {employee.Room}");
                Console.WriteLine($"Telefón: {employee.Phone}");
                Console.WriteLine($"E-mail: {employee.Email}");
                Console.WriteLine($"Funkcia: {employee.Position}");
                Console.WriteLine();
                index++;
            }
        }
    }
}
