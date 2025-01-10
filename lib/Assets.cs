using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace arpg;

public class Asset
{
    public readonly Texture2D Texture;
    public readonly List<Rectangle> Frames = [];

    public Asset(Texture2D texture, int frameAmount)
    {
        Texture = texture;

        int frameWidth = Texture.Width / frameAmount;
        for (int i = 0; i < frameAmount; i++)
        {
            Frames.Add(new Rectangle(frameWidth * i, 0, frameWidth, texture.Height));
        }
    }
}

public static class Assets
{
    public static class Fonts
    {
        public static SpriteFont MonogramExtened => GetFont("fonts/monogram_extended");
    }

    public static class Environment
    {
        public static Asset Cobblestone => GetTexture("environment/cobblestone_3");
    }

    public static class Player
    {
        public static Asset Idle => GetTexture("player/player_idle");
        public static Asset Walk => GetTexture("player/player_walk");
    }

    public static class Spells
    {
        public static Asset Fireball => GetTexture("spells/fireball");
        public static Asset Spark => GetTexture("spells/spark");
    }

    public static class Monsters
    {
        public static class Skeleton
        {
            public static Asset Idle => GetTexture("monsters/skeleton_ready");
            public static Asset Walk => GetTexture("monsters/skeleton_walk");
            public static Asset Death => GetTexture("monsters/skeleton_dead_near");
            public static Asset Corpse => GetTexture("monsters/skeleton_corpse");
        }
    }

    private static Dictionary<string, SpriteFont> _fonts = [];
    private static Dictionary<string, Asset> _textures = [];

    public static void Load(ContentManager contentManager)
    {
        AddFont(contentManager, "fonts/monogram_extended");

        AddTexture(contentManager, "environment/cobblestone_3", 1);

        AddTexture(contentManager, "player/player_idle", 10);
        AddTexture(contentManager, "player/player_walk", 8);

        AddTexture(contentManager, "spells/fireball", 5);
        AddTexture(contentManager, "spells/spark", 6);

        AddTexture(contentManager, "monsters/skeleton_ready", 3);
        AddTexture(contentManager, "monsters/skeleton_walk", 6);
        AddTexture(contentManager, "monsters/skeleton_dead_near", 5);
        AddTexture(contentManager, "monsters/skeleton_corpse_1", 1);
        AddTexture(contentManager, "monsters/skeleton_corpse_2", 1);
    }

    private static void AddFont(ContentManager contentManager, string path)
    {
        var font = contentManager.Load<SpriteFont>("fonts/monogram_extended");
        _fonts.Add(path, font);
    }

    private static SpriteFont GetFont(string path)
    {
        try
        {
            SpriteFont font = _fonts[path];
            return font;
        }
        catch (ArgumentNullException)
        {
            throw new SystemException($"Font {path} not found.");
        }
    }

    private static void AddTexture(ContentManager contentManager, string path, int frameAmount)
    {
        Texture2D texture = contentManager.Load<Texture2D>(path);
        _textures.Add(path, new Asset(texture, frameAmount));
    }

    private static Asset GetTexture(string path)
    {
        try
        {
            return _textures[path];
        }
        catch (ArgumentNullException)
        {
            throw new SystemException($"Texture {path} not found.");
        }
    }
}
