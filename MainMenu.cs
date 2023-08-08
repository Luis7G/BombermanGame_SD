using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class MainMenu
{
    public Texture2D background;

    public MainMenu(GraphicsDevice graphicsDevice)
    {
        background = ContentUtils.Loadtexture(graphicsDevice, "./assets/textures/Main menu background.jpg");
    }

    public void Update(GameTime time, GameLoop gameLoop, SpriteBatch spriteBatch)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.S) && gameLoop.isMainMenuVisible)
        {
            System.Environment.Exit(0);
        }

        if (Keyboard.GetState().IsKeyDown(Keys.N) && gameLoop.isMainMenuVisible)
        {
            this.Hide(gameLoop); 
            FieldGenerator.Generate(gameLoop.GetScene());
        }
    }

    public void Show(GameLoop gameLoop)
    {
        gameLoop.isMainMenuVisible = true; 
    }

    public void Hide(GameLoop gameLoop)
    {
        gameLoop.isMainMenuVisible = false; 
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont font, int width, int high)
    {
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        spriteBatch.DrawString(font, "Bomberman", new Vector2(width/5, high/3), Color.White, 0.0f, new Vector2(), 3.0f, SpriteEffects.None, 0);
        spriteBatch.DrawString(font, "[N] Comenzar nueva partida", new Vector2(width/5, high/2), Color.White, 0.0f, new Vector2(), 1.0f, SpriteEffects.None, 0);
        spriteBatch.DrawString(font, "[C] Cargar partida", new Vector2(width/5, high/2 + high/10), Color.White, 0.0f, new Vector2(), 1.0f, SpriteEffects.None, 0);
        spriteBatch.DrawString(font, "[S] Salir", new Vector2(width/5, high/2 + high/5), Color.White, 0.0f, new Vector2(), 1.0f, SpriteEffects.None, 0);

    }
}