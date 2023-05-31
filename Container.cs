﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShip2
{
    public enum ContainerType
    {
        Normal,
        Valuable
    }

    public enum ContainerTemperature
    {
        Normal,
        Cold
    }

    public class Container
    {
        private static readonly Random random = new Random();

        public ContainerType Type { get; set; }
        public ContainerTemperature Temperature { get; set; }
        public int Weight { get; set; }
        public int MaxLoad { get; set; } = 120000;

        public Container()
        {
            Weight = random.Next(4000, 30001);
        }

        public override string ToString()
        {
            return $"Type: {Type}, Temperature: {Temperature}";
        }
    }
}
