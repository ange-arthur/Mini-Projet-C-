using GestionCommercial.Entites;
using GestionCommercial.Entites.Enums;
using GestionCommercial.Services;
using GestionCommercial.Services.Exceptions;
using System;
using System.Collections.Generic;

namespace GestionCommercial
{
    class Program
    {
        static ClientService clientService = new ClientService();

        public void Menu()
        {
            Console.WriteLine("\n\t ----- Application de gestion des clients ----- \n ");
            Console.WriteLine(" 1 - Afficher tous les clients");
            Console.WriteLine(" 2 - Ajouter un client");
            Console.WriteLine(" 3 - Supprimer un client");
            Console.WriteLine(" 4 - Rechercher un client");
            Console.WriteLine(" 5 - Enregistrement");
            Console.WriteLine(" 6 - Trier");
            Console.WriteLine(" Q- Quitter");
            Console.Write("\t\n Donnez votr choix : ");
        }
        static void Main(string[] args)
        {
            string choixOperation;
            do
            {
                Menu();
                choixOperation = Console.ReadLine();
                switch (choixOperation)
                {
                    case "1":
                        AfficherTousClients(clientService.TousClients());
                        break;
                    case "2":
                        AjouterClient();
                        break;
                    case "3":
                        SupprimerClient();
                        break;
                    case "4":
                        RechercherClientsParNom();
                        break;
                    case "5":
                        Enregistrement();
                        break;
                    case "6":
                        Trier();
                        break;
                }

            } while (choixOperation != "Q");
        }

        private static void Trier()
        {
            clientService.Trier();
        }

        private static void Enregistrement()
        {
            clientService.Enregistrer();
        }

        private static void SupprimerClient()
        {
            Console.Write("Donnez le nom du client à supprimer :");
            string nomClient = Console.ReadLine();
            clientService.Supprimer(nomClient);
        }

        private static void RechercherClientsParNom()
        {
            Console.Write("Donnez le nom du client à chercher :");
            string nomClient = Console.ReadLine();
            List<Client> clients = clientService.Rechercher(nomClient);
            AfficherTousClients(clients);
        }

        private static void AjouterClient()
        {
            Client client = new Client();

            Console.WriteLine("Saisie d'un nouveau client :");

            Console.Write("Nom :");
            client.Nom = Console.ReadLine();

            Console.Write("Prénom :");
            client.Prenom = Console.ReadLine();

            Console.Write("Civilité (1:Homme,2:Femme) :");
            var CiviliteNombre = Console.ReadLine();
            client.Civilite = (Civilites)int.Parse(CiviliteNombre);

            Console.Write("Date de naissance :");
            client.DateNaissance = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Nationalité (1:Burkinabe,2:Canadienne,3:Française) :");
            var NationaliteNombre = Console.ReadLine();
            client.Nationalite = (Nationalites)int.Parse(NationaliteNombre);

            Console.Write("Email :");
            client.Email = Console.ReadLine();

            try
            {
                clientService.Ajouter(client);
            }
            catch (AjouterClientException exception)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\t Le client n'a pas été ajouter :" 
                    + exception.Message);

                Console.ResetColor();
            }
            

        }

        private static void AfficherTousClients(List<Client> listClients)
        {
            Console.Clear();
            Console.WriteLine("\t --- Liste des clients --- \n");
            foreach (var client in listClients)
            {
                Console.WriteLine(client);
            }

        }
    }
}
