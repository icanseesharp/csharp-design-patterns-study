using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class FacetedBuilder
    {
        public class Person
        {
            public string Name { get; set; }
            public string CompanyName { get; set; }
            public string Job { get; set; }
            public int AnnualIncome { get; set; }

            public override string ToString()
            {                
                return string.Format("Name : {0}\tCompany : {1}\tJob : {2}\t", Name, CompanyName, Job).ToString();
            }
        }

        public class PersonBuilder //facade - to keep references
        {
            //reference!
            protected Person person = new Person();
            public PersonJobBuilder Works => new PersonJobBuilder(person);

            public static implicit operator Person (PersonBuilder pb)
            {
                return pb.person;
            }
        }

        public class PersonJobBuilder : PersonBuilder
        {
            public PersonJobBuilder(Person person)
            {
                this.person = person;
            }

            public PersonJobBuilder Called(string Name)
            {
                person.Name = Name;
                return this;
            }

            public PersonJobBuilder At(string companyName)
            {
                person.CompanyName = companyName;
                return this;
            }

            public PersonJobBuilder As(string designation)
            {
                person.Job = designation;
                return this;
            }

            public PersonJobBuilder Earning(int amount)
            {
                person.AnnualIncome = amount;
                return this;
            }
        } //builder

        //public static void Main()
        //{
        //    Person pb = new PersonBuilder().Works.Called("Sagar").At("Digitas").As("Associate").Earning(0);
        //    Console.WriteLine(pb.ToString());
        //    Console.ReadKey();
        //}
    }
}
