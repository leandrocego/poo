public class Cavalo : Peca
{
    public Cavalo(EnumCor cor, int linha, int coluna) : base(cor, linha, coluna) { }

    public override bool MovimentoValido(int linhaDestino, int colunaDestino, Peca[,] tabuleiro)
    {
        int deltaLinha = Math.Abs(linhaDestino - Linha);
        int deltaColuna = Math.Abs(colunaDestino - Coluna);

        return (deltaLinha == 2 && deltaColuna == 1) || (deltaLinha == 1 && deltaColuna == 2);
    }
}
    