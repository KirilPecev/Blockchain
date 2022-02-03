using System.Text.Json;

namespace MyBlockchain
{
    public static class Startup
    {
        public static Blockchain StudyHardCoin { get; set; } = new Blockchain();

        public static void Start()
        {
            var startTime = DateTime.Now;
            Console.WriteLine($"Mining process started! ---- {startTime}");
            StudyHardCoin.CreateTransaction(new Transaction("Henry", "MaHesh", 10));
            StudyHardCoin.ProcessPendingTransactions("Bill");

            StudyHardCoin.CreateTransaction(new Transaction("MaHesh", "Henry", 5));
            StudyHardCoin.CreateTransaction(new Transaction("MaHesh", "Henry", 5));
            StudyHardCoin.ProcessPendingTransactions("Bill");
            //studyHardCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
            //Console.WriteLine($"Process: 33% ---- {DateTime.Now - startTime}");

            //studyHardCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));
            //Console.WriteLine($"Process: 33% ---- {DateTime.Now - startTime}");

            //studyHardCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));
            //Console.WriteLine($"Process: 100% ---- {DateTime.Now - startTime}");
            var finishTime = DateTime.Now;
            Console.WriteLine($"Mining process finished! ---- {finishTime} ---- Needed time: {finishTime - startTime}");

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(StudyHardCoin, new JsonSerializerOptions { WriteIndented = true }));

            Console.WriteLine("=========================");
            Console.WriteLine($"Henry' balance: {StudyHardCoin.GetBalance("Henry")}");
            Console.WriteLine($"MaHesh' balance: {StudyHardCoin.GetBalance("MaHesh")}");
            Console.WriteLine($"Bill' balance: {StudyHardCoin.GetBalance("Bill")}");

            //Console.WriteLine($"Is Chain Valid: {studyHardCoin.IsValid()}");

            //Console.WriteLine($"Update amount to 1000");
            //studyHardCoin.Chain[1].Data = "{sender:Henry,receiver:MaHesh,amount:1000}";

            //Console.WriteLine($"Is Chain Valid: {studyHardCoin.IsValid()}");

            //studyHardCoin.Chain[1].Hash = studyHardCoin.Chain[1].CalculateHash();

            //Console.WriteLine($"Is Chain Valid after hack: {studyHardCoin.IsValid()}");

            //Console.WriteLine($"Update the entire chain");
            //studyHardCoin.Chain[2].PreviousHash = studyHardCoin.Chain[1].Hash;
            //studyHardCoin.Chain[2].Hash = studyHardCoin.Chain[2].CalculateHash();
            //studyHardCoin.Chain[3].PreviousHash = studyHardCoin.Chain[2].Hash;
            //studyHardCoin.Chain[3].Hash = studyHardCoin.Chain[3].CalculateHash();

            //Console.WriteLine($"Is Chain Valid after bigger update: {studyHardCoin.IsValid()}");
        }
    }
}
