using GestionCommercial.Entites;
using GestionCommercial.Entites.Enums;
using GestionCommercial.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace GestionCommercial.Services
{
    public class ClientService
    {
        private List<Client> ClientCollection;

        public ClientService()
        {
            // Chargement des clients déja enregistrer
            //this.ClientCollection = this.Ouvrir();

            if(this.ClientCollection == null)
            {
                this.ClientCollection = new List<Client>();

                ClientCollection.Add(new Client()
                {
                    Nom = "Guira",
                    Prenom = "Ismael",
                    Civilite = Civilites.Homme,
                    ComptePrepaye = new ComptePrepaye() { SoldePrepaye = 100 },
                    DateNaissance = new DateTime(2000, 1, 1),
                    Email = "Madani.Ali@gmail.com",
                    Nationalite = Nationalites.Burkinabe
                });

                ClientCollection.Add(new Client()
                {
                    Nom = "Killiam",
                    Prenom = "Mbappe",
                    Civilite = Civilites.Homme,
                    ComptePrepaye = new ComptePrepaye() { SoldePrepaye = 20 },
                    DateNaissance = new DateTime(2001, 1, 1),
                    Email = "Chami.Moad@gmail.com",
                    Nationalite = Nationalites.Française
                });

                ClientCollection.Add(new Client()
                {
                    Nom = "Berger",
                    Prenom = "Jacques",
                    Civilite = Civilites.Femme,
                    DateNaissance = new DateTime(2001, 1, 1),
                    Email = "Chami.Mouna@gmail.com",
                    Nationalite = Nationalites.Canadienne
                });
            }
           
        }

        public void Trier()
        {
            this.ClientCollection.Sort();
        }

        /*private List<Client> Ouvrir()
        {
            XmlSerializer xmlSerializer =
               new XmlSerializer(typeof(List<Client>));

            string FileName = "Clients.xml";
            if (File.Exists(FileName))
            {
                StreamReader streamReader =
               new StreamReader("Clients.xml");

                List<Client> liste_clients = (List<Client>)xmlSerializer
                    .Deserialize(streamReader);
                streamReader.Close();

                return liste_clients;
            }
            else
                return null;
        }*/

        public void Ajouter(Client client)
        {
            // Vérification du nom du client
            if (!this.VerificationNom(client.Nom))
            {
                string message = "Le nom du client est incorecte";
                throw new AjouterClientException(message);
            }

            // Vérification de l'age est supérieur à 18 ans
            if (!this.VerificationAge(client.DateNaissance))
            {
                string message = "Le du client est mineur, " +
                   "il est inférieur à 18 ans";
                throw new AjouterClientException(message);
            }

            // Vérification de l'email
            if (!this.VerificationEmail(client.Email))
            {
                string message = "Email non valide";
                throw new AjouterClientException(message);
            }
 
            this.ClientCollection.Add(client);
        }

        public void Enregistrer()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Client>));
            StreamWriter streamWriter = new StreamWriter("Clients.xml");

            xmlSerializer.Serialize(streamWriter, this.ClientCollection);

            streamWriter.Close();
        }

        public void Supprimer(string nomClient)
        {
            var clients = this.Rechercher(nomClient);
            if (clients != null && clients.Count > 0)
            {
                this.ClientCollection.Remove(clients.First());
            }
        }

        public List<Client> Rechercher(string nomClient)
        {
            return this.ClientCollection
                 .Where(c => c.Nom.ToLower().Contains(nomClient.ToLower()))
                 .ToList();
        }

        public List<Client> TousClients()
        {
            return ClientCollection;
        }


        #region Verifications
        private bool VerificationEmail(string email)
        {
            Regex RegexEmail = new Regex("^(\\w+([-_.]\\w+)*)[@](((outlook.)|(gmail.)|(hotmail.)|(yahoo.))(([a-z]{2})|([a-z]{3})))$");
            if (RegexEmail.IsMatch(email))
                return true;
            else
                return false;
        }

        private bool VerificationAge(DateTime dateNaissance)
        {
            TimeSpan Difference = DateTime.Now - dateNaissance;
            float Age = (int)Difference.TotalDays / 365;

            if (Age >= 18) return true;
            return false;
        }

        private bool VerificationNom(string nom)
        {
            Regex NameRegex = new Regex("^[a-zA-Z]+\\w+$");
            if (NameRegex.IsMatch(nom))
                return true;
            else
                return false;
        }
        #endregion
    }
}
