using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace TD_8___TADJER_Guillaume
{
    class Program
    {
        static void Main(string[] args)
        {
            Banque test = new Banque(new List<CompteBancaire>(), "Banque");
            // Pour simplifier les modifications de ce code dans le cadre de sa correction, 
            // vous pouvez saisir les chemins d'accès des fichiers dans la ligne ci-dessous :
            string[] chemins = { "Clients-Original.csv", "Clients.csv" };
            // Le premier chemin correspond au fichier initial, le second chemin au fichier d'arrivée (celui dans lequel s'effectuent les modifications)
            // Les deux chemins peuvent toutefois être identiques, auquel cas, il ne faut en marquer qu'un
            test.ReadFile(chemins[0]);
            for(int i = 0; i < test.Compte.Count; i++)
            {
                Console.WriteLine(test.Compte[i].ToString());
            }
            test.Augmente(0.2f);
            for (int i = 0; i < test.Compte.Count; i++)
            {
                Console.WriteLine(test.Compte[i].ToString());
            }
            test.WriteFile(chemins[chemins.Length - 1]);
        }
    }
}
