//Librer�as relacionados con la representaci�n gr�fica y f�sica del juego.
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

class Block : GameObject
{
    // Si es verdadero el bloque se puede destruir con una bomba
    public bool Destructible = false;

    // la caja explota cuando una bomba la destruya
    public bool Explosive = false;

    // Un array de texturas para los bloques. Parece que se esperan tres texturas, pero solo se cargan dos en el c�digo.
    public static Texture2D[] Textures = new Texture2D[3];

    //Constructor
    public Block(bool destructible, bool explosive)
    {
        this.Destructible = destructible;
        this.Explosive = explosive;
    }

    //M�todo para cargar las texturas de los bloques desde el directorio de texturas.
    public static void LoadTextures(GraphicsDevice graphicsDevice)
    {
        string[] textures = { "crate_01.png", "crate_42.png" };
        for (int i = 0; i < textures.Length; i++)
        {
            //Se utiliza el m�todo ContentUtils.Loadtexture para cargar las texturas en Block.Textures.
            Block.Textures[i] = ContentUtils.Loadtexture(graphicsDevice, "./assets/textures/" + textures[i]);
        }
    }

    //M�todo para inicializar un bloque
    public override void Initialize(GraphicsDevice graphicsDevice)
    {
        //Define la textura del bloque dependiendo de si es destructible o no.
        this.Texture = this.Destructible ? Block.Textures[1] : Block.Textures[0];

        //Crea el cuerpo f�sico asociado al bloque en el mundo del juego. 
        this.Body = this.Scene.World.CreateRectangle(this.Size.X - 0.1f, this.Size.Y - 0.1f, this.Rotation, this.Position, 0.0f, this.Destructible && this.Destructible ? BodyType.Dynamic : BodyType.Static);
        this.Body.FixedRotation = false;
        this.Body.Tag = this;

        if (this.Destructible)
        {
            //Se crea un cuerpo din�mico para el bloque que puede interactuar con otros objetos en el mundo f�sico.
            this.Body.Mass = 1000000000000000000.0f;
            this.Body.OnCollision += this.OnCollision;
        }
    }

    //Procesar colisiones con otros objetos en la escena.
    public bool OnCollision(Fixture self, Fixture other, Contact contact)
    {
        if (this.Destructible)
        {
            Body body = other.Body;
            if (body.Tag is Fire)
            {
                this.Destroy(); // Llama a la funci�n Destroy cuando el bloque es destruido
            }
        }

        return true;
    }


    //M�todo llamado cuando el bloque es destruido.
    public override void Destroy()
    {
        base.Destroy(); //Llamar al m�todo Destroy de la clase base GameObject.
    }
}