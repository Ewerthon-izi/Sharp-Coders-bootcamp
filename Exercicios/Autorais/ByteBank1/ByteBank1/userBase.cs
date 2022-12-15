namespace ByteBank1
{
    public class userBase
    {
        public string email { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public double saldo { get; set; }

        public bool retirarSaldo(double valor)
        {
            if (this.saldo - valor >= 0)
            {
                this.saldo -= valor;
                return true;
            }
            else
            {
                return false;
            }

        }

        public void AddSaldo(double valor)
        {
            this.saldo += valor;
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