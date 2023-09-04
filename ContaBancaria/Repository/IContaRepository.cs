using ContaBancaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria.Repository
{
    public interface IContaRepository
    {

        //Métodos Crud
        public void ProcurarPorNumero(int numero);
        public void ListarTodasContas();
        public void Cadastrar(Conta conta);
        public void Atualizar(Conta conta);
        public void Deletar(int numero);

        //Métodos Bancários
        public void Sacar(int numeroConta, decimal valorSaque);
        public void Depositar(int numeroConta, decimal valorDeposito);
        public void Transferir(int numeroContaOrigem, int numeroContaDestino, decimal valorTransferencia);


    }
}
