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
        private string selknj2pos;

        private bool ch;
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
            protected List<Knjiga> posudene_knjige= new List<Knjiga>();
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

            public void add_knjiga(Knjiga x) {
                posudene_knjige.Add(x);
            }

            public void remove_knjiga(Knjiga x)
            {
                posudene_knjige.Remove(x);
            }

            public Knjiga get_knjiga(int x)
            {
                return posudene_knjige[x];
            }

            public int knjige_count()
            {
                return posudene_knjige.Count();
            }
            public Knjiga knjige_list(int x) {
                return posudene_knjige[x];
            }

        }
        public Form1()
        {
            InitializeComponent();
            ch = false;
            Knjiga knj1 = new Knjiga("hlapić","Ivana","Brlić");
            Knjiga knj2 = new Knjiga("hlapić", "Ivana", "Brlić");
            Knjiga knj3 = new Knjiga("preobrazba", "Franc", "Kafka");


            knjigatableadd(knj1);
            knjigatableadd(knj2);
            knjigatableadd(knj3);


            Clan cl1 = new Clan("Jan", "Kovać");
            clantableadd(cl1);

            Clan cl2 = new Clan("Karlo", "Preloznjak");
            clantableadd(cl2);

            knjigezaposudbu();
            clanovizaposudbu();
            posudeneknjigecombobox();



        }

        #region knjiga add
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < numericUpDown1.Value; i++) 
            {
            string s2= textBox2.Text;
            string s3 = textBox3.Text;
            try
            {
                Knjiga nk = new Knjiga(s2, s3.Substring(0, s3.IndexOf(" ")), s3.Substring(s3.IndexOf(" ")));

                knjigatableadd(nk);
            }
            catch { } }
            
            
           
        }

        private void knjigatableadd(Knjiga x) {
            nasaknjiznica.knjigeadd(x);
            dataGridView1.Rows.Add(x.get_ID(), x.get_ime(), x.get_autorime() + " " + x.get_autorprezime());
            knjigezaposudbu();
            posudeneknjigecombobox();
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

        private void posudeneknjigecombobox()
        {
            comboBox4.Items.Clear();
            foreach (Knjiga kji in nasaknjiznica.knjige)
            {
                if (kji.get_clanposudio() != 0)
                {
                    comboBox4.Items.Add(kji.get_ID());
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
            if (textBox4.Text != "" && textBox1.Text != "") 
            {
            string s2 = textBox4.Text;
            string s3 = textBox1.Text;
            Clan ncl = new Clan(s2,s3);
            clantableadd(ncl);
            textBox1.Clear();
            textBox4.Clear();

            }



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
         
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        } 
        
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
                if (ch && listBox1.SelectedIndex!=-1 && listBox1.SelectedItem.ToString()!= "posudio : slobodna")
                {
                


                    string a = listBox1.SelectedItem.ToString();
                    string b = a.Substring(a.Length - 1);


                    listBox1.Items.Clear();


                    int brojknj = nasaknjiznica.clanovi[Int32.Parse(b) - 1].knjige_count();
                    for (int i = 0; i < brojknj; i++)
                    {
                        Knjiga posudio = nasaknjiznica.clanovi[Int32.Parse(b) - 1].get_knjiga(i);
                        string knjigaid = posudio.get_ID();
                        listBox1.Items.Add(knjigaid);
                    }
                    ch = false;
                    listBox1.SelectedIndex = -1;
                }
               

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
            if ((comboBox2.SelectedItem != null) && (comboBox1.SelectedItem != null)) 
            { 
            selclanpos = comboBox2.SelectedItem.ToString();
            selknjpos = comboBox1.SelectedItem.ToString();
            nasaknjiznica.knjige.ElementAt(Int32.Parse(selknjpos)-1).set_clanposudio(Int32.Parse(selclanpos));
            nasaknjiznica.clanovi.ElementAt(Int32.Parse(selclanpos)-1).add_knjiga(nasaknjiznica.knjige[Int32.Parse(selknjpos) - 1]);
            knjigezaposudbu();
            posudeneknjigecombobox();
            comboBox1.ResetText();
            comboBox2.ResetText();
            listBox1.Items.Clear();
            }
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null) 
            {
            selknj2pos = comboBox4.SelectedItem.ToString();
            nasaknjiznica.knjige.ElementAt(Int32.Parse(selknj2pos) - 1).set_clanposudio(0);
                nasaknjiznica.clanovi.ElementAt(Int32.Parse(selclanpos) - 1).remove_knjiga(nasaknjiznica.knjige[Int32.Parse(selknj2pos) - 1]);
            knjigezaposudbu();
            posudeneknjigecombobox();
            comboBox4.ResetText();
            listBox1.Items.Clear();
            }
            
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
                listBox1.Items.Clear();
                DataGridViewRow row = dataGridView1.CurrentCell.OwningRow;
            string value = "";
           
            value = row.Cells["ID"].Value.ToString();
          
            int posudio = nasaknjiznica.knjige[Int32.Parse(value) - 1].get_clanposudio();
                if (posudio != 0) listBox1.Items.Add("posudio : ID-" + posudio);
                else listBox1.Items.Add("posudio : slobodna");
            ch = true;

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            listBox1.Items.Clear();
            DataGridViewRow row = dataGridView2.CurrentCell.OwningRow;
            string value = "";

            value = row.Cells["IDk"].Value.ToString();
            int brojknj = nasaknjiznica.clanovi[Int32.Parse(value)-1].knjige_count();
            for (int i = 0; i < brojknj; i++) { 
                Knjiga posudio = nasaknjiznica.clanovi[Int32.Parse(value) - 1].get_knjiga(i);
                string knjigaid = posudio.get_ID();
                listBox1.Items.Add(knjigaid);
            }

          
           

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
