using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech;


namespace BrainLucy
{
    class Program
    {
        static void Main(string[] args)
        {
            Vocabulaire vocabulaire = new Vocabulaire(); //  charge le vocabulaire de l'ia
            vocabulaire.lireFichierXML();
           // Listen listen = new Listen(); // ecoute l'user parler
            Console.ReadLine();
        }










    }
}
