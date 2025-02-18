using System;
using System.Collections.Generic;
using System.Linq;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ItemTooltip
{
    public static ItemTooltip Instance { get; } = new ItemTooltip();
    public static Item Item { get; private set; }

    private static SpriteFont _font = Assets.Fonts.MonogramExtened;
    private static int _PADDING = 5;
    private static int _ELEMENT_SPACING = 4;
    private static int _STRING_SPACING = 2;

    private List<TooltipElement> _elements = new();
    private int _tooltipWidth;
    private int _tooltipHeight;

    public void SetItem(Item item)
    {
        Item = item;
        BuildTooltip(Item);
    }

    private void BuildTooltip(Item item)
    {
        _elements.Clear();
        List<int> stringWidths = [];

        string uniqueNameString = GetUniqueName(item);
        if (uniqueNameString.Count() != 0)
        {
            AddText(uniqueNameString, item.Rarity.GetColor(), ref stringWidths);
        }

        string nameString = GetRarityName(item);
        AddText(nameString, item.Rarity.GetColor(), ref stringWidths);

        if (item is MaterialItem material)
        {
            AddHorizontalRule();
            string stackSizeString =
                $"Stack Size:{material.StackQuantity}/{material.MaxStackQuantity}";
            AddText(stackSizeString, Color.Gray, ref stringWidths);
            AddHorizontalRule();
            AddText(material.Description, Color.White, ref stringWidths);
        }
        else if (item is EquippableItem equippable)
        {
            AddHorizontalRule();

            if (equippable.LevelRequirement > 1)
            {
                AddText(
                    $"Requires Level {equippable.LevelRequirement}",
                    Color.Gray,
                    ref stringWidths
                );
            }

            AddText($"Item Level {equippable.Level}", Color.Gray, ref stringWidths);

            if (equippable.BaseAffixes.Count > 0)
            {
                AddHorizontalRule();
                foreach (Affix baseAffix in equippable.BaseAffixes)
                {
                    AddText(baseAffix.ToStringBase(), ItemColors.Normal, ref stringWidths);
                }
            }

            if (equippable.ImplicitAffixes.Count > 0)
            {
                AddHorizontalRule();
                foreach (Affix implicitAffix in equippable.ImplicitAffixes)
                {
                    AddText(implicitAffix.ToString(), ItemColors.Magic, ref stringWidths);
                }
            }

            if (equippable.Prefixes.Count + equippable.Suffixes.Count > 0)
            {
                AddHorizontalRule();

                foreach (Affix prefix in equippable.Prefixes)
                {
                    AddText(prefix.ToString(), ItemColors.Magic, ref stringWidths);
                }

                foreach (Affix suffix in equippable.Suffixes)
                {
                    AddText(suffix.ToString(), ItemColors.Magic, ref stringWidths);
                }
            }

            if (equippable is IUnique unique)
            {
                AddHorizontalRule();
                foreach (Affix affix in unique.UniqueAffixes)
                {
                    AddText(affix.ToString(), ItemColors.Magic, ref stringWidths);
                }

                AddHorizontalRule();
                AddText(unique.UniqueFlavorText, ItemColors.Unique, ref stringWidths);
            }
        }

        CalculateTooltipSize(stringWidths);
    }

    private void CalculateTooltipSize(List<int> stringWidths)
    {
        _tooltipWidth = (stringWidths.Count > 0 ? stringWidths.Max() : 100) + _PADDING * 2;
        _tooltipHeight =
            _PADDING * 2 + _elements.Sum(e => e.Height + _ELEMENT_SPACING) - _ELEMENT_SPACING;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 tooltipOrigin = new(
            (Game1.NativeResolution.Width - _tooltipWidth) / 2,
            (Game1.NativeResolution.Height - _tooltipHeight) / 2
        );

        DrawBackground(spriteBatch, tooltipOrigin);

        Vector2 position = tooltipOrigin + new Vector2(_PADDING, _PADDING);
        foreach (var element in _elements)
        {
            if (element.IsLine)
                DrawHorizontalLine(spriteBatch, position, element.Color);
            else
                DrawString(spriteBatch, element.Text, position, element.Color);

            position.Y += element.Height + _ELEMENT_SPACING;
        }
    }

    private void AddText(string text, Color color, ref List<int> stringWidths)
    {
        Vector2 textSize = _font.MeasureString(text);
        _elements.Add(new TooltipElement(text, color, (int)textSize.Y));
        stringWidths.Add((int)textSize.X);
    }

    private void AddHorizontalRule()
    {
        _elements.Add(new TooltipElement());
    }

    private string GetUniqueName(Item item)
    {
        if (item is IUnique unique)
            return unique.UniqueName;
        return "";
    }

    private string GetRarityName(Item item)
    {
        return item switch
        {
            EquippableItem equip when equip.Rarity == Rarity.Normal => $"{item.Name}",
            EquippableItem equip when equip.Rarity == Rarity.Magic => $"Magic {item.Name}",
            EquippableItem equip when equip.Rarity == Rarity.Rare => $"Rare {item.Name}",
            EquippableItem equip when equip.Rarity == Rarity.Unique => $"Unique {item.Name}",
            _ => $"{item.Name}",
        };
    }

    private void DrawBackground(SpriteBatch spriteBatch, Vector2 origin)
    {
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle((int)origin.X, (int)origin.Y, _tooltipWidth, _tooltipHeight),
            null,
            new Color(0, 0, 0, 0.85f),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.ItemTooltipBackground
        );
    }

    private void DrawString(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
    {
        spriteBatch.DrawString(
            _font,
            text,
            position,
            color,
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.ItemTooltipContent
        );
    }

    private void DrawHorizontalLine(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle((int)position.X, (int)position.Y, _tooltipWidth - _PADDING * 2, 1),
            null,
            color,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.ItemTooltipContent
        );
    }

    private class TooltipElement
    {
        public string Text { get; }
        public Color Color { get; }
        public int Height { get; }
        public bool IsLine { get; }

        public TooltipElement(string text, Color color, int height)
        {
            Text = text;
            Color = color;
            Height = height;
            IsLine = false;
        }

        public TooltipElement(Color? color = null)
        {
            Color = color ?? Color.Gray;
            Height = 1;
            IsLine = true;
        }
    }
}
