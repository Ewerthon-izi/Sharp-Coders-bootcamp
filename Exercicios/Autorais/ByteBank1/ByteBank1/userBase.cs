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