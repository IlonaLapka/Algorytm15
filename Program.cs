using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;

namespace Algorytm15
{
    internal class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            
            Console.WriteLine(Hexcolor(255, 99, 71));
            Console.WriteLine(Hexcolor(184, 134, 11));
            Console.WriteLine(Hexcolor(189, 183, 107));
            Console.WriteLine(Hexcolor(0, 0, 205));
            Blend(new List<string>() { "#000000", "#778899" });
            Blend(new List<string>() { "#E6E6FA", "#FF69B4", "#B0C4DE" });
        }

        static string Hexcolor(byte red, byte green, byte blue)
        {
            string hexString = Convert.ToHexString(new byte[] { red, green, blue });
            var response = client.GetStringAsync($"https://colornames.org/search/json/?hex={hexString}").Result;

            Color c = JsonConvert.DeserializeObject<Color>(response);
            return "#" +  hexString + " " + c.Name;
        }

        static void Blend(List<string> colors)
        {
            int sumRed, sumGreen, sumBlue;
            sumRed = sumGreen = sumBlue = 0;
            foreach (var item in colors)
            {
                byte[] c = Convert.FromHexString(item[1..]);
                sumRed += Convert.ToInt32(c[0]);
                sumGreen += Convert.ToInt32(c[1]);
                sumBlue += Convert.ToInt32(c[2]);
            }
            double red = Convert.ToDouble(sumRed) / colors.Count;
            double green = Convert.ToDouble(sumGreen) / colors.Count;
            double blue = Convert.ToDouble(sumBlue) / colors.Count;
            Console.WriteLine(Hexcolor(Convert.ToByte(Math.Round(red)), 
                Convert.ToByte(Math.Round(green)), 
                Convert.ToByte(Math.Round(blue))));
        }
    }
}
