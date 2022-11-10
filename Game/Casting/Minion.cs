using System;
using System.Linq;

namespace Entry.Game.Casting
{
    /// <summary>
    /// <para>A thing that participates in the game.</para>
    /// <para>
    /// The responsibility of Actor is to keep track of its appearance, position and velocity in 2d 
    /// space.
    /// </para>
    /// </summary>
    public class Minion : CreateableObject
    {
        /// <summary>
        /// Constructs an instance of the minion enemy and gives default enemy stats
        /// </summary>
        public Minion() : base(
        new Attribute[] 
        {   
            new Attribute("name", "minion"),
            new Attribute("health", 10000),
            new Attribute("attack", 10000),
            new Attribute("defense", 10),
            new Attribute("heat_resistance", false)
        })
        {
            
        }
    }
}