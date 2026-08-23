namespace AutoCheck.ConsoleApp.Models
{
  public class ItemVistoria
  {
    public string Nome { get; set; }
    public string Status { get; set; }

    public ItemVistoria(string nome, string status)
    {
      if (status.ToLower() != "bom" && status.ToLower() != "regular" && status.ToLower() != "ruim")
      {
        Console.WriteLine("Status inválido. Utilize apenas: 'Bom', 'Regular' ou 'Ruim'.");
      }
      Nome = nome;
      Status = status;
    }

    public int ObterPontuacao()
    {
      int pontuacao = 0;

      if (Status.ToLower() == "bom")
      {
        pontuacao = 10;
      }
      else if (Status.ToLower() == "regular")
      {
        pontuacao = 5;
      }
      else if (Status.ToLower() == "ruim")
      {
        pontuacao = 0;
      }
      return pontuacao;
    }
  }
}