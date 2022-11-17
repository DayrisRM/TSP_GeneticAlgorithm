using TSP_Problem.Abstractions;
using TSP_Problem_Common.Models;

namespace TSP_Problem.Services
{
    public class LoadFileCitiesService: ILoadFileService<List<City>>
    {      

        public List<City> LoadFile(string nameFile)
        {
            var cities = new List<City>();

            var pathFile = @"../../../Data/" + nameFile;
            using (var reader = new StreamReader(pathFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(' ');

                    var id = int.Parse(values[0]);
                    var x = int.Parse(values[1]);
                    var y = int.Parse(values[2]);

                    if (id > 0)
                    {
                        var city = new City()
                        {
                            Id = id,
                            X = x,
                            Y = y
                        };

                        cities.Add(city);
                    }

                }
            }

            return cities;
        }
    }
}
