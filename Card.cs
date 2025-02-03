using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoSimulator
{
    class Card
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public int Value { get; set; }

        public Card(string row)
        {
            Name = row.Split(";")[0];
            Img = row.Split(";")[1];
            Value = int.Parse(row.Split(";")[2]);
        }
        public Card(string name, string img, int value) 
        {
            Name = name;
            Img = img;
            Value = value;
        }

    }
}
