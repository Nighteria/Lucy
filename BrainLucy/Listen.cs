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

            lireKeyMotReconnu(e.Result.Text);
        }

        public  void lireKeyMotReconnu(string motReconnu)
        {

            Dictionary<string, List<String>> map = this.vocabulaire.getMapValue();

            //     string[] words = this.vocabulaire.getTableauKeyString().Split('|');

            //=====acculer 5 fois et executer cette methode


            //======================
            string TableauEnChaine = "";

            for (int i = 0; i < map.Count; i++)
            {
                List<String> elem = map.Values.ElementAt(i);
             //  Console.WriteLine(map.Keys.ElementAt(i));
                string key = map.Keys.ElementAt(i);


                foreach (string valeur in elem)
                {
                    if (motReconnu == valeur)
                    {
                        TableauEnChaine += key + "|";
                    }
                }
            }
            //======================



            string[] words = TableauEnChaine.Split('|');//tableau de k
            Console.WriteLine("tab: " + this.vocabulaire.getTableauKeyString());

            string plusGrandOccurence = "";
            int val = 0;
            Dictionary<string, int> boxKey = new Dictionary<string, int>();

            foreach (string item in words)
            {
                if (item != "")
                {
                    if (boxKey.ContainsKey(item) == false)
                    {
                        boxKey.Add(item, 1);
                    }
                    else
                    {
                        boxKey[item]++;
                        if ((boxKey[item]) > val) // recupere la key avec la plus grande pertinance
                        {
                            plusGrandOccurence = item;
                            val = boxKey[item];
                        }


                    }
                }
            }
            Console.WriteLine("plus grand est : " + plusGrandOccurence);


        }






    }
}
