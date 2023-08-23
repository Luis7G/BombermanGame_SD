//Librerías del framework XNA o MonoGame utilizado para crear juegos en C#.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// AnimatedSprite es usado para renderizar una textura con animacion usando un atlas

// La siguiente clase es subclase de GameObject
class AnimatedSprite : GameObject
{
    //Números de filas y columnas en la hoja de sprites.
    public int Rows; 

    public int Columns;

    private int frames; //Cantidad total de cuadros en la animación.

    private int currentFrame; //El cuadro actual que se muestra en la animación.

    //Constructor de la clase
    public AnimatedSprite(Texture2D texture, int rows, int columns, int frames = 0)
    {
        this.Texture = texture; //La textura que contiene los sprites animados.
        this.Rows = rows;
        this.Columns = columns;
        this.currentFrame = 0;
        this.frames = frames == 0 ? Rows * Columns : frames; //Opcional, la cantidad total de cuadros en la animación. Si no se proporciona, se calculará automáticamente como rows * columns.
    }

    //Método para actualizar el estado del sprite animado
    public override void Update(GameTime time)
    {
        //Se incrementa el valor de currentFrame en uno y, si alcanza la cantidad total de cuadros (frames), lo reinicia a cero, lo que permite la animación cíclica
        this.currentFrame++;
        if (this.currentFrame == this.frames)
        {
            this.currentFrame = 0;
        }
    }

    //Método para dibujar el sprite animado en la pantalla.
    public override void Render(GameTime time, SpriteBatch spriteBatch)
    {
        //Se calculan las coordenadas y dimensiones del fragmento de la textura a mostrar en función del cuadro actual (currentFrame
        int width = this.Texture.Width / this.Columns;
        int height = this.Texture.Height / this.Rows;
        int row = this.currentFrame / this.Columns;
        int column = this.currentFrame % this.Columns;

        Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
        Vector2 scale = new Vector2(this.Size.X / sourceRectangle.Width, this.Size.Y / sourceRectangle.Height);

        spriteBatch.Draw(this.Texture, this.Position, sourceRectangle, Color.White, this.Rotation, this.Origin, scale, SpriteEffects.None, 0);
    }
}
