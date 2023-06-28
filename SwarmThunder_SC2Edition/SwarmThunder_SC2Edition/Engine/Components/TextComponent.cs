
using SFML.Graphics;

public class TextComponent : GameObject
{
    private Text text;

    public void Setup(string massage, uint fontSize)
    {
        text = new(massage,new Font("arial.ttf"), fontSize);
    }

    public override void Awake(GameObjArgs args)
    {
        text.Position = args.Position;
        
    }
}