using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace arpg;

public static class Assets
{
    private static OrderedDictionary Textures = [];

    public static Texture2D GetTextures(string name)
    {
        Texture2D? texture = (Texture2D?)Textures[name];
        if (texture is null)
            throw new SystemException($"Texture {name} not found.");
        return texture;
    }

    public static List<Texture2D> GetTextures(params string[] names)
    {
        List<Texture2D?> textures = names.Select(key => (Texture2D?)Textures[key]).ToList();
        for (int i = 0; i < textures.Count; i++)
        {
            var name = names[i];
            var texture = textures[i];
            if (texture is null)
                throw new SystemException($"Texture {name} not found.");
        }
        return textures;
    }

    public static void Load(ContentManager contentManager)
    {
        AddTexture(contentManager, "spells/fireball_1");
        AddTexture(contentManager, "spells/fireball_2");
        AddTexture(contentManager, "spells/fireball_3");
        AddTexture(contentManager, "spells/fireball_4");
        AddTexture(contentManager, "spells/fireball_5");

        AddTexture(contentManager, "player/player_idle");
        AddTexture(contentManager, "player/player_walk");

        AddTexture(contentManager, "monsters/skeleton_ready_1");
        AddTexture(contentManager, "monsters/skeleton_ready_2");
        AddTexture(contentManager, "monsters/skeleton_ready_3");
    }

    private static void AddTexture(ContentManager contentManager, string path)
    {
        Textures.Add(path, contentManager.Load<Texture2D>(path));
    }
}
