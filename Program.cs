using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

new GameLoop().Run();

// Clase Main del juego que act�a como el ciclo principal del juego y maneja la inicializaci�n, actualizaci�n y renderizado del juego.
public class GameLoop : Game
{
    private GraphicsDeviceManager graphics;

    private SpriteBatch spriteBatch;

    private SpriteFont font;

    private Scene scene = null;

    private Camera camera = new Camera();

    private MainMenu mainMenu;
    private PauseMenu pauseMenu;

    public bool isMainMenuVisible;
    public bool isPauseMenuVisible;
    private bool isEscapePressed = false;

    // Configurar el entorno del juego, como las preferencias de tama�o de ventana, el directorio de contenido, la visibilidad del cursor del mouse, etc.
    public GameLoop()
    {
        this.graphics = new GraphicsDeviceManager(this);
        this.graphics.PreferredBackBufferHeight = 855;
        this.graphics.PreferredBackBufferWidth = 855;
        this.graphics.GraphicsProfile = GraphicsProfile.Reach;

        this.Window.AllowUserResizing = true;
        this.Window.AllowAltF4 = true;

        this.Content.RootDirectory = "Content";
        this.IsMouseVisible = true;
    }

    // Realizar la inicializaci�n de componentes, cargar texturas y fuentes, configurar la escena y los men�s.
    protected override void Initialize()
    {
        Ground.LoadTextures(this.GraphicsDevice);
        Bomb.LoadTextures(this.GraphicsDevice);
        Block.LoadTextures(this.GraphicsDevice);
        Player.LoadTextures(this.GraphicsDevice);
        Fire.LoadTextures(this.GraphicsDevice);

        base.Initialize();

        this.scene = new Scene(this.GraphicsDevice);


        mainMenu = new MainMenu(GraphicsDevice);
        pauseMenu = new PauseMenu(GraphicsDevice);
        isMainMenuVisible = true;
        isPauseMenuVisible = false;

        this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

        this.font = ContentUtils.LoadFont(this.GraphicsDevice, "./assets/fonts/PressStart2P.ttf");

    }

    //Controla la aparici�n y desaparici�n del men� de pausa, as� como la actualizaci�n de la escena y los men�s.
    protected override void Update(GameTime time)
    {

        if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !isMainMenuVisible && !isEscapePressed)
        {
            isPauseMenuVisible = !isPauseMenuVisible;
            isEscapePressed = true;
        }

        if (Keyboard.GetState().IsKeyUp(Keys.Escape) && !isMainMenuVisible && isEscapePressed)
        {
            isEscapePressed = false;
        }

        base.Update(time);
        mainMenu.Update(time, this, this.spriteBatch);
        pauseMenu.Update(time, this, mainMenu);

        this.scene.Update(time, this);
    }

    // Dibuja los objetos en la escena, as� como los men�s principales y de pausa.
    protected override void Draw(GameTime time)
    {
        GraphicsDevice.Clear(Color.Black);

        this.spriteBatch.Begin();

        this.scene.Render(time, this.spriteBatch);

        //this.spriteBatch.DrawString(this.font, "Bomberman", new Vector2(10, 10), Color.White, 0.0f, new Vector2(), 0.6f, SpriteEffects.None, 0);

        if (isMainMenuVisible)
        {
            mainMenu.Draw(spriteBatch, font, this.graphics.PreferredBackBufferHeight, this.graphics.PreferredBackBufferHeight);
        }

        if (isPauseMenuVisible)
        {
            pauseMenu.Draw(spriteBatch, font, this.graphics.PreferredBackBufferHeight, this.graphics.PreferredBackBufferHeight);
        }


        this.spriteBatch.End();

        base.Draw(time);
    }

    // Devuelve una referencia a la instancia de la clase Scene, lo que permite a otras partes del juego acceder y manipular la escena.
    public Scene GetScene()
    {
        return scene;
    }
}



