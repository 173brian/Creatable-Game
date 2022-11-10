using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;

public abstract class CreateableObject
{
    private int attributeNum;
    private Attribute[] attributes;
    protected internal Key key;
    private string className;
    private string outputObjAttributeLocation;
    public CreateableObject(Attribute[] attributes)
    {
        this.attributes = attributes;
        this.attributeNum = attributes.Length;
        className = this.GetType().ToString().Split('.')[this.GetType().ToString().Split('.').Length - 1];
        outputObjAttributeLocation = $"Data\\Fragments\\{className}.txt";
        setKey();

        // --DEBUG INFORMATION--
        // --DEBUG INFORMATION--
    }

    public int AttributeNum
    {
        get
        {
            return this.attributeNum;
        }
    }

    public Key Key
    {
        get
        {
            return this.key;
        }
    }

    public Attribute[] ObjectAttributes
    {
        get
        {
            return this.attributes;
        }
        private set
        {
            this.attributes = value;
        }
    }

    private void setKey()
    {
        this.key = KeySerivce.GetKeyList.Where(key => key.AttributeName == className).First();
    }

    /// <summary>
    /// Takes a filepath to a document, compares the document to the original object attributes and changes
    /// them if an acceptable substitute has been found.
    /// </summary>
    public bool overrideAttributes(string filePath)
    {
        List<Attribute> newAttributes = new List<Attribute>();
        try
        {
            StreamReader r = new StreamReader($"{filePath}");
            if(KeyCheck(r))
            {
                while(!r.EndOfStream)
                {
                    string[] subStrings = r.ReadLine().Split(' ');
                    if (subStrings.Length != 2) // Attribute key/value size
                    {    
                        throw new AttributeSizeException(subStrings);
                    }
                    else
                    {
                        newAttributes.Add(new Attribute(subStrings[0], subStrings[1]));
                    }
                }
                r.Close();
                // If the object has a different number of attributes:
                if (newAttributes.Count != this.AttributeNum)
                {
                    throw new InvalidFormatException(newAttributes, this);
                }
                // Else check compatibility:
                else
                {
                    Attribute[] AttributesArray = new Attribute[ObjectAttributes.Length];
                    for (int i = 0; i < AttributesArray.Length; i++)
                        AttributesArray[i] = ObjectAttributes[i].DeepCopy();
                    Attribute[] newAttributesArray = newAttributes.ToArray();
                    for (int i = 0; i < AttributesArray.Length; i++)
                    {
                        if (newAttributesArray[i].AttributeName == AttributesArray[i].AttributeName)
                        {
                            try
                            {
                                if(AttributesArray[i].Value is int)
                                {
                                    AttributesArray[i] = (new Attribute(AttributesArray[i].AttributeName, int.Parse(newAttributesArray[i].Value)));
                                }
                                else if (AttributesArray[i].Value is bool)
                                {
                                    if (newAttributesArray[i].Value == "true")
                                        AttributesArray[i] = (new Attribute(AttributesArray[i].AttributeName, true));
                                    else if (newAttributesArray[i].Value == "false")
                                        AttributesArray[i] = (new Attribute(AttributesArray[i].AttributeName, false));
                                    else
                                        throw new FormatException();
                                }
                                else 
                                {
                                    AttributesArray[i] = newAttributesArray[i];
                                }
                            }
                            catch(FormatException)
                            {
                                Console.WriteLine("Invalid types passed into attribute.");
                                return false;
                            }
                        }
                        else
                        {
                            throw new InvalidFormatException(newAttributes, this);
                        }
                    }
                    string name = ReturnClassName(this.GetType());
                    Console.WriteLine($"Attribute check for {name} succeeded! ({this.AttributeNum} attributes updated successfully.)");
                    this.ObjectAttributes = AttributesArray;
                }
            System.Threading.Thread.Sleep(1000);
            return true;
            }
            else
            {
                return false;
            }
        }  
        catch(FileNotFoundException)
        {
            Console.WriteLine($"Filepath error: {filePath}.");
        }
        catch(InvalidFormatException)
        {
            Console.WriteLine("Attributes didn't meet specifications.");
        }
        catch(AttributeSizeException)
        {
            Console.WriteLine("Attribute size was invalid.");
        }
        catch(KeyLockException)
        {
            Console.WriteLine("Input key was incorrect.");
        }
        System.Threading.Thread.Sleep(4000);
        return false;
    }

    private static string ReturnClassName(Type thisObj)
    {
        string returnString = "";
        if (thisObj.ToString().Contains('.'))
            returnString = (thisObj.FullName.Split('.')[thisObj.FullName.Split('.').Length - 1]);
        else
            returnString = (thisObj.FullName);
        return returnString;
    }

    private bool KeyCheck(StreamReader reader)
    {
        string[] subStrings = reader.ReadLine().Split(' ');
        if (subStrings.Length != 2) // Attribute key/value size
        {    
            throw new AttributeSizeException(subStrings);
        }
        else
        {
            if (subStrings[0] == "key" && subStrings[1] == key.Value)
                return true;
            else
            {
                reader.Close();
                throw new KeyLockException(subStrings[1], key, this);   
            }             
        }
    }

    // PES: The below method is not well written
    // Returns a list of all the class names that inherit from the CreateableObjectClass
    public static List<string> GetObjectList()
    {
        Type myType = typeof(CreateableObject);
        List<string> classes = new List<string>();
        Type[] objects = Assembly.GetAssembly(typeof(CreateableObject)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(CreateableObject))).ToArray();
        foreach (Type type in objects)
            classes.Add(ReturnClassName(type));
        return classes;
    }

    public void PrintInformation()
    {
        // Print Header Information:
        Random random = new Random();
        Queue<string> fileInfo = new Queue<string>();
        Queue<string> attributeInfo = new Queue<string>();
        try
        {
            // Copy file contents to a string list
            using (StreamReader reader = new StreamReader(outputObjAttributeLocation))
            {
                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    fileInfo.Enqueue(line);
                }
                reader.Close();
            }
            if(fileInfo.Count == 0)
            {
                throw new FileNotFoundException();
            }

            // Copy base object attributes to a string list
            attributeInfo.Enqueue($"key {this.Key.ToString()}");
            foreach (Attribute a in this.ObjectAttributes)
            {
                attributeInfo.Enqueue($"{a.AttributeName} {a.Value}");
            }

            // Clear file contents
            int NumberOfRetries = 5;
            int DelayOnRetry = 3000;
            for (int i=1; i <= NumberOfRetries; ++i) {
                try {
                    // Do stuff with file
                    File.WriteAllText(@$"{outputObjAttributeLocation}",string.Empty);
                    break; // When done we can break loop
                }
                catch (IOException e) when (i <= NumberOfRetries) {
                    // You may check error code to filter some exceptions, not every error
                    // can be recovered.
                    Console.WriteLine($"Error: File reading error, file is busy. Attempt {i} of {NumberOfRetries}");
                    System.Threading.Thread.Sleep(DelayOnRetry);
                }
            }
            
            // Write "key" in rand
            StreamWriter writer = new StreamWriter(outputObjAttributeLocation);
            string inputKey = fileInfo.Dequeue();
            if (!(inputKey.Length >= 4))
            {
                writer.Close();
                throw new SizeMismatchException(inputKey.Length, 4);
            }
            writer.Write($"{RandWrite(inputKey.Substring(0,4), attributeInfo.Dequeue().Substring(0,4), writer)}");
            writer.WriteLine(this.Key.ToHiddenString());

            // Write the rest of the attributes
            while(fileInfo.Count != 0 && attributeInfo.Count != 0)
            {
                writer.WriteLine($"{RandWrite(fileInfo.Dequeue(), attributeInfo.Dequeue(), writer)}");
            }
            writer.Close();
            return;
        }
        catch(IndexOutOfRangeException)
        {
            File.WriteAllText(@$"{outputObjAttributeLocation}",string.Empty);
            PrintInformation();
        }
        catch(SizeMismatchException)
        {
            File.WriteAllText(@$"{outputObjAttributeLocation}",string.Empty);
            PrintInformation();
        }
        catch(ArgumentOutOfRangeException)
        {

            File.WriteAllText(@$"{outputObjAttributeLocation}",string.Empty);
            PrintInformation();
        }
        catch(System.IO.FileNotFoundException)
        {
            StreamWriter writer = new StreamWriter(outputObjAttributeLocation);
            PrintHeader(this.Key, writer);
            PrintBody(this.Key, writer, this);
            writer.Close();
            PrintInformation();
        }
    }

    private string RandWrite(string input, string reference, StreamWriter writer)
    {
        if (input.Length != reference.Length)
        {
            writer.Close();
            throw new SizeMismatchException(input.Length, reference.Length);
        }
        int strSize = reference.Length;
        string output = "";
        Random rand = new Random();
        for (int i = 0; i < strSize; i++)
        {
            if (input[i] != reference[i])
            {
                if (rand.Next(0, 10) == 0)
                    output += reference[i];
                else
                    output += '?';
            }
            else
            {
                output += reference[i];
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
            string line = "";
            foreach (char character in a.AttributeName.ToString())
                line += '?';
            line += ' ';
            foreach (char character in a.Value.ToString())
                line += '?';
            writer.WriteLine(line);
        }
    }

    public Attribute LocateAttribute(string name)
    {
        foreach (Attribute a in attributes)
        {
            if (a.AttributeName == name)
            {
                return a;
            }
        }
        return null;
    }
}