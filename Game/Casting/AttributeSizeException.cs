using System;
using System.Collections.Generic;

public class AttributeSizeException : Exception
{
    public AttributeSizeException(string[] attributes) : base($"The Attribute is of an invalid size: {attributes.Length} instead of 2")
    {
        foreach(string attribute in attributes)
        {
            Console.Write(attribute + ": ");
        }
    }
}