using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCommercial.Entites
{
    public class ComptePrepaye
    {
        public double SoldePrepaye { set; get; }

        public double Solde
        {
            get
            {
                return this.SoldePrepaye;
            }
        }
    }
}
