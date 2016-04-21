using System;
using System.Collections.Generic;

namespace GreatInheritance
{
    class Program
    {
        private static void Main(string[] args)
        {
            // Vilken/vilka konstruktorer körs?
            var myStudent = new Student();
            var anotherStudent = new Student("Ellen", "Nu", "100101001", 'G');

            // Du vet hur heltal i en array kan sorteras, men...
            int[] numbers = {21, 5, 7, 213, 3, -1};
            Array.Sort(numbers);

            foreach (var number in numbers)
            {
                Console.Write($"{number,4}");
            }
            Console.WriteLine("\n");

            // ...hur sortera referenser till objekt av typen Student?
            var students = new Student[4];

            students[0] = new Student("Mats", "Loock", "1234567890", 'U');
            students[2] = new Student("Ellen", "Nu", "1001010001", 'G');
            students[3] = new Student("Nisse", "Hult", "5603260123", 'U');

            Array.Sort(students); // Använder CompareTo(object obj)
            foreach (var student in students)
            {
                // Använder operatorn ?. tillsammans med ?? för att kunna skriva ut
                // strängen <null> då student refererar till null.
                Console.WriteLine(student?.ToString() ?? "<null>");
            }
            Console.WriteLine();


            var list = new List<Person>();

            list.Add(new Student("Mats", "Loock", "1234567890", 'U'));
            list.Add(null);
            list.Add(new Student("Ellen", "Nu", "1001010001", 'G'));
            list.Add(new Student("Nisse", "Hult", "5603260123", 'U'));

            list.Sort();
            foreach (var item in list)
            {
                // Använder operatorn ?. tillsammans med ?? för att kunna skriva ut
                // strängen <null> då student refererar till null.
                Console.WriteLine(item?.ToString() ?? "<null>");
            }
            Console.WriteLine();
        }
    }
}