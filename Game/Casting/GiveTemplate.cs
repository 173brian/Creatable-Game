using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public abstract class GiveTemplate
{
    CreateableObject c;
    public GiveTemplate(CreateableObject parent)
    {
        c = parent;
    }


    public void PrintInformation(string destination)
    {
        // Print Header Information:
        Random random = new Random();
        Queue<string> fileInfo = new Queue<string>();
        Queue<string> attributeInfo = new Queue<string>();
        try
        {
            // Copy file contents to a string list
            StreamReader reader = new StreamReader(destination);
            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                fileInfo.Enqueue(line);
            }
            reader.Close();

            // Copy base object attributes to a string list
            attributeInfo.Enqueue($"key {c.Key.ToString()}");
            foreach (Attribute a in c.ObjectAttributes)
            {
                attributeInfo.Enqueue(c.ToString());
            }

            // Clear file contents
            File.WriteAllText(@$"{destination}",string.Empty);
            
            // Write "key" in rand
            StreamWriter writer = new StreamWriter(destination);
            writer.Write($"{RandWrite(fileInfo.Dequeue().Substring(0,4), attributeInfo.Dequeue().Substring(0,4))}");
            writer.Write(c.Key.ToHiddenString());

            // Write the rest of the attributes
            while(fileInfo.Count != 0 && attributeInfo.Count != 0)
            {
                writer.WriteLine($"{RandWrite(fileInfo.Dequeue(), attributeInfo.Dequeue())}");
            }
            writer.Close();
            return;
        }
        catch(FileNotFoundException)
        {
            StreamWriter writer = new StreamWriter(destination);
            PrintHeader(c.Key, writer);
            PrintBody(c.Key, writer, c);
            writer.Close();
            PrintInformation(destination);
        }
    }

    private string RandWrite(string input, string reference)
    {
        if (input.Length != reference.Length)
        {
            throw new SizeMismatchException(input.Length, reference.Length);
        }
        int strSize = reference.Length;
        string output = "";
        Random rand = new Random();
        for (int i = 0; i < strSize; i++)
        {
            if (input[i] != reference[i])
            {
                if (reference[i] == ' ' || rand.Next(0, 10) == 0)
                    output += reference[i];
                else
                    output += '?';
            }
        }
        return output;
    }

    // Generates header placeholder values
    private void PrintHeader(Key key, StreamWriter writer)
    {
        writer.WriteLine($"??? {key.ToHiddenString()}");
    }

    // Generates body placeholder values
    private void PrintBody(Key key, StreamWriter writer, CreateableObject c)
    {
        foreach (Attribute a in c.ObjectAttributes)
        {
            foreach (char character in a.AttributeName)
                writer.Write('?');
            writer.Write(' ');
            foreach (char character in a.Value)
                writer.Write('?');
        }
    }
}