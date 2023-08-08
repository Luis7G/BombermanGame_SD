
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using tainicom.Aether.Physics2D.Dynamics;

// Las escenas son una coleccion de objetos renderizados que se dibujan en pantalla

public  class Scene
{   // Lista de objetos en la escena
    // Estos son actualizados y renderizados en orden
    public List<GameObject> Objects = new List<GameObject>();

    // Simulador de fisicas del mundo. Tomado del Aether.Physics2D
    public World World = null;

    // Dispositivo de gráficos usados por los objetos de la escena. Tomado de Xna.Framework.Graphics
    public GraphicsDevice GraphicsDevice = null;

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

    public void ClearObjects()
    {
        Objects.Clear();
    }
}