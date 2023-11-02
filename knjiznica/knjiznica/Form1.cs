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
        private knjiznica nasaknjiznica = new knjiznica();
        private string selknjpos;
        private string selclanpos;
        public class knjiznica
        {
            public List<Clan> clanovi = new List<Clan>();
            public List<Knjiga> knjige = new List<Knjiga>();

            public void knjigeadd(Knjiga x) {
                knjige.Add(x);
            }

            public void clanadd(Clan x) {
                clanovi.Add(x);
            }

        }


        public class Knjiga
        {
            protected int ID;
            static int IDbrojac = 1;
            protected string ime;
            protected string autorime;
            protected string autorprezime;
            protected int clanposudio;

            public string get_ID()
            {
                return ID.ToString();
            }
            public string get_ime(){
              return ime; 
            }

            public string get_autorime()
            {
                return autorime;
            }

            public string get_autorprezime()
            {
                return autorprezime;
            }

            public int get_clanposudio()
            {
                return clanposudio;
            }

            public void set_clanposudio(int x)
            {
                clanposudio=x;
            }

            public Knjiga(string ime_,string autorime_, string autorprezime_) {
                ID = IDbrojac;
                ime = ime_;
                autorime = autorime_;
                autorprezime = autorprezime_;
                IDbrojac++;
                clanposudio = 0;
            }
        }


        public class Clan
        {
            protected int OIB;
            static int IDbrojac=1;
            protected string ime;
            protected string prezime;
            public Clan(string ime_, string prezime_)
            {
                OIB = IDbrojac;
                ime = ime_;
                prezime = prezime_;
                IDbrojac++;
            }


            public string get_OIB()
            {
                return OIB.ToString();
            }

            public string get_ime()
            {
                return ime.ToString();
            }

            public string get_prezime()
            {
                return prezime.ToString();
            }

        }
        public Form1()
        {
            InitializeComponent();
            Knjiga knj1 = new Knjiga("hlapić","ivana","brlić");
            knjigatableadd(knj1);
            Clan cl1 = new Clan("Jan", "Kovać");
            clantableadd(cl1);
            knjigezaposudbu();
            clanovizaposudbu();




        }

        #region knjiga add
        private void button1_Click(object sender, EventArgs e)
        {
           
            string s2= textBox2.Text;
            string s3 = textBox3.Text;
            try
            {
                Knjiga nk = new Knjiga(s2, s3.Substring(0, s3.IndexOf(" ")), s3.Substring(s3.IndexOf(" ")));

                knjigatableadd(nk);
            }
            catch { }
            
           
        }

        private void knjigatableadd(Knjiga x) {
            nasaknjiznica.knjigeadd(x);
            dataGridView1.Rows.Add(x.get_ID(), x.get_ime(), x.get_autorime() + " " + x.get_autorprezime());
            knjigezaposudbu();
        }



        private void knjigezaposudbu() {
            comboBox1.Items.Clear();
            foreach (Knjiga kji in nasaknjiznica.knjige)
            {
                if (kji.get_clanposudio() == 0)
                {
                    comboBox1.Items.Add(kji.get_ID());
                }
            }
        }




        #endregion

        #region clan add

        private void clantableadd(Clan x)
        {
            nasaknjiznica.clanadd(x);
            dataGridView2.Rows.Add(x.get_OIB(),x.get_ime(), x.get_prezime());
            clanovizaposudbu();
        }




        private void button2_Click(object sender, EventArgs e)
        {
            string s2 = textBox4.Text;
            string s3 = textBox1.Text;
            Clan ncl = new Clan(s2,s3);

           clantableadd(ncl);
        }



        private void clanovizaposudbu()
        {
            comboBox2.Items.Clear();
            foreach (Clan cl in nasaknjiznica.clanovi)
            {
                    comboBox2.Items.Add(cl.get_OIB());
              
            }
        }
        #endregion





        #region prazno
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //listBox1.Items.Add();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        } 
        
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
  private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            


        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            selclanpos = comboBox2.SelectedItem.ToString();
            selknjpos = comboBox1.SelectedItem.ToString();
            nasaknjiznica.knjige.ElementAt(Int32.Parse(selknjpos)-1).set_clanposudio(Int32.Parse(selclanpos));
            knjigezaposudbu();
            comboBox1.ResetText();
            comboBox2.ResetText();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            listBox1.Items.Add(dataGridView1.SelectedCells[0].RowIndex);
        }
    }
}
