namespace RecordVsStruct
{
    internal class Program
    {
        public record Person(string FirstName, string LastName, string surname);     // positional notation
        public record Citizen : Person, ICitizen                                               // regular notation with Inheritance
        {
            public Citizen(string City, Person person): base(person)
            {
                this.City = City;
            }
            public Citizen(string City, string FirstName, string LastName, string surname) : base(FirstName, LastName, surname)
            {
                this.City = City;
            }

            public string City;

            string ICitizen.City { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }

        public interface ICitizen
        {
            public string City { get; set; }
        }

        record Team_R(string Name, List<string> Members);
        record struct Team_RS(string Name, List<string> Members);


        static void Main(string[] args)
        {
            var person1 = new Person("John", "Josh", "Smith");
            var person2 = person1 with { FirstName = "Jim"};                // Creating a new instance based on an existing one

            var citizen1 = new Citizen("London", "John", "Josh", "Smith");
            var citizen2 = new Citizen("London", person2);

            //A record declared using positional notation cannot be reassigned to properties, but they can be changed through special methods.
            var members = new List<string> { "Monika", "Joe", "Ross" };
            var team_R = new Team_R("Tigers", members);
            //team_R.Members = new List<string> { "Chandler", "Phoebe" };     // compiler error
                                                                            // (Error CS8852 Init-only property or indexer 'Team.Members' can only be assigned in an
                                                                            // object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor)
            team_R.Members.Add("Chandler");                                 // no error

            var team_RS = new Team_RS("Tigers", members);
            team_RS.Members = new List<string> { "Chandler", "Phoebe" };    // no error

            Console.WriteLine(person1);                                     // Person { FirstName = John, LastName = Josh, surname = Smith }
            Console.WriteLine(citizen2);
        }
    }
}