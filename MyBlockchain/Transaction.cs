namespace MyBlockchain
{
    public class Transaction
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public int Amount { get; set; }

        public Transaction(string fromAddress, string toAddress, int amount)
        {
            this.FromAddress = fromAddress;
            this.ToAddress = toAddress;
            this.Amount = amount;
        }
    }
}
