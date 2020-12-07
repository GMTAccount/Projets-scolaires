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
    class CompteBancaire
    {
        // Initialisation des variables
        string nomClient; // Nom du client
        float solde; // Solde
        bool estBloque; // True : compte bloqué, False : compte non bloqué
        int nombreEchec; // Nombre d'échecs de paiement
        static int nombreClient; // Nombre total de clients
        static int nombreBloque; // Nombre total de personnes bloquées
        /// <summary>
        /// Création du compte
        /// </summary>
        /// <param name="nom1">Nom du client</param>
        /// <param name="solde1">Solde initial</param>
        public CompteBancaire(string nom1, float solde1, bool etat)
        {
            if (nom1 != null)
            {
                this.nomClient = nom1; // Nom du client
            }
            this.solde = solde1; // Solde
            this.nombreEchec = 0; // Pas d'échec
            this.estBloque = etat; // Compte non bloqué
            nombreClient++;
        }
        /// <summary>
        /// Retour du nom du client
        /// </summary>
        public string NomClient
        {
            get { return nomClient; }
        }
        /// <summary>
        /// Retour du solde
        /// </summary>
        public float Solde
        {
            get { return solde; }
        }
        /// <summary>
        /// Retour du flag EstBloque (qui informe si un compte est bloqué = true ou non = false
        /// </summary>
        public bool EstBloque
        {
            get { return this.estBloque; }
        }
        /// <summary>
        /// Retour du nombre de clients
        /// </summary>
        public int NombreClient
        {
            get { return nombreClient; }
        }
        /// <summary>
        /// Retour du nombre de clients bloqués
        /// </summary>
        public int NombreBloque
        {
            get { return nombreBloque; }
        }
        /// <summary>
        /// Crédit du compte bancaire
        /// </summary>
        /// <param name="ajout">Valeur à créditer</param>
        public void Credit(float ajout)
        {
            this.solde += ajout;
            if (this.estBloque) // Déblocage du compte
            {
                nombreBloque--;
                this.estBloque = false;
            }
        }
        /// <summary>
        /// Débit sur le compte et test s'il y a blocage ou non
        /// </summary>
        /// <param name="retrait">Valeur à retirer</param>
        public bool Debit(float retrait)
        {
            bool test = false;
            if ((this.solde >= retrait) && (!this.estBloque)) // Teste si le compte est assez approvisionné ou est bloqué
            {
                this.solde = this.solde - retrait;
                this.nombreEchec = 0;
                test = true;
            }
            else
            {
                this.nombreEchec++; // Ajout d'un échec
                if (this.nombreEchec >= 2) // 2 échecs : blocage immédiat du compte
                {
                    this.estBloque = true;
                    this.nombreEchec = 0;
                    nombreBloque++;
                }
            }
            return test;
        }
        /// <summary>
        /// Affichage du contenu du compte
        /// </summary>
        public string ToString()
        {
            if (this.estBloque) return (this.nomClient + ";" + this.solde + ";t");
            return (this.nomClient + ";" + this.solde + ";f");
        }
    }
}
