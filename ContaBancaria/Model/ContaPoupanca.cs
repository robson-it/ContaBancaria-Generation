using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria.Model
{
    internal class ContaPoupanca : Conta
    {

        int aniversario;

        public ContaPoupanca(int numero, int agencia, int tipo, string titular, decimal saldo, int aniversario)
            : base(numero, agencia, tipo, titular, saldo)
        {
            this.aniversario = aniversario;
        }

        public void setAniversario(int aniversario) { this.aniversario = aniversario; }
        public decimal getAniversario() { return aniversario; }

        public override void VisualizarConta()
        {
            base.VisualizarConta();
            Console.WriteLine($"    Dia aniversário: {this.getAniversario()}                                     ");
            Console.WriteLine("                                                                                  ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                                                                  ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
           
        }

    }
}
