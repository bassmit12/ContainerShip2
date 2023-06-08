using ContainerShip2;

public class Layer
{
    public int width;
    public int length;
    private int height;
    public List<Container>[,] layout;

    public Layer(int width, int length)
    {
        this.width = width;
        this.length = length;
        this.height = 0;
        layout = new List<Container>[width, length];
    }


    public bool TryPlaceColdContainer(Container container, List<Layer> layers)
    {
        for (int x = 0; x < width; x++)
        {
            if (layout[x, 0] == null)
            {
                int stackWeight = GetWeightAtPosition(x, 0, layers);

                // Check if the weight of the container itself exceeds the limit
                if (stackWeight + container.Weight <= 120000)
                {
                    layout[x, 0] = new List<Container>() { container };
                    return true;
                }
            }
        }

        return false;
    }


    public bool TryPlaceNormalContainer(Container container, List<Layer> layers)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 1; y < length; y++) // Start from y = 1 to skip the first row
            {
                int stackWeight = GetWeightAtPosition(x, y, stacks); // Get the total weight of the stack

                if (stackWeight + container.Weight <= 120000) // Check weight limit
                {
                    if (layout[x, y] == null)
                    {
                        layout[x, y] = new List<Container>() { container };
                        return true;
                    }
<<<<<<< Updated upstream:Layer.cs
                }
                else if (layout[x, y].Count < layout[0, 0].Count)
                {
                    int stackWeight = GetWeightAtPosition(x, y, layers); // Get the total weight of the stack
=======
>>>>>>> Stashed changes:Stack.cs

                    else if (layout[x, y] != null && layout[x, y].Count < layout[0, 0]?.Count)
                    {
                        layout[x, y].Add(container);
                        return true;

                    }
                }
                
            }
        }

        return false;
    }

<<<<<<< Updated upstream:Layer.cs






    public int GetWeightAtPosition(int x, int y, List<Layer> layers)
=======
    public int GetWeightAtPosition(int x, int y, List<Stack> stacks)
>>>>>>> Stashed changes:Stack.cs
    {
        int totalWeight = 0;
        int lowestContainerWeight = int.MaxValue;

        foreach (Layer stack in layers)
        {
            if (stack.layout[x, y] != null)
            {
                totalWeight += stack.layout[x, y].Sum(container => container.Weight);
                lowestContainerWeight = Math.Min(lowestContainerWeight, stack.layout[x, y].Last().Weight);
            }
        }

        return totalWeight - lowestContainerWeight;
    }








    public void PrintStack()
    {
        for (int y = 0; y < length; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (layout[x, y] != null)
                {
                    Console.Write(GetContainerSymbol(layout[x, y][0]) + " ");
                }
                else
                {
                    Console.Write(". ");
                }
            }
            Console.WriteLine();
        }
    }

    private char GetContainerSymbol(Container container)
    {
        switch (container.Type)
        {
            case ContainerType.Normal:
                switch (container.Temperature)
                {
                    case ContainerTemperature.Normal:
                        return 'N';
                    case ContainerTemperature.Cold:
                        return 'C';
                }
                break;
            case ContainerType.Valuable:
                switch (container.Temperature)
                {
                    case ContainerTemperature.Normal:
                        return 'V';
                    case ContainerTemperature.Cold:
                        return 'B';
                }
                break;
        }

        return ' ';
    }
}
