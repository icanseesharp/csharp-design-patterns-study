using System;
using System.Collections.Generic;

namespace DesignPatterns
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Yuge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            Name = name ?? "";
            Color = color;
            Size = size;
        }

    }

    public interface Ispecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, Ispecification<T> spec);
    }

    public class ColorSpecification : Ispecification<Product>
    {
        private Color Color;
        public ColorSpecification(Color color)
        {
            this.Color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == this.Color;
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, Ispecification<Product> spec)
        {
            foreach (var p in items)
            {
                if (spec.IsSatisfied(p)) yield return p;
            }
        }
    }


    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> Products, Size Size)
        {
            foreach (var product in Products)
            {
                if (product.Size == Size) yield return product;
            }
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> Products, Color Color)
        {
            foreach (var product in Products)
            {
                if (product.Color == Color) yield return product;
            }
        }

        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> Products, Size Size, Color Color)
        {
            foreach (var product in Products)
            {
                if (product.Size == Size && product.Color == Color) yield return product;
            }
        }
    }

    public class OpenClosePrinciple
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Medium);
            var house = new Product("House", Color.Blue, Size.Large);
            Product[] products = { apple, tree, house };

            var pf = new ProductFilter();
            Console.WriteLine("Green Products by Old Method (filter implemented in the class) : \n");
            var smallProducts = pf.FilterBySize(products, Size.Small);
            var greenProducts = pf.FilterByColor(products, Color.Green);
            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine(string.Format("{0} is Green", p.Name));
            }
            Console.WriteLine("\n--------------------------------\n");
            Console.WriteLine(@"Green Products by New Method (filter implemented as a specification)");

            var bf = new BetterFilter();
            var colorSpec = new ColorSpecification(Color.Green);
            foreach (var p in bf.Filter(products, colorSpec))
            {
                Console.WriteLine(string.Format("{0} is Green", p.Name));
            }

            Console.ReadKey();
        }
    }
}
