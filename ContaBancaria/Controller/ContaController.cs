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
        int numero = 0;

        public void Atualizar(Conta conta)
        {
            var buscaConta = BuscarNaCollection(conta.getNumero());
            if (buscaConta is not null)
            {
                var index = listaContas.IndexOf(buscaConta);
                listaContas[index] = conta;
                Console.Clear();
                Console.WriteLine($"A conta {conta.getNumero()} foi atualizada com sucesso!");
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta {numero} não foi encontrada!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }

        public void Cadastrar(Conta conta)
        {
            listaContas.Add(conta);
            Console.WriteLine($"A conta número {conta.getNumero()} foi criada com sucesso!");
        }

        public void Deletar(int numero)
        {
            var conta = BuscarNaCollection(numero);
            if (conta is not null)
            {
                if (listaContas.Remove(conta))
                {
                    Console.Clear();
                    Console.WriteLine($"\nA conta {conta.getNumero()} foi removida com sucesso!");
                };
            }
            else
            {
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
                conta.Depositar(valorDeposito);
                Console.WriteLine($"Depósito na conta {numeroConta} realizado com sucesso!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta {numero} não foi encontrada!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }

        public void ListarTodasContas()
        {
            foreach(var conta in listaContas)
            {
                conta.VisualizarConta();
            }
        }

        public void ProcurarPorNumero(int numero)
        {
            var conta = BuscarNaCollection(numero);
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
                Console.WriteLine((conta.Sacar(valorSaque))? $"Saque na conta {numeroConta} realizado com sucesso!":"");
            }
            else
            {
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
                    contaDestino.Depositar(valorTransferencia);
                    Console.WriteLine($"A transferência da conta {numeroContaOrigem} para a conta {numeroContaDestino} realizada com sucesso! {valorTransferencia.ToString("C")}");
                }
            }
            else
            {
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

         
    }
}
