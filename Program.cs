using System;

namespace BT_NET
{
    class Program
    {
        static void Main(string[] args)
        {
            QLSV db = new QLSV();
            int choose;
            bool flash = true;
            do {
                Menu_Board();
                Console.Write("\tChon: ");
                int.TryParse(Console.ReadLine(), out choose);
                switch(choose) {
                    case 0:
                        flash = false;
                        break;
                    case 1:
                        db.AddStudents();
                        break;
                    case 2:
                        db.ShowStudents();
                        break;
                    case 3: 
                        db.ModifyStudents();
                        break;
                    case 4:
                        db.DeleteStudents();
                        break;
                    case 5:
                        db.sortStudents();
                        break;
                    default:
                        Console.WriteLine("\tError: unknown solution");
                        break;
                }
            } while (flash);
        }
        
        static void Menu_Board() {
            Console.WriteLine("\t\t Menu board....");
            Console.WriteLine("\t\t1. Add students");
            Console.WriteLine("\t\t2. Show List students");
            Console.WriteLine("\t\t3. Modify students Infomation");
            Console.WriteLine("\t\t4. Delete students");
            Console.WriteLine("\t\t5. Sort list students");
            Console.WriteLine("\t\t0. Exit(0)");
        }
    }
}
