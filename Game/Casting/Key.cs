public class Key : Attribute
{
    private string className;
    private string key;
    private int knownChars;
    public Key(string className, string key) : base(className, key)
    {
        this.className = className;
        this.key = key;
        this.knownChars = 0;
    }

    public override string ToString()
    {
        return key.ToString();
    }

    public int GetKnownChars
    {
        get
        {
            return this.knownChars;
        }
    }

    public void IncrementKnownChars()
    {
        if (knownChars < this.Value.Length)
        {
            knownChars++;
        }
    }

    public string ToHiddenString()
    {
        string HiddenString = "";
        for (int i = 0; i < this.key.Length; i++)
        {
            if (i < knownChars)
                HiddenString += this.key[i];
            else
                HiddenString += '?';
        }
        return HiddenString;
    }
}