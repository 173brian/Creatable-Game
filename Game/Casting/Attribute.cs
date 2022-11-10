public class Attribute
{
    private dynamic value;
    private string attribute;
    public Attribute(string attribute, dynamic value)
    {
        this.attribute = attribute;
        this.value = value;
    }

    public dynamic Value
    {
        get
        {
            return this.value;
        }
    }

    public string AttributeName
    {
        get 
        {
            return this.attribute;
        }
    }

    public Attribute DeepCopy()
    {
        return new Attribute(this.AttributeName, this.value);
    }

    public override string ToString()
    {
        return $"{AttributeName} {value}";
    }
}