using System;
using System.Windows.Forms;

namespace JogoXadrez
{
    public partial class Form1 : Form
    {
        private Button[,] botoesTabuleiro; 
        private Tabuleiro tabuleiro; 
        private Peca? pecaSelecionada; 
        private EnumCor jogadorAtual; 

        public Form1()
        {
            InitializeComponent();
            tabuleiro = new Tabuleiro(); 
            botoesTabuleiro = new Button[8, 8]; 
            jogadorAtual = EnumCor.Branco; 
            GerarTabuleiro();
        }

        private void GerarTabuleiro()
        {
            int tamanhoCasa = 60; 
            for (int linha = 0; linha < 8; linha++)
            {
                for (int coluna = 0; coluna < 8; coluna++)
                {
                    Button btn = new Button
                    {
                        Size = new System.Drawing.Size(tamanhoCasa, tamanhoCasa),
                        Location = new System.Drawing.Point(coluna * tamanhoCasa, linha * tamanhoCasa),
                        Tag = new Tuple<int, int>(linha, coluna) 
                    };

                    if ((linha + coluna) % 2 == 0)
                    {
                        btn.BackColor = System.Drawing.Color.White; 
                    }
                    else
                    {
                        btn.BackColor = System.Drawing.Color.Gray; 
                    }

                    btn.Click += BotaoTabuleiro_Click;

                    botoesTabuleiro[linha, coluna] = btn;

                    this.Controls.Add(btn);

                    AtualizarBotaoComPeca(btn, linha, coluna);
                }
            }
        }
        private void BotaoTabuleiro_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var coordenadas = (Tuple<int, int>)btn.Tag;
            int linha = coordenadas.Item1;
            int coluna = coordenadas.Item2;

            Peca? pecaNaCasa = tabuleiro.Tab[linha, coluna]; 

            if (pecaNaCasa != null)
            {
                if (pecaNaCasa.Cor == jogadorAtual)
                {
                    if (pecaSelecionada == null || pecaSelecionada != pecaNaCasa)
                    {
                        pecaSelecionada = pecaNaCasa;
                        MessageBox.Show($"Você selecionou a peça: {pecaNaCasa.GetType().Name} na posição {linha}, {coluna}.");
                    }
                    else
                    {
                        pecaSelecionada = null;
                        MessageBox.Show("Peça desmarcada.");
                    }
                }
                else
                {
                    MessageBox.Show("Você não pode mover a peça do adversário.");
                }
            }
            else if (pecaSelecionada != null)
            {
                if (pecaSelecionada.MovimentoValido(linha, coluna, tabuleiro.Tab))
                {
                    tabuleiro.MoverPeca(pecaSelecionada, linha, coluna);
                    MessageBox.Show($"Movimento realizado para {linha}, {coluna}.");
                    AlternarTurno();
                }
                else
                {
                    MessageBox.Show("Movimento inválido.");
                }

                AtualizarTabuleiro();
                pecaSelecionada = null;
            }
        }
        private void AtualizarTabuleiro()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    AtualizarBotaoComPeca(botoesTabuleiro[i, j], i, j);
                }
            }
        }

        private void AtualizarBotaoComPeca(Button btn, int linha, int coluna)
        {
            Peca? peca = tabuleiro.Tab[linha, coluna];

            if (peca != null)
            {
                btn.Text = peca.GetType().Name[0].ToString();
            }
            else
            {
                btn.Text = string.Empty; 
            }
        }

        private void AlternarTurno()
        {
            jogadorAtual = jogadorAtual == EnumCor.Branco ? EnumCor.Preto : EnumCor.Branco;
        }
    }
}
