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

            for (int i = 0; i < stacks.Count; i++)
            {
                Console.WriteLine($"Stack {i + 1}:");
                stacks[i].PrintStack();
                Console.WriteLine();
            }
        }
    }
}