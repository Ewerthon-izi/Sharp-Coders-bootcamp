using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

namespace ByteBank1 {

    
    public class User : userBase
    {
        public User()
        {
            Console.WriteLine(" Bem vindo\nEntre com seus dados");
            Console.Write("Nome: ");
            this.nome = Console.ReadLine();
            Console.Write("senha: ");
            this.password = Console.ReadLine();
            Console.Write("email: ");
            this.email = Console.ReadLine();
            Console.Write("telefone: ");
            this.phone = Console.ReadLine();
            Console.Write("cpf: ");
            this.cpf = Console.ReadLine();
        }

        public User newUser()
        {
            Console.WriteLine(" Bem vindo\nEntre com seus dados");
            Console.Write("Nome: ");
            this.nome = Console.ReadLine();
            Console.Write("senha: ");
            this.password = Console.ReadLine();
            Console.Write("email: ");
            this.email = Console.ReadLine();
            Console.Write("telefone: ");
            this.phone = Console.ReadLine();
            Console.Write("cpf: ");
            this.cpf = Console.ReadLine();
            /*
             Verificar se o cpf é valido, usar dps
            if (!(Regex.IsMatch(this.cpf, "([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})")))
            */
            return this;
        }

        public override string ToString()
        {
            return "Nome: " + this.nome + "\nEmail: " + this.email + "\nTelefone: " + this.phone + "\nCPF: " + this.cpf + "\n-----------------\n";
        }
        
    }

    public class Program {

        static void ShowMenu() {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Detalhes de um usuário");
            Console.WriteLine("4 - Total armazenado no banco");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }

        public static void Main(string[] args) {

            Console.WriteLine("Antes de começar a usar, vamos configurar alguns valores: ");

            List<User> usuarios = new List<User>();
            
            string cpfAux;
            int option;

            do {
                ShowMenu();
                option = int.Parse(Console.ReadLine());

                Console.WriteLine("-----------------");

                foreach (var i in usuarios)
                {
                    Console.WriteLine(i);
                }

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
                        cpfAux = Console.ReadLine();
                        if(usuarios.RemoveAll(x => x.cpf == cpfAux) >= 1)
                            Console.WriteLine("Usuario removido com sucesso");
                        else
                            Console.WriteLine("Usuario não encontrado");
                        break;
                }

                Console.WriteLine("-----------------");

            } while (option != 0);

            Console.WriteLine(usuarios.Count());
            foreach(var i in usuarios)
            {
                Console.WriteLine(i);
            }

        }

    }

}