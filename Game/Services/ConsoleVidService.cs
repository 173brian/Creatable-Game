using System;
static class ConsoleVidService
{
    private static int lineSize = 100;

    public static void helperWrite(string message)
    {
        bool newLineFlag = false;
        for (int i = 0; i < message.Length; i++)
        {
            Console.Clear();
            if(message[i] == ' ')
                Console.Write("/0o0\\");
            else
                Console.Write("/0-0\\");
            Console.Write(" : ");
            if(i % lineSize == 0 && i != 0)
                newLineFlag = true;
            if(message[i] == ' ' && newLineFlag)
            {
                message = message.Insert(i, "\n");
                message = message.Remove(i+1,1);
                newLineFlag = false;
            }
            Console.Write(message.Substring(0,i + 1));
            System.Threading.Thread.Sleep(10);
        }
        Console.WriteLine("\n\nPress any key to continue...");
        Console.ReadKey();
    }

    public static void MessageWrite(string message)
    {
        bool newLineFlag = false;
        Console.Clear();
        for (int i = 0; i < message.Length; i++)
        {
            if(i % lineSize == 0 && i != 0)
                newLineFlag = true;
            if(message[i] == ' ' && newLineFlag)
            {
                message = message.Insert(i, "\n");
                message = message.Remove(i+1,1);
                newLineFlag = false;
            }
            Console.Write(message[i]);
        }
        Console.WriteLine("\n\nPress any key to continue...");
        Console.ReadKey();
    }

}