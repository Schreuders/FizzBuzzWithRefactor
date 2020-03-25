using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace fizzBuzzWithRefactor
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Catalog();
            options.Add("NumToCompute % 3 = 0", "fizz");
            options.Add("NumToCompute % 5 = 0", "buzz");
            options.Add("Convert(NumToCompute, 'System.String') LIKE '*3*'", "Fizz");
            options.Add("Convert(NumToCompute, 'System.String') LIKE '*5*'", "Buzz");

            var item = new Counter(1, 100, options);
            item.Output();
        }
    }

    internal class Counter
    {
        private int min;
        private int max;
        private Catalog catalog;

        internal Counter(int min, int max, Catalog catalog)
        {
            this.min = min;
            this.max = max;
            this.catalog = catalog;
        }

        internal void Output()
        {
            for (int i = min; i <= max; i++)
            {
                Console.WriteLine(catalog.GetString(i));
            }
        }
    }

    internal class Catalog
    {
        private List<Spec> specs;


        internal class Spec
        {
            internal string Test;
            internal string Output;
        }

        internal Catalog()
        {
            specs = new List<Spec>();
        }

        internal void Add(string test, string Output)
        {
            specs.Add(new Spec() { Test = test, Output = Output });
        }

        internal string GetString(int Number)
        {
            var builder = new StringBuilder();
            foreach (var x in specs)
            {
                builder.Append(ComputeTest(Number, x.Test) ? x.Output : "");
            }
            string outputString = builder.ToString();

            return String.IsNullOrWhiteSpace(outputString) ? Number.ToString() : outputString;
        }

        internal bool ComputeTest(int Number, string Test)
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn();
            dc1.DataType = typeof(int);
            dc1.ColumnName = "NumToCompute";
            dc1.DefaultValue = Number;
            dt.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn("Eval", typeof (bool), Test);
            
            dt.Columns.Add(dc2);
            DataRow rd = dt.NewRow();
            dt.Rows.Add(rd);

            return (bool) (dt.Rows[0]["Eval"]);
        }
    }
}
