using System;
using System.Diagnostics;
namespace projekt_1._2
{
    class Program
    {
       static int licznikOperacji;
       static int[] tablica = new int[Convert.ToInt32(Math.Pow(2, 20))];
       static void Main(string[] args)
        {            
            Random r = new Random();
            int szukana = r.Next(0, tablica.Length);
            for (int i = 0; i < tablica.Length; i++)
            {
                tablica[i] = i;
            }
            Array.Sort(tablica);
            Console.WriteLine("Szukana: "+ szukana);
            int a = WyszukiwanieBinarne(szukana);
            Console.WriteLine("Pozycja w indexie: " + a);            
            pesymistyczny();
            srednia(szukana);          
        }

        
        static int WyszukiwanieBinarne(int szukana) 
        {
            
            int lewa = 0;
            int prawa = tablica.Length-1;
            while (lewa <= prawa)
            {
                licznikOperacji++;
                int pozycja = (lewa + prawa) / 2;
                if (tablica[pozycja] == szukana) return pozycja;
                else if (tablica[pozycja] > szukana) prawa = pozycja - 1;
                else if (tablica[pozycja] < szukana) lewa = pozycja + 1;
            }
            return -1;
        }

        static void pesymistyczny() // Złożoność pesymistyczna (dla liczby której nie ma w zakresie)
        {
            int szukana = -1;
            int liczbaoperacji = 10;
            long T = 0;
            long min = long.MinValue;
            long max = long.MaxValue;
            for (int i = 0; i < liczbaoperacji+2; i++)
            {
                long Start = Stopwatch.GetTimestamp();
                    WyszukiwanieBinarne(szukana);
                long Stop = Stopwatch.GetTimestamp();
                long czas = Stop - Start;
                T += czas;
                if (czas < min) min = czas;
                if (czas > max) max = czas;
             }
            T = T - min - max;
            double srednia = T*1.0 / liczbaoperacji/Stopwatch.Frequency;
            Console.WriteLine("Złożoność Pesymistyczna\nśredni czas: {0} Liczba operacji: {1}\n", srednia.ToString("F8"), licznikOperacji / 12);            
        }

        static void srednia(int szukana) // złożoność średnia
        {            
            int liczbaoperacji = 10;
            long T ;
            long min = long.MinValue;
            long max = long.MaxValue;
            for (int i = 0; i < tablica.Length; i++)
            {
                licznikOperacji = 0;
                int numer = tablica[i];
                T = 0;
                for (int j = 0; j < liczbaoperacji + 2; j++)
                {
                    long Start = Stopwatch.GetTimestamp();
                    WyszukiwanieBinarne(numer);
                    long Stop = Stopwatch.GetTimestamp();
                    long czas = Stop - Start;
                    T += czas;
                    if (czas < min) min = czas;
                    if (czas > max) max = czas;
                   
                }                
                if (i  == szukana){
                    T = T - min - max;
                    double srednia = T * 1.0 / liczbaoperacji / Stopwatch.Frequency;
                    Console.WriteLine("Złożoność Średnia\nśredni czas: {0} Liczba operacji: {1}", srednia.ToString("F8"), licznikOperacji / 12);                                     
                }               
            }
        }
    }
}
