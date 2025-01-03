using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace arpg;

public static class Assets
{
    public static class Fonts
    {
        public static SpriteFont MonogramExtened => GetFont("fonts/monogram_extended");
    }

    public static class Player
    {
        public static Texture2D Idle => GetTexture("player/player_idle");
        public static Texture2D Walk => GetTexture("player/player_walk");
    }

    public static class Spells
    {
        public static List<Texture2D> Fireball => GetTextures("spells/fireball", 5);
    }

    public static class Monsters
    {
        public static class Skeleton
        {
            public static List<Texture2D> Idle => GetTextures("monsters/skeleton_ready", 3);
            public static List<Texture2D> Walk => GetTextures("monsters/skeleton_walk", 6);
            public static List<Texture2D> Death => GetTextures("monsters/skeleton_dead_near", 5);
            public static List<Texture2D> Corpse => GetTextures("monsters/skeleton_corpse", 2);
        }
    }

    private static Dictionary<string, SpriteFont> _fonts = [];
    private static Dictionary<string, Texture2D> _textures = [];

    public static void Load(ContentManager contentManager)
    {
        AddFont(contentManager, "fonts/monogram_extended");

        AddTexture(contentManager, "player/player_idle");
        AddTexture(contentManager, "player/player_walk");

        AddTextures(contentManager, "spells/fireball", 5);

        AddTextures(contentManager, "monsters/skeleton_ready", 3);
        AddTextures(contentManager, "monsters/skeleton_walk", 6);
        AddTextures(contentManager, "monsters/skeleton_dead_near", 5);
        AddTextures(contentManager, "monsters/skeleton_corpse", 2);
    }

    private static SpriteFont GetFont(string name)
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

    private static Texture2D GetTexture(string name)
    {
        try
        {
            Texture2D texture = _textures[name];
            return texture;
        }
        catch (ArgumentNullException)
        {
            throw new SystemException($"Texture {name} not found.");
        }
    }

    private static List<Texture2D> GetTextures(params string[] names)
    {
        List<Texture2D> textures = [];
        foreach (var name in names)
        {
            textures.Add(GetTexture(name));
        }
        return textures;
    }

    private static List<Texture2D> GetTextures(string baseName, int amount)
    {
        List<Texture2D> textures = [];
        for (int i = 1; i < amount + 1; i++)
        {
            string path = $"{baseName}_{i}";
            textures.Add(GetTexture(path));
        }
        return textures;
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

    private static void AddTextures(ContentManager contentManager, string baseName, int amount)
    {
        for (int i = 1; i < amount + 1; i++)
        {
            string path = $"{baseName}_{i}";
            _textures.Add(path, contentManager.Load<Texture2D>(path));
        }
    }
}
