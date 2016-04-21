using System;

namespace GreatInheritance
{
    internal class Person : IComparable, IComparable<Person>, IEquatable<Person>
    {
        #region Konstruktorer

        // Med hjälp av this pekas i samma klass ut den konstruktor som också
        // ska anropas. Detta gör att kod som sköter initieringen kan koncentreras
        // till en enda konstruktor; lämpligen den konstruktor som har flest parametrar.
        // (En enklare lösning vore att se till att konstruktorn med flest parametrar
        // även hade standarvärden, men detta exempel ska visa på användandet av this
        // i samband med konstruktorer.)
        public Person()
            : this("N") // Anropar den konstruktor som tar ett förnamn som argument.
        {
            // Tom!
        }

        public Person(string firstName)
            : this(firstName, "N") // Anropar den konstruktor som tar ett förnamn och efternamn som argument.
        {
            // Tom!
        }

        public Person(string firstName, string lastName)
            : this(firstName, lastName, "okänt!")
            // Anropar den konstruktor som tar ett förnamn, efternamn och personnummer som argument.
        {
            // Tom!
        }

        public Person(string firstName, string lastName, string crn)
        {
            FirstName = firstName;
            LastName = lastName;
            CivicRegistrationNumber = crn;
        }

        #endregion

        #region Egenskaper

        public string FirstName { get; set; }
        protected string LastName { get; set; }
        protected string CivicRegistrationNumber { get; set; }

        #endregion

        #region Implementering av interface

        // Resultat:
        //  < 0 : aktuell instans är mindre än specificerad instans
        //  = 0 : aktuell instans är lika med specificerad instans
        //  > 0 : aktuell instans är större än specificerad instans
        public virtual int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            var other = obj as Person;

            if (other == null)
            {
                throw new ArgumentException("Object is not a Person.");
            }

            return CompareTo(other);
        }

        // Resultat:
        //  < 0 : aktuell instans är mindre än specificerad instans
        //  = 0 : aktuell instans är lika med specificerad instans
        //  > 0 : aktuell instans är större än specificerad instans
        public int CompareTo(Person other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Equals(other))
            {
                return 0;
            }

            // Sorteringsordning: LastName -> FirstName -> CivicRegistrationNumber
            var result = string.Compare(LastName, other.LastName);
            if (result == 0)
            {
                result = string.Compare(FirstName, other.FirstName);
                if (result == 0)
                {
                    result = string.Compare(CivicRegistrationNumber, other.CivicRegistrationNumber);
                }
            }

            return result;
        }

        // Implementation av interfacet IEquatable<Person> och indikerar om aktuellt
        // objekt är lika med specificerat objekt.
        public bool Equals(Person other)
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

            return string.Equals(CivicRegistrationNumber, other.CivicRegistrationNumber) &&
                   string.Equals(LastName, other.LastName) &&
                   string.Equals(FirstName, other.FirstName);
        }

        #endregion

        #region Överskuggningar

        // Överskuggar metoden Equals i basklassen Object och returnerar
        // ett booleskt värde indikerande om aktuellt objekt är lika med
        // specificerat objekt.
        public override bool Equals(object obj)
        {
            return Equals(obj as Person);
        }

        // Överskuggar metoden GetHashCode i basklassen Object och returnerar
        // ett numeriskt värde av aktuellt objekt.
        // http://stackoverflow.com/a/263416/377476
        public override int GetHashCode()
        {
            unchecked // "overflow" är ok
            {
                var hash = (int) 2166136261;

                hash = (hash * 16777619) ^ FirstName?.GetHashCode() ?? 0;
                hash = (hash * 16777619) ^ LastName?.GetHashCode() ?? 0;
                hash = (hash * 16777619) ^ CivicRegistrationNumber?.GetHashCode() ?? 0;

                return hash;
            }
        }

        // Överskuggar metoden ToString i basklassen Object och returnerar 
        // en textbeskrivning av aktuellt objekt.
        public override string ToString() => $"{LastName}, {FirstName}, {CivicRegistrationNumber}";

        #endregion

        #region Operatoröverlagring

        // Överlagring av operatorn ==
        public static bool operator ==(Person p1, Person p2)
        {
            // Om både p1 och p2 är null returnera true, eller om p1 är null 
            // men inte p2 returnera false, annars jämför de två objekten.
            return p1?.Equals(p2) ?? ReferenceEquals(p2, null);
        }

        // Överlagring av operatorn !=
        public static bool operator !=(Person p1, Person p2)
        {
            return !(p1 == p2);
        }

        #endregion
    }
}