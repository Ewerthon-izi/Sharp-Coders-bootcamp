namespace ByteBank1
{
    public class userBase
    {
        private string email;
        private string nome;
        private string cpf;
        private string password;
        private string phone;
        private double saldo;
        public string Email { get => email; set => email = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Password { get => password; set => password = value; }
        public string Phone { get => phone; set => phone = value; }
        public double Saldo { get => saldo; set => saldo = value; }

        public bool retirarSaldo(double valor)
        {
            if (this.Saldo - valor >= 0)
            {
                this.Saldo -= valor;
                return true;
            }
            else
            {
                return false;
            }

        }

        public void AddSaldo(double valor)
        {
            if (valor > 0)
                this.Saldo += valor;
            else
                Console.WriteLine("Valor informado invalido");
        }

        public bool Login(string email, string password)
        {
            if (!(this.Email == email))
                return false;
            if(!(this.Password== password)) 
                return false;
            return true;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }


    }
}