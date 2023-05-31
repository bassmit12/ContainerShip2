using ContainerShip2;

namespace ContainerShip2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Ship ship = new Ship(4, 3);

            // Create a list of containers
            List<Container> containers = new List<Container>();

            for (int i = 0; i < 20; i++)
            {
                containers.Add(new Container { Type = ContainerType.Normal, Temperature = ContainerTemperature.Normal });
            }

            for (int i = 0; i < 7; i++)
            {
                containers.Add(new Container { Type = ContainerType.Normal, Temperature = ContainerTemperature.Cold });
            }

            for (int i = 0; i < 7; i++)
            {
                containers.Add(new Container { Type = ContainerType.Valuable, Temperature = ContainerTemperature.Cold });
            }

            for (int i = 0; i < 1; i++)
            {
                containers.Add(new Container { Type = ContainerType.Valuable, Temperature = ContainerTemperature.Normal });
            }

            List<Container> coldContainers = containers
                .Where(container => container.Temperature == ContainerTemperature.Cold)
                .OrderByDescending(container => container.Weight)
                .ToList();

            List<Container> normalContainers = containers
                .Where(container => container.Temperature != ContainerTemperature.Cold)
                .OrderByDescending(container => container.Weight)
                .ToList();

            ship.PlaceColdContainers(coldContainers);
            ship.PlaceNormalContainers(normalContainers);
            ship.PrintLayout();
            // Calculate the weight for each position on the lowest stack

        }
    }
}
