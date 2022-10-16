using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.Utility
{
    public class SD
    {
        //Roles: 
        public const string Role_Admin = "Admin";
        public const string Role_Employe = "Employee";
        public const string Role_Individual= "Individual";

        //initial status when the order is created
        public const string StatusEnAttente= "EnAttente";
        //If it's a cutomer the the payement is approved, we change the status to approved
        public const string StatusApprouve = "Approuvé";
        public const string StatusAnnule = "Annulé";
        public const string StatusRembourse = "Remboursé";

        //Initial
        public const string PayementStatusEnattente = "En attente";
        //Payement is done it will be approved
        public const string PayementStatusApprouve = "Approuvé";
        public const string PayementStatusRejete = "Rejeté";

        //Key name of session
        public const string SessionCart = "SessionChariot";

    }
}
