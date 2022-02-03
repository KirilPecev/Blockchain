using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace MyBlockchain
{
    public class P2PServer : WebSocketBehavior
    {
        private bool chainSynched = false;

        private WebSocketServer wss = null;

        public void Start()
        {
            this.wss = new WebSocketServer($"ws://127.0.0.1:{Program.Port}");
            this.wss.AddWebSocketService<P2PServer>("/Blockchain");
            this.wss.Start();

            Console.WriteLine($"Started server at ws://127.0.0.1:{Program.Port}");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "Hi Server")
            {
                Console.WriteLine(e.Data);
                this.Send("Hi Client");
            }
            else
            {
                Blockchain newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);

                if (newChain.IsValid() && newChain.Chain.Count > Program.StudyHardCoin.Chain.Count)
                {
                    List<Transaction> newTransactions = new List<Transaction>();
                    newTransactions.AddRange(newChain.PendingTransactions);
                    newTransactions.AddRange(Program.StudyHardCoin.PendingTransactions);

                    newChain.PendingTransactions = newTransactions;
                    Startup.StudyHardCoin = newChain;
                }

                if (!chainSynched)
                {
                    Send(JsonConvert.SerializeObject(Program.StudyHardCoin));
                    chainSynched = true;
                }
            }
        }
    }
}
