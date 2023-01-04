using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace ByteBank1 {

    
    public class User : userBase
    {
        private bool resultExp;
        public User()
        {
            Console.WriteLine(" Bem vindo\nEntre com seus dados");
            Console.Write("Nome: ");
            this.nome = Console.ReadLine();
            Console.Write("senha: ");
            /* 
             email é muito dificil validar com expressão regular dado as variedades possiveis de email, entao resolvi nao utilizar
             */
            this.password = Console.ReadLine();
            Console.Write("email: ");
            this.email = Console.ReadLine();
            Console.Write("telefone: ");
            do{
                this.phone = Console.ReadLine();
                /*
                Essa expressão permite colocar o telefone com parentes e hifem, porem nao sao obrigatorios, 
                como nao deixei claro como deve ser inserido o telefone resolvi deixar opcional
                Caso fosse utilizado em um banco de dados, o dado deveria ser padronizado
                */
                this.resultExp = Regex.IsMatch(this.phone, "^\\(?(?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\\)? ?(?:[2-8]|9[1-9])[0-9]{3}\\-?[0-9]{4}$");
                if (!this.resultExp)
                    Console.WriteLine("Insira um telefone valido");
            } while (!this.resultExp);
            do {
                Console.Write("cpf: ");
                this.cpf = Console.ReadLine();
                /*
                Mesmo caso do comentario anterior, permite colocar ponto e hifem mas e opcional
                */
                this.resultExp = Regex.IsMatch(this.cpf, "([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})");
                if (!this.resultExp)
                    Console.WriteLine("Insira um CPF valido");
            } while (!this.resultExp);
        }

        public override string ToString()
        {
            return "Nome: " + this.nome + "\nEmail: " + this.email + "\nTelefone: " + this.phone + "\nCPF: " + this.cpf + "\nSaldo: " + this.saldo + "\n-----------------\n";
        }
        
    }

    public class Program {

        static void ShowMenu() {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Detalhes de um usuário");
            Console.WriteLine("4 - Todos usuarios cadastrados");
            Console.WriteLine("5 - transferencia bancaria");
            Console.WriteLine("6 - Depositar");
            Console.WriteLine("7 - Sacar");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }

        static string Tranferencia(User owner, User destiny, double valor)
        {
            if (owner.retirarSaldo(valor))
            {
                destiny.AddSaldo(valor);
                return "Tranferencia realizada com sucesso";
            }
            else
                return "Saldo insuficiente";
        }

        public static void Main(string[] args) {

            Console.WriteLine("Antes de começar a usar, vamos configurar alguns valores: ");

            List<User> usuarios = new List<User>();
            
            string cpfAux1, cpfAux2;
            double valorTAux;
            int option;
            User objectAux1, objectAux2;

            do {
                ShowMenu();
                option = int.Parse(Console.ReadLine());

                Console.WriteLine("-----------------");

                switch (option) {
                    case 0:
                        Console.WriteLine("Estou encerrando o programa...");
                        break;
                    case 1:
                        usuarios.Add(new User());
                        break;
                    case 2:
                        Console.Write("digite o cpf do usuario a deletar: ");
                        cpfAux1 = Console.ReadLine();
                        if (usuarios.RemoveAll(x => x.cpf == cpfAux1) >= 1)
                            Console.WriteLine("Usuario removido com sucesso");
                        else
                            Console.WriteLine("Usuario não encontrado");
                        break;
                    case 3:
                        Console.Write("digite o cpf do usuario a consultar: ");
                        cpfAux1 = Console.ReadLine();
                        objectAux1 = (User)usuarios.FirstOrDefault(x => x.cpf == cpfAux1);
                        if (objectAux1 != null)
                            Console.WriteLine(objectAux1);
                        else
                            Console.WriteLine("Usuario não encontrado");
                        break;
                    case 4:
                        foreach (User i in usuarios)
                            Console.WriteLine(i);
                        break;
                    case 5:
                        //Como o sistema não possui um sistema de login, o cpf do usuario deve ser informado, caso possuisse um login o usuario nao precisaria informar seu cpf
                        Console.Write("Digite o seu cpf: ");
                        cpfAux1 = Console.ReadLine();
                        Console.Write("Digite o cpf do usuario a receber a transferencia: ");
                        cpfAux2 = Console.ReadLine();
                        Console.Write("Digite o valor da transferencia: ");
                        valorTAux = double.Parse(Console.ReadLine());
                        objectAux1 = (User)usuarios.FirstOrDefault(x => x.cpf == cpfAux1);
                        if (objectAux1 == null) {
                            Console.WriteLine("Usuario que fara a transferencia não encontrado");
                            break;
                        }
                        objectAux2 = (User)usuarios.FirstOrDefault(x => x.cpf == cpfAux2);
                        if (objectAux2 == null) {
                            Console.WriteLine("Destinatario não encontrado");
                            break;
                        }
                        Console.WriteLine(Tranferencia(objectAux1, objectAux2, valorTAux));
                        break;
                    case 6:
                        Console.Write("Digite o cpf da conta a ser depositada: ");
                        cpfAux1 = Console.ReadLine();
                        objectAux1 = (User)usuarios.FirstOrDefault(x => x.cpf == cpfAux1);
                        if (objectAux1 == null)
                        {
                            Console.WriteLine("Usuario não encontrado");
                            break;
                        }
                        Console.Write("Valor a ser adicionado: ");
                        valorTAux = double.Parse(Console.ReadLine());
                        objectAux1.AddSaldo(valorTAux);
                        break;
                    case 7:
                        Console.Write("Digite o cpf da conta a ser sacada: ");
                        cpfAux1 = Console.ReadLine();
                        objectAux1 = (User)usuarios.FirstOrDefault(x => x.cpf == cpfAux1);
                        if (objectAux1 == null)
                        {
                            Console.WriteLine("Usuario não encontrado");
                            break;
                        }
                        Console.Write("Digite o valor do saque: ");
                        valorTAux = double.Parse(Console.ReadLine());
                        if (objectAux1.retirarSaldo(valorTAux))
                            Console.WriteLine("Saque realizado com sucesso");
                        else
                            Console.WriteLine("Saldo insuficiente");
                        break;
                    default:
                        Console.WriteLine("Codigo digitado não encontrado");
                        break;
                }

                Console.WriteLine("-----------------");

            } while (option != 0);

        }

    }

}