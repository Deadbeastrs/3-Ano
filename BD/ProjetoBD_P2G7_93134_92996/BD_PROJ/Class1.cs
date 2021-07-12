using System;
using System.Collections.Generic;
using System.Text;

namespace BD_PROJ
{
    class Class1
    {
        private String Conncetion;

        public Class1() : base()
        {
            //Mudar a string da conexão para a desejada
            Conncetion = "Data Source = " + "tcp: mednat.ieeta.pt\\SQLSERVER, 8101" + "; " + "Initial Catalog = " + "p2g7" + "; uid = " + "p2g7" + "; " + "password = " + "Gui_Los!2000:1";
            //Conncetion = "data source=.\\SQLEXPRESS;integrated security=true;initial catalog=FESTIVAL_ORGANIZER";
        }

        public string getConnection()
        {
            return Conncetion;
        }


    }
}
