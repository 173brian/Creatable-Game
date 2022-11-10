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
    public class Player : CreateableObject
    {
        /// <summary>
        /// Constructs an instance of the Player and gives default player stats
        /// </summary>
        public Player() : base(
        new Attribute[] 
        {   
            new Attribute("name", "Player"),
            new Attribute("health", 100),
            new Attribute("attack", 100),
            new Attribute("defense", 10),
            new Attribute("heat_resistance", false)
        })
        {
            
        }
    }
}