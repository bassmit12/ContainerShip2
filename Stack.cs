using ContainerShip2;

public class Stack
{
    public int width;
    public int length;
    private int height;
    public List<Container>[,] layout;

    public Stack(int width, int length)
    {
        this.width = width;
        this.length = length;
        this.height = 0;
        layout = new List<Container>[width, length];
    }


    public bool TryPlaceColdContainer(Container container)
    {
        for (int x = 0; x < width; x++)
        {
            if (layout[x, 0] == null)
            {
                layout[x, 0] = new List<Container>() { container };
                return true;
            }
        }

        return false;
    }

    public bool TryPlaceNormalContainer(Container container)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                if (layout[x, y] == null)
                {
                    layout[x, y] = new List<Container>() { container };
                    return true;
                }
                else if (layout[x, y].Count < layout[0, 0].Count)
                {
                    layout[x, y].Add(container);
                    return true;
                }
            }
        }

        return false;
    }

    public int GetWeightAtPosition(int x, int y, List<Stack> stacks)
    {
        int totalWeight = 0;

        foreach (Stack stack in stacks)
        {
            if (stack.layout[x, y] != null)
            {
                totalWeight += stack.layout[x, y].Sum(container => container.Weight);
            }
        }

        return totalWeight;
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
