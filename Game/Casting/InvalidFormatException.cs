using System;
using System.Collections.Generic;

public class InvalidFormatException : Exception
{
    public InvalidFormatException(List<Attribute> fileAttributes, CreateableObject c) 
    : base($"The document didn't meet the attribute requirements: {fileAttributes.Count} instead of {c.AttributeNum}")
    {
        foreach(Attribute attribute in fileAttributes)
            Console.WriteLine($"{attribute.AttributeName} : {attribute.Value}");
        Console.WriteLine("Instead of: ");
        foreach(Attribute attribute in c.ObjectAttributes)
            Console.WriteLine($"{attribute.AttributeName} : {attribute.Value}");
    }
}