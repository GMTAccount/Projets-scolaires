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
    class Banque
    {
        List<CompteBancaire> comptes;
        string nom_de_banque;
        /// <summary>
        /// Constructeur de la classe Banque
        /// </summary>
        /// <param name="compte">Liste de comptes à intégrer à la banque</param>
        /// <param name="nom">Nom de la banque</param>
        public Banque(List<CompteBancaire> compte, string nom)
        {
            this.comptes = compte; // listes de comptes
            this.nom_de_banque = nom; // Nom de la banque
        }
        /// <summary>
        /// Retour de la liste des comptes de la banque (à des fins d'affichage)
        /// </summary>
        public List<CompteBancaire> Compte
        {
            get { return this.comptes; }
        }
        /// <summary>
        /// Retour du nom de la banque
        /// </summary>
        public string Nom
        {
            get { return this.nom_de_banque; }
        }
        /// <summary>
        /// Lecture d'un fichier contenant tous les comptes clients, et ajout dans l'instance en cours
        /// </summary>
        /// <param name="filename">Chemin d'accès au fichier</param>
        public void ReadFile(string filename)
        {
            StreamReader fichier1 = new StreamReader(filename); // Ouverture du flux
            try
            {
                // Si le fichier existe, alors le traitement commence
                string ligne1 = fichier1.ReadLine();
                while (ligne1 != null) // On parcourt toutes les lignes du fichier
                {
                    string[] client = ligne1.Split(';');
                    bool bloque = true;
                    if (client[2] == "f")
                    {
                        bloque = false;
                    }
                    this.comptes.Add(new CompteBancaire(client[0], (float) Convert.ToDouble(client[1]), bloque));
                    ligne1 = fichier1.ReadLine();
                }
            }
            catch (FileNotFoundException erreur)
            {
                Console.WriteLine("Le fichier correspondant n'a pas pu être trouvé");
                Console.WriteLine(erreur.Message);
                Console.WriteLine("Veuillez saisir un nouveau chemin : ");
                fichier1 = new StreamReader(Console.ReadLine());
            }
            catch (Exception erreur)
            {
                Console.WriteLine(erreur.Message);
            }
            finally
            {
                if(fichier1 != null)
                {
                    fichier1.Close();
                }
            }
        }
        /// <summary>
        /// Augmentation de tous les comptes en fonction des profits annuels de la banque
        /// </summary>
        /// <param name="augmentation">Pourcentage d'augmentation</param>
        public void Augmente(float augmentation)
        {
            if(this.comptes.Count > 0 && this.comptes != null)
            {
                for (int i = 0; i < this.comptes.Count; i++)
                {
                    // Seuls les comptes non bloqués sont à augmenter
                    if (!this.comptes[i].EstBloque)
                    {
                        this.comptes[i].Credit(this.comptes[i].Solde * augmentation);
                    }
                }
            }
            else
            {
                Console.WriteLine("Erreur : il n'y a aucun client dans la banque");
            }
        }
        /// <summary>
        /// Écriture des comptes dans un fichier (pour en garder une trace après la fin de l'excécution du code)
        /// </summary>
        /// <param name="filename">Chemin d'accès du fichier</param>
        public void WriteFile(string filename)
        {
            // Ouverture du flux d'écriture
            StreamWriter fichier = new StreamWriter(filename);
            try
            {
                // Écriture de toutes les lignes
                for (int i = 0; i < this.comptes.Count; i++)
                {
                    fichier.WriteLine(this.comptes[i].ToString());
                }
            }
            catch (FileNotFoundException erreur)
            {
                Console.WriteLine("Le fichier correspondant n'a pas pu être trouvé");
                Console.WriteLine(erreur.Message);
                Console.WriteLine("Veuillez saisir un nouveau chemin : ");
                fichier = new StreamWriter(Console.ReadLine());
            }
            catch (Exception erreur)
            {
                Console.WriteLine(erreur.Message);
            }
            finally
            {
                fichier.Close();
            }
        }
    }
}
