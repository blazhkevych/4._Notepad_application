using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace task;

public class ColorName
{
    public ColorName(string name, Color color)
    {
        Name = name;
        Color = color;
    }

    public string Name { get; }
    public Color Color { get; }

    public static ReadOnlyCollection<ColorName> Colors { get; }
        = Array.AsReadOnly(new[]
        {
            new("Красный", System.Windows.Media.Colors.Red),
            new ColorName("Зелённый", System.Windows.Media.Colors.Green),
            new ColorName("Синий", System.Windows.Media.Colors.Yellow)
        });
}