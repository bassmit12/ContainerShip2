using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShip2
{
    public class Balancer
    {
        private Ship ship;

        public Balancer(Ship ship)
        {
            this.ship = ship;
        }

        public bool IsShipInBalance()
        {
            int totalWeight = ship.CalculateTotalWeight();
            int leftWeight = 0;
            int rightWeight = 0;
            int middleWeight = 0;
            int middleLaneStartCol = ship.width / 2;

            for (int i = 0; i < ship.stacks.Count; i++)
            {
                for (int y = 0; y < ship.stacks[i].length; y++)
                {
                    for (int x = 0; x < ship.stacks[i].width; x++)
                    {
                        if (ship.stacks[i].layout[x, y] != null)
                        {
                            if (ship.width % 2 == 0) // Even number of columns
                            {
                                if (x < middleLaneStartCol)
                                {
                                    leftWeight += ship.stacks[i].layout[x, y].Sum(container => container.Weight);
                                }
                                else
                                {
                                    rightWeight += ship.stacks[i].layout[x, y].Sum(container => container.Weight);
                                }
                            }
                            else // Odd number of columns
                            {
                                if (x < middleLaneStartCol)
                                {
                                    leftWeight += ship.stacks[i].layout[x, y].Sum(container => container.Weight);
                                }
                                else if (x > middleLaneStartCol)
                                {
                                    rightWeight += ship.stacks[i].layout[x, y].Sum(container => container.Weight);
                                }
                                else
                                {
                                    middleWeight += ship.stacks[i].layout[x, y].Sum(container => container.Weight);
                                }
                            }
                        }
                    }
                }
            }

            int maxAllowedDifference = (int)(totalWeight * 0.2);
            int weightDifference = 0;

            if (ship.width % 2 == 0) // Even number of columns
            {
                weightDifference = Math.Abs(leftWeight - rightWeight);
            }
            else // Odd number of columns
            {
                weightDifference = Math.Abs((leftWeight + middleWeight / 2) - (rightWeight + middleWeight / 2));
            }

            return weightDifference <= maxAllowedDifference;
        }





    }
}
