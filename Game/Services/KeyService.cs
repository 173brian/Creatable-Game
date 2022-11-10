using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


public class KeySerivce
{
    private static string filePath = "Game\\Private Data\\Keys.txt";
    private int keyLength = 10;
    private static List<Key> keyList;
    public KeySerivce()
    {
        List<string> classNames = CreateableObject.GetObjectList();
        List<string> existingKeys = new List<string>();
        try
        {
            StreamReader reader = new StreamReader(filePath);
            while(!reader.EndOfStream)
                existingKeys.Add(reader.ReadLine().Split(' ')[0]);
            reader.Close();
            if (existingKeys.Count > classNames.Count)
                DeleteKeys(filePath, existingKeys, classNames);
            else if (existingKeys.Count < classNames.Count)
                RefreshKeys(filePath, classNames);
        }
        catch(FileNotFoundException)
        {
            GenerateKeysFor(filePath, classNames);
        }
        keyList = ReadKeys(filePath);
    }

    private void DeleteKeys(string filepath, List<string> oldKeys, List<string> newKeys)
    {
        List<string> existingKeys = new List<string>();
        foreach (string s in newKeys)
        {
            if (oldKeys.Contains(s))
                oldKeys.Remove(s);
        }
        StreamReader reader = new StreamReader(filePath);
        while (!reader.EndOfStream)
            existingKeys.Add(reader.ReadLine());
        reader.Close();
        List<string> itemsToBeRemoved = new List<string>();
        foreach (string s in existingKeys)
            foreach (string d in oldKeys)
                if (s.Contains(d)) 
                    itemsToBeRemoved.Add(s);
        foreach (string s in itemsToBeRemoved)
            existingKeys.Remove(s);
        File.WriteAllText(@$"{filepath}",string.Empty);
        StreamWriter writer = new StreamWriter(filePath);
        foreach (string s in existingKeys)
        {
            writer.WriteLine(s);
        }
        writer.Close();
    }
    private void RefreshKeys(string filePath, List<string> classNames)
    {
        StreamReader reader = new StreamReader(filePath);
        while(!reader.EndOfStream)
        {
            string className = reader.ReadLine().Split(" : ")[0];
            if (classNames.Contains(className))
                classNames.Remove(className);
        }
        reader.Close();
        if (classNames.Count != 0)
        {
            GenerateKeysFor(filePath, classNames);
        }
    }

    private void GenerateKeysFor(string destination, List<string> classNames)
    {
        List<string> keysAlreadyGenerated = new List<string>();
        StreamReader reader = new StreamReader(filePath);
        while(!reader.EndOfStream)
            keysAlreadyGenerated.Add(reader.ReadLine());
        reader.Close();
        StreamWriter writer = new StreamWriter(destination);
        foreach (string preGen in keysAlreadyGenerated)
            writer.WriteLine(preGen);
        foreach (string className in classNames)
            writer.WriteLine($"{className} : {KeyGen()}");
        writer.Close();
    }

    public Attribute KeyGen()
    {
        string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        Random random = new Random();
        string randKey = "";
        for (int i = 0; i < keyLength; i++)
        {
            randKey += characters[random.Next(0, characters.Length)];
        }
        return new Attribute("key", randKey);
    }

    public List<Key> ReadKeys(string filePath)
    {
        List<Key> keys = new List<Key>();
        StreamReader reader = new StreamReader(filePath);
        while(!reader.EndOfStream)
        {
            string[] classAndKey = reader.ReadLine().Split(" : key ");
            Key key = new Key(classAndKey[0], classAndKey[1]);
            keys.Add(key);
        }
        return keys;
    }
    
    public static List<Key> GetKeyList
    {
        get 
        {
            return keyList; 
        }
    }

    public static string ClassLookupByKey(string key)
    {
        foreach(Key k in keyList)
        {
            if (k.Value.Contains(key))
            {
                return k.AttributeName;
            }
        }
        return null;
    }
}