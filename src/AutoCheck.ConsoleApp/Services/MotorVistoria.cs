using AutoCheck.ConsoleApp.Models;

namespace AutoCheck.ConsoleApp.Services
{
    public class MotorVistoria
    {
        public void ProcessarEExibirVistoria(Veiculo veiculo, int indice, int total)
        {
            Console.WriteLine($"==========================================================================");
            Console.WriteLine($"[{indice}/{total}] PROCESSANDO VISTORIA");
            Console.WriteLine($"--------------------------------------------------------------------------");

            // Dados do Veículo
            Console.WriteLine("> DADOS DO VEÍCULO:");
            string tipoVeiculo = veiculo.GetType().Name;
            if (veiculo is Carro c)
            {
                Console.WriteLine($"- Tipo: Carro ({c.Marca})");
                Console.WriteLine($"- Modelo: {c.Modelo}");
                Console.WriteLine($"- Ano: {c.Ano} | Quilometragem: {c.Quilometragem:N0} km");
                Console.WriteLine($"- Atributo Específico: {c.QuantidadePortas} Portas");
            }
            else if (veiculo is Moto m)
            {
                Console.WriteLine($"- Tipo: Moto ({m.Marca})");
                Console.WriteLine($"- Modelo: {m.Modelo}");
                Console.WriteLine($"- Ano: {m.Ano} | Quilometragem: {m.Quilometragem:N0} km");
                Console.WriteLine($"- Atributo Específico: {m.Cilindradas} cc");
            }
            else if (veiculo is Caminhao cam)
            {
                Console.WriteLine($"- Tipo: Caminhão ({cam.Marca})");
                Console.WriteLine($"- Modelo: {cam.Modelo}");
                Console.WriteLine($"- Ano: {cam.Ano} | Quilometragem: {cam.Quilometragem:N0} km");
                Console.WriteLine($"- Atributo Específico: {cam.QuantidadeEixos} Eixos | Cap. Carga: {cam.CapacidadeCargaToneladas:F1} Toneladas");
            }

            Console.WriteLine();
            Console.WriteLine($"> AVALIAÇÃO DOS ITENS INSPECIONADOS ({veiculo.VistoriaRealizada.Count} ITENS):");

            int PontuacaoObtida = 0;
            int PontuacaoMaxima = veiculo.VistoriaRealizada.Count * 10;

            List<ItemVistoria> itensCriticos = new List<ItemVistoria>();
            List<ItemVistoria> itensAtencao = new List<ItemVistoria>();

            foreach (ItemVistoria item in veiculo.VistoriaRealizada)
            {
                int pontos = item.ObterPontuacao();
                PontuacaoObtida += pontos;

                string tag = "[OK]";
                if (item.Status == "regular") tag = "[!]";
                else if (item.Status == "ruim") tag = "[X]";

                // -35 é para alinhar a saída, garantindo que o status e a pontuação fiquem alinhados à direita
                Console.WriteLine($"{tag} {item.Nome,-35} Status: {item.Status} ({pontos} pontos)");

                if (item.Status.Equals("Ruim", StringComparison.CurrentCultureIgnoreCase))
                {
                    itensCriticos.Add(item);
                }
                else if (item.Status.Equals("Regular", StringComparison.CurrentCultureIgnoreCase))
                {
                    itensAtencao.Add(item);
                }
            }

            // Cálculo percentual de aprovação
            double percentual = 0.0;
            if (PontuacaoMaxima > 0)
            {
                percentual = ((double)PontuacaoObtida / PontuacaoMaxima) * 100.0;
            }

            // Classificação final
            string classificacao = "";
            string acao = "";

            // 90% a 100%: Aprovado com Excelência
            if (percentual >= 90.0)
            {
                classificacao = "APROVADO COM EXCELÊNCIA";
                acao = "Liberado para compra/revenda imediata.";
            }
            // 60% a 89%: Aprovado com Apontamentos
            else if (percentual >= 60.0)
            {
                classificacao = "APROVADO COM APONTAMENTOS";
                acao = "Exige desconto na compra para reparos da oficina.";
            }
            // Abaixo de 60%: Reprovado na Vistoria
            else
            {
                classificacao = "REPROVADO NA VISTORIA";
                acao = "Veículo recusado pela concessionária.";
            }

            Console.WriteLine();
            Console.WriteLine("> RESUMO DA PONTUAÇÃO:");
            Console.WriteLine($"- Pontuação Atingida: {PontuacaoObtida} de {PontuacaoMaxima} pontos possíveis");
            Console.WriteLine($"- Percentual de Aprovação: {percentual:F1}%");
            Console.WriteLine($"- Classificação Final: [{classificacao}]");
            Console.WriteLine($"- Parecer: {acao}");

            // Relatório de Pendências e Apontamentos, e Recomendações
            Console.WriteLine();
            Console.WriteLine("> RELATÓRIO DE MANUTENÇÃO E RECOMENDAÇÕES DA OFICINA:");

            if (itensCriticos.Count == 0 && itensAtencao.Count == 0)
            {
                Console.WriteLine("✔ Nenhuma pendência mecânica identificada. Veículo liberado para operação!");
            }
            else
            {
                if (itensCriticos.Count > 0)
                {
                    Console.WriteLine("✖ ITENS CRÍTICOS / REPROVADOS (AÇÃO IMEDIATA):");
                    foreach (ItemVistoria item in itensCriticos)
                    {
                        Console.WriteLine($"  - {item.Nome}: Substituição/reparo urgente obrigatório antes do uso.");
                    }
                }

                if (itensAtencao.Count > 0)
                {
                    Console.WriteLine("⚠ ITENS DE ATENÇÃO (REVISÃO PREVENTIVA):");
                    foreach (ItemVistoria item in itensAtencao)
                    {
                        Console.WriteLine($"  - {item.Nome}: Realizar regulagem, limpeza e checagem preventiva.");
                    }
                }
            }
            Console.WriteLine();
        }
    }

}