using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineGameWebSocket
{
    //public class Point
    //{
    //    public int x { get; set; }
    //    public int y { get; set; }
    //}
    public class NewLine
    {
        public NewLine(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }

        public Point start { get; set; }
        public Point end { get; set; }
    }
    public class LineGame
    {
        private enum Player { One, Two};
        private Player currentPlayer = Player.One;
        private List<NewLine> lines = new List<NewLine>();
        private bool startAdded;
        private Point startPoint;
        private Point endPoint;
        private bool isValidStart(Point point)
        {
            if (lines.Count == 0) return true;
            if(lines.Count == 1)
            {
                var end = lines[lines.Count - 1].end.x == point.x && lines[lines.Count - 1].end.y == point.y;
                var begining = lines[0].start.x == point.x && lines[0].start.y == point.y;
                return end || begining;
            }
            var startEnd = lines[lines.Count - 1].end.x == point.x && lines[lines.Count - 1].end.y == point.y;
            var startBegining = lines[0].end.x == point.x && lines[0].end.y == point.y;
            return startEnd || startBegining;
        }
        private bool isValidEnd(Point point)
        {
            if (lines.Count == 0) return true;
            //check if valid end point
            //rules:
            // 1. not intersect
            // 2. not more than 1 point from either axis

            return true;
        }
        private void AddLine(NewLine newLine) 
        { 
            startAdded = false;
            if(lines.Count > 0 && lines[0].start.x == newLine.start.x && lines[0].start.y == newLine.start.y)
            {
                lines.Insert(0, newLine);
                return;
            }
            lines.Add(newLine); 
        }
        public bool AddPoint(Point point, MSG omsg)
        {
            if (point == null)
            {
                omsg = null;
                return false;
            }

            if (!startAdded)
            {
                //check if point matches an end
                var isValid = isValidStart(point);
                if (isValid)
                {
                    startPoint = point;
                    omsg.msg = Constants.VALID_START_NODE;
                    omsg.body = new ResponseBody();
                    omsg.body.heading = currentPlayer == Player.One ? Constants.PLAYER1 : Constants.PLAYER2;
                    omsg.body.message = Constants.SELECT_SECOND;
                    return startAdded = true;
                }
                omsg.msg = Constants.INVALID_START_NODE;
                omsg.body = new ResponseBody();
                omsg.body.heading = currentPlayer == Player.One ? Constants.PLAYER1 : Constants.PLAYER2;
                omsg.body.message = Constants.SELECT_SECOND;
                return false;
            }
            else
            {
                var validMove = isValidEnd(point);
                endPoint = point;
                var line = new NewLine(startPoint, endPoint);
                AddLine(line);
                currentPlayer = currentPlayer == Player.One ? Player.Two : Player.One;
                omsg.msg = Constants.VALID_END_NODE;
                omsg.body = new ResponseBody();
                omsg.body.heading = currentPlayer == Player.One ? Constants.PLAYER1 : Constants.PLAYER2;
                omsg.body.newLine = line;
                return true;
            }
        }
    }
}
