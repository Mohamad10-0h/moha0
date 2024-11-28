using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Hospital Management System");

        Console.Write("Enter username: "); // تسجيل الدخول للمستخدم
        string username = Console.ReadLine();
       
        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        User loggedInUser = Database.Users.Find(u => u.Username == username && u.Password == password);

        if (loggedInUser != null) // تحقق مما إذا كان المستخدم موجود في قاعدة البيانات
        {          // تحديد القسم الذي ينتمي إليه المستخدم  
            switch (loggedInUser.Department.ToLower()) 
            {
                case "hr":
                    HRService hr = new HRService();
                    hr.ReceiveComplaint(new Complaint("This is a test complaint.", loggedInUser.Username));
                    break;
                case "accounting":
                    AccountingService accounting = new AccountingService();
                    accounting.GenerateFinancialReport();
                    break;
                case "reception":
                    ReceptionService reception = new ReceptionService();
                    Patient newPatient = new Patient("John Doe", 30, "Flu");
                    reception.RegisterPatient(newPatient);
                    reception.DischargePatient(newPatient);
                    break;
                case "admin":
                    AdministrationService admin = new AdministrationService();
                    admin.SendInstructions("Please review the new policies.");
                    break;
                default:
                    Console.WriteLine("Invalid department."); // في حالة عدم تطابق القسم مع اي قسم من الاقسام المعروفة
                    break;
            }
        }
        else // في حالة عدم وجود المستخدم
        {
            Console.WriteLine("Invalid username or password.");
        }
        Console.WriteLine("Forgot your password? Type 'yes' to reset."); // خيار نسيان كلمة المرور
        string resetResponse = Console.ReadLine();
       
        if (resetResponse.ToLower() == "yes")
        {
            Console.Write("Enter your username to reset password: ");
            string resetUsername = Console.ReadLine();
            Console.WriteLine($"Password reset link sent to {resetUsername}"); // طباعة رسالة تفيد بإرسال رابط إعادة تعيين كلمة المرور
        }
       
        Console.WriteLine("Exiting the application...");
    }
}