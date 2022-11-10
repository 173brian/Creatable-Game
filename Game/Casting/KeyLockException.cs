using System;
public class KeyLockException : Exception
{
    public KeyLockException(string inputKey, Key key, CreateableObject c) : base("Key code was incorrect.")
    {
        Console.WriteLine($"The key \"{inputKey}\" was input instead of \"{key.ToString()}\" for the object {c}");
    }
}