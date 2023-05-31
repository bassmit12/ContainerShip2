using System;
using System.Collections.Generic;

namespace ContainerShip2
{
    public class Ship
    {
        private int width;
        private int length;
        public List<Stack> stacks;

        public Ship(int width, int length)
        {
            this.width = width;
            this.length = length;
            stacks = new List<Stack>();
        }

        public void PlaceColdContainers(List<Container> containers)
        {
            foreach (Container container in containers)
            {
                bool placed = false;

                foreach (Stack stack in stacks)
                {
                    if (stack.TryPlaceColdContainer(container))
                    {
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    Stack newStack = new Stack(width, length);
                    newStack.TryPlaceColdContainer(container);
                    stacks.Add(newStack);
                }
            }
        }

        public void PlaceNormalContainers(List<Container> containers)
        {
            foreach (Container container in containers)
            {
                bool placed = false;

                foreach (Stack stack in stacks)
                {
                    if (stack.TryPlaceNormalContainer(container))
                    {
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    Stack newStack = new Stack(width, length);
                    newStack.TryPlaceNormalContainer(container);
                    stacks.Add(newStack);
                }
            }
        }



        public void PrintLayout()
        {
            Console.WriteLine("");
            Console.WriteLine("Layout with containers:");

            Dictionary<(int, int), int> positionWeights = new Dictionary<(int, int), int>();

            for (int i = 0; i < stacks.Count; i++)
            {
                Console.WriteLine($"Stack {i + 1}:");
                stacks[i].PrintStack();

                for (int y = 0; y < stacks[i].length; y++)
                {
                    for (int x = 0; x < stacks[i].width; x++)
                    {
                        if (!positionWeights.ContainsKey((x, y)))
                        {
                            int totalWeight = stacks[i].GetWeightAtPosition(x, y, stacks);
                            positionWeights.Add((x, y), totalWeight);
                        }
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("Weights:");

            foreach (var positionWeight in positionWeights)
            {
                Console.WriteLine($"Position {positionWeight.Key} - Total weight: {positionWeight.Value}");
            }
        }



    }
}