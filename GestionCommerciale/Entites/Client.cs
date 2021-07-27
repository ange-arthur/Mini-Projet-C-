using GestionCommercial.Entites.Enums;
using System;
using System.Collections.Generic;
using System.Text; 

namespace GestionCommercial.Entites
{
    public class Client : IComparable
    {
        public string Nom { set; get; }
        public String Prenom { set; get; }
        public DateTime DateNaissance { set; get; }
        public String Email { set; get; }
        public Civilites Civilite { set; get; }
        public Nationalites Nationalite { set; get; }

        public ComptePrepaye ComptePrepaye { set; get; }

        public String NomComplet
        {
            get
            {
                return this.Nom + " " + this.Prenom;
            }
        }

        public String Resume
        {
            get
            {
                String le_resume = $"{this.NomComplet} ["
                     + $"{ ((this.ComptePrepaye == null) ? 0 : this.ComptePrepaye.SoldePrepaye)} dollar,"
                     + $"{this.Civilite.ToString()},"
                     + $"{this.Nationalite.ToString()},"
                     + $"{this.DateNaissance.ToShortDateString()},"
                     + $"{this.Email}]";
                return le_resume;
            }
        }

        public int CompareTo(object obj)
        {
            Client client = obj as Client;

            return String.Compare(this.Nom, client.Nom);
        }

        public override string ToString()
        {
            return this.Resume;
        }




    }
}
