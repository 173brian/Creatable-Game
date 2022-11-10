class Book : CreateableObject
{
    public Book() : base(
        new Attribute[] 
        {
            new Attribute("pages", 250),
            new Attribute("flammable", true)
        }
    )
    {

    }
}