using ContaBancaria.Model;
using ContaBancaria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public void Cadastrar(Conta conta)
        {
            listaContas.Add(conta);
            Console.WriteLine($"A conta número {conta.getNumero()} foi criada com sucesso!");
        }

        public void Deletar(int numero)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
    }
}
