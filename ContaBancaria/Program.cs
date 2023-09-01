using ContaBancaria.Model;
using System;

namespace ContaBancaria
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int opcao;
            Conta conta01 = new Conta(1, 4123, 1, "Robson Alves Rocha", 10000000.00M);
            Conta conta02 = new Conta(2, 4123, 2, "Robson Alves Rocha", 50000000.00M);

            ContaPoupanca contaPoupanca01 = new ContaPoupanca(1, 4123, 2, "Robson Alves Rocha", 10000000.00M, 22);
            ContaPoupanca contaPoupanca02 = new ContaPoupanca(2, 4123, 2, "Robson Alves Rocha", 50000000.00M, 11);

            ContaCorrente contaCorrente01 = new ContaCorrente(1, 4123, 1, "Robson Alves Rocha", 10000000.00M, 5000M);
            ContaCorrente contaCorrente02 = new ContaCorrente(2, 4123, 1, "Robson Alves Rocha", 50000000.00M, 15000M);

            while (true)
            {
                Menu();

                opcao = Convert.ToInt32(Console.ReadLine());

                if(opcao == 0)
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
                        KeyPress();
                        break;
                    case 2:
                        Console.WriteLine("Listar todas as contas\n\n");
                        Console.Clear();
                        conta01.VisualizarConta();
                        Console.WriteLine("");
                        conta02.VisualizarConta();
                        Console.WriteLine("");
                        contaPoupanca01.VisualizarConta();
                        Console.WriteLine("");
                        contaPoupanca02.VisualizarConta();
                        Console.WriteLine("");
                        contaCorrente01.VisualizarConta();
                        Console.WriteLine("");
                        contaCorrente02.VisualizarConta();
                        KeyPress();
                        break;
                    case 3:
                        Console.WriteLine("Consultar dados da conta - por número\n\n"); 
                        KeyPress();
                        break;
                    case 4:
                        Console.WriteLine("Atualizar dados da conta\n\n");
                        KeyPress();
                        break;
                    case 5:
                        Console.WriteLine("Apagar a conta\n\n");
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
                        Console.WriteLine("\nSeu saldo é de: " + conta01.getSaldo().ToString("C"));
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
            Console.WriteLine("$            9 - Exibir saldo                                                     $");
            Console.WriteLine("$            0 - Sair                                                             $");
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

        static void Sobre() {
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