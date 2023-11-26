using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace knjiznica
{


    public partial class Form1 : Form
    {
        private knjiznica nasaknjiznica = new knjiznica();
        private string selknjpos;
        private string selclanpos;
        private string selknj2pos;
        private int IDposudba;

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

            public string get_IDbrojac()
            {
                return IDbrojac.ToString();
            }
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

            public Knjiga(string ime_, string autorime_, string autorprezime_, int clanposudio_)
            {
                ID = IDbrojac;
                ime = ime_;
                autorime = autorime_;
                autorprezime = autorprezime_;
                IDbrojac++;
                clanposudio = clanposudio_;
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

            string connetionString;
            SqlConnection cnn;
            connetionString = @"Server=localhost\SQLEXPRESS;Database=knjiznica;Trusted_Connection=True;User Id=Ivort;Password=sqlknjiznica";
    
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand inic;
            SqlDataReader dataReader;
            String sql, Output="";

            
            sql = "SELECT *FROM[dbo].[Knjiga]";
            inic = new SqlCommand(sql, cnn);
            dataReader = inic.ExecuteReader();
            while (dataReader.Read()) {
               
                Knjiga knj1 = new Knjiga(dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString(), Int32.Parse(dataReader.GetValue(4).ToString()));

                knjigatableadd(knj1);
            }
            dataReader.Close();
            inic.Dispose();
            cnn.Close();

           
            
            cnn.Open();
            sql = "SELECT *FROM[dbo].[Clan]";
            inic = new SqlCommand(sql, cnn);
            dataReader = inic.ExecuteReader();
            while (dataReader.Read())
            {
                
                Clan cl1 = new Clan(dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString());
                clantableadd(cl1);
            }
            dataReader.Close();
            inic.Dispose();
            cnn.Close();



            cnn.Open();
            sql = "SELECT *FROM[dbo].[Posudbe]";
            inic = new SqlCommand(sql, cnn);
            dataReader = inic.ExecuteReader();
            while (dataReader.Read())
            {
                nasaknjiznica.clanovi.ElementAt(Int32.Parse(dataReader.GetValue(1).ToString()) - 1).add_knjiga(nasaknjiznica.knjige[Int32.Parse(dataReader.GetValue(2).ToString()) - 1]);






                int a = Int32.Parse(dataReader.GetValue(0).ToString());
                if (IDposudba < a) { IDposudba = a; }
            }
            IDposudba++;
            dataReader.Close();
            inic.Dispose();
            cnn.Close();








            ch = false;
            

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
                String ime = s3.Substring(0, s3.IndexOf(" "));
                String prezime = s3.Substring(s3.IndexOf(" "));
                Knjiga nk = new Knjiga(s2, ime, prezime);
                knjigatableadd(nk);

                    string connetionString;
                    SqlConnection cnn;
                    connetionString = @"Server=localhost\SQLEXPRESS;Database=knjiznica;Trusted_Connection=True;User Id=Ivort;Password=sqlknjiznica";

                    using (cnn = new SqlConnection(connetionString))
                    {
                        cnn.Open();

                        string sql = "INSERT INTO [dbo].[Knjiga] ([ID], [Naziv], [Imeautora], [Prezimeautora]) " +
                                     "VALUES (@ID, @Naziv, @Imeautora, @Prezimeautora)";

                        using (SqlCommand command = new SqlCommand(sql, cnn))
                        {
                            command.Parameters.AddWithValue("@ID", nasaknjiznica.knjige[nasaknjiznica.knjige.Count - 1].get_ID());
                            command.Parameters.AddWithValue("@Naziv", nasaknjiznica.knjige[nasaknjiznica.knjige.Count - 1].get_ime());
                            command.Parameters.AddWithValue("@Imeautora", nasaknjiznica.knjige[nasaknjiznica.knjige.Count - 1].get_autorime());
                            command.Parameters.AddWithValue("@Prezimeautora", nasaknjiznica.knjige[nasaknjiznica.knjige.Count - 1].get_autorprezime());

                            command.ExecuteNonQuery();
                            Console.WriteLine("Data Inserted Successfully!");
                        }
                    }
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

                string connetionString;
                SqlConnection cnn;
                connetionString = @"Server=localhost\SQLEXPRESS;Database=knjiznica;Trusted_Connection=True;User Id=Ivort;Password=sqlknjiznica";

                using (cnn = new SqlConnection(connetionString))
                {
                    cnn.Open();

                    string sql = "INSERT INTO [dbo].[Clan]([OIB],[Ime],[Prezime]) " +
                                 "VALUES (@OIB, @Ime, @Prezime)";

                    using (SqlCommand command = new SqlCommand(sql, cnn))
                    {
                        command.Parameters.AddWithValue("@OIB", nasaknjiznica.clanovi[nasaknjiznica.clanovi.Count-1].get_OIB());
                        command.Parameters.AddWithValue("@Ime", nasaknjiznica.clanovi[nasaknjiznica.clanovi.Count - 1].get_ime());
                        command.Parameters.AddWithValue("@Prezime", nasaknjiznica.clanovi[nasaknjiznica.clanovi.Count - 1].get_prezime());

                        command.ExecuteNonQuery();
                        Console.WriteLine("Data Inserted Successfully!");
                    }
                }


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

                string connetionString;
                SqlConnection cnn;
                connetionString = @"Server=localhost\SQLEXPRESS;Database=knjiznica;Trusted_Connection=True;User Id=Ivort;Password=sqlknjiznica";


                using (cnn = new SqlConnection(connetionString))
                {
                    cnn.Open();

                    string sql = "INSERT INTO [dbo].[Posudbe]([ID],[IDclana],[IDknjige]) " +
                                 "VALUES (@ID, @Clan, @Knjiga)";

                    using (SqlCommand command = new SqlCommand(sql, cnn))
                    {
                        command.Parameters.AddWithValue("@ID", IDposudba);
                        IDposudba++;
                        command.Parameters.AddWithValue("@Clan", nasaknjiznica.clanovi.ElementAt(Int32.Parse(selclanpos) - 1).get_OIB());
                        command.Parameters.AddWithValue("@Knjiga", nasaknjiznica.knjige.ElementAt(Int32.Parse(selknjpos) - 1).get_ID());

                        command.ExecuteNonQuery();
                        Console.WriteLine("Data Inserted Successfully!");
                    }
                }


                using (cnn = new SqlConnection(connetionString))
                {
                    cnn.Open();

                    string sql = "UPDATE [dbo].[Knjiga] SET Posuđena = " + nasaknjiznica.clanovi.ElementAt(Int32.Parse(selclanpos) - 1).get_OIB() + " WHERE ID =" + nasaknjiznica.knjige.ElementAt(Int32.Parse(selknjpos) - 1).get_ID() + ";";
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    using (SqlCommand command = new SqlCommand(sql, cnn))
                    {
                        
                        adapter.UpdateCommand = new SqlCommand(sql, cnn);
                        adapter.UpdateCommand.ExecuteNonQuery();
                       
                    }
                }



            }
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null) 
            {
            selknj2pos = comboBox4.SelectedItem.ToString();
                int a = nasaknjiznica.knjige.ElementAt(Int32.Parse(selknj2pos) - 1).get_clanposudio();

             nasaknjiznica.clanovi.ElementAt(a-1).remove_knjiga(nasaknjiznica.knjige[Int32.Parse(selknj2pos) - 1]);
             nasaknjiznica.knjige.ElementAt(Int32.Parse(selknj2pos) - 1).set_clanposudio(0);
            
            knjigezaposudbu();
            posudeneknjigecombobox();
            comboBox4.ResetText();
            listBox1.Items.Clear();
            }

            string connetionString;
            SqlConnection cnn;
            connetionString = @"Server=localhost\SQLEXPRESS;Database=knjiznica;Trusted_Connection=True;User Id=Ivort;Password=sqlknjiznica";



            using (cnn = new SqlConnection(connetionString))
            {
                cnn.Open();

                string sql = "DELETE FROM [dbo].[Posudbe]  WHERE IDknjige=" + nasaknjiznica.knjige.ElementAt(Int32.Parse(selknj2pos) - 1).get_ID() + ";";
                SqlDataAdapter adapter = new SqlDataAdapter();
                using (SqlCommand command = new SqlCommand(sql, cnn))
                {

                    adapter.DeleteCommand = new SqlCommand(sql, cnn);
                    adapter.DeleteCommand.ExecuteNonQuery();

                }
            }


            using (cnn = new SqlConnection(connetionString))
            {
                cnn.Open();

                string sql = "UPDATE [dbo].[Knjiga] SET Posuđena = 0 WHERE ID= " + nasaknjiznica.knjige.ElementAt(Int32.Parse(selknj2pos) - 1).get_ID() + ";";
                SqlDataAdapter adapter = new SqlDataAdapter();
                using (SqlCommand command = new SqlCommand(sql, cnn))
                {

                    adapter.UpdateCommand = new SqlCommand(sql, cnn);
                    adapter.UpdateCommand.ExecuteNonQuery();

                }
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
    }
}
