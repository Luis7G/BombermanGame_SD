using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

internal class Fire : GameObject
{
    // Tiempo restante hasta que la bomba explote. Cuando el tiempo restante llega a cero, el efecto de explosión se elimina.
    public float TimeLeft = 1.0f;

    // Textura de la bomba
    public static Texture2D FireTexture = null;

    public static void LoadTextures(GraphicsDevice graphicsDevice)
    {
        Fire.FireTexture = ContentUtils.Loadtexture(graphicsDevice, "./assets/textures/explosion.png");
    }

    public override void Initialize(GraphicsDevice graphicsDevice)
    {

        // Define la textura del efecto de explosión y crea un cuerpo físico rectangular estático en el mundo del juego.
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
            // Se elimina el efecto de explosión de la escena.
            this.Scene.Remove(this);
        }

    }
}
