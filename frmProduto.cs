using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vendas.Models;
using Vendas.Controller;

namespace Vendas
{
    public partial class frmProduto : Form
    {
        public frmProduto()
        {
            InitializeComponent();
        }

        private void frmProduto_Load(object sender, EventArgs e)
        {
            ConProduto conProduto = new ConProduto();
            List<Produto> produtos = conProduto.listaproduto();
            dgvProduto.DataSource = produtos;
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
            this.ActiveControl = txtNome;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNome.Text == "" || txtQuantidade.Text == "" || txtPreco.Text == "")
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Campos Obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    ConProduto conProduto = new ConProduto();
                    if (conProduto.RegistroRepetido(txtNome.Text) == true)
                    {
                        MessageBox.Show("Produto já existe em nossa base de dados!", "Produto Repetido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNome.Text = string.Empty;
                        txtQuantidade.Text = string.Empty;
                        txtPreco.Text = string.Empty;
                        this.ActiveControl = txtNome;
                        return;
                    }
                    else
                    {
                        int quantidade = Convert.ToInt32(txtQuantidade.Text.Trim());
                        if (quantidade == 0)
                        {
                            MessageBox.Show("Por favor, a quantidade tem que ser maior que zero(0)!", "Quantidade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.ActiveControl = txtQuantidade;
                            return;
                        }
                        else
                        {
                            conProduto.Inserir(txtNome.Text, quantidade, txtPreco.Text);
                            MessageBox.Show("Produto inserido com sucesso!", "Inserção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtNome.Text = string.Empty;
                            txtQuantidade.Text = string.Empty;
                            txtPreco.Text = string.Empty;
                            this.ActiveControl = txtNome;
                            List<Produto> produtos = conProduto.listaproduto();
                            dgvProduto.DataSource = produtos;
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pbxLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text == string.Empty)
                {
                    MessageBox.Show("Por favor, digite um ID válido!", "Localizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.ActiveControl = txtId;
                    return;
                }
                else
                {
                    ConProduto conProduto = new ConProduto();
                    int Id = Convert.ToInt32(txtId.Text.Trim());
                    conProduto.Localizar(Id);
                    txtNome.Text = conProduto.nome;
                    txtPreco.Text = Convert.ToString(conProduto.preco);
                    txtQuantidade.Text = Convert.ToString(conProduto.quantidade);
                    btnAtualizar.Enabled = true;
                    btnExcluir.Enabled = true;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text == string.Empty || txtNome.Text == string.Empty || txtPreco.Text == string.Empty || txtQuantidade.Text == string.Empty)
                {
                    MessageBox.Show("Existem campos sem preencher. Por favor, preencha todos os campos!", "Campos Obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    int Id = Convert.ToInt32(txtId.Text.Trim());
                    int quantidade = Convert.ToInt32(txtQuantidade.Text.Trim());
                    ConProduto conProduto = new ConProduto();
                    conProduto.Atualizar(Id, txtNome.Text, quantidade, txtPreco.Text);
                    MessageBox.Show("Produto atualizado com sucesso!", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<Produto> produtos = conProduto.listaproduto();
                    dgvProduto.DataSource = produtos;
                    txtId.Text = string.Empty;
                    txtNome.Text = string.Empty;
                    txtPreco.Text = string.Empty;
                    txtQuantidade.Text = string.Empty;
                    this.ActiveControl = txtNome;
                    btnAtualizar.Enabled = false;
                    btnExcluir.Enabled = false;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text == string.Empty)
                {
                    MessageBox.Show("Por favor, entre com um ID válido!", "ID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ActiveControl = txtId;
                    return;
                }
                else
                {
                    int Id = Convert.ToInt32(txtId.Text.Trim());
                    ConProduto conProduto = new ConProduto();
                    conProduto.Excluir(Id);
                    MessageBox.Show("Produto excluído com sucesso!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<Produto> produtos = conProduto.listaproduto();
                    dgvProduto.DataSource = produtos;
                    txtId.Text = string.Empty;
                    txtNome.Text = string.Empty;
                    txtPreco.Text = string.Empty;
                    txtQuantidade.Text = string.Empty;
                    btnAtualizar.Enabled = false;
                    btnExcluir.Enabled = false;
                    this.ActiveControl = txtId;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void dgvProduto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvProduto.Rows[e.RowIndex];
                this.dgvProduto.Rows[e.RowIndex].Selected = true;
                txtId.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                txtQuantidade.Text = row.Cells[2].Value.ToString();
                txtPreco.Text = row.Cells[3].Value.ToString();
            }
            btnAtualizar.Enabled = true;
            btnExcluir.Enabled = true;
        }
    }
}
