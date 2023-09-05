using ContaBancaria.Controller;
using ContaBancaria.Model;
using System;
using System.ComponentModel.Design;

namespace ContaBancaria
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int opcao, numeroAgencia, tipoConta, aniversario, numero;
            string? titular;
            decimal saldoConta, limiteConta;

            ContaController contas = new ContaController();
            //Conta conta01 = new Conta(1, 4123, 1, "Robson Alves Rocha", 10000000.00M);
            //Conta conta02 = new Conta(2, 4123, 2, "Robson Alves Rocha", 50000000.00M);

            ContaPoupanca contaPoupanca01 = new ContaPoupanca(contas.GerarNumero(), 4123, 2, "Robson Alves Rocha", 10000000.00M, 22);
            contas.Cadastrar(contaPoupanca01);

            ContaPoupanca contaPoupanca02 = new ContaPoupanca(contas.GerarNumero(), 4123, 2, "Robson Alves Rocha", 50000000.00M, 11);
            contas.Cadastrar(contaPoupanca02);

            ContaCorrente contaCorrente01 = new ContaCorrente(1, 4123, 1, "Robson Alves Rocha", 10000000.00M, 5000M);
            ContaCorrente contaCorrente02 = new ContaCorrente(2, 4123, 1, "Robson Alves Rocha", 50000000.00M, 15000M);

            while (true)
            {
                Menu();

                try
                {
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Digite um valor inteiro entre 1 e 9");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    opcao = 0;
                }
                

                if (opcao == 9)
                {
                    Console.Clear();
                    Console.WriteLine("\n#CASHTAG BANK - CUIDANDO DO SEU DINHEIRO COMO SE FOSSE NOSSO!");
                    Sobre();
                    Console.ResetColor();
                    System.Environment.Exit(0);
                }

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("Nova conta\n\n");

                        Console.WriteLine("Digite o número agência: ");
                        numeroAgencia = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Digite o nome do titular: ");
                        titular = Console.ReadLine();
                        titular ??= string.Empty;

                        do
                        {
                            Console.WriteLine("Digite o tipo da conta [1]Corrente [2]Poupança: ");
                            tipoConta = Convert.ToInt32(Console.ReadLine());
                        } while (tipoConta != 1 & tipoConta != 2);

                        Console.WriteLine("Digite o saldo da conta: ");
                        saldoConta = Convert.ToDecimal(Console.ReadLine());
                        switch (tipoConta)
                        {
                            case 1:
                                Console.WriteLine("Digite o limite da conta: ");
                                limiteConta = Convert.ToDecimal(Console.ReadLine());
                                contas.Cadastrar(new ContaCorrente(contas.GerarNumero(), numeroAgencia, tipoConta, titular, saldoConta, limiteConta));
                                break;
                            case 2:
                                Console.WriteLine("Digite o dia do aniversário da conta: ");
                                aniversario = Convert.ToInt32(Console.ReadLine());
                                contas.Cadastrar(new ContaPoupanca(contas.GerarNumero(), numeroAgencia, tipoConta, titular, saldoConta, aniversario));
                                break;
                        }



                        KeyPress();
                        break;
                    case 2:
                        Console.WriteLine("Listar todas as contas\n\n");
                        Console.Clear();
                        contas.ListarTodasContas();
                        KeyPress();
                        break;
                    case 3:
                        Console.WriteLine("Consultar dados da conta - por número\n\n");
                        do
                        {
                            Console.WriteLine("Digite o número da conta: ");
                            try
                            {
                                numero = Convert.ToInt32(Console.ReadLine());
                                contas.ProcurarPorNumero(numero);
                                break;
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Digite um número de conta válido!");
                                KeyPress();
                            }
                        } while (true);
                        KeyPress();
                        break;
                    case 4:
                        Console.WriteLine("Atualizar dados da conta\n\n");

                        Console.WriteLine("Digite o número conta: ");
                        numero = Convert.ToInt32(Console.ReadLine());

                        var conta = contas.BuscarNaCollection(numero);

                        if (conta is not null)
                        {
                            Console.WriteLine("Digite o número agência: ");
                            numeroAgencia = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Digite o nome do titular: ");
                            titular = Console.ReadLine();
                            titular ??= string.Empty;

                            tipoConta = conta.getTipo();

                            Console.WriteLine("Digite o saldo da conta: ");
                            saldoConta = Convert.ToDecimal(Console.ReadLine());
                            switch (tipoConta)
                            {
                                case 1:
                                    Console.WriteLine("Digite o limite da conta: ");
                                    limiteConta = Convert.ToDecimal(Console.ReadLine());
                                    contas.Atualizar(new ContaCorrente(numero, numeroAgencia, tipoConta, titular, saldoConta, limiteConta));
                                    break;
                                case 2:
                                    Console.WriteLine("Digite o dia do aniversário da conta: ");
                                    aniversario = Convert.ToInt32(Console.ReadLine());
                                    contas.Atualizar(new ContaPoupanca(numero, numeroAgencia, tipoConta, titular, saldoConta, aniversario));
                                    break;
                            }

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"A conta {numero} não foi encontrada!");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }

                        

                        KeyPress();
                        break;
                    case 5:
                        Console.WriteLine("Apagar a conta\n\n");
                        do
                        {
                            Console.WriteLine("Digite o número da conta: ");
                            try
                            {
                                numero = Convert.ToInt32(Console.ReadLine());
                                contas.Deletar(numero);
                                break;
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Digite um número de conta válido!");
                            }
                        } while (true);
                        KeyPress();
                        break;
                    case 6:
                        Console.WriteLine("Saque\n\n");
                        KeyPress();
                        break;
                    case 7:
                        Console.WriteLine("Depósito\n\n");
                        KeyPress();
                        break;
                    case 8:
                        Console.WriteLine("Transferência entre Contas\n\n");
                        KeyPress();
                        break;
                    case 9:

                        KeyPress();
                        break;
                    default:
                        Console.WriteLine("\nOpção Inválida!\n");
                        KeyPress();
                        break;
                }
            }

        }

        private static ConsoleKeyInfo consoleKeyInfo;
        static void Menu()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                                                                   ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("""$             _  _    _____           _____ _    _ _______       _____            $""");
            Console.WriteLine("""$           _| || |_ / ____|   /\    / ____| |  | |__   __|/\   / ____|           $""");
            Console.WriteLine("""$          |_  __  _| |       /  \  | (___ | |__| |  | |  /  \ | |  __            $""");
            Console.WriteLine("""$           _| || |_| |      / /\ \  \___ \|  __  |  | | / /\ \| | |_ |           $""");
            Console.WriteLine("""$          |_  __  _| |____ / ____ \ ____) | |  | |  | |/ ____ \ |__| |           $""");
            Console.WriteLine("""$            |_||_|  \_____/_/    \_\_____/|_|  |_|  |_/_/    \_\_____|           $""");
            Console.WriteLine("""$                                                                                 $""");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                                     -- BANK --                                    ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("$                                                                                 $");
            Console.WriteLine("$            1 - Criar Conta                                                      $");
            Console.WriteLine("$            2 - Listar todas as Contas                                           $");
            Console.WriteLine("$            3 - Buscar Conta por Numero                                          $");
            Console.WriteLine("$            4 - Atualizar Dados da Conta                                         $");
            Console.WriteLine("$            5 - Apagar Conta                                                     $");
            Console.WriteLine("$            6 - Sacar                                                            $");
            Console.WriteLine("$            7 - Depositar                                                        $");
            Console.WriteLine("$            8 - Transferir valores entre Contas                                  $");
            //Console.WriteLine("$            9 - Exibir saldo                                                     $");
            Console.WriteLine("$            9 - Sair                                                             $");
            Console.WriteLine("$                                                                                 $");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                                                                   ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                                                                   ");
            Console.WriteLine("Entre com a opção desejada:                                                        ");
            Console.WriteLine("                                                                                   ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        static void Sobre()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                                                                   ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("$   Projeto Desenvolvido por:                                                     $");
            Console.WriteLine("$   Robson Alves Rocha - robsonrocha.dev@gmail.com                                $");
            Console.WriteLine("$   https://github.com/robson-it                                                  $");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                                                                   ");
            Console.ResetColor();
        }

        static void KeyPress()
        {
            do
            {
                Console.Write("\nPressione Enter para Continuar...");
                consoleKeyInfo = Console.ReadKey();
            } while (consoleKeyInfo.Key != ConsoleKey.Enter);
        }
    }
}