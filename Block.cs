using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

class Block : GameObject
{
    // Si es verdadero el bloque se puede destruir con una bomba
    public bool Destructible = false;

    // la caja explota cuando una bomba explota la destruya
    public bool Explosive = false;

    // Texturas
    public static Texture2D[] Textures = new Texture2D[3];

    public Block(bool destructible, bool explosive)
    {
        this.Destructible = destructible;
        this.Explosive = explosive;
    }

    public static void LoadTextures(GraphicsDevice graphicsDevice)
    {
        string[] textures = { "crate_01.png", "crate_42.png" };
        for (int i = 0; i < textures.Length; i++)
        {
            Block.Textures[i] = ContentUtils.Loadtexture(graphicsDevice, "./assets/textures/" + textures[i]);
        }
    }


    public override void Initialize(GraphicsDevice graphicsDevice)
    {
        this.Texture = this.Destructible ? Block.Textures[1] : Block.Textures[0];

        this.Body = this.Scene.World.CreateRectangle(this.Size.X - 0.1f, this.Size.Y - 0.1f, this.Rotation, this.Position, 0.0f, this.Destructible && this.Destructible ? BodyType.Dynamic : BodyType.Static);
        this.Body.FixedRotation = false;
        this.Body.Tag = this;

        if (this.Destructible)
        {
            this.Body.Mass = 1000000000000000000.0f;
            this.Body.OnCollision += this.OnCollision;
        }
    }

    // Procesar colisiones con otros objetos en la escena.

    public bool OnCollision(Fixture self, Fixture other, Contact contact)
    {
        if (this.Destructible)
        {
            Body body = other.Body;
            if (body.Tag is Fire)
            {
                this.Destroy(); // Llama a la función Destroy cuando el bloque es destruido
            }
        }

        return true;
    }



    public override void Destroy()
    {
        base.Destroy();

    }




}