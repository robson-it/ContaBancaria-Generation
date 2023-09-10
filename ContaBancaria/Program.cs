using ContaBancaria.Controller;
using ContaBancaria.Model;
using System;
using System.ComponentModel.Design;
using System.Data;
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

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[NÚMERO DA AGÊNCIA]");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        numeroAgencia = ValidaNumeroAgencia("novaConta");


                        if (numeroAgencia < 0)
                        {
                            break;
                        }

                        string manter = "";
                        do
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n[NOME DO TITULAR]");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            titular = Console.ReadLine();
                            titular ??= string.Empty;

                            if (titular.Equals(string.Empty))
                            {

                                do
                                {
                                    Console.WriteLine("Nenhum nome foi digitado, deseja prosseguir assim mesmo? [S] Sim [N] Não, digitar novamente.");
                                    manter = Console.ReadLine();
                                    if (!manter.ToUpper().Equals("N") && !manter.ToUpper().Equals("S"))
                                    {
                                        Console.WriteLine("Selecione uma opção válida!");
                                    }
                                } while (!manter.ToUpper().Equals("N") && !manter.ToUpper().Equals("S"));
                            }
                            else
                            {
                                manter = "S";
                            }
                        } while (manter.ToUpper().Equals("N"));

                        tipoConta = ValidaTipoConta();
                        if (tipoConta < 0)
                        {
                            break;
                        }

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[SALDO INICIAL]");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        saldoConta = ValidaValorDecimal("novo");

                        if (saldoConta < 0)
                        {
                            break;
                        }

                        switch (tipoConta)
                        {
                            case 1:
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n[LIMITE DA CONTA]");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                limiteConta = ValidaValorDecimal("novo");
                                if (limiteConta < 0)
                                {
                                    break;
                                }
                                contas.Cadastrar(new ContaCorrente(contas.GerarNumero(), numeroAgencia, tipoConta, titular, saldoConta, limiteConta));
                                KeyPress();
                                break;
                            case 2:
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n[ANIVERSÁRIO DA CONTA]");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                aniversario = ValidaAniversarioConta("novo");
                                if (aniversario < 1)
                                {
                                    break;
                                }
                                contas.Cadastrar(new ContaPoupanca(contas.GerarNumero(), numeroAgencia, tipoConta, titular, saldoConta, aniversario));
                                KeyPress();
                                break;
                        }

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

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[NÚMERO DA CONTA]");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
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

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[NÚMERO DA CONTA]");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        numero = ValidaNumeroConta();


                        Conta conta = null;
                        if (numero > 0)
                        {
                            conta = contas.BuscarNaCollection(numero);
                        }

                        if (conta is not null)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n[NÚMERO DA AGÊNCIA]");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            numeroAgencia = ValidaNumeroAgencia("existente", conta);

                            if (numeroAgencia < 0)
                            {
                                break;
                            }


                            do
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n[NOME DO TITULAR]");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                titular = Console.ReadLine();
                                titular ??= string.Empty;

                                if (titular.Equals(string.Empty))
                                {

                                    do
                                    {
                                        Console.WriteLine("Nenhum nome foi digitado, deseja manter o titular anterior? [S] Sim [N] Não, digitar novamente.");
                                        manter = Console.ReadLine();
                                        if (!manter.ToUpper().Equals("N") && !manter.ToUpper().Equals("S"))
                                        {
                                            Console.WriteLine("Selecione uma opção válida!");
                                        } else if (manter.ToUpper().Equals("S"))
                                        {
                                            titular = conta.getTitular();
                                        }
                                    } while (!manter.ToUpper().Equals("N") && !manter.ToUpper().Equals("S"));
                                }
                                else
                                {
                                    manter = "S";
                                }
                            } while (manter.ToUpper().Equals("N"));


                            tipoConta = conta.getTipo();

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n[SALDO DA CONTA]");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            saldoConta = ValidaValorDecimal("saldoExistente", conta);
                            if (saldoConta < 0)
                            {
                                break;
                            }

                            switch (tipoConta)
                            {
                                case 1:
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\n[LIMITE DA CONTA]");
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    limiteConta = ValidaValorDecimal("limiteExistente", conta);
                                    if (limiteConta < 0)
                                    {
                                        break;
                                    }
                                    contas.Atualizar(new ContaCorrente(numero, numeroAgencia, tipoConta, titular, saldoConta, limiteConta));
                                    break;
                                case 2:
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\n[ANIVERSÁRIO DA CONTA]");
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    aniversario = ValidaAniversarioConta("existente", conta);
                                    if (aniversario < 1)
                                    {
                                        break;
                                    }
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
                        Console.Clear();
                        Console.WriteLine("Apagar a conta\n\n");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[NÚMERO DA CONTA]");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
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
                        Console.Clear();
                        Console.WriteLine("Saque");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[NÚMERO DA CONTA]");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;

                        numero = ValidaNumeroConta();
                        if (numero < 0)
                        {
                            break;
                        }
                        else
                        {
                            conta = contas.BuscarNaCollection(numero);
                            if (conta is not null)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n[VALOR DO SAQUE]");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
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
                                    contas.Sacar(numero, valor);
                                    KeyPress();
                                    Console.Clear();
                                    break;
                                }
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"A conta {numero} não foi encontrada!");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                KeyPress();
                                break;
                            }

                        }
                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Fim Realizar um saque ++++++++++++++++++++++++++++++++++++++++++++++++++++

                    //+++++++++++++++++++++++++++++++++++++++++++++++++ Realizar um depósito +++++++++++++++++++++++++++++++++++++++++++++++++++++
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Depósito");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[NÚMERO DA CONTA]");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        numero = ValidaNumeroConta();
                        if (numero < 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n[VALOR DO DEPÓSITO]");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
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

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[CONTA ORIGEM]");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        numero = ValidaNumeroConta();
                        if (numero < 0)
                        {
                            break;
                        }
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[CONTA DESTINO]");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        numeroDestino = ValidaNumeroConta();
                        if (numeroDestino < 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n[VALOR DA TRANSFERÊNCIA]");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
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

                    case 0:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("                                                                                  ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("                              ::Operações Realizadas::                            ");
                        Console.WriteLine($"                                                                                 ");
                        contas.ListarOperacoesRealizadas();
                        Console.WriteLine("                                                                                  ");
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("                                                                                  ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"                                                                                 ");
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
            Console.WriteLine("$            0 - Listar Operações realizadas                                      $");
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
                    Console.WriteLine("\nDigite o número da conta: ");
                    numeroConta = Convert.ToInt32(Console.ReadLine());
                    if (numeroConta > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nO número da conta não pode ser 0!");
                    }

                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("\nNúmero inválido! [Enter] Tentar novamente | \"C\" Cancelar operação:");
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

                        Console.WriteLine("\nDigite o número da agência: ");
                        numeroAgencia = Convert.ToInt32(Console.ReadLine());
                        if (numeroAgencia <= 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\nO número da agência deve ser maior que 0, digite novamente!");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("\nNúmero inválido! [Enter] Tentar novamente | \"M\" Manter agência atual | \"C\" Cancelar operação:");
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

                } while (numeroAgencia <= 0);

            }
            else if (tipo.Equals("novaConta"))
            {
                do
                {
                    try
                    {
                        Console.WriteLine("\nDigite o número da agência: ");
                        numeroAgencia = Convert.ToInt32(Console.ReadLine());
                        if (numeroAgencia <= 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\nO número da agência deve ser maior que 0, digite novamente!");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("\nNúmero inválido! [Enter] Tentar novamente | \"C\" Cancelar operação:");
                        var opcao = Console.ReadLine();
                        if (opcao.ToUpper().Equals("C"))
                        {
                            numeroAgencia = -1;
                            CancelarOperacao();

                            break;
                        }

                    }

                } while (numeroAgencia <= 0);
            }

            return numeroAgencia;
        }

        static int ValidaAniversarioConta(string tipo, Conta conta = null)
        {
            int aniversarioConta;
            if (tipo.Equals("novo"))
            {
                do
                {
                    try
                    {
                        Console.WriteLine("\nDigite o dia de aniversário da conta: ");
                        aniversarioConta = Convert.ToInt32(Console.ReadLine());
                        if (aniversarioConta < 1 || aniversarioConta > 30)
                        {
                            Console.Clear();
                            Console.WriteLine("\nO dia do aniversário da conta deve estar entre 1 e 30, digite novamente!");
                        }

                    }
                    catch (Exception e)
                    {

                        Console.Clear();
                        Console.WriteLine("\nDígito inválido! [Enter] Tentar novamente | \"C\" Cancelar operação:");
                        var opcao = Console.ReadLine();
                        if (opcao.ToUpper().Equals("C"))
                        {
                            aniversarioConta = -1;
                            CancelarOperacao();

                            break;
                        }
                        aniversarioConta = -1;

                    }
                } while (aniversarioConta < 1 || aniversarioConta > 30);

            }
            else if (tipo == "existente")
            {
                do
                {
                    try
                    {
                        Console.WriteLine("\nDigite o dia de aniversário da conta: ");
                        aniversarioConta = Convert.ToInt32(Console.ReadLine());
                        if (aniversarioConta < 1 || aniversarioConta > 30)
                        {
                            Console.Clear();
                            Console.WriteLine("\nO dia do aniversário da conta deve estar entre 1 e 30, digite novamente!");
                        }

                    }
                    catch (Exception e)
                    {

                        Console.Clear();
                        Console.WriteLine("\nDígito inválido! [Enter] Tentar novamente | \"M\" Manter agência atual | \"C\" Cancelar operação:");
                        var opcao = Console.ReadLine();
                        if (opcao.ToUpper().Equals("C"))
                        {
                            aniversarioConta = -1;
                            CancelarOperacao();

                            break;
                        }
                        if (opcao.ToUpper().Equals("M"))
                        {
                            aniversarioConta = conta.getAniversarioConta();
                            break;
                        }
                        aniversarioConta = -1;

                    }
                } while (aniversarioConta < 1 || aniversarioConta > 30);

            }
            else
            {
                aniversarioConta = 0;
            }
            return aniversarioConta;
        }

        static int ValidaTipoConta()
        {
            int tipoConta;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\nDigite o tipo da conta [1]Corrente [2]Poupança: ");
                    tipoConta = Convert.ToInt32(Console.ReadLine());
                    if (tipoConta != 1 && tipoConta != 2)
                    {
                        Console.Clear();
                        Console.WriteLine("\nDigite um número válido para o tipo de conta!");
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("\nEntrada inválida! [Enter] Tentar novamente | \"C\" Cancelar operação:");
                    if (Console.ReadLine().ToUpper().Equals("C"))
                    {
                        tipoConta = -1;
                        CancelarOperacao();

                        break;
                    }
                    tipoConta = 0;
                }
            } while (tipoConta != 1 && tipoConta != 2);
            return tipoConta;
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

                        Console.WriteLine("\nDigite o valor R$: ");
                        valorDecimal = Convert.ToDecimal(Console.ReadLine());
                        if (valorDecimal < 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\nO valor precisa ser maior que R$ 0.00");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("\nValor R$ inválido! [Enter] Tentar novamente | \"C\" Cancelar operação:");
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
                        Console.WriteLine("\nDigite o valor R$: ");
                        valorDecimal = Convert.ToDecimal(Console.ReadLine());
                        if (valorDecimal < 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\nO valor precisa ser maior que R$ 0.00");
                        }
                        else
                        {
                            break;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("\nValor R$ inválido! [Enter] Tentar novamente | \"M\" Manter valor atual | \"C\" Cancelar operação: ");
                        string opcao = Console.ReadLine();
                        if (opcao.ToUpper().Equals("C"))
                        {
                            valorDecimal = -1;
                            CancelarOperacao();

                            break;
                        }
                        else if (opcao.ToUpper().Equals("M"))
                        {
                            if (tipo.Equals("saldoExistente"))
                            {
                                valorDecimal = conta.getSaldo();
                                break;
                            }
                            else if (tipo.Equals("limiteExistente"))
                            {
                                valorDecimal = conta.getLimite();
                                break;
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