using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
namespace LineGameWebSocket
{
    internal class Program
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public static class parser
        {
            public static MSG FormResponse(ClientBody clientBody, LineGame lineGame)
            {
                MSG msg = new MSG();
                msg.id = clientBody.id;
                switch (clientBody.msg)
                {
                    case Constants.INITIALIZE:
                        msg.msg= Constants.INITIALIZE;
                        var body = new ResponseBody();
                        body.heading = "Player 1";
                        body.message = "Awaiting Player 1's Move";
                        msg.body = body;
                        break;
                    case Constants.NODE_CLICKED:
                        //msg.msg = Constants.VALID_START_NODE;//TODO query game engine for appropriate response
                        //msg.body = new ResponseBody();
                        Point point = JsonConvert.DeserializeObject<Point>(clientBody.body.ToString());
                        var response = lineGame.AddPoint(point, msg);    
                        break;

                }
                return msg;
            }
        }
        public class Respond : WebSocketBehavior
        {
            private LineGame lineGame = new LineGame();
            protected override void OnMessage(MessageEventArgs e)
            {
                base.OnMessage(e);
                Console.WriteLine(e.Data);
                ClientBody clientBody = JsonConvert.DeserializeObject<ClientBody>(e.Data);
                string sMsg = JsonConvert.SerializeObject(parser.FormResponse(clientBody, lineGame));
                Send(sMsg);
            }

        }
        static void Main(string[] args)
        {
             var server = new WebSocketServer("ws://localhost:8081");
            server.AddWebSocketService<Respond>("/");
            server.Start();
            Console.ReadKey();
            server.Stop();
        }
    }
}
