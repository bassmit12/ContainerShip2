using ContainerShip2;

// Soort van:
// Om kapseizen te voorkomen moet ten minste 50% van het maximum gewicht van een schip zijn benut.
// Het schip moet in evenwicht zijn: het volledige gewicht van de containers voor iedere helft mag niet meer dan 20% van de totale lading verschillen.

// Gedaan:
// Een volle container weegt maximaal 30 ton. Een lege container weegt 4000 kg.
// Alle containers die gekoeld moeten blijven moeten in de eerste rij worden geplaatst vanwege de stroomvoorziening die aan de voorkant van elk schip zit.
// De afmeting van een schip moet instelbaar zijn in de applicatie, waarbij de hoogte en breedte in containers aangegeven kan worden.

// Hoort te werken maar werkt niet:
// Het maximum gewicht bovenop een container is 120 ton.

// To Do:
// Er mag niets bovenop een container met waardevolle lading worden gestapeld; wel mogen deze containers zelf op andere containers geplaatst worden.
// Een container met waardevolle lading moet altijd via de voor- of achterkant te benaderen zijn. Je mag er vanuit gaan dat ook gestapelde containers te benaderen zijn.



namespace ContainerShip2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Ship ship = new Ship(3, 3);

            // Create a list of containers
            List<Container> containers = new List<Container>();

            for (int i = 0; i < 100; i++)
            {
                containers.Add(new Container { Type = ContainerType.Normal, Temperature = ContainerTemperature.Normal });
            }

            for (int i = 0; i < 5; i++)
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

            ship.SortAndPlaceContainers(containers);

            ship.printInfo();
        }
    }
}

