using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class MainMenu
{
    public Texture2D background; //Textura utilizada como fondo para el menú principal.

    //El constructor de la clase carga la textura de fondo.
    public MainMenu(GraphicsDevice graphicsDevice)
    {
        background = ContentUtils.Loadtexture(graphicsDevice, "./assets/textures/Main menu background.jpg");
    }

    public void Update(GameTime time, GameLoop gameLoop, SpriteBatch spriteBatch)
    {
        // Si se presiona la tecla "S" y el menú principal está visible, el juego se cierra.
        if (Keyboard.GetState().IsKeyDown(Keys.S) && gameLoop.isMainMenuVisible)
        {
            System.Environment.Exit(0);
        }

        // Si se presiona la tecla "N" y el menú principal está visible, se oculta el menú principal y se genera el campo de juego.
        if (Keyboard.GetState().IsKeyDown(Keys.N) && gameLoop.isMainMenuVisible)
        {
            this.Hide(gameLoop); 
            FieldGenerator.Generate(gameLoop.GetScene());
        }
    }

    // Mostrar y ocultar el menú principal
    public void Show(GameLoop gameLoop)
    {
        gameLoop.isMainMenuVisible = true; 
    }

    public void Hide(GameLoop gameLoop)
    {
        gameLoop.isMainMenuVisible = false; 
    }

    // Dibujar los elementos del menú principal en la pantalla
    public void Draw(SpriteBatch spriteBatch, SpriteFont font, int width, int high)
    {
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        spriteBatch.DrawString(font, "Bomberman", new Vector2(width/5, high/3), Color.White, 0.0f, new Vector2(), 3.0f, SpriteEffects.None, 0);
        spriteBatch.DrawString(font, "[N] Comenzar nueva partida", new Vector2(width/5, high/2), Color.White, 0.0f, new Vector2(), 1.0f, SpriteEffects.None, 0);
        spriteBatch.DrawString(font, "[C] Cargar partida", new Vector2(width/5, high/2 + high/10), Color.White, 0.0f, new Vector2(), 1.0f, SpriteEffects.None, 0);
        spriteBatch.DrawString(font, "[S] Salir", new Vector2(width/5, high/2 + high/5), Color.White, 0.0f, new Vector2(), 1.0f, SpriteEffects.None, 0);

    }
}