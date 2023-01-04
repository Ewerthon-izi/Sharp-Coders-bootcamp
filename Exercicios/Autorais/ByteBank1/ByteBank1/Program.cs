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
            do{
                Console.Write("telefone: ");
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

        static void ShowMenuBeforeLogin()
        {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Login");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }

        static void ShowMenuAfterLogin() {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Detalhes de um usuário");
            Console.WriteLine("4 - Todos usuarios cadastrados");
            Console.WriteLine("5 - transferencia bancaria");
            Console.WriteLine("6 - Depositar");
            Console.WriteLine("7 - Sacar");
            Console.WriteLine("8 - Sair da conta");
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

            string cpfAux1, cpfAux2, email, password;
            double valorTAux;
            int option;
            User objectAux, currentUser = null;

            do
            {
                ShowMenuBeforeLogin();
                option = int.Parse(Console.ReadLine());
                Console.WriteLine("-----------------");

                switch(option)
                {
                    case 0:
                        Console.WriteLine("Estou encerrando o programa...");
                        break;
                    case 1:
                        usuarios.Add(new User());
                        break;
                    case 2:
                        Console.Write("email: ");
                        email = Console.ReadLine();
                        Console.Write("password: ");
                        password = Console.ReadLine();
                        //Shalow copy, caso eu altere o currentUser ira alterar o usurio original tbm
                        currentUser = (User)usuarios.FirstOrDefault(x => x.email == email);
                        if (currentUser == null)
                            Console.WriteLine("Email ou senha invalida");
                        else
                        {
                            if (currentUser.Login(email, password))
                                Console.WriteLine("Login efetuado com sucesso");
                            else {
                                Console.WriteLine("Email ou senha invalida");
                                currentUser = null;
                            }
                        }
                        break;
                    default: 
                        Console.WriteLine("Opção digitada invalida");
                        break;

                }

                while(currentUser != null && option != 0) {
                    ShowMenuAfterLogin();
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
                            objectAux = (User)usuarios.FirstOrDefault(x => x.cpf == cpfAux1);
                            if (objectAux != null)
                                Console.WriteLine(objectAux);
                            else
                                Console.WriteLine("Usuario não encontrado");
                            break;
                        case 4:
                            foreach (User i in usuarios)
                                Console.WriteLine(i);
                            break;
                        case 5:
                            Console.Write("Digite o cpf do usuario a receber a transferencia: ");
                            cpfAux2 = Console.ReadLine();
                            Console.Write("Digite o valor da transferencia: ");
                            valorTAux = double.Parse(Console.ReadLine());
                            objectAux = (User)usuarios.FirstOrDefault(x => x.cpf == cpfAux2);
                            if (objectAux == null) {
                                Console.WriteLine("Destinatario não encontrado");
                                break;
                            }
                            Console.WriteLine(Tranferencia(currentUser, objectAux, valorTAux));
                            break;
                        case 6: 
                            Console.Write("Valor a ser adicionado: ");
                            valorTAux = double.Parse(Console.ReadLine());
                            currentUser.AddSaldo(valorTAux);
                            break;
                        case 7:
                            Console.Write("Digite o valor do saque: ");
                            valorTAux = double.Parse(Console.ReadLine());
                            if (currentUser.retirarSaldo(valorTAux))
                                Console.WriteLine("Saque realizado com sucesso");
                            else
                                Console.WriteLine("Saldo insuficiente");
                            break;
                        case 8:
                            currentUser = null;
                            break;
                        default:
                            Console.WriteLine("Codigo digitado não encontrado");
                            break;
                    }

                    Console.WriteLine("-----------------");

                }
                Console.Clear();
            } while (currentUser == null && option != 0);

        }

    }

}