using System;
using System.Diagnostics;

namespace GreatInheritance
{
    class Student : Person, IComparable, IComparable<Student>, IEquatable<Student>
    {
        #region Konstruktorer

        // Innan denna konstruktors kropp körs så
        // körs "default"-konstruktorn i basklassen Person.
        public Student()
        {
            Grade = '-';
        }

        // Innan denna konstruktors kropp körs så
        // körs den konstruktor i basklassen Person som har tre parametrar.
        public Student(string firstName, string lastName, string crn, char grade)
            : base(firstName, lastName, crn)
        {
            Grade = grade;
        }

        #endregion

        #region Egenskaper

        public char Grade { get; set; }

        #endregion

        #region IComparable

        // Denna metod används av ramverket för att kunna sortera referenser.
        public override int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            var other = obj as Student;

            if (other == null)
            {
                throw new ArgumentException("Object is not a Student.");
            }

            return CompareTo(other);
        }

        #endregion

        #region IComparable<Student>

        // Resultat:
        //  < 0 : aktuell instans är mindre än specificerad instans
        //  = 0 : aktuell instans är lika med specificerad instans
        //  > 0 : aktuell instans är större än specificerad instans
        public int CompareTo(Student other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Equals(other))
            {
                return 0;
            }

            // Sorteringsordning: LastName -> FirstName -> CivicRegistrationNumber -> Grade
            var result = base.CompareTo(other);
            if (result == 0)
            {
                result = Grade.CompareTo(other.Grade);
            }

            return result;
        }

        #endregion

        #region IEquatable<Student>

        // Implementation av interfacet IEquatable<Student> och indikerar om aktuellt
        // objekt är lika med specificerat objekt.
        public bool Equals(Student other)
        {
            if (other == null || GetType() != other.GetType())
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetHashCode() != other.GetHashCode())
            {
                return false;
            }

            return base.Equals(other) &&
                   Grade.Equals(other.Grade);
        }

        #endregion

        #region Överskuggningar

        // Överskuggar metoden Equals i basklassen Person och returnerar
        // ett booleskt värde indikerande om aktuellt objekt är lika med
        // specificerat objekt.
        public override bool Equals(object obj)
        {
            return Equals(obj as Student);
        }

        // Överskuggar metoden GetHashCode i basklassen Person och returnerar
        // ett numeriskt värde av aktuellt objekt.
        // http://stackoverflow.com/a/263416/377476
        public override int GetHashCode()
        {
            unchecked // "overflow" är ok
            {
                var hash = (int)2166136261;

                hash = (hash * 16777619) ^ base.GetHashCode();
                hash = (hash * 16777619) ^ Grade.GetHashCode();

                return hash;
            }
        }

        // Överskuggar metoden ToString i basklassen Person och
        // returnerar en textbeskrivning av aktuellt objekt.
        public override string ToString() => string.Join(", ", LastName, FirstName, CivicRegistrationNumber, Grade);

        #endregion

        #region Operatoröverlagring

        // Överlagring av operatorn ==
        public static bool operator ==(Student s1, Student s2)
        {
            // Om både p1 och p2 är null returnera true, eller om p1 är null 
            // men inte p2 returnera false, annars jämför de två objekten.
            return s1?.Equals(s2) ?? ReferenceEquals(s2, null);
        }

        // Överlagring av operatorn !=
        public static bool operator !=(Student s1, Student s2)
        {
            return !(s1 == s2);
        }

        #endregion
    }
}