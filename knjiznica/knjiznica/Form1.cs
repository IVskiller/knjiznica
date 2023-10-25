using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace knjiznica
{


    public partial class Form1 : Form
    {
        public class knjiznica
        {
            public List<Clan> clanovi = new List<Clan>();
            public List<Knjiga> knjige = new List<Knjiga>();
        }
        public class Knjiga
        {
            protected int ID;
            static int IDbrojac=0;
            protected string ime;
            protected string autorime;
            protected string autorprezime;

            public Knjiga(string ime_,string autorime_, string autorprezime_) {
                ID = IDbrojac;
                ime = ime_;
                autorime = autorime_;
                autorprezime = autorprezime_;
                IDbrojac++;
            }

        }

        public class Clan
        {
            protected int OIB;
            static int IDbrojac;
            protected string ime;
            protected string prezime;
            public Clan(string ime_, string prezime_)
            {
                OIB = IDbrojac;
                ime = ime_;
                prezime = prezime_;
                IDbrojac++;
            }

        }
        public Form1()
        {
            InitializeComponent();
            Knjiga knj1 = new Knjiga("hlapić","ivana","brlić");


        }

    }
}
