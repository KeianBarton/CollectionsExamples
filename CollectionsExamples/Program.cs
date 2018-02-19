using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace CollectionsExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static void ArrayExample()
        {
            string[] daysOfWeek =
            {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };
            object[] implicitConversion = daysOfWeek;
            string tuesday = daysOfWeek[1];
            daysOfWeek[5] = "PartyDay";
            foreach (string day in daysOfWeek)
                Console.WriteLine(day);
            for(var i=0; i<daysOfWeek.Length; i++)
                Console.WriteLine(daysOfWeek[i]);

            int x1;
            int[] x2;
            int x3 = 5;
            int[] x4 = new int[5];
            int[] x5 = new int[1] { 1+1 };

            int[] x6 = new int[] { 1, 2 };
            var x7 = new int[2] { 1, 2 };
            int[] x8 = { 1, 2 };
            var x9 = x8;
            ReferenceEquals(x8, x9);

            int[] arrayToCopyTo = new int[4];
            daysOfWeek.CopyTo(arrayToCopyTo, 0);
            //Array.Copy(...);

            ICollection<string> collection =
                (ICollection<string>)daysOfWeek;
            var isReadOnly = daysOfWeek.IsReadOnly; // true
            (collection as string[])[5] = "SlaveDay";

            int[] x10 = { 1, 2, 3 };
            // x10.Add(4); Will not compile
            ((ICollection<int>)x10).Add(4); // Will compile but will throw error
        }

        static void ListExample()
        {
            var presidents = new List<string>
            {
                "Bill Clinton",
                "George W Bush"
            };
            var capacity = presidents.Capacity;
            var specificCapacityList = new List<string>(1000000000);
            var foo1 = specificCapacityList.AsReadOnly();
            IReadOnlyCollection<string> foo2 =
                new ReadOnlyCollection<string>(specificCapacityList);
        }

        static void CollectionExample()
        {
            var presidents = new Collection<string>
            {
                "Bill Clinton",
                "George W Bush"
            };
            var bill = presidents[0];
        }

        static void ObservableCollection()
        {
            ObservableCollection<string> presidents = new
                ObservableCollection<string>
            {
                "Ronald Reagan",
                "George HW Bush"
            };
            presidents.CollectionChanged += OnCollectionChanged;

            presidents.Add("Bill Clinton");
            presidents.Remove("Ronald Reagan");
        }

        static void OnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
        }

        static void LinkedListExample()
        {
            var presidents = new LinkedList<string>();
            presidents.AddLast("Richard Nixon");
            presidents.AddLast("Jimmy Carter");
            LinkedListNode<string> element = presidents.Find("Richard Nixon");
            presidents.AddAfter(element, "Gerald Ford");
        }

        static void StackExample()
        {
            Stack<string> books = new Stack<string>();
            books.Push("First book");
            books.Push("Second book");
            books.Push("Top of the pile book");
            var topItem = books.Pop();
        }

        static void QueueExample()
        {
            Queue<string> books = new Queue<string>();
            books.Enqueue("First book");
            books.Enqueue("Second book");
            books.Enqueue("Top of the pile book");
            var bottomItem = books.Dequeue();
        }

        static void DictionaryExample()
        {
            var pms = new Dictionary<string, PrimeMinister>
            {
                { "MT", new PrimeMinister("Margaret Thatcher", 1979) },
                { "TB", new PrimeMinister("Tony Blair", 1997) }
            };
            foreach (var pm in pms.Values) // or pms.Keys
                Console.WriteLine(pm);
            foreach (var pm in pms)
                Console.WriteLine(pm.Key + ", " + pm.Value);

            var lookup1 = pms["MT"];
            var isFound = pms.TryGetValue("DC", out var lookup2);
            pms["MT"] = new PrimeMinister("Maggie Thatcher", 1979);
            pms.Add("GB", new PrimeMinister("Gordon Brown", 2007 ));

            var primeMinisters = new Dictionary<string, PrimeMinister>
                (StringComparer.InvariantCultureIgnoreCase)
            {
                { "MT", new PrimeMinister("Margaret Thatcher", 1979) },
                { "TB", new PrimeMinister("Tony Blair", 1997) }
            };

            var pmsReadOnly = new ReadOnlyDictionary
                <string, PrimeMinister>(primeMinisters);

            var pmsSorted = new SortedList<string, PrimeMinister>
            (new UncasedStringComparer())
            {
                { "MT", new PrimeMinister("Margaret Thatcher", 1979) }
            };

            var pmsUsingKeyedCollections = new PrimeMinstersByYearDictionary()
            {
                new PrimeMinister("Margaret Thatcher", 1979)
            };
        }

        static void SetExamples()
        {
            var bigCities = new HashSet<string>
                (StringComparer.InvariantCultureIgnoreCase)
            { "New York", "Manchester", "Paris" };
            var isAdded = bigCities.Add("PARIS");


            var cities1 = new HashSet<string>{ "New York", "Manchester" };
            string[] cities2 = { "Manchester", "Sheffield" };
            var cities3 = cities1.Intersect(cities2); // LINQ intersection creates a new collection
            cities1.IntersectWith(cities2); // Manchester

            // Union
            cities1.UnionWith(cities2);
            // Symmetric difference
            // Values from either collection, but not in both
            cities1.SymmetricExceptWith(cities2);
            // Difference - every element in 1st set, but not in 2nd
            cities1.ExceptWith(cities2);

            cities1.SetEquals(cities2);

            var sortedCities = new SortedSet<string>
            (StringComparer.InvariantCultureIgnoreCase)
            { "Foo", "Bla" };
        }

        static void EnumeratorExample<T>(IEnumerable<T> collection)
        {
            using (IEnumerator<T> enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    T item = enumerator.Current;
                    Console.WriteLine(item);
                    // enumerator.Reset() - can be used to return to start of collection
                }
            }
        }

        static void EnumeratingWhileChanging()
        {
            var days = new List<string>
            {
                "Monday",
                "Tuesday",
                "Wednesday"
            };
            try
            {
                foreach(var day in days)
                {
                    days[1] = "Newday";
                    Console.WriteLine(day);
                }
            }
            catch(InvalidOperationException)
            {
                Console.WriteLine("foo");
            }
        }

        static void EnumerableCovarianceExample()
        {
            // We can cast any derived type to its base type
            string str = "Foo";
            object obj = str;

            var strList = new List<string> { "Monday", "Tuesday" };
            //List<object> implicitConversion = strList;
            //var explicitConversion = (List<object>) strList;

            IEnumerable<object> objEnumerable = strList;
        }

        static void MultidimensionalArraysExample()
        {
            double[,] heights = new double[50, 100];
            // double[] lengths = heights;

            int[] oneDim = new int[10];
            //int[,] twoDim = oneDim;

            float[,] tempsGrid = new float[4, 3];

            for (int x = tempsGrid.GetLowerBound(0);
                x < tempsGrid.GetUpperBound(0); x++)
            {
                for (int y = tempsGrid.GetLowerBound(1);
                    y <= tempsGrid.GetUpperBound(1); y++)
                {
                    tempsGrid[x, y] = x + 10 * y;
                }
            }
            foreach(float temperature in tempsGrid)
                Console.WriteLine(temperature); // will lose grid structure

            Console.WriteLine((new string[2, 3]).Length); // displays 6
            Console.WriteLine((new string[4, 3]).GetLength(0)); // displays 4
            Console.WriteLine((new string[2, 1]).Rank); // displays 2
        }

        static void BoundsExamples()
        {
            var multiDimArray = new string[4, 3];
            multiDimArray.GetLowerBound(0); // 0
            multiDimArray.GetUpperBound(1); // 3
        }

        static void JaggedArraysExamples()
        {
            float[][] tempsGrid = new float[3][]
            {
                new float[3],
                new float[2],
                new float[4]
            };

            float[][] tempsGrid2 = new float[3][];
            Console.WriteLine(tempsGrid2.Rank);   // 1
            Console.WriteLine(tempsGrid2.Length); // 3
        }
    }
}
