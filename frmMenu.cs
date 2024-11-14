using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vendas
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
            menuStrip1.ForeColor = System.Drawing.Color.White;
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduto produto = new frmProduto();
            produto.Show();
        }

        private void vendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVenda venda = new frmVenda();
            venda.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fechar = MessageBox.Show("Deseja realmente fechar o programa?", "Fechar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (fechar == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void pbxProduto_Click(object sender, EventArgs e)
        {
            frmProduto produto = new frmProduto();
            produto.Show();
        }

        private void pbxVendas_Click(object sender, EventArgs e)
        {
            frmVenda venda = new frmVenda();
            venda.Show();
        }

        private void pbxSair_Click(object sender, EventArgs e)
        {
            var fechar = MessageBox.Show("Deseja realmente fechar o programa?", "Fechar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (fechar == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }
    }
}
