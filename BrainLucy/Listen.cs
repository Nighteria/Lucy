using System;
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
        Vocabulaire vocabulaire = new Vocabulaire();


        public Listen() // ecoute l'user parler
        {


            // Create an in-process speech recognizer for the en-US locale.
            using (
            SpeechRecognitionEngine recognizer =
              new SpeechRecognitionEngine(
                new System.Globalization.CultureInfo("fr")))
            {

                // crée et charge la grammaire grammar.

                this.vocabulaire.lireFichierXML();
                //     Console.WriteLine(vocabulaire.recupererVocabulaire().Enabled);
                recognizer.LoadGrammar(this.vocabulaire.recupererVocabulaire());

                // Add un event for the speech recognized event.
                recognizer.EmulateRecognize("allume");

                recognizer.SpeechRecognized +=
                  new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

                // Configure input to the speech recognizer.
                recognizer.SetInputToDefaultAudioDevice();

                // Start asynchronous, continuous speech recognition.
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                // Keep the console window open.
                while (true)
                {

                    Console.ReadLine();
                }

            }




        }


        // lance  l'event SpeechRecognized .
         void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Text: " + e.Result.Text);
            
            motReconnu(e.Result.Text);

        }

        public  void motReconnu(string motReconnu)
        {
            
            Dictionary<string, List<String>> map = this.vocabulaire.getMapValue();
            for (int i = 0; i < map.Count; i++)
            {
                List<String> elem = map.Values.ElementAt(i);
                Console.WriteLine(map.Keys.ElementAt(i));

                foreach (string valeur in elem)
                {
                    String pipi = valeur;
                    Console.WriteLine("mot: " + pipi);
                }
            }

        }






    }
}
