using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineGameWebSocket
{
    public static class Constants
    {
        public const string INITIALIZE = "INITIALIZE";
        public const string NODE_CLICKED = "NODE_CLICKED";
        public const string VALID_START_NODE = "VALID_START_NODE";
        public const string VALID_END_NODE = " VALID_END_NODE";
        public const string INVALID_START_NODE = "INVALID_START_NODE";        
        public const string INVALID_END_NODE = "INVALID_END_NODE";
        public const string GAME_OVER = "GAME_OVER";
        public const string PLAYER1 = "Player 1";
        public const string PLAYER2 = "Player 2";
        public const string SELECT_SECOND = "Select a second node to complete the line.";
        public const string BAD_START = "Not a valid starting position.";
        public const string BAD_END = "Invalid move!";
    }
}
