//using System.Reflection.Metadata.Ecma335;

namespace ByteBank1
{

    public class Program
    {

        static void ShowMenu()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("         Menu Principal");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1 - Inserir novo Cliente");
            Console.WriteLine("2 - Deletar um Cliente");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um Cliente");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta (Validação por Senha Requerida)");
            Console.WriteLine("0 - Para sair do programa");
            Console.WriteLine("----------------------------------");
            Console.Write("Digite a opção desejada: ");
        }

        static void ShowMenuConta()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("     Menu Transações de Conta");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1 - Depositar");
            Console.WriteLine("2 - Transferir");
            Console.WriteLine("3 - Sacar");
            Console.WriteLine("0 - Logout e Retornar Menu Anterior");
            Console.WriteLine("----------------------------------");
            Console.Write("Digite a opção desejada: ");
        }

        static void MenuConta(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            int option;
            do
            {
                ShowMenuConta();
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 0:
                        Console.Clear();
                        break;
                    case 1:
                        Console.Clear();
                        DepositarValor(cpfs, senhas, saldos);
                        break;
                    case 2:
                        Console.Clear();
                        TransferirValor(cpfs, senhas, saldos);
                        break;
                    case 3:
                        Console.Clear();
                        SacarValor(cpfs, senhas, saldos);
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpção Inválida!! Selecione uma das opções abaixo!\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            } while (option != 0);
        }

        static int BuscaCPF (List<string> cpfs, string cpfToFind)
        {
            int indexCPF = cpfs.FindIndex(cpf => cpf == cpfToFind);
            return indexCPF;
        }
        static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("    Cadastro de Novo Cliente");
            Console.WriteLine("----------------------------------");
            Console.Write("Digite o CPF (apenas números): ");
            string cpf = Console.ReadLine();
            cpfs.Add(cpf);
            Console.Write("Digite o Nome do Cliente: ");
            titulares.Add(Console.ReadLine().ToUpper());
            Console.Write("Digite a senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
            Console.WriteLine("\nConta criada com Sucesso!\n");
            ApresentaConta(BuscaCPF(cpfs, cpf), cpfs, titulares, saldos);
            Console.WriteLine();
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o CPF para excluir: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = BuscaCPF(cpfs, cpfParaDeletar);
            if (indexParaDeletar == -1)
            {
                Console.WriteLine("\nNão foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.\n");
            }
            else if (indexParaDeletar != -1 && saldos[indexParaDeletar] != 0)
            {
                Console.WriteLine("\nNão foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: Conta com Saldo Disponível.\n");
            }
            else
            {
                cpfs.Remove(cpfParaDeletar);
                titulares.RemoveAt(indexParaDeletar);
                senhas.RemoveAt(indexParaDeletar);
                saldos.RemoveAt(indexParaDeletar);
                Console.WriteLine("\nConta deletada com sucesso\n");
            }
        }

        static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("  Listagem de Contas de Clientes");
            Console.WriteLine("----------------------------------");
            for (int i = 0; i < cpfs.Count; i++)
            {
                ApresentaConta(i, cpfs, titulares, saldos);
            }
            Console.WriteLine("----------------------------------\n");
        }

        static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("   Detalhe de Conta de Cliente");
            Console.WriteLine("----------------------------------");
            Console.Write("Digite o CPF: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaApresentar = BuscaCPF(cpfs, cpfParaApresentar);
            if (indexParaApresentar == -1)
            {
                Console.WriteLine("\nNão foi possível apresentar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.\n");
                return;
            }
            Console.WriteLine("----------------------------------");
            ApresentaConta(indexParaApresentar, cpfs, titulares, saldos);
            Console.WriteLine("----------------------------------\n");
        }

        static void ApresentarValorAcumulado(List<double> saldos)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine(" Relatório Financeiro - Depósitos");
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Total acumulado no banco: R$ {saldos.Sum():F2}");
            Console.WriteLine("----------------------------------\n");
        }

        static void ApresentaConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R$ {saldos[index]:F2}");
        }

        static bool DepositarValor(List<string> cpfs, List<string> senhas, List<double> saldos, string cpfDepositar = "", double valorDeposito = 0)
        {
            bool ctlMsg = false;
            if (cpfDepositar == "")
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine("        Depósito em Conta");
                Console.WriteLine("----------------------------------");
                Console.Write("Informe o CPF para Depósito de Valor: ");
                cpfDepositar = Console.ReadLine();
                ctlMsg = true;
            }
            int indexCpfDepositar = BuscaCPF(cpfs, cpfDepositar);
            if (indexCpfDepositar == -1)
            {
                Console.WriteLine("\nNão foi possível executar esta Operação");
                Console.WriteLine("MOTIVO: Conta não encontrada.\n");
            }
            else
            {
                if (valorDeposito == 0d)
                {
                    Console.Write("Informe o Valor a ser Depositado: R$ ");
                    valorDeposito = double.Parse(Console.ReadLine());
                }
                if (valorDeposito > 0d)
                {
                    saldos[indexCpfDepositar] += valorDeposito;
                    if (ctlMsg == true)
                    {
                        Console.WriteLine("\nDepósito realizado com Sucesso!\n");
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("\nNão foi possível realizar esta Operação");
                    Console.WriteLine("MOTIVO: Valor de depósito inválido!\n");
                }
            }
            return false;
        }

        static void TransferirValor(List<string> cpfs, List<string> senhas, List<double> saldos)
        {
            bool ctlTransactionSaque = false;
            bool ctlTransactionDeposito = false;
            Console.WriteLine("----------------------------------");
            Console.WriteLine("   Transferência entre Contas");
            Console.WriteLine("----------------------------------");
            Console.Write("Informe o CPF de Saque para Transferência de Valor: ");
            string cpfSacar = Console.ReadLine();
            int indexCpfSacar = BuscaCPF(cpfs, cpfSacar);
            if (indexCpfSacar == -1) 
            {
                Console.WriteLine("\nNão foi possível executar esta Operação");
                Console.WriteLine("MOTIVO: Conta de Origem NÃO encontrada.\n");
                return;
            }
            Console.Write("Informe o CPF de Destino da Transferência de Valor: ");
            string cpfDepositar = Console.ReadLine();
            int indexCpfDepositar = BuscaCPF(cpfs, cpfDepositar);
            if (indexCpfDepositar == -1)
            {
                Console.WriteLine("\nNão foi possível executar esta Operação");
                Console.WriteLine("MOTIVO: Conta de Destino NÃO encontrada.\n");
                return;
            }
            Console.Write("Informe o valor da Transferência: R$ ");
            double valorTransferencia = double.Parse(Console.ReadLine());
            ctlTransactionSaque = SacarValor(cpfs, senhas, saldos, cpfSacar, valorTransferencia);
            if (ctlTransactionSaque)
            {
                ctlTransactionDeposito = DepositarValor(cpfs, senhas, saldos, cpfDepositar, valorTransferencia);
                if (ctlTransactionDeposito == false)
                {
                    DepositarValor(cpfs, senhas, saldos, cpfSacar, valorTransferencia);
                }
            } 
            if (ctlTransactionDeposito == true && ctlTransactionSaque == true)
            {
                Console.WriteLine("\nTransferência Realizada com Sucesso!\n");
            }
        }

        static bool SacarValor(List<string> cpfs, List<string> senhas, List<double> saldos, string cpfSacar = "", double valorSaque = 0)
        {
            bool ctlMsg = false;
            if (cpfSacar == "")
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine("         Saque em Conta");
                Console.WriteLine("----------------------------------");
                Console.Write("Informe o CPF para Saque de Valor: ");
                cpfSacar = Console.ReadLine();
                ctlMsg = true;
            }
            int indexCpfSacar = BuscaCPF(cpfs, cpfSacar);

            if (indexCpfSacar == -1)
            {
                Console.WriteLine("\nNão foi possível executar esta Operação");
                Console.WriteLine("MOTIVO: Conta não encontrada.\n");
            }
            else
            {
                if (valorSaque == 0d)
                {
                    Console.Write("Informe o Valor a ser Sacado: R$ ");
                    valorSaque = double.Parse(Console.ReadLine());
                }
                if (ValidaSenha(cpfs, senhas, cpfSacar))
                {
                    if (valorSaque > 0d && valorSaque <= saldos[indexCpfSacar])
                    {
                        saldos[indexCpfSacar] -= valorSaque;
                        if (ctlMsg == true)
                        {
                            Console.WriteLine("\nSaque Realizado com Sucesso!\n");
                        }
                        return true;
                    }
                    else if (valorSaque <= 0)
                    {
                        Console.WriteLine("\nNão foi possível executar esta Operação");
                        Console.WriteLine("Valor de Saque inválido!\n");
                    }
                    else
                    {
                        Console.WriteLine("\nNão foi possível executar esta Operação");
                        Console.WriteLine("Saldo Insuficiente!\n");
                    }
                }
            }
            return false;
        }

        static bool ValidaSenha(List<string> cpfs, List<string> senhas, string cpf = "")
        {
            int ctlTentativas = 1;
            if (cpf == "")
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine("    Login em Conta de Cliente");
                Console.WriteLine("----------------------------------");
                Console.Write("Informe o CPF para acessar as operações de Conta ou Digite '0' para retornar: ");
                cpf = Console.ReadLine();
            }
            int cpfCheck = BuscaCPF(cpfs, cpf);
            if ( cpfCheck == -1 && cpf != "0")
            {
                Console.WriteLine("Não foi possível executar esta Operação");
                Console.WriteLine("MOTIVO: Conta não encontrada, tente novamente!");
                return false;
            }
            while (ctlTentativas <= 3 && cpf != "0")
            {
                Console.WriteLine("----------------------------------");
                Console.Write("Informe a senha para continuar: ");
                string senhaCheck = Console.ReadLine();
                Console.WriteLine("----------------------------------");
                if (senhaCheck != senhas[cpfCheck] && ctlTentativas < 3)
                {
                    Console.WriteLine($"Senha incorreta! Restam {3 - ctlTentativas} tentativas!");
                }
                else if (senhaCheck == senhas[cpfCheck])
                {
                    return true;
                }
                ctlTentativas += 1;
            }
            if (cpf != "0")
            {
                Console.WriteLine($"Senha incorreta! Número de tentativas ESGOTADAS!");
            }
            return false;
        }

        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            
            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            int option;

            do
            {
                ShowMenu();
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 0:
                        Console.WriteLine("\nEncerrando o programa... Obrigado por utilizar o ByteBank!!\n");
                        break;
                    case 1:
                        Console.Clear();
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        Console.Clear();
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        Console.Clear();
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        Console.Clear();
                        ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        Console.Clear();
                        ApresentarValorAcumulado(saldos);
                        break;
                    case 6:
                        Console.Clear();
                        if (ValidaSenha(cpfs, senhas))
                        {
                            MenuConta(cpfs, titulares, senhas, saldos);
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpção Inválida!! Selecione uma das opções abaixo!\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

            } while (option != 0);

        }

    }

}