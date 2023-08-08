using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;

class Bomb : GameObject
{
    // Poder de explosionde la bomba
    public int Power = 1;

    // Tiempo restante hasta que la bomba explote
    public float TimeLeft = 3.0f;

    // Textura de la bomba
    public static Texture2D BombTexture = null;

    public static void LoadTextures(GraphicsDevice graphicsDevice)
    {
        Bomb.BombTexture = ContentUtils.Loadtexture(graphicsDevice, "./assets/textures/bomb.png");
    }

    public override void Initialize(GraphicsDevice graphicsDevice)
    {
        this.Texture = Bomb.BombTexture;

        this.Body = this.Scene.World.CreateCircle(12.0f, 0.0f, this.Position, BodyType.Static);
        this.Body.FixedRotation = false;
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

        // Animate bomb scale and rotation
        float total = (float)time.TotalGameTime.TotalSeconds;
        this.Scale = (float)(Math.Cos(total * 3.0f)) * 0.1f + 0.9f;
    }
}
