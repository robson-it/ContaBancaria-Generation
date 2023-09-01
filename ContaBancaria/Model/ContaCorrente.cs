using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria.Model
{
    public class ContaCorrente : Conta
    {

        decimal limite;

        public ContaCorrente(int numero, int agencia, int tipo, string titular, decimal saldo, decimal limite) 
            : base(numero, agencia, tipo, titular, saldo)
        {
            this.limite = limite;
        }

        public void setLimite(decimal limite) { this.limite = limite; }
        public decimal getLimite() { return limite; }

        public override bool Sacar(decimal valorSaque)
        {
            if ((this.limite + getSaldo()) < valorSaque)
            {
                Console.WriteLine("Saldo insuficiente!");
                return false;
            }
            setSaldo(valorSaque - getSaldo());
            return true;
        }


        public override void VisualizarConta() {
            base.VisualizarConta();
            Console.WriteLine($"    Limite disponível: {this.getLimite().ToString("C")}                          ");
            Console.WriteLine("                                                                                  ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                                                                  ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            
        }
    }
}
