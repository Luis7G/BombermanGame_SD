using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class PauseMenu
{
    private Texture2D background;

    public PauseMenu(GraphicsDevice graphicsDevice)
    {
        background = ContentUtils.Loadtexture(graphicsDevice, "./assets/textures/pause.png");
    }

    public void Update(GameTime time, GameLoop gameLoop, MainMenu mainMenu)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.V) && gameLoop.isPauseMenuVisible)
        {
            this.Hide(gameLoop); 
            gameLoop.GetScene().ClearObjects();
            mainMenu.Show(gameLoop);
        }

        if (Keyboard.GetState().IsKeyDown(Keys.C) && gameLoop.isPauseMenuVisible)
        {
            this.Hide(gameLoop); 
        }

        if (Keyboard.GetState().IsKeyDown(Keys.R) && gameLoop.isPauseMenuVisible)
        {
            this.Hide(gameLoop); 
            FieldGenerator.Generate(gameLoop.GetScene());
        }
    }

    public void Show(GameLoop gameLoop)
    {
        gameLoop.isPauseMenuVisible = true; 
    }

    public void Hide(GameLoop gameLoop)
    {
        gameLoop.isPauseMenuVisible = false; 
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont font, int width, int high)
    {
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        spriteBatch.DrawString(font, "Pause", new Vector2(width/5, high/3), Color.White, 0.0f, new Vector2(), 3.0f, SpriteEffects.None, 0);
        spriteBatch.DrawString(font, "[C] Continuar partida", new Vector2(width/5, high/2), Color.White, 0.0f, new Vector2(), 1.0f, SpriteEffects.None, 0);
        spriteBatch.DrawString(font, "[R] Reiniciar partida", new Vector2(width/5, high/2 + high/10), Color.White, 0.0f, new Vector2(), 1.0f, SpriteEffects.None, 0);
        spriteBatch.DrawString(font, "[V] Salir al menu principal", new Vector2(width/5, high/2 + high/5), Color.White, 0.0f, new Vector2(), 1.0f, SpriteEffects.None, 0);
    }
}