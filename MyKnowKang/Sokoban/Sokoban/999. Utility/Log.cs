using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal class Log
    {
        LinkedList<KeyValuePair<int, string>> logMessage = new LinkedList<KeyValuePair<int, string>>();

        int _logStartX = 0;
        int _logStartY = 0;

        public Log( int logStartX, int logStartY )
        {
            _logStartX = logStartX;
            _logStartY = logStartY;
        }

        public void AddLogMessage( int lineInterval, string message )
        {
            logMessage.AddLast( new KeyValuePair<int, string>( lineInterval, message ) );
        }

        public void RemoveLast()
        {
            logMessage.RemoveLast();
        }

        public void Clear()
        {
            logMessage.Clear();
        }

        public void Render()
        {
            int logYOffset = 0;
            foreach ( var message in logMessage )
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition( _logStartX, _logStartY + logYOffset );
                Console.Write( message.Value );

                logYOffset += message.Key;
            }
        }
    }
}
