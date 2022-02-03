using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MyBlockchain
{
    public class Block
    {
        public int Index { get; set; }

        public DateTime TimeStamp { get; set; }

        public string PreviousHash { get; set; }

        public string Hash { get; set; }

        public IList<Transaction> Transactions { get; set; }

        public int Nonce { get; set; }

        public Block(DateTime timeStamp, string previosHash, IList<Transaction> transactions, int? difficulty = null)
        {
            this.Index = 0;
            this.TimeStamp = timeStamp;
            this.PreviousHash = previosHash;
            this.Transactions = transactions;

            if (difficulty != null)
            {
                this.Mine((int)difficulty);
            }
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes($"{this.TimeStamp}-{this.PreviousHash ?? ""}-{JsonSerializer.Serialize(this.Transactions)}-{this.Nonce}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }

        public void Mine(int difficulty)
        {
            string leadingZeroes = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeroes)
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }
}
