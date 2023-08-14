using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using tainicom.Aether.Physics2D.Dynamics;

class Bomb : GameObject
{
    // Poder de explosion de la bomba, que indica cuántos bloques de fuego se propagarán desde la bomba al explotar.
    public int Power = 1;

    // Tiempo restante hasta que la bomba explote
    public float TimeLeft = 3.0f;

    // Textura de la bomba
    public static Texture2D BombTexture = null;
  
    // Método para cargar las texturas de la bomba desde el directorio de texturas.
    public static void LoadTextures(GraphicsDevice graphicsDevice)
    {
        Bomb.BombTexture = ContentUtils.Loadtexture(graphicsDevice, "./assets/textures/bomb.png");
    }

    public override void Initialize(GraphicsDevice graphicsDevice)
    {
        // Define la textura de la bomba
        this.Texture = Bomb.BombTexture;

        // Crea un cuerpo físico circular para la bomba en el mundo del juego.
        this.Body = this.Scene.World.CreateCircle(12.0f, 0.0f, this.Position, BodyType.Static);
        this.Body.FixedRotation = false;
        this.Body.Tag = this;
    }

    // Este método se llama en cada actualización del juego. 
    public override void Update(GameTime time)
    {
        base.Update(time);

        float delta = (float)time.ElapsedGameTime.TotalSeconds;
        // Resta el tiempo transcurrido desde la última actualización a TimeLeft, lo que representa el tiempo restante antes de que la bomba explote.
        this.TimeLeft -= delta;
        if (this.TimeLeft < 0)
        {
            this.Scene.Remove(this);
            this.Explode();
        }

        // Animate bomb scale and rotation
        float total = (float)time.TotalGameTime.TotalSeconds;
        this.Scale = (float)(Math.Cos(total * 3.0f)) * 0.1f + 0.9f;
    }

    //Explota la bomba y crea elementos de fuego.

    //El fuego matará al jugador y destruirá las paredes.

    public void Explode()
    {
        var fire = new Fire();
        fire.Position.X = this.Position.X;
        fire.Position.Y = this.Position.Y;
        this.Scene.Add(fire);

        // Crea una serie de objetos de tipo Fire que representan las explosiones en diferentes direcciones, según el poder de la bomba. 
        for (int i = 1; i <= Power; i++)
        {
            var l = new Fire();
            l.Position.X = this.Position.X + i * 30;
            l.Position.Y = this.Position.Y;
            this.Scene.Add(l);

            var r = new Fire();
            r.Position.X = this.Position.X - i * 30;
            r.Position.Y = this.Position.Y;
            this.Scene.Add(r);

            var u = new Fire();
            u.Position.Y = this.Position.Y + i * 30;
            u.Position.X = this.Position.X;
            this.Scene.Add(u);

            var d = new Fire();
            d.Position.Y = this.Position.Y - i * 30;
            d.Position.X = this.Position.X;
            this.Scene.Add(d);
        }
    }


}
