using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class DependencyInversion
    {
        public enum Relationship
        {
            Parent,
            Child,
            Sibling
        }

        public class Person
        {
            public string Name;

            public Person(string name)
            {
                Name = name;
            }
        }

        public interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        public class Relationships : IRelationshipBrowser //low-level module
        {            
            private List<ValueTuple<Person, Relationship, Person>> relations = new List<ValueTuple<Person, Relationship, Person>>();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add(new ValueTuple<Person, Relationship, Person> { Item1 = parent, Item2 = Relationship.Parent, Item3 = child });
                relations.Add(new ValueTuple<Person, Relationship, Person> { Item1 = child, Item2 = Relationship.Child, Item3 = parent });
            }

            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                return relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent).Select(c => c.Item3);
            }
        }

        public class Research //high-level module
        {
            public Research(IRelationshipBrowser relationshipBrowser)
            {
                foreach(var p in relationshipBrowser.FindAllChildrenOf("John"))
                {
                    Console.WriteLine("John has a child called : {0}", p.Name);
                }                 
            }
            static void Main(string[] args)
            {
                var parent = new Person("John");
                var child1 = new Person("Chris");
                var child2 = new Person("Mary");
                
                var relationships = new Relationships();
                relationships.AddParentAndChild(parent, child1);
                relationships.AddParentAndChild(parent, child2);

                new Research(relationships);
                Console.ReadKey();
            }
        }
    }
}
