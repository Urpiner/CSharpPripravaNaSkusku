using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace AddressBook.CommonLibrary
{
    public class EmployeeList : List<Employee>
    {
        public static EmployeeList? LoadFromJson(FileInfo jsonFile)
        {
            string jsonText;
            EmployeeList? employeeList = null;


            jsonText = File.ReadAllText(jsonFile.FullName);
            employeeList = JsonSerializer.Deserialize<EmployeeList>(jsonText);

            //zakomentovany kod: ak chces rethrownut exceptiony pri loadovani jsonu

            //try
            //{
            //    jsonText = File.ReadAllText(jsonFile.FullName);
            //    employeeList = JsonSerializer.Deserialize<EmployeeList>(jsonText);
            //}
            //catch (JsonException ex)
            //{
            //    // Re-throw the exception to be caught by the outer method
            //    throw new JsonException("Error occurred during JSON deserialization.", ex);
            //}
            //catch (IOException ex)
            //{
            //    // Re-throw the exception to be caught by the outer method
            //    throw new IOException("Error occurred while reading the JSON file.", ex);
            //}

            return employeeList;
        }

        public void SaveToJson(FileInfo? jsonFile)
        {
            string jsonText = JsonSerializer.Serialize(this);
            File.WriteAllText(jsonFile.FullName, jsonText);
        }

        public IEnumerable<string> GetPositions()
        {
            return this.Select(x => x.Position).Distinct().OrderByDescending(x => x);
        }

        public IEnumerable<string> GetMainWorkplaces()
        {
            return this.Where(x => x.Workplace != null).Select(x => x.Workplace).Distinct().OrderByDescending(x => x);
            //return this.Select(x => x.WorkPlace).Distinct().OrderByDescending(x => x);
        }

        public SearchResult Search(string? mainWorkplace = null, string? position = null, string? name = null)
        {
            Employee[] employees = this.Where(x => EvaluateCond(mainWorkplace, x.Workplace, true) && EvaluateCond(position, x.Position, true) && EvaluateCond(name, x.Name, true)).Select(x => x).ToArray();
 
            return new SearchResult(employees);
        }

        private bool EvaluateCond(string? stringCriterion, string stringValue, bool checkIfStrContainsOtherStr) 
        { 
            if (stringCriterion == null)
            {
                return true;
            }
            else
            {
                if (checkIfStrContainsOtherStr)
                {
                    string strCritLower = stringCriterion.ToLower();
                    string strValLower = stringValue.ToLower();
                    return strValLower.Contains(strCritLower);
                }
                else
                {
                    return stringCriterion == stringValue;
                }
                
            }
        }
    }
}
