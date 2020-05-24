using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace QuickTest
{
    class Program
    {
        public class ChartData
        {
            public string label;
            public int value;

            public ChartData(string l, int v)
            {
                label = l;
                value = v;
            }
        }

        public static void Main()
        {

        }
    }
}
