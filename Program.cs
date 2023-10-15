List<Employee> employeeList = new List<Employee> {};

bool quit = false;
string confirmString = "err";
init();

while (quit == false)
{
    ConsoleKey input =Console.ReadKey().Key;

        switch (input)
    {
        case ConsoleKey.D1:
            createEmployee();
            break;

        case ConsoleKey.D2:
            viewEmployees();
            break;

        case ConsoleKey.D3:
            searchEmployee();
            break;

        case ConsoleKey.D4:
            updateEmployeeCSV();
            quit = true;
            break;

        case ConsoleKey.D5:
            printOptions();
            break;

        default:
            printOptions();
            error();
            break;
    }
}

void init()
{
    
    setEmployeeList();

    printTitle();
    Thread.Sleep(1000);

    printOptions();
    
}

void printTitle ()
{
    Console.Clear();
    Console.WriteLine(@" ________                      __                                 ______           _          ______                        ");
    Console.WriteLine(@"|_   __  |                    [  |                               |_   _ `.        / |_       |_   _ \                       ");
    Console.WriteLine(@"  | |_ \_| _ .--..--.  _ .--.  | |  .--.   _   __  .---.  .---.    | | `. \ ,--. `| |-',--.    | |_) |  ,--.   .--.  .---.  ");
    Console.WriteLine(@"  |  _| _ [ `.-. .-. |[ '/'`\ \| |/ .'`\ \[ \ [  ]/ /__\\/ /__\\   | |  | |`'_\ : | | `'_\ :   |  __'. `'_\ : ( (`\]/ /__\\ ");
    Console.WriteLine(@" _| |__/ | | | | | | | | \__/ || || \__. | \ '/ / | \__.,| \__.,  _| |_.' /// | |,| |,// | |, _| |__) |// | |, `'.'.| \__., ");
    Console.WriteLine(@"|________|[___||__||__]| ;.__/[___]'.__.'[\_:  /   '.__.' '.__.' |______.' \'-;__/\__/\'-;__/|_______/ \'-;__/[\__) )'.__.' ");
    Console.WriteLine(@"                      [__|                \__.'                                                                             ");
}

void printOptions ()
{
    Console.Clear();

    Console.WriteLine(" Employee DataBase\n---------------------\n1. Create new employee\n2. View all employees\n3. Search or Delete employee\n4. Exit\n---------------------");
}

void createEmployee() 
{
    string name;
    string title;
    string input;

    Console.WriteLine("\n  * Create Employee *\n");
    Console.Write("What is the employee's full name? - ");

        input = Console.ReadLine();
    if (input != "")
    {
        name = input;

            Console.Write("What is the employee's Title? - ");
            title = Console.ReadLine();

        employeeList.Add(new Employee (name, title));
        printOptions();

       Console.WriteLine("\n  * New employee has been added! *");
    } else
    {
        printOptions();
    }
  
}

void viewEmployees()
{
    printOptions();

    foreach (Employee em in employeeList)
    {
        Console.WriteLine(em.getDetails());
    }
}

void searchEmployee()
{
    bool exit = false;
    printOptions();
    Console.WriteLine(" Search for employee");

    while (exit == false)
    {
            Console.Write("\nWhat is the employee Name or ID? - ");
        try {

            string input = Console.ReadLine().ToLower().Trim();
            if (String.IsNullOrWhiteSpace(input))
            {
                printOptions();
                exit = true;
            } else if (int.TryParse(input, out int inputNum))
            {
                
                var  filtered = employeeList.Where(emp => emp.getId().ToString().Contains(inputNum.ToString()));
                List <Employee> emps = new List<Employee> {};
                
                int i = 0;
                foreach (var emp in filtered)
                {
                    emps.Add(emp);
                    Console.WriteLine(($"{i+1}. ") +emp.getDetails());
                    i++;

                }
                if (emps.Count < 1)
                {
                    Console.WriteLine("\nList is empty");
                }

                Console.WriteLine ("\nDelete numbered employee\nInput y to search again\nOr any other key to continue...");

                input = Console.ReadLine().ToLower().Trim();
                if (int.TryParse(input, out inputNum))
                {
                        Employee deleteEmp = emps[inputNum-1];
                        deleteEmployee(deleteEmp);
                        exit = true;
                } else  if (input == "y")
                    {
                        printOptions();
                        exit = false;
                    } else 
                    {
                        printOptions();
                        exit = true;
                    }
            } else 
            {
                var filtered = employeeList.Where(emp => emp.getName().ToLower().Contains(input));

                List <Employee> emps = new List<Employee> {};
                
                int i = 0;
                foreach (var emp in filtered)
                {
                    emps.Add(emp);
                    Console.WriteLine(($"{i+1}. ") +emp.getDetails());
                    i++;

                }
                if (emps.Count < 1)
                {
                    Console.WriteLine("\nList is empty");
                }

                Console.WriteLine ("\nDelete numbered employee\nInput y to search again\nOr any other key to continue...");

                input = Console.ReadLine().ToLower().Trim();
                if (int.TryParse(input, out inputNum))
                {
                        Employee deleteEmp = emps[inputNum-1];
                        deleteEmployee(deleteEmp);
                        exit = true;
                } else  if (input == "y")
                    {
                        printOptions();
                        exit = false;
                    } else 
                    {
                        printOptions();
                        exit = true;
                    }

            }
        } catch
        {
            error();
        }
    }
}

bool deleteEmployee( Employee deleteEmp)
{
    bool confirmExit = false;

    while (confirmExit == false)
    {
        confirm("delete", deleteEmp.getName());

        switch (confirmString)
        {
            case "y":
                employeeList.Remove(deleteEmp);

                printOptions();
                Console.WriteLine("\n  Employee Deleted!");
                confirmExit = true;
                return true;
                break;
            
            case "n":
                printOptions();
                confirmExit = true;
                return false;
                break;

            default:
            
            error();
            break;

        }
    } 
    return false;
}

void updateEmployeeCSV ()
{
    using (StreamWriter file = new StreamWriter ("Employee-List.csv"))
    {
        foreach (Employee emp in employeeList)
        {
            file.WriteLine(convertEmployeeToString(emp));
        }
    }
}

void setEmployeeList ()
{
    string [] employeeTxt = File.ReadAllLines("Employee-List.csv");
    foreach(string emp in employeeTxt)
    {
        employeeList.Add(convertStringToEmployee(emp));
    }


}

void error()
{
    Console.WriteLine("\n  Please provide a valid input ");
}

void confirm (string action, string thing)
{


    string [] affirmations = {"y", "ya", "yes", "yep", "yah", "yeah", "yesir", "yes sir", "mhmm", "m hmm"};
    string [] negations = {"n", "no", "nah", "naw", "nope", "no sir", "nosir","nhnh", "nh nh"};

    Console.Write($"\nAre you sure you want to {action} {thing}? - ");    
    string input = Console.ReadLine();

    if (affirmations.Contains(input.ToLower().Trim()))
    {
        confirmString = "y";
    } else if (negations.Contains(input.ToLower().Trim()))
    {
        confirmString = "n";
    } else
    {
        confirmString = "err";
    }
}

string convertEmployeeToString (Employee emp)
{
    string [] info = {emp.getName(), emp.getTitle(), emp.getId().ToString(), emp.getStartDate()};
    string conInfo = String.Join(",", info);

    return conInfo;
}

Employee convertStringToEmployee (string conInfo)
{
    string [] info = conInfo.Split(",");
    return new Employee(info[0],info[1],info[2],info[3]);
}

