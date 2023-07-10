using System.Text.Json;

namespace LINQ_in_Manhattan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            string jsonString = File.ReadAllText("../../../data.json");
            FeatureCollection data = JsonSerializer.Deserialize<FeatureCollection>(jsonString);
            //1st query
            var AllNeighborhoods = data.features
                .Select(x => x.properties.neighborhood);


            foreach (var x in AllNeighborhoods)
            {
                count++;
                Console.WriteLine(x);
            }
            Console.WriteLine("The result of the query for all neighborhoods is : " + count);
            count = 0;

            //2nd query
            var notNullNeighborhoods = data.features
                .Where(feature => !string.IsNullOrEmpty(feature.properties.neighborhood))
                .Select(x => x.properties.neighborhood);

            foreach (var x in notNullNeighborhoods)
            {
                count++;
                Console.WriteLine(x);
            }
            Console.WriteLine("The result of the query for all neighborhoods that have a name is : " + count);
            count = 0;

            //3nd query & 4th query
            var distinctNeighborhoodsNotNull = data.features
                .Where(feature => !string.IsNullOrEmpty(feature.properties.neighborhood))
                .Select(x => x.properties.neighborhood)
                .Distinct();
            ;


            foreach (var x in distinctNeighborhoodsNotNull)
            {
                count++;
                Console.WriteLine(x);
            }
            Console.WriteLine("The result of the query for all neighborhoods with no duplicates : " + count);
            count = 0;

            //5th query
            var reWrite = from feature in data.features
                          select feature.properties.neighborhood;

            foreach (var x in reWrite)
            {
                count++;
                Console.WriteLine(x);
            }
            Console.WriteLine("The result of the query for all neighborhoods written in query syntax is : " + count);
        }
    }

    public class FeatureCollection
    {
        public string type { get; set; }
        public List<Feature> features { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Properties
    {
        public string zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string address { get; set; }
        public string borough { get; set; }
        public string neighborhood { get; set; }
        public string county { get; set; }
    }



}