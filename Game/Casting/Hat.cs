class Hat : CreateableObject
{
    public Hat() : base(
        new Attribute[] 
        {
            new Attribute("weight", .5),
            new Attribute("flammable", true),
            new Attribute("Heat_protection", true)
        }
    )
    {

    }
}