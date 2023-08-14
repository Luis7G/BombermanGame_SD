
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using tainicom.Aether.Physics2D.Dynamics;

// Las escenas son una coleccion de objetos renderizados que se dibujan en pantalla
// Representa el entorno y la lógica del juego, controlando la gestión de objetos, la simulación física y el proceso de actualización y renderizado. 

public class Scene
{   // Lista de objetos en la escena
    // Estos son actualizados y renderizados en orden
    public List<GameObject> Objects = new List<GameObject>();

    // Simulador de fisicas del mundo. Tomado del Aether.Physics2D
    public World World = null;

    // Dispositivo de gráficos usados por los objetos de la escena. Tomado de Xna.Framework.Graphics
    public GraphicsDevice GraphicsDevice = null;

    // El constructor inicializa la instancia del mundo físico, establece la gravedad en cero (indicando que no hay gravedad en este caso)
    // y almacena la referencia al dispositivo gráfico utilizado.
    public Scene(GraphicsDevice graphicsDevice)
    {
        this.GraphicsDevice = graphicsDevice;
        this.World = new World();
        this.World.Gravity = new Vector2(0.0f, 0.0f);
    }

    // Añade un nuevo objeto a la escena
    public void Add(GameObject obj)
    {
        if (obj.Scene != null)
        {
            throw new System.Exception("El objeto no tiene una escena definida.");
        }

        obj.Scene = this;
        this.Objects.Add(obj);
        obj.Initialize(this.GraphicsDevice);
    }

    // Remueve un objeto de la escena}
    public void Remove(GameObject obj)
    {
        this.Objects.Remove(obj);

        // Si el objeto tiene un cuerpo físico asociado, lo elimina del mundo físico.
        if (obj.Body != null)
        {
            this.World.RemoveAsync(obj.Body);
        }
    }

    // Actualiza el estado de los objetos en la escena
    public void Update(GameTime time, GameLoop gameLoop)
    {
        this.World.Step((float)time.ElapsedGameTime.TotalSeconds);

        GameObject[] objs = this.Objects.ToArray();
        // Realiza una iteración de la simulación física, actualiza todos los objetos en la escena (excepto si el menú de pausa está visible).
        if (!gameLoop.isPauseMenuVisible)
        {
            foreach (GameObject obj in objs)
            {
                obj.Update(time);
            }
        }
    }

    // Renderiza los objetos en la escena
    public void Render(GameTime time, SpriteBatch spriteBatch)
    {
        foreach (GameObject obj in this.Objects)
        {
            obj.Render(time, spriteBatch);
        }
    }

    // Borra todos los objetos de la lista de objetos de la escena.
    public void ClearObjects()
    {
        Objects.Clear();
    }
}