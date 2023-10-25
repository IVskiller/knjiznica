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
            List<Clan> clanovi = new List<Clan>();
            List<Knjiga> knjige = new List<Knjiga>();
        }
        public class Knjiga
        {
            protected int ID;
            static int IDbrojac;
            protected string ime;
            protected string autorime;
            protected string autorprezime;
        }

        public class Clan
        {
            protected int OIB;
            static int IDbrojac;
            protected string ime;
            protected string prezime;
        }
        public Form1()
        {
            InitializeComponent();
        }

    }
}
