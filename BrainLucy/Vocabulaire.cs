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
using System.Text.RegularExpressions;

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
            XDocument monXml = XDocument.Load("E:/projet/xml/test.xml");
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
            string motRecuTest = "test";
            List<string> listeKeyMotReconnu = new List<string>();
            string TableauEnChaine = "";

            for (int i = 0; i< this.map.Count;i++ )
            {
               List<String> elem = this.map.Values.ElementAt(i);
                //Console.WriteLine(map.Keys.ElementAt(i));
                string key = map.Keys.ElementAt(i);
                foreach (string valeur in elem)
                {
                    String pipi = valeur;
                //    Console.WriteLine("mot: " + pipi);
                    if(motRecuTest == pipi)
                    {
                        //  Console.WriteLine("mot recu = mot Dico");
                        listeKeyMotReconnu.Add("nyyaa"); // ajoute

                        listeKeyMotReconnu.Add(key); // ajoute
                        listeKeyMotReconnu.Add("noooo"); // ajoute
                        listeKeyMotReconnu.Add("testName"); // ajoute
                        TableauEnChaine += key + "||";
                    }
                }
            }


            //parcour liste key des mot reconnu
            string TableauEnChaineTest = "testName|testName|vale|lol|testName";
            string keyAllOccurence = "";
            int nombreOccurence = 0;





            string[] words = TableauEnChaineTest.Split('|');

            Dictionary<string, int> oddw = new Dictionary<string, int>();
            
            foreach (string item in words)
            {
                if (item != "")
                {
                    if (oddw.ContainsKey(item) == false)
                    {
                        oddw.Add(item, 1);
                    }
                    else
                    {
                        oddw[item]++;
                    }
                }
            }

            foreach (var item in oddw)
            {
                Console.WriteLine(item);
                item.Value

            }
      
        













            foreach (string key in listeKeyMotReconnu)
            {


                //    Console.WriteLine("KEY : "+ key);

                //  Console.WriteLine(TableauEnChaineTest);




                /*
                
                                     string searchTerm =  key ;

                          //recherche requete link pour retourner nombre occurence
                          var matchQuery = from word in listeKeyMotReconnu
                                           where word.ToLowerInvariant() == searchTerm.ToLowerInvariant()
                                           select word;

                          // Count the matches, which executes the query.
                          int wordCount = matchQuery.Count();
                          Console.WriteLine("{0} occurrences(s) of the search term \"{1}\" were found.", wordCount, searchTerm);
                          */
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
