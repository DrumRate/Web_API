using System;
using System.Collections.Generic;
using System.Text;

namespace MainProject
{
    class ConsoleFacade
    {
        public delegate void ConsoleRead(string message);
        public event ConsoleRead onConsoleReading;
        public void WriteInConsole(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLineConsole()
        {
            var message = Console.ReadLine();
            onConsoleReading?.Invoke(message);
            return message;
        }

    }
}
