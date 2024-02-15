using System; //not necessary it seems?

namespace Classes
{

    public class App
    {      
        public static void Main(){

             var p1 = new Person("Scotty", "Beam", new DateOnly(1970, 1, 1));

             var p2 = new Person("Ana", "Stasia", new DateOnly(1990, 1, 1));

             var people = new List<Person> {p1, p2};

             p1.Pets.Add(new Dog("Good Boy", "Woof")); // since Person has a list of pets, we can use .Add
             p1.Pets.Add(new Cat("CatCat", "Mewmew"));
             p2.Pets.Add(new Dog("Dogggggy", "bork"));

             foreach (var person in people)
             {
                foreach(var pet in person.Pets) //Pets is the name of the list of pets; probably not a great idea naming it that way
                {
                    Console.WriteLine($"{person.FullName} has a {pet}"); // pet.ToString() gave out the non-overriden version, why?
                }
             }

             Console.WriteLine(people.Count);
             Console.WriteLine(p1);


             var c1 = new Cat("Muffin", "Meow!");
             var d1 = new Dog("Mutty", "Bark!");

             Console.WriteLine(c1.Noise());
             Console.WriteLine(d1.Noise());
             Console.WriteLine(c1.ToString()); // normal, inherited ToString from Pets
             Console.WriteLine(d1.ToString('v')); //goes to the verbose override version
        }
    }
    public class Person // only need 'public Person(string firstName, string lastName, DateOnly birthday) in C# 12'
        {
            public Person(string firstName, string lastName, DateOnly birthday){
                _firstName = firstName;
                _lastName = lastName;
                _birthday = birthday; //DateOnly itself is a struct
                _fullName = $"{this._firstName} {this._lastName}";
            }

            public List<Pets> Pets { get;} = new();
            private string _firstName;
            private string _lastName;
            private DateOnly _birthday;
            private readonly string _fullName;

            public string FirstName {get => _firstName; set => _firstName = value;}
            public string LastName {get => _lastName; set => _lastName = value;}
            public DateOnly Birthday {get => _birthday; set => _birthday = value;}
            public string FullName {get => _fullName;}

            public override string ToString(){ //if this was for inheritance only we'd use 'virtual'
                return $"{_fullName}'s birthday is on the {Birthday}."; 
            }
        }
    public abstract class Pets // this will be the parent class for Dogs and Cats, so it's 'abstract'
    {
        public Pets (string name, string noise)
        {
            _name = name;
            _noise = noise;
        }
        protected string _name; //protected so the subclasses can use these properties
        protected readonly string _noise;

         public string Noise()  => this._noise; //short version 
        //public string Noise() {return this._noise;}   //longer version of 

        public override string ToString()
        {
            return $@"{_name} is a {GetType().Name} that can make ""{_noise}"" noises."; //@ here so we can use quotation marks inside of quotation marks
        }
    }
    public class Cat : Pets
    {
        public Cat(string name, string noise) : base (name, noise){}
        

    }
    public class Dog : Pets
    {
        public Dog (string name, string noise) : base (name, noise){}

        public string ToString(char ch) // 'overload' with extra parameter
        {
            if (char.ToLower(ch) == 'v')
            {
                return $"This is the verbose print of the Dog class. The name of the dog is {_name}";
            } else return base.ToString();
        }
    }
}