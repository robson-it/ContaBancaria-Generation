using ContaBancaria.Controller;
using ContaBancaria.Model;
using System;
using System.ComponentModel.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContaBancaria
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int opcao, numeroAgencia, tipoConta, aniversario, numero, numeroDestino;
            string? titular;
            decimal saldoConta, limiteConta, valor;

            //Criando uma instância da classe ContaController na variável contas. 
            ContaController contas = new ContaController();

            //Cadastrando contas fictícias +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ContaPoupanca contaPoupanca01 = new ContaPoupanca(contas.GerarNumero(), 4123, 2, "Robson Alves Rocha", 10000000.00M, 22);
            contas.Cadastrar(contaPoupanca01);

            ContaPoupanca contaPoupanca02 = new ContaPoupanca(contas.GerarNumero(), 4123, 2, "Robson Alves Rocha", 50000000.00M, 11);
            contas.Cadastrar(contaPoupanca02);

            ContaCorrente contaCorrente01 = new ContaCorrente(contas.GerarNumero(), 4123, 1, "Robson Alves Rocha", 10000000.00M, 5000M);
            contas.Cadastrar(contaCorrente01);

            ContaCorrente contaCorrente02 = new ContaCorrente(contas.GerarNumero(), 4123, 1, "Robson Alves Rocha", 50000000.00M, 15000M);
            contas.Cadastrar(contaCorrente02);
            //Cadastrando contas fictícias  FIM ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // Inicio do loop que exibe o menue realiza a operação selecionada
            while (true)
            {

                //Chama o método Menu, que exibe o menu na tela.
                Menu();

                //Leitura da opção e tratamento de exceção
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


                //Finaliza o programa caso a opção digitada seja 9.
                if (opcao == 9)
                {
                    Console.Clear();
                    Console.WriteLine("\n#CASHTAG BANK - CUIDANDO DO SEU DINHEIRO COMO SE FOSSE NOSSO!");
                    Sobre();
                    Console.ResetColor();
                    System.Environment.Exit(0);
                }

                //Switch para a opção selecionada.
                switch (opcao)
                {
                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Cadastrar nova conta ++++++++++++++++++++++++++++++++++++++++++++++++
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\nNova conta\n");
                        numeroAgencia = ValidaNumeroAgencia("novaConta");
                        if (numeroAgencia < 0)
                        {
                            break;
                        }
                        Console.WriteLine("Digite o nome do titular: ");
                        titular = Console.ReadLine();
                        titular ??= string.Empty;

                        do
                        {
                            Console.WriteLine("Digite o tipo da conta [1]Corrente [2]Poupança: ");
                            tipoConta = Convert.ToInt32(Console.ReadLine());
                        } while (tipoConta != 1 & tipoConta != 2);

                        Console.WriteLine("[Saldo da conta]");
                        saldoConta = ValidaValorDecimal("novo");

                        if (saldoConta < 0)
                        {
                            break;
                        }

                        switch (tipoConta)
                        {
                            case 1:
                                Console.WriteLine("[Limite da conta]");
                                limiteConta = ValidaValorDecimal("novo");
                                if (limiteConta < 0)
                                    {
                                        break;
                                    }
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
                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Fim Cadastrar nova conta +++++++++++++++++++++++++++++++++++++++++++++++++

                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Listar todas as contas ++++++++++++++++++++++++++++++++++++++++++++++++++
                    case 2:
                        Console.Clear();
                        Console.WriteLine("\nListar todas as contas\n");
                        contas.ListarTodasContas();
                        KeyPress();
                        break;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Fim Listar todas as contas ++++++++++++++++++++++++++++++++++++++++++++++

                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Consultar dados de uma conta por número +++++++++++++++++++++++++++++++++
                    case 3:
                        Console.Clear();
                        Console.WriteLine("\nConsultar dados da conta - por número\n");

                        numero = ValidaNumeroConta();
                        if (numero > 0)
                        {
                            Console.Clear();
                            contas.ProcurarPorNumero(numero);
                        }
                        else if (numero < 0)
                        {
                            break;
                        }
                        KeyPress();
                        break;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Fim Consultar dados de uma conta por número ++++++++++++++++++++++++++++++

                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Atualizar dados de uma conta existente +++++++++++++++++++++++++++++++++++
                    case 4:
                        Console.Clear();
                        Console.WriteLine("\nAtualizar dados da conta\n");
                        numero = ValidaNumeroConta();
                        Conta conta = null;
                        if (numero > 0)
                        {
                            conta = contas.BuscarNaCollection(numero);
                        }

                        if (conta is not null)
                        {
                            numeroAgencia = ValidaNumeroAgencia("existente", conta);
                            if (numeroAgencia < 0)
                            {
                                break;
                            }

                            Console.WriteLine("Digite o nome do titular: ");
                            titular = Console.ReadLine();
                            titular ??= string.Empty;

                            tipoConta = conta.getTipo();
                            
                            Console.WriteLine("[Saldo da conta]");
                            saldoConta = ValidaValorDecimal("saldoExistente", conta);
                            if (saldoConta < 0)
                            {
                                break;
                            }
                            switch (tipoConta)
                            {
                                case 1:
                                    Console.WriteLine("[Limite da conta]");
                                    limiteConta = ValidaValorDecimal("limiteExistente", conta);
                                    if (limiteConta < 0)
                                    {
                                        break;
                                    }
                                    contas.Atualizar(new ContaCorrente(numero, numeroAgencia, tipoConta, titular, saldoConta, limiteConta));
                                    break;
                                case 2:
                                    Console.WriteLine("Digite o dia do aniversário da conta: ");
                                    aniversario = Convert.ToInt32(Console.ReadLine());
                                    contas.Atualizar(new ContaPoupanca(numero, numeroAgencia, tipoConta, titular, saldoConta, aniversario));
                                    break;
                            }

                        }
                        else if (numero < 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\nA conta {numero} não foi encontrada!");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }


                        KeyPress();
                        break;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Fim Atualizar dados de uma conta existente +++++++++++++++++++++++++++++++

                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Deletar uma conta existente ++++++++++++++++++++++++++++++++++++++++++++++
                    case 5:
                        Console.WriteLine("Apagar a conta\n\n");
                        numero = ValidaNumeroConta();
                        if (numero < 0)
                        {
                            break;
                        }
                        else
                        {
                            contas.Deletar(numero);
                        }
                        KeyPress();
                        break;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Fim Deletar uma conta existente ++++++++++++++++++++++++++++++++++++++++++

                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Realizar um saque ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    case 6:
                        Console.WriteLine("Saque\n\n");

                        numero = ValidaNumeroConta();
                        if (numero < 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("[Valor do saque]");
                            valor = ValidaValorDecimal("novo");
                            Console.Clear();
                            if(valor == 0) {
                                Console.WriteLine("Tente novamente com um valor maior que R$ 0.00");
                                KeyPress();
                            }else if (valor < 0)
                            {
                                break;
                            }
                            else
                            {
                                contas.Sacar(numero, valor);
                                KeyPress();
                                Console.Clear();
                                break;
                            }
                            break; 
                        }
                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Fim Realizar um saque ++++++++++++++++++++++++++++++++++++++++++++++++++++

                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Realizar um depósito +++++++++++++++++++++++++++++++++++++++++++++++++++++
                    case 7:
                        Console.WriteLine("Depósito\n\n");
                        numero = ValidaNumeroConta();
                        if (numero < 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("[Valor do depósito]");
                            valor = ValidaValorDecimal("novo");
                            Console.Clear();

                            if (valor == 0)
                            {
                                Console.WriteLine("Tente novamente com um valor maior que R$ 0.00");
                                KeyPress();
                            }
                            else if (valor < 0)
                            {
                                break;
                            }
                            else
                            {
                                contas.Depositar(numero, valor);
                                KeyPress();
                                Console.Clear();
                                break;
                            }
                            break;

                        }

                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Fim Realizar um depósito +++++++++++++++++++++++++++++++++++++++++++++++++

                    case 8:
                        Console.WriteLine("\nTransferência entre Contas\n");

                        Console.WriteLine("Conta de origem: ");
                        numero = ValidaNumeroConta();
                        if (numero < 0)
                        {
                            break;
                        }
                        Console.WriteLine("Conta de destino: ");
                        numeroDestino = ValidaNumeroConta();
                        if (numeroDestino < 0)
                        {
                            break;
                        }
                        else
                        {

                            Console.WriteLine("[Valor da transferência]");
                            valor = ValidaValorDecimal("novo");
                            Console.Clear();

                            if (valor == 0)
                            {
                                Console.WriteLine("Tente novamente com um valor maior que R$ 0.00");
                                KeyPress();
                            }
                            else if (valor < 0)
                            {
                                break;
                            }
                            else
                            {
                                contas.Transferir(numero, numeroDestino, valor);
                                KeyPress();
                                Console.Clear();
                                break;
                            }
                            break;
                        }

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

        //*********************************************************************************************************************
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

        //*********************************************************************************************************************
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

        //*********************************************************************************************************************
        static int ValidaNumeroConta()
        {
            var numeroConta = 0;
            do
            {
                try
                {
                    Console.WriteLine("Digite o número da conta: ");
                    numeroConta = Convert.ToInt32(Console.ReadLine());
                    if (numeroConta > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("O número da conta não pode ser 0!");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Número inválido! [Enter] Tentar novamente | \"C\" Cancelar operação:");
                    if (Console.ReadLine().ToUpper().Equals("C"))
                    {
                        numeroConta = -1;
                        CancelarOperacao();
                        break;
                    }
                }
            } while (true);


            return numeroConta;
        }

        //*********************************************************************************************************************
        static int ValidaNumeroAgencia(string tipo, Conta conta = null)
        {
            var numeroAgencia = 0;

            if (tipo.Equals("existente"))
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Digite o número da agência: ");
                        numeroAgencia = Convert.ToInt32(Console.ReadLine());
                        if (numeroAgencia == 0)
                        {
                            Console.WriteLine("O número da agência não pode ser 0, digite novamente!");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Número inválido! [Enter] Tentar novamente | \"M\" Manter agência atual | \"C\" Cancelar operação:");
                        var opcao = Console.ReadLine();
                        if (opcao.ToUpper().Equals("M"))
                        {
                            numeroAgencia = conta.getAgencia();
                        }
                        else if (opcao.ToUpper().Equals("C"))
                        {
                            numeroAgencia = -1;
                            CancelarOperacao();
                        }

                    }

                } while (numeroAgencia == 0);

            }
            else if (tipo.Equals("novaConta"))
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Digite o número da agência: ");
                        numeroAgencia = Convert.ToInt32(Console.ReadLine());
                        if (numeroAgencia == 0)
                        {
                            Console.WriteLine("O número da agência não pode ser 0, digite novamente!");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Número inválido! [Enter] Tentar novamente | \"C\" Cancelar operação:");
                        var opcao = Console.ReadLine();
                        if (opcao.ToUpper().Equals("C"))
                        {
                            numeroAgencia = -1;
                            CancelarOperacao();
                        }

                    }

                } while (numeroAgencia == 0);
            }

            return numeroAgencia;
        }

        //*********************************************************************************************************************
        static decimal ValidaValorDecimal(string tipo, Conta conta = null)
        {
            decimal valorDecimal;
            if (tipo == "novo")
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Digite o valor R$: ");
                        valorDecimal = Convert.ToDecimal(Console.ReadLine());
                        if (valorDecimal < 0)
                        {
                            Console.WriteLine("O valor precisa ser maior que R$ 0.00");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Valor R$ inválido! [Enter] Tentar novamente | \"C\" Cancelar operação:");
                        if (Console.ReadLine().ToUpper().Equals("C"))
                        {
                            valorDecimal = -1;
                            CancelarOperacao();
                            break;
                        }
                    }
                } while (true);
            }
            else
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Digite o valor R$: ");
                        valorDecimal = Convert.ToDecimal(Console.ReadLine());
                        if (valorDecimal < 0)
                        {
                            Console.WriteLine("O valor precisa ser maior que R$ 0.00");
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                    catch (Exception e)
                    {
                        if (tipo.Equals("saldoExistente"))
                        {
                            Console.WriteLine("Valor R$ inválido! [Enter] Tentar novamente | \"M\" Manter valor atual | \"C\" Cancelar operação: ");
                        }
                        else {
                            Console.WriteLine("Valor R$ inválido! [Enter] Tentar novamente | \"C\" Cancelar operação: ");
                        }
                        
                        string opcao = Console.ReadLine();
                        if (opcao.ToUpper().Equals("C"))
                        {
                            valorDecimal = -1;
                            CancelarOperacao();
                            break;
                        } else if (opcao.ToUpper().Equals("M"))
                        {
                            if (tipo.Equals("saldoExistente"))
                            {
                                valorDecimal = conta.getSaldo();
                                break;
                            }
                            else if (tipo.Equals("limiteExistente"))
                            {
                                Console.WriteLine("É obrigatório digitar o limite!");
                            }
 
                        } 
                    }
                } while (true);
            }


            return valorDecimal;
        }


        //*********************************************************************************************************************
        static void CancelarOperacao()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nOperação cancelada com sucesso!");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            KeyPress();
        }
        //*********************************************************************************************************************

    }
}