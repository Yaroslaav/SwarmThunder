
using SFML.Graphics;

public interface IDrawable
{
    public int ZPosition { get; set; }
    public Shape GetShape();
}
