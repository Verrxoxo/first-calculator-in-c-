using System.Globalization;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace calc2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vítejte v kalkulačce!");

            while (true)
            {
                Console.Write("Zadejte výraz (např. 5+3): ");
                string vyraz = Console.ReadLine();

                // Nahrazení desetinné tečky desetinnou čárkou
                vyraz = vyraz.Replace('.', ',');


                // Rozdělení výrazu na čísla a operátory
                string[] casti = vyraz.Split(new char[] { '+', '-', '*', '/', '%' }, StringSplitOptions.RemoveEmptyEntries);

                if (casti.Length == 2)
                {
                    double cislo1, cislo2;


                // přizpůsobení se desetinnému oddělovači
                        CultureInfo culture = new CultureInfo("cs-CZ");
                    if (vyraz.Contains(','))
                    {
                        culture.NumberFormat.NumberDecimalSeparator = ",";
                    }
                    else if (vyraz.Contains('.'))
                    {
                        culture.NumberFormat.NumberDecimalSeparator = ".";
                    }

                    if (double.TryParse(casti[0], NumberStyles.Float, culture, out cislo1))
                    {
                        if (double.TryParse(casti[1], NumberStyles.Float, culture, out cislo2))
                        {
                            // Zjištění operátoru
                            char operatorSymbol = vyraz[casti[0].Length];
                            double vysledek = Vypocti(cislo1, cislo2, operatorSymbol);
                            Console.WriteLine("Výsledek: " + vysledek);
                        }
                        else
                        {
                            Console.WriteLine("Neplatný formát druhého čísla.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Neplatný formát prvního čísla.");
                    }
                }
                else
                {
                    Console.WriteLine("Neplatný formát výrazu.");
                }

                Console.WriteLine("Přejete si provést další výpočet? (ano/ne)");
                string odpoved = Console.ReadLine();
                if (odpoved.ToLower() != "ano")
                {
                    break;
                }
            }
        }

        // Metoda pro výpočet operace
        static double Vypocti(double cislo1, double cislo2, char operatorSymbol)
        {
            switch (operatorSymbol)
            {
                case '+':
                    return cislo1 + cislo2;
                case '-':
                    return cislo1 - cislo2;
                case '*':
                    return cislo1 * cislo2;
                case '/':
                    if (cislo2 != 0)
                        return cislo1 / cislo2;
                    else
                            return 0; 
          
                case '%':
                        if (cislo2 != 0)
                            return cislo1 % cislo2;
                        else
                            Console.Write("Chyba: Dělení nulou!");
                        return 0;
                default:
                        throw new ArgumentException("Neplatný operátor.");
                }
            }
        }
    }
