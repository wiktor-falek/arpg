using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace arpg;

public static class Assets
{
    private static OrderedDictionary Textures = [];

    public static void Load(ContentManager contentManager)
    {
        Textures.Add("spells/fireball_1", contentManager.Load<Texture2D>("spells/fireball_1"));
        Textures.Add("spells/fireball_2", contentManager.Load<Texture2D>("spells/fireball_2"));
        Textures.Add("spells/fireball_3", contentManager.Load<Texture2D>("spells/fireball_3"));
        Textures.Add("spells/fireball_4", contentManager.Load<Texture2D>("spells/fireball_4"));
        Textures.Add("spells/fireball_5", contentManager.Load<Texture2D>("spells/fireball_5"));
    }

    public static List<Texture2D> GetTextures(params string[] names)
    {
        List<Texture2D> textures = names.Select(key => (Texture2D)Textures[key]).ToList();
        return textures;
    }
}
