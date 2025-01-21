using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class MonsterSpawner(double frequency, int offscreenDistance)
{
    // public List<Monster> MonsterPool = [];
    public double Frequency = frequency;
    public int OffscreenDistance = offscreenDistance;
    public Rectangle SpawnEdge { 
        get => new(
            // TODO: relative to the player
            640 / 2 - OffscreenDistance,
            360 / 2 - OffscreenDistance,
            640 + OffscreenDistance * 2,
            360 + OffscreenDistance * 2
        ); 
    }
    private double _timer = 0f;

    public void Update(GameTime gameTime)
    {
        double elapsed = gameTime.ElapsedGameTime.TotalSeconds;
        _timer += elapsed;

        if (_timer >= Frequency)
        {
            Vector2 topLeftCorner = new(SpawnEdge.Left, SpawnEdge.Top);
            Vector2 topRightCorner = new(SpawnEdge.Right, SpawnEdge.Top);
            Vector2 bottomLeftCorner = new(SpawnEdge.Left, SpawnEdge.Bottom);
            Vector2 bottomRightCorner = new(SpawnEdge.Right, SpawnEdge.Bottom);

            List<(Vector2, Vector2)> lineSegments = [
                (topLeftCorner, topRightCorner),
                (bottomRightCorner, topRightCorner),
                (bottomLeftCorner, bottomRightCorner),
                (bottomLeftCorner, topLeftCorner)
            ];

            Random rng = new();
            int index = rng.Next(lineSegments.Count);

            var (start, end) = lineSegments[index];

            bool isXAxis = start.Y == end.Y;

            int x;
            int y;

            if (isXAxis)
            {
                int rngMin = (int)Math.Min(start.X, end.X);
                int rngMax = (int)Math.Max(start.X, end.X);
                x = rng.Next(rngMin, rngMax);
                y = (int)start.Y;
            }
            else
            {
                x = (int)start.X;
                int rngMin = (int)Math.Min(start.Y, end.Y);
                int rngMax = (int)Math.Max(start.Y, end.Y);
                y = rng.Next(rngMin, rngMax);
            }

            Monster monster = new()
            {
                Position = new(x, y)
            };
            GameState.Actors.Add(monster);

            _timer -= Frequency;
        }
    }
}