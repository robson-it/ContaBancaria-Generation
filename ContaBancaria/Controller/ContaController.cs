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
                Console.WriteLine($"A conta {conta.getNumero()} foi atualizada com sucesso!");
            }
            else
            {
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
                    Console.WriteLine($"A conta {conta.getNumero()} foi removida com sucesso!");
                };
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta {numero} não foi encontrada!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }

        public void Depositar(int numeroConta, decimal valorDeposito)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Transferir(int numeroContaOrigem, int numeroContaDestino, decimal valorTransferencia)
        {
            throw new NotImplementedException();
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
