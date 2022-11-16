using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineGameWebSocket
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ResponseBody
    {
        public NewLine newLine { get; set; }
        public string heading { get; set; }
        public object message { get; set; }
    }
    public class ClientBody
    {
        public int id { get; set; }
        public string msg { get; set; }
        public object body { get; set; }
    }
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Line
    {
        public Point start { get; set; }
        public Point end { get; set; }
    }

    public class MSG
    {
        public int id { get; set; }
        public string msg { get; set; }
        public ResponseBody body { get; set; }
    }
}
