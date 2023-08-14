using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

// los objetos del juego son autonomos y tienen su logica.
// Los objetos del juego se colocaran en la escena que se requiera.
// La clase de GameObject debe ser heredada por otros objetos que implementaran el control requerido.

public class GameObject
{
    public static int ID = 0;

    // Identificador del objeto
    // Cada objeto tiene su propio ID.
    public int Id = ID++;

    //Visibilidad del objeto. Si es falso el objeto no se renderizara.
    public bool Visible = true;

    // Escena donde el objeto se utiliza
    public Scene Scene = null;

    // Textura 2D que usa el objeto
    public Texture2D Texture;

    // Posicion del objeto en la pantalla 2D
    public Vector2 Position = new Vector2(0.0f, 0.0f);

    // Tamaño del objeto en la pantalla
    public Vector2 Size = new Vector2(45.0f, 45.0f);

    // Punto central u Origen del objeto.
    public Vector2 Origin = new Vector2(15.0f, 15.0f);

    // Escala del objeto aplicado despues de darle el tamaño
    public float Scale = 1.0f;

    // Rotacion del objeto
    public float Rotation = 0.0f;

    // Simulacion de fisicas. Tomado del Aether.Physics2D
    public Body Body = null;

    // Revisar si el objeto esta colicionando con otro objeto.
    public bool Colliding(GameObject obj)
    {
        Rectangle a = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Size.X, (int)this.Size.Y);
        Rectangle b = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, (int)obj.Size.X, (int)obj.Size.Y);

        return a.Intersects(b);
    }


    // Dibuja el objeto en la pantalla. El objeto es renderizado en un sprite batch o lote de sprites
    public virtual void Render(GameTime time, SpriteBatch spriteBatch)
    {
        Vector2 scale = new Vector2(this.Size.X / this.Texture.Width, this.Size.Y / this.Texture.Height) * this.Scale;

        Vector2 origin = new Vector2(this.Origin.X / this.Size.X * this.Texture.Width, this.Origin.Y / this.Size.Y * this.Texture.Height);

        Vector2 position = new Vector2(this.Position.X + this.Origin.X, this.Position.Y + this.Origin.Y);

        spriteBatch.Draw(this.Texture, position, new Rectangle(0, 0, this.Texture.Width, this.Texture.Height), Color.White, this.Rotation, origin, scale, SpriteEffects.None, 0);
    }

    // Inicializa el objeto. Carga los recursos si es necesario. Se llama cuando el objeto se carga a la escena.
    public virtual void Initialize(GraphicsDevice graphicsDevice) { }

    // Actualiza la logica del juego. 
    public virtual void Update(GameTime time)
    {
        if (this.Body != null)
        {
            this.Position = this.Body.Position;
            this.Rotation = this.Body.Rotation;
        }
    }

    // Remueve el objeto de la escena.
    public virtual void Destroy()
    {
        this.Scene.Remove(this);
    }
}