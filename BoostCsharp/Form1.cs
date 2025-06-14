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

namespace BoostCsharp
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        int idSelectionne = -1;
        public Form1()
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Fiderana\Documents\Visual Studio 2012\Projects\BoostCsharp\BoostCsharp\Database1.mdf;Integrated Security=True");
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Personne' table. You can move, or remove it, as needed.
            Afficher();
            

        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            conn.Open();

            string requete = "INSERT INTO Personne VALUES ('"+textBoxNom.Text+"','"+textBoxPrenom.Text+"','"+textBoxAge.Value+"','"+textBoxEmail.Text+"')";
            cmd = new SqlCommand(requete, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Insértion réussie", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBoxNom.Clear();
            textBoxPrenom.Clear();
            textBoxAge.Value = 0;
            textBoxEmail.Clear();
            idSelectionne = -1;

            conn.Close();
            Afficher();
        }

        public void Afficher() 
        {
            conn.Open();

            string requete = "SELECT * FROM Personne";
            SqlDataAdapter da = new SqlDataAdapter(requete, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            conn.Open();

            string requete = "UPDATE Personne SET Nom = '"+textBoxNom.Text+"', Prenom ='" + textBoxPrenom.Text + "', Age ='" + textBoxAge.Value + "', Email='" + textBoxEmail.Text + "' WHERE Id ='"+idSelectionne+"'";
            cmd = new SqlCommand(requete, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Modification réussie", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            conn.Close();
            Afficher();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            idSelectionne = Convert.ToInt32(row.Cells[0].Value);
            textBoxNom.Text = row.Cells[1].Value.ToString();
            textBoxPrenom.Text = row.Cells[2].Value.ToString();
            textBoxAge.Value = Convert.ToDecimal(row.Cells[3].Value);
            textBoxEmail.Text = row.Cells[4].Value.ToString();
        }

        private void buttonSupprimer_Click(object sender, EventArgs e)
        {
            conn.Open();

            string requete = "DELETE FROM Personne WHERE Id ='" + idSelectionne + "'";
            cmd = new SqlCommand(requete, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Suppression réussie", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            conn.Close();
            Afficher();
        }
    }
}
