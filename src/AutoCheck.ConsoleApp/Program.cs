using AutoCheck.ConsoleApp.Models;
using AutoCheck.ConsoleApp.Services;

List<Veiculo> Vistorias = new List<Veiculo>();
MotorVistoria Motor = new MotorVistoria();

bool executando = true;

while (executando)
{
  Console.WriteLine("==================================================");
  Console.WriteLine("    AUTOCHECK.NET - MOTOR DE VISTORIA VEICULAR    ");
  Console.WriteLine("==================================================");
  Console.WriteLine("1. Realizar Nova Vistoria");
  Console.WriteLine("2. Exibir Relatório das Vistorias");
  Console.WriteLine("0. Sair");
  Console.WriteLine("==================================================");
  Console.Write("Escolha uma opção: ");

  string opcao = Console.ReadLine()?.Trim() ?? "";

  switch (opcao)
  {
    case "1":
      CadastrarVistoria(Vistorias);
      break;
    case "2":
      ExibirRelatorios(Vistorias, Motor);
      break;
    case "0":
      executando = false;
      Console.WriteLine("\nEncerrando a aplicação!");
      break;
    default:
      Console.WriteLine("\n[Opção Inválida! Pressione ENTER para tentar novamente]");
      Console.ReadLine();
      break;
  }
}


void CadastrarVistoria(List<Veiculo> vistorias)
{
  Console.Clear();
  Console.WriteLine("=== NOVA VISTORIA VEICULAR ===");
  Console.WriteLine("Selecione o Tipo de Veículo:");
  Console.WriteLine("1. Carro");
  Console.WriteLine("2. Moto");
  Console.WriteLine("3. Caminhão");
  Console.Write("Opção: ");
  string tipoOpcao = Console.ReadLine()?.Trim() ?? "";

  if (tipoOpcao != "1" && tipoOpcao != "2" && tipoOpcao != "3")
  {
    Console.WriteLine("Tipo de veículo inválido! Operação cancelada.");
    PressionarTeclaParaContinuar();
    return;
  }

  Console.Write("Marca: ");
  string marca = Console.ReadLine() ?? "Genérica";

  Console.Write("Modelo: ");
  string modelo = Console.ReadLine() ?? "Padrão";

  Console.Write("Ano: ");
  int.TryParse(Console.ReadLine(), out int ano);

  Console.Write("Quilometragem: ");
  int.TryParse(Console.ReadLine(), out int km);

  Veiculo veiculo = null;

  // Carro
  if (tipoOpcao == "1")
  {
    Console.Write("Quantidade de Portas: ");
    int.TryParse(Console.ReadLine(), out int portas);
    if (portas <= 0)
    {
      // Definindo valor padrão caso a entrada seja inválida
      portas = 4;
    }
    veiculo = new Carro(marca, modelo, ano, km, portas);
  }
  // Moto
  else if (tipoOpcao == "2")
  {
    Console.Write("Cilindradas (cc): ");
    int.TryParse(Console.ReadLine(), out int cilindradas);
    // Definindo valor padrão caso a entrada seja inválida
    if (cilindradas <= 0)
    {
      cilindradas = 150;
    }
    veiculo = new Moto(marca, modelo, ano, km, cilindradas);
  }
  // Caminhão
  else if (tipoOpcao == "3")
  {
    Console.Write("Quantidade de Eixos: ");
    int.TryParse(Console.ReadLine(), out int eixos);
    // Definindo valor padrão caso a entrada seja inválida
    if (eixos <= 0)
    {
      eixos = 2;
    }

    Console.Write("Capacidade de Carga (Toneladas): ");
    double.TryParse(Console.ReadLine(), out double carga);

    //  Definindo valor padrão caso a entrada seja inválida
    if (carga <= 0)
    {
      carga = 10.0;
    }
    veiculo = new Caminhao(marca, modelo, ano, km, eixos, carga);
  }

  // Checklist 
  List<string> checklist = veiculo.ObterChecklistObrigatorio();

  Console.WriteLine("\n--- INÍCIO DA AVALIAÇÃO DOS ITENS ---");
  Console.WriteLine("Informe o status para cada item: [1] Bom | [2] Regular | [3] Ruim");

  foreach (string itemNome in checklist)
  {
    string status = "";
    while (status != "bom" && status != "regular" && status != "ruim")
    {
      Console.Write($"Item '{itemNome}': ");
      string stOpcao = Console.ReadLine()?.Trim() ?? "";

      if (stOpcao == "1" || stOpcao.Equals("bom", StringComparison.OrdinalIgnoreCase)) status = "bom";
      else if (stOpcao == "2" || stOpcao.Equals("regular", StringComparison.OrdinalIgnoreCase)) status = "regular";
      else if (stOpcao == "3" || stOpcao.Equals("ruim", StringComparison.OrdinalIgnoreCase)) status = "ruim";
      else Console.WriteLine("  --> Status inválido. Digite 1 (Bom), 2 (Regular) ou 3 (Ruim).");
    }

    veiculo.AdicionarItemVistoriado(itemNome, status);
  }

  vistorias.Add(veiculo);

  Console.WriteLine("\n✔ Vistoria cadastrada com sucesso!");
  PressionarTeclaParaContinuar();
}

void ExibirRelatorios(List<Veiculo> vistorias, MotorVistoria motor)
{
  Console.Clear();
  Console.WriteLine("==================================================");
  Console.WriteLine("          RELATÓRIO GERAL DE VISTORIAS            ");
  Console.WriteLine("==================================================");

  if (vistorias.Count == 0)
  {
    Console.WriteLine("\nNenhuma vistoria realizada até o momento.");
  }
  else
  {
    for (int i = 0; i < vistorias.Count; i++)
    {
      motor.ProcessarEExibirVistoria(vistorias[i], i + 1, vistorias.Count);
    }
    Console.WriteLine("==================================================");
    Console.WriteLine("         FIM DO PROCESSAMENTO DE VISTORIAS        ");
    Console.WriteLine("==================================================");
  }

  PressionarTeclaParaContinuar();
}

void PressionarTeclaParaContinuar()
{
  Console.WriteLine("\nPressione ENTER para voltar ao menu...");
  Console.ReadLine();
  Console.Clear();
}