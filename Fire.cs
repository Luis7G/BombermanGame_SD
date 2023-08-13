using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

internal class Fire : GameObject
{
    // Tiempo restante hasta que la bomba explote
    public float TimeLeft = 1.0f;

    // Textura de la bomba
    public static Texture2D FireTexture = null;

    public static void LoadTextures(GraphicsDevice graphicsDevice)
    {
        Fire.FireTexture = ContentUtils.Loadtexture(graphicsDevice, "./assets/textures/explosion.png");
    }

    public override void Initialize(GraphicsDevice graphicsDevice)
    {
        this.Texture = Fire.FireTexture;

        this.Body = this.Scene.World.CreateRectangle(this.Size.X, this.Size.Y, this.Rotation, this.Position, 0.0f, BodyType.Static);
        this.Body.Tag = this;
    }

    public override void Update(GameTime time)
    {
        base.Update(time);

        float delta = (float)time.ElapsedGameTime.TotalSeconds;
        this.TimeLeft -= delta;
        if (this.TimeLeft < 0)
        {
            this.Scene.Remove(this);
        }

    }
}
