using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria.Model
{
    public class Conta
    {

        private int numero;
        private int agencia;
        private int tipo;
        private string titular = string.Empty;
        private decimal saldo;

        public Conta(int numero, int agencia, int tipo, string titular, decimal saldo)
        {
            this.numero = numero;
            this.agencia = agencia;
            this.tipo = tipo;
            this.titular = titular;
            this.saldo = saldo;
        }

        public int getNumero() { 
            return numero; 
        }
        public int getAgencia() { 
            return agencia; 
        }
        public int getTipo() { 
            return tipo; 
        }
        public string getTitular() { 
            return titular; 
        }
        public decimal getSaldo() { 
            return saldo; 
        }
        public void setNumero(int numero) { 
            this.numero = numero; 
        }
        public void setAgencia(int agencia) { 
            this.agencia = agencia; 
        }
        public void setTipo(int tipo) { 
            this.tipo = tipo; 
        }
        public void setTitular(string titular) { 
            this.titular = titular; 
        }
        public void setSaldo(decimal saldo) { 
            this.saldo = saldo; 
        }

        public bool Sacar(decimal valorSaque) {
            if (saldo < valorSaque) {
                Console.WriteLine("Saldo insuficiente!");
                return false;
            }
            this.setSaldo (valorSaque - saldo);
            return true;
        }

        public bool Depositar(decimal valorDeposito) { 
            if(valorDeposito > 0)
            {
                this.setSaldo(valorDeposito + saldo);
                return true;
            }
            Console.WriteLine("Digite um valor válido!");
            return false;
        }

        public void VisualizarConta() {

            string tipoConta = "";

            switch (this.tipo) {
                case 1:
                    tipoConta = "Conta corrente";
                    break;
                case 2:
                    tipoConta = "Conta poupança";
                    break;
            }

            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                                                                  ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("                                 ::Dados da conta::                               ");
            Console.WriteLine($"                                                                                 ");
            Console.WriteLine($"    Número da conta: {this.numero}                                               ");
            Console.WriteLine($"    Número da agência: {this.agencia}                                            ");
            Console.WriteLine($"    Tipo da conta: {tipoConta}                                                   ");
            Console.WriteLine($"    Titular da conta: {this.titular}                                             ");
            Console.WriteLine($"    Saldo da conta: {this.saldo.ToString("C")}                                   ");
            Console.WriteLine("                                                                                  ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                                                                  ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }

    }
}
