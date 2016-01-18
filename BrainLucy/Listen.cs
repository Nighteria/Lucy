﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech;


namespace BrainLucy
{
    class Listen
    {


        public  Listen() // ecoute l'user parler
        {



            // Create an in-process speech recognizer for the en-US locale.
            using (
            SpeechRecognitionEngine recognizer =
              new SpeechRecognitionEngine(
                new System.Globalization.CultureInfo("fr")))
            {

                // crée et charge la grammaire grammar.
                Vocabulaire vocabulaire = new Vocabulaire();
              //  Console.WriteLine(vocabulaire.recupererVocabulaire().Enabled);
               recognizer.LoadGrammar(vocabulaire.recupererVocabulaire());

                // Add un event for the speech recognized event.
                recognizer.SpeechRecognized +=
                  new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

                // Configure input to the speech recognizer.
                recognizer.SetInputToDefaultAudioDevice();

                // Start asynchronous, continuous speech recognition.
               recognizer.RecognizeAsync(RecognizeMode.Multiple);

                // Keep the console window open.
                while (true)
                {
                  //  recognizer.EmulateRecognize("test");

                    Console.ReadLine();
                }

            }
           


           
        }


        // lance  l'event SpeechRecognized .
        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Text: " + e.Result.Text);


        }

        public static void motReconnu()
        {

            Vocabulaire vocabulaire2 = new Vocabulaire();
            Dictionary<string, List<String>> map = vocabulaire2.getMapValue();
            for (int i = 0; i < map.Count; i++)
            {
                List<String> elem = map.Values.ElementAt(i);
                //Console.WriteLine(map.Keys.ElementAt(i));

                foreach (string valeur in elem)
                {
                    String pipi = valeur;
                    Console.WriteLine("mot: " + pipi);
                }
            }

        }






    }
}
