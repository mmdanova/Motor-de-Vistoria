namespace AutoCheck.ConsoleApp.Models
{
  public class ItemVistoria
  {
    public string Nome { get; set; }
    public string Status { get; set; }

    public ItemVistoria(string nome, string status)
    {
      Nome = nome;
      Status = status;
    }

    public int ObterPontuacao()
    {
      int pontuacao = 0;

      if (Status.Equals("bom", StringComparison.CurrentCultureIgnoreCase))
      {
        pontuacao = 10;
      }
      else if (Status.Equals("regular", StringComparison.CurrentCultureIgnoreCase))
      {
        pontuacao = 5;
      }
      else if (Status.Equals("ruim", StringComparison.CurrentCultureIgnoreCase))
      {
        pontuacao = 0;
      }
      return pontuacao;
    }
  }
}