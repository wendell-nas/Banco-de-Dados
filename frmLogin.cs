using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckPecas2._0
{
    public partial class frmLogin : Form
    {   //INSERINDO O BANCO DE DADOS
        string strConexao = @"Data Source=.\SQLEXPRESS;Initial Catalog=dbCheckPecas;User ID=sa;Password=sql2022"; //PEGO ESSE CODIGO NA PROPIEDADE DO BD
        SqlConnection conexao; //INICIO UMA CONEXAO
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Text;

            conexao = new SqlConnection(strConexao); //CONECTO MEU BANCO DE DADOS NA FUNCAO CONEXAO
            SqlCommand cmd = new SqlCommand(); //CRIO UM COMANDO PARA REALIZAR ALGUMAS ATIVIDADES
            cmd.Connection = conexao; //CONECTO MINHA CONEXAO
            cmd.CommandText = "SELECT loginUsuario FROM tblUsuarios WHERE loginUsuario = @acesso"; //INSIRO UM CMD TEXT PARA FAZER A BUSCA DOS DADOS
            cmd.Parameters.AddWithValue("@acesso", usuario); //CRIO UM CMD PARAMETROS PARA MUDAR A STRING
            conexao.Open(); //ABRO MINHA CONEXAO

            if (cmd.ExecuteScalar() == null) //VERIFICAO PRA VER SE TEM ALGUM DADO "ESSE CMD EXECUTE É PARA BUSCAR OS DADOS"
            {
                DialogResult resposta = MessageBox.Show("Usuário inexistente! " +
                "Deseja realizar o cadastro?", "CADASTRO", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question); // CASO A BUSCA PELOS DADOS SEREM NULL 

                if (resposta != DialogResult.Yes)
                {
                    cmd.CommandText = "INSERT INTO tblUsuarios VALUES(@login,@senha)"; //FACO ESSE CMD TEXT PARA INSERIR OS DADOS NO MEU BD
                    cmd.Parameters.AddWithValue("@login", usuario);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    cmd.ExecuteNonQuery(); // FACO ESSE CMD EXECUTE PARA ADICIONAR OS DADOS NO BD
                    MessageBox.Show("Cadastro realizado com sucesso!");
                }
                else
                {
                    txtSenha.ResetText();
                    txtUsuario.ResetText();
                }
            }
            else
            {
                cmd.CommandText = "SELECT senhaUsuario FROM tblUsuarios WHERE loginUsuario = @user"; //USO ESSE CMD PARA VERIFICAR O USUARIO
                cmd.Parameters.AddWithValue("@user", usuario);
                if (senha != cmd.ExecuteScalar().ToString()) // USO ESSE IF PARA VERIFICAR SE A SENHA ESTA CORRETA 
                {
                    MessageBox.Show("A senha não confere!");
                }

            }
            
        }
    }
}
