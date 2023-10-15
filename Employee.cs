public class Employee
{
    protected List <int> IDS = new List<int>{};

    protected string name;
    protected int employeeID; 

    protected string title;
    protected DateTime startDate;
     Random random = new Random();

 



    public Employee (string Name)
    {

        name = Name;
        int ran = random.Next(0,9999);
        makeID();
        title = "Employee";
        startDate = DateTime.Now;
    }


    public Employee (string Name, string Title)

    {

        name = Name;
        makeID();
        if (String.IsNullOrWhiteSpace(title))
        {
            title = "Employee";
        } else
        {
        title = Title;}
        startDate = DateTime.Now;
    }

    public Employee (string Name, string Title, string ID, string Date)
    {
        name = Name;
        title = Title;
        employeeID = int.Parse(ID);
        startDate = DateTime.Parse(Date);
    }



    public string getDetails ()
    {
        string date = startDate.ToString("MM/dd/yyyy");
        return $"{name}, {title}\n   Employee ID: {employeeID}\n   Start Date: {date}\n";
    }
    

    public string getName ()
    {
        return name;
    }

    public void setName(string Name)
    {
        name = Name;
    }

    public string getTitle ()
    {
        return title;
    }

    public void setTitle (string Title)
    {
        title = Title;
    }

    public int getId ()
    {
        return employeeID;
    }

    public string getStartDate ()
    {
        return startDate.ToString("MM/dd/yyyy");
    }

    public void setStartDate (string Date)

    {
        if (!(DateTime.TryParse(Date, out startDate)))
        {
            Console.WriteLine("Error Setting Date\nInvalid Date Format");
        }
    }

    protected void makeID ()
    {
        bool exit = false;
        while (exit == false)
        {
            exit = true;
            int ran = random.Next(1000,9999);
             string stringID = ran.ToString();

            // string stringID = ($"{ran:0000}");


            // if (stringID.Count() <= 3)
            // {
            //     while (stringID.Count() <= 3)
            //     {
            //         stringID = "0" + stringID;
            //     }
            // }
            


            string [] employeeTxt = File.ReadAllLines("Employee-List.csv");
            List <string> IDS = new List<string>{};

            foreach (string id in employeeTxt)
            {
                string [] emp = id.Split(",");
                IDS.Add(emp[2]);
            }

            foreach (string id in IDS)
            {
                if (stringID == id)
                {
                    exit = false;
                }
            }
 
            if (exit == true)
            {
                employeeID = int.Parse(stringID);
            }


                

            



        }
        

    }


}

