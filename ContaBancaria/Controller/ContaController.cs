using ContaBancaria.Model;
using ContaBancaria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria.Controller
{
    public class ContaController : IContaRepository
    {

        private readonly List<Conta> listaContas = new List<Conta>();
        private readonly List<String> operacoesRealizadas = new List<string>();
        int numero = 0;

        public void Atualizar(Conta conta)
        {
            var buscaConta = BuscarNaCollection(conta.getNumero());
            if (buscaConta is not null)
            {
                var index = listaContas.IndexOf(buscaConta);
                listaContas[index] = conta;
                operacoesRealizadas.Add($"[CONTA ATUALIZADA]:\n " +
                                    $"Conta:{conta.getNumero()} |\n " +
                                    $"Agência:{conta.getAgencia()} |\n " +
                                    "Tipo:" + ((conta.getTipo() == 1) ? "Conta Corrente" : "Conta Poupança") + " |\n " +
                                    $"Titular:{conta.getTitular()} |\n " +
                                    $"Saldo:{conta.getSaldo()} |\n " +
                                    "" + ((conta.getTipo() == 1) ? "Limite:" + conta.getLimite() : "Aniversário:" + conta.getAniversarioConta())+ "\n\n");
                Console.Clear();
                Console.WriteLine($"A conta {conta.getNumero()} foi atualizada com sucesso!");
            }
            else
            {
                operacoesRealizadas.Add("[TENTATIVA DE ATUALIZAR CONTA SEM SUCESSO]\n\n");
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta {numero} não foi encontrada!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }

        public void Cadastrar(Conta conta)
        {
            listaContas.Add(conta);
            operacoesRealizadas.Add($"[CONTA CADASTRADA]:\n " +
                                    $"Conta:{conta.getNumero()} |\n " +
                                    $"Agência:{conta.getAgencia()} |\n " +
                                    "Tipo:" + ((conta.getTipo() == 1) ? "Conta Corrente" : "Conta Poupança") + " |\n " +
                                    $"Titular:{conta.getTitular()} |\n " +
                                    $"Saldo:{conta.getSaldo()} |\n " +
                                    "" + ((conta.getTipo() == 1) ? "Limite:" + conta.getLimite() : "Aniversário:" + conta.getAniversarioConta()) + "\n\n");
            Console.WriteLine($"A conta número {conta.getNumero()} foi criada com sucesso!");
        }

        public void Deletar(int numero)
        {
            var conta = BuscarNaCollection(numero);
            if (conta is not null)
            {
                if (listaContas.Remove(conta))
                {
                    operacoesRealizadas.Add($"[CONTA DELETADA]:\n " +
                                    $"Conta:{conta.getNumero()} |\n " +
                                    $"Agência:{conta.getAgencia()} |\n " +
                                    "Tipo:" + ((conta.getTipo() == 1) ? "Conta Corrente" : "Conta Poupança") + " |\n " +
                                    $"Titular:{conta.getTitular()} |\n " +
                                    $"Saldo:{conta.getSaldo()} |\n " +
                                    "" + ((conta.getTipo() == 1) ? "Limite:" + conta.getLimite() : "Aniversário:" + conta.getAniversarioConta()) + "\n\n");
                    Console.Clear();
                    Console.WriteLine($"\nA conta {conta.getNumero()} foi removida com sucesso!");
                };
            }
            else
            {
                operacoesRealizadas.Add("[TENTATIVA DE DELETAR CONTA SEM SUCESSO]\n\n");
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nA conta {numero} não foi encontrada!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }

        public void Depositar(int numeroConta, decimal valorDeposito)
        {
            var conta = BuscarNaCollection(numeroConta);

            if (conta is not null)
            {
                operacoesRealizadas.Add($"[DEPÓSITO NA CONTA]:\n " +
                                    $"Conta:{conta.getNumero()} |\n " +
                                    $"Agência:{conta.getAgencia()} |\n " +
                                    "Tipo:" + ((conta.getTipo() == 1) ? "Conta Corrente" : "Conta Poupança") + " |\n " +
                                    $"Titular:{conta.getTitular()} |\n " +
                                    $"Saldo:{conta.getSaldo()} + {valorDeposito.ToString("C")} => {conta.getSaldo() + valorDeposito} |\n " +
                                    "" + ((conta.getTipo() == 1) ? "Limite:" + conta.getLimite() : "Aniversário:" + conta.getAniversarioConta()) + "\n\n");

                conta.Depositar(valorDeposito);
                
                Console.WriteLine($"Depósito na conta {numeroConta} realizado com sucesso!");
            }
            else
            {
                operacoesRealizadas.Add("[TENTATIVA DE DEPÓSITO SEM SUCESSO]\n\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta {numero} não foi encontrada!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }

        public void ListarTodasContas()
        {
            foreach (var conta in listaContas)
            {
                conta.VisualizarConta();
                operacoesRealizadas.Add("[LISTAR TODAS AS CONTAS]\n\n");
            }
        }

        public void ListarOperacoesRealizadas()
        {
            operacoesRealizadas.Add("[LISTAR OPERAÇÕES REALIZADAS]\n\n");
            foreach (var operacao in operacoesRealizadas)
            {
                
                Console.WriteLine(operacao);
            }
        }

        public void ProcurarPorNumero(int numero)
        {
            var conta = BuscarNaCollection(numero);
            operacoesRealizadas.Add("[PROCURAR CONTA POR NÚMERO]\n\n");
            if (conta is not null)
            {
                conta.VisualizarConta();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta {numero} não foi encontrada!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }

        public void Sacar(int numeroConta, decimal valorSaque)
        {
            var conta = BuscarNaCollection(numeroConta);
            
            if(conta is not null)
            {
                operacoesRealizadas.Add($"[SAQUE NA CONTA]:\n " +
                                    $"Conta:{conta.getNumero()} |\n " +
                                    $"Agência:{conta.getAgencia()} |\n " +
                                    "Tipo:" + ((conta.getTipo() == 1) ? "Conta Corrente" : "Conta Poupança") + " |\n " +
                                    $"Titular:{conta.getTitular()} |\n " +
                                    $"Saldo:{conta.getSaldo()} - {valorSaque.ToString("C")} => {conta.getSaldo() - valorSaque} |\n " +
                                    "" + ((conta.getTipo() == 1) ? "Limite:" + conta.getLimite() : "Aniversário:" + conta.getAniversarioConta()) + "\n\n");
                Console.WriteLine((conta.Sacar(valorSaque))? $"Saque na conta {numeroConta} realizado com sucesso!":"");
            }
            else
            {
                operacoesRealizadas.Add("[TENTATIVA DE SAQUE SEM SUCESSO]\n\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta {numero} não foi encontrada!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }

        public void Transferir(int numeroContaOrigem, int numeroContaDestino, decimal valorTransferencia)
        {
            var contaOrigem = BuscarNaCollection(numeroContaOrigem);
            var contaDestino = BuscarNaCollection(numeroContaDestino);

            if(contaOrigem is not null && contaDestino is not null)
            {
                if (contaOrigem.Sacar(valorTransferencia))
                {
                    operacoesRealizadas.Add($"[TRANSFERÊNCIA ENTRE CONTAS]:\n " +
                                    $"Conta Origem: {contaOrigem.getNumero()} | Conta Destino: {contaDestino.getNumero()} \n " +
                                    $"Agência Origem: {contaOrigem.getAgencia()} | Agência Destino:{contaDestino.getAgencia()}\n " +
                                    "Origem: " + ((contaOrigem.getTipo() == 1) ? "Conta Corrente" : "Conta Poupança") + " | " +
                                    "Destino: " + ((contaDestino.getTipo() == 1) ? "Conta Corrente" : "Conta Poupança") + " |\n " +
                                    $"Titular de Origem: {contaOrigem.getTitular()} | Titular de Destino: {contaDestino.getTitular()}\n " +
                                    $"Saldo de Origem: {contaOrigem.getSaldo() + valorTransferencia} - {valorTransferencia.ToString("C")} | " +
                                    $"Saldo de Destino: {contaDestino.getSaldo()} + {valorTransferencia.ToString("C")} |\n " +
                                    "" + ((contaOrigem.getTipo() == 1) ? "Limite Origem: " + contaOrigem.getLimite() : "Aniversário Origem: " + contaOrigem.getAniversarioConta()) +
                                    " | " + ((contaDestino.getTipo() == 1) ? "Limite Destino: " + contaDestino.getLimite() : "Aniversário Destino: " + contaDestino.getAniversarioConta()) +
                                    "\n\n");
                    contaDestino.Depositar(valorTransferencia);
                    Console.WriteLine($"A transferência da conta {numeroContaOrigem} para a conta {numeroContaDestino} realizada com sucesso! {valorTransferencia.ToString("C")}");
                }
            }
            else
            {
                operacoesRealizadas.Add("[TENTATIVA DE TRANSFERÊNCIA SEM SUCESSO]\n\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine((BuscarNaCollection(numeroContaOrigem) is not null) ? $"A conta de origem [{numeroContaOrigem}] não foi encontrada!" : "");
                Console.WriteLine((BuscarNaCollection(numeroContaDestino) is not null) ? $"A conta de destino [{numeroContaDestino}] não foi encontrada!" : "");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }

        //Métodos Auxiliares

        public int GerarNumero()
        {
            return ++numero;
        }

        //Método para buscar um objeto conta através do número
        public Conta? BuscarNaCollection(int numero)
        {
            foreach(var conta in listaContas)
            {
                if (conta.getNumero() == numero)
                    return conta;
                
            }
            return null;
        }

        public void ListarContasPorTitular(string titular)
        {
            var contasPorTitular = (from conta in listaContas
                                    where conta.getTitular().Contains(titular)
                                    select conta).ToList();
            contasPorTitular.ForEach(conta => conta.VisualizarConta());
            if(contasPorTitular.Count() == 0)
            {
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nenhuma conta foi encontrada contendo o titular digitado!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            };
        }

         
    }
}
