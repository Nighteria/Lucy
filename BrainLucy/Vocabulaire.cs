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
        //création map de liste de mot  
        Dictionary <string, List<String>> map = new Dictionary<string, List<String>>();

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



            //parcour tout les noeuds phrase
            while (xpathNodeIterator.MoveNext())
            {

                //création liste de mot
                List<String> SousListe = new List<String>();    
                XPathNodeIterator xpathNodeIteratormot =xpathNodeIterator.Current.Select("mot");

                string attribut = xpathNodeIterator.Current.GetAttribute("name", String.Empty);
                //nombre de mot
                //int nombreMot = (int)xpathNodeIteratormot.Count;

                while (xpathNodeIteratormot.MoveNext()) //parcours les mots
                {

                    string mot = xpathNodeIteratormot.Current.Value.ToString().Trim();
                    // envoie de la valeur à liste choix    
                    if (mot != null)
                    {
                        setChoix(mot);
                        SousListe.Add(mot);
                        //envoi la sous liste à la grosse liste
                    }


                }
                this.map.Add(attribut, SousListe);
                
            }
            
            //parcour la map pour afficher les valeur et leur clé
           for (int i = 0; i< this.map.Count;i++ )
            {
               List<String> elem = this.map.Values.ElementAt(i);      
                //Console.WriteLine(map.Keys.ElementAt(i));

                foreach (string valeur in elem)
                {
                    String pipi = valeur;
                   //  Console.WriteLine("mot: " + pipi);
                }
            }
    

        }



        public Grammar recupererVocabulaire() //stock vocabulaire dans la class
        {

            //construction grammaire
            GrammarBuilder builder = new GrammarBuilder();
            //  builder.Append(new Choices(new string[] { "Anne", "Mary" }));
            // builder = getChoix().ToGrammarBuilder();
            builder.Append(this.choix);
            //charge grammaire
            Grammar grammar = new Grammar(builder);
            grammar.Name = "listeMot";

            return grammar;

        }

        public void setChoix(string mot)
        {
            this.choix.Add(mot);
        
        }

        public Choices getChoix()
        {
            return this.choix;
        }

        public Dictionary<string, List<String>> getMapValue() // retourne tout les mots de la grammaire avec ses attributs
        {
            return this.map;
        }

    }
}
