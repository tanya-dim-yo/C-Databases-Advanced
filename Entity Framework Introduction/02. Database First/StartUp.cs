using SoftUni.Data;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            var empl1 = context.Employees.Find(1);

            Console.WriteLine(empl1.FirstName);
        }
    }
}
