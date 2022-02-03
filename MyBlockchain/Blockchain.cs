namespace MyBlockchain
{
    public class Blockchain
    {
        public int Difficulty { get; set; } = 3;

        public int Reward { get; set; } = 100;

        public IList<Block> Chain { get; set; }

        private Block LatestBlock => this.Chain.Last();

        public IList<Transaction> PendingTransactions = new List<Transaction>();

        public Blockchain()
        {
            this.Chain = new List<Block>() { new Block(DateTime.Now, null, new List<Transaction>(), this.Difficulty) };
        }

        public void AddBlock(Block block)
        {
            block.Index = this.LatestBlock.Index + 1;
            block.PreviousHash = this.LatestBlock.Hash;
            block.Mine(this.Difficulty);
            this.Chain.Add(block);
        }

        public bool IsValid()
        {
            bool result = true;

            for (int i = 1; i < this.Chain.Count; i++)
            {
                Block currentBlock = this.Chain[i];
                Block previousBlock = this.Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    result = false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    result = false;
                }
            }

            return result;
        }

        public void CreateTransaction(Transaction transaction)
        {
            this.PendingTransactions.Add(transaction);
        }

        public void ProcessPendingTransactions(string minerAddress)
        {
            Block block = new Block(DateTime.Now, this.LatestBlock.Hash, this.PendingTransactions);
            this.AddBlock(block);

            this.PendingTransactions = new List<Transaction>();
            this.CreateTransaction(new Transaction(null, minerAddress, this.Reward));
        }

        public int GetBalance(string toAddress) => this.Chain.SelectMany(x => x.Transactions).FirstOrDefault(x => x.FromAddress == null && x.ToAddress == toAddress)?.Amount ?? 0;
    }
}
