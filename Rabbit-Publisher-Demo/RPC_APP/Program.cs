using System;
using System.Threading.Tasks;

namespace RPC_APP
{
    class Program
    {
        public static void Main(string[] args)
        {
            Task t = InvokeAsync(11);
            t.Wait();
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static async Task InvokeAsync(int n)
        {
            RpcClient rpcClient = new RpcClient();
            string fib = await rpcClient.CallAsync(n.ToString());
            Console.WriteLine($"Requesting fib({n})={fib}");
            rpcClient.Close();
        }
    }
}