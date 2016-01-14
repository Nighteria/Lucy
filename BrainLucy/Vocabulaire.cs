using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Speech.Recognition;
using System.Speech;
using System.Collections;
namespace BrainLucy
{

    class Vocabulaire
    {
        Choices choix = new Choices();

        public Vocabulaire() // class utilisé pour  gérer le vocabulaire de l'IA
        {


        }

        public void lireFichierXML()
        {
            //charge le fichier xml
            XDocument monXml = XDocument.Load("G:/projet/xml/test.xml");
            XPathNavigator xpathNavigator = monXml.CreateNavigator();
            //selectionne tout les noeuds phrase
            XPathNodeIterator xpathNodeIterator = xpathNavigator.Select("donnees/phrase");

            //création map de liste de mot  
            Dictionary<string, List<String>> map = new Dictionary<string, List<String>> ();

            //parcour tout les noeuds phrase
            while (xpathNodeIterator.MoveNext())
            {

                //création liste de mot
                List<String> SousListe = new List<String>();    
                XPathNodeIterator xpathNodeIteratormot =xpathNodeIterator.Current.Select("mot");
                string attribut = xpathNodeIterator.Current.GetAttribute("name", String.Empty);

                while (xpathNodeIteratormot.MoveNext()) //parcours les mots
                {
                    string mot = xpathNodeIteratormot.Current.Value.ToString().Trim();
                    // envoie de la valeur à liste choix            
                    this.choix.Add(xpathNodeIteratormot.Current.Value.ToString().Trim());
                    SousListe.Add(xpathNodeIteratormot.Current.Value.ToString().Trim());
                    //envoi la sous liste à la grosse liste

                }
                map.Add(attribut, SousListe);

            }
            
            //parcour la map pour afficher les valeur et leur clé
           for (int i = 0; i< map.Count;i++ )
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



        public Grammar recupererVocabulaire() //stock vocabulaire dans la class
        {

            //construction grammaire
            GrammarBuilder builder = new GrammarBuilder();
            builder.Append(this.choix);

            //charge grammaire
            Grammar grammar = new Grammar(builder);
            grammar.Name = "listeMot";

            return grammar;

        }

        public void setChoix(Choices choix)
        {
            this.choix = choix;//
        }

        public Choices getChoix()
        {
            return choix;
        }

    }
}
