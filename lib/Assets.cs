using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace arpg;

public static class Assets
{
    public static class Fonts
    {
        public static SpriteFont MonogramExtened => GetFont("fonts/monogram_extended");
    }

    private static Dictionary<string, SpriteFont> _fonts = [];
    private static OrderedDictionary _textures = [];

    public static SpriteFont GetFont(string name)
    {
        try
        {
            SpriteFont font = _fonts[name];
            return font;
        }
        catch (ArgumentNullException)
        {
            throw new SystemException($"Texture {name} not found.");
        }
    }

    public static Texture2D GetTextures(string name)
    {
        try
        {
            Texture2D texture = (Texture2D)_textures[name];
            return texture;
        }
        catch (ArgumentNullException)
        {
            throw new SystemException($"Texture {name} not found.");
        }
    }

    public static List<Texture2D> GetTextures(params string[] names)
    {
        try
        {
            List<Texture2D> textures = names.Select(key => (Texture2D)_textures[key]).ToList();
            return textures;
        }
        catch (ArgumentNullException)
        {
            throw new SystemException($"Texture *some texture name im lazy* not found.");
        }
    }

    public static void Load(ContentManager contentManager)
    {
        AddFont(contentManager, "fonts/monogram_extended");

        AddTexture(contentManager, "player/player_idle");
        AddTexture(contentManager, "player/player_walk");

        AddTexture(contentManager, "spells/fireball_1");
        AddTexture(contentManager, "spells/fireball_2");
        AddTexture(contentManager, "spells/fireball_3");
        AddTexture(contentManager, "spells/fireball_4");
        AddTexture(contentManager, "spells/fireball_5");

        AddTexture(contentManager, "monsters/skeleton_ready_1");
        AddTexture(contentManager, "monsters/skeleton_ready_2");
        AddTexture(contentManager, "monsters/skeleton_ready_3");
    }

    private static void AddFont(ContentManager contentManager, string path)
    {
        var font = contentManager.Load<SpriteFont>("fonts/monogram_extended");
        _fonts.Add(path, font);
    }

    private static void AddTexture(ContentManager contentManager, string path)
    {
        _textures.Add(path, contentManager.Load<Texture2D>(path));
    }
}
