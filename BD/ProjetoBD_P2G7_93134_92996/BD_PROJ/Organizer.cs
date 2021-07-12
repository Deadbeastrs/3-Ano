using System;
using System.Collections.Generic;
using System.Text;

namespace BD_PROJ
{
    class Organizer
    {
        private String _NIF;
        private String _Name;
        private int _telemovel;
        private String _email;

        public Organizer() : base()
        {
        }
        public Organizer(String NIF, String Name, int telemovel, String email) : base()
        {
            this._Name = Name;
            this._NIF = NIF;
            this._telemovel = telemovel;
            this._email = email;
        }

        public String NIF
        {
            get { return _NIF; }
            set { _NIF = value; }
        }

        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public int Telemovel
        {
            get { return _telemovel; }
            set { _telemovel = value; }
        }

        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public override String ToString()
        {
            return  _NIF + ":" + _Name;
        }
    }
}
