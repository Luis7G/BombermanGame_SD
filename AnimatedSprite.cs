using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// AnimatedSprite es usado para renderizar una textura con animacion usando un atlas
class AnimatedSprite : GameObject
{
    public int Rows;

    public int Columns;

    private int frames;

    private int currentFrame;

    public AnimatedSprite(Texture2D texture, int rows, int columns, int frames = 0)
    {
        this.Texture = texture;
        this.Rows = rows;
        this.Columns = columns;
        this.currentFrame = 0;
        this.frames = frames == 0 ? Rows * Columns : frames;
    }

    public override void Update(GameTime time)
    {
        this.currentFrame++;
        if (this.currentFrame == this.frames)
        {
            this.currentFrame = 0;
        }
    }

    public override void Render(GameTime time, SpriteBatch spriteBatch)
    {
        int width = this.Texture.Width / this.Columns;
        int height = this.Texture.Height / this.Rows;
        int row = this.currentFrame / this.Columns;
        int column = this.currentFrame % this.Columns;

        Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
        Vector2 scale = new Vector2(this.Size.X / sourceRectangle.Width, this.Size.Y / sourceRectangle.Height);

        spriteBatch.Draw(this.Texture, this.Position, sourceRectangle, Color.White, this.Rotation, this.Origin, scale, SpriteEffects.None, 0);
    }
}
