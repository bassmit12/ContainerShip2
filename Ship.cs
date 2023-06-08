using System;
using System.Collections.Generic;

namespace ContainerShip2
{
    public class Ship
    {
        public int width;
        public int length;
        public List<Layer> layers;

        private Balancer balancer;

        public Ship(int width, int length)
        {
            this.width = width;
            this.length = length;

            layers = new List<Layer>();

            balancer = new Balancer(this);
        }

        public void SortAndPlaceContainers(List<Container> containers)
        {
            List<Container> normalNormalContainers = containers
                .Where(container => container.Temperature == ContainerTemperature.Normal && container.Type == ContainerType.Normal)
                .OrderByDescending(container => container.Weight)
                .ToList();

            List<Container> coldNormalContainers = containers
                .Where(container => container.Temperature == ContainerTemperature.Cold && container.Type == ContainerType.Normal)
                .OrderByDescending(container => container.Weight)
                .ToList();

            List<Container> normalValuableContainers = containers
                .Where(container => container.Temperature == ContainerTemperature.Normal && container.Type == ContainerType.Valuable)
                .OrderByDescending(container => container.Weight)
                .ToList();

            List<Container> coldValuableContainers = containers
                .Where(container => container.Temperature == ContainerTemperature.Cold && container.Type == ContainerType.Valuable)
                .OrderByDescending(container => container.Weight)
                .ToList();

            PlaceColdContainers(coldNormalContainers);
            PlaceColdContainers(coldValuableContainers);

            PlaceNormalContainers(normalNormalContainers);
            PlaceNormalContainers(normalValuableContainers);
        }

        public void PlaceColdContainers(List<Container> containers)
        {
            foreach (Container container in containers)
            {
                bool placed = false;

                foreach (Layer stack in layers)
                {
                    if (stack.TryPlaceColdContainer(container, layers))
                    {
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    Layer newStack = new Layer(width, length);
                    newStack.TryPlaceColdContainer(container, layers);
                    layers.Add(newStack);
                }
            }
        }

        public void PlaceNormalContainers(List<Container> containers)
        {
            foreach (Container container in containers)
            {
                bool placed = false;

                foreach (Layer stack in layers)
                {
                    if (stack.TryPlaceNormalContainer(container, layers))
                    {
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    Layer newStack = new Layer(width, length);
                    newStack.TryPlaceNormalContainer(container, layers);
                    layers.Add(newStack);
                }
            }
        }


        public int CalculateTotalWeight()
        {
            int totalWeight = 0;

            foreach (Layer stack in layers)
            {
                for (int y = 0; y < stack.length; y++)
                {
                    for (int x = 0; x < stack.width; x++)
                    {
                        if (stack.layout[x, y] != null)
                        {
                            totalWeight += stack.layout[x, y].Sum(container => container.Weight);
                        }
                    }
                }
            }

            return totalWeight;
        }

        public void printInfo()
        {
            PrintLayout();
            PrintSummary();
            PrintSideWeights();
            PrintBalanceStatus();
        }



        public void PrintLayout()
        {
            Console.WriteLine("");
            Console.WriteLine("Layout with containers:");

            Dictionary<(int, int), int> positionWeights = new Dictionary<(int, int), int>();

            for (int i = 0; i < layers.Count; i++)
            {
                Console.WriteLine($"Stack {i + 1}:");
                layers[i].PrintStack();

                for (int y = 0; y < layers[i].length; y++)
                {
                    for (int x = 0; x < layers[i].width; x++)
                    {
                        if (!positionWeights.ContainsKey((x, y)))
                        {
                            int totalWeight = layers[i].GetWeightAtPosition(x, y, layers);
                            positionWeights.Add((x, y), totalWeight);
                        }
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("Container loads:");

            foreach (var positionWeight in positionWeights)
            {
                int x = positionWeight.Key.Item1 + 1;
                int y = positionWeight.Key.Item2 + 1;
                Console.WriteLine($"Position ({x}, {y}) - Total weight: {positionWeight.Value}");
            }
        }

        public void PrintSummary()
        {
            int totalWeight = CalculateTotalWeight();
            int maxWeight = length * width * 120000;
            double percentage = (double)totalWeight / maxWeight * 100;
            Console.WriteLine("");
            Console.WriteLine($"Total weight of all containers: {totalWeight} kg");
            Console.WriteLine($"Percentage of max weight used: {percentage:F0}%");

        }

        public void PrintSideWeights()
        {
            int middleLaneStartCol = width / 2;

            if (width % 2 == 0) // Even number of columns
            {
                int leftWeight = 0;
                int rightWeight = 0;
                int totalWeight = 0;

                for (int i = 0; i < layers.Count; i++)
                {
                    for (int y = 0; y < layers[i].length; y++)
                    {
                        for (int x = 0; x < layers[i].width; x++)
                        {
                            if (layers[i].layout[x, y] != null)
                            {
                                if (x < middleLaneStartCol)
                                {
                                    leftWeight += layers[i].layout[x, y].Sum(container => container.Weight);
                                }
                                else
                                {
                                    rightWeight += layers[i].layout[x, y].Sum(container => container.Weight);
                                }

                                totalWeight += layers[i].layout[x, y].Sum(container => container.Weight);
                            }
                        }
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("Side Weights:");
                Console.WriteLine($"Left side weight: {leftWeight} kg ({((double)leftWeight / totalWeight) * 100:F0}% of total weight)");
                Console.WriteLine($"Right side weight: {rightWeight} kg ({((double)rightWeight / totalWeight) * 100:F0}% of total weight)");
            }
            else // Odd number of columns
            {
                int leftWeight = 0;
                int middleWeight = 0;
                int rightWeight = 0;
                int totalWeight = 0;

                for (int i = 0; i < layers.Count; i++)
                {
                    for (int y = 0; y < layers[i].length; y++)
                    {
                        for (int x = 0; x < layers[i].width; x++)
                        {
                            if (layers[i].layout[x, y] != null)
                            {
                                if (x < middleLaneStartCol)
                                {
                                    leftWeight += layers[i].layout[x, y].Sum(container => container.Weight);
                                }
                                else if (x > middleLaneStartCol)
                                {
                                    rightWeight += layers[i].layout[x, y].Sum(container => container.Weight);
                                }
                                else
                                {
                                    middleWeight += layers[i].layout[x, y].Sum(container => container.Weight);
                                }

                                totalWeight += layers[i].layout[x, y].Sum(container => container.Weight);
                            }
                        }
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("Side Weights:");
                Console.WriteLine($"Left side weight: {leftWeight} kg ({((double)leftWeight / totalWeight) * 100:F0}% of total weight)");
                Console.WriteLine($"Middle side weight: {middleWeight} kg ({((double)middleWeight / totalWeight) * 100:F0}% of total weight)");
                Console.WriteLine($"Right side weight: {rightWeight} kg ({((double)rightWeight / totalWeight) * 100:F0}% of total weight)");
            }
        }

        public void PrintBalanceStatus()
        {
            bool isBalanced = balancer.IsShipInBalance();

            Console.WriteLine("");
            Console.WriteLine("Balance Status:");
            Console.WriteLine(isBalanced ? "The ship is in balance." : "The ship is not in balance.");
        }

    }
}