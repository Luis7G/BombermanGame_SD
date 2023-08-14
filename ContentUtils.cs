using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System.IO;

class ContentUtils
{
    // Carga una textura de una imagen    
    public static Texture2D Loadtexture(GraphicsDevice graphicsDevice, string fname)
    {
        FileStream fileStream = new FileStream(fname, FileMode.Open);
        Texture2D texture = Texture2D.FromStream(graphicsDevice, fileStream);
        fileStream.Dispose();
        return texture;
    }

    // Carga una fuente de un archivo ttf
    public static SpriteFont LoadFont(GraphicsDevice graphicsDevice, string fname)
    {
        //Se utiliza el método TtfFontBaker.Bake para convertir los datos de fuente en una fuente de sprite.
        //Se especifica un tamaño de fuente de 20 y se incluyen varios rangos de caracteres (básico latino, suplemento latino, latín extendido A y cirílico) para la fuente.
        return TtfFontBaker.Bake(File.ReadAllBytes(fname), 20, 1024, 1024, new[]
            {
                CharacterRange.BasicLatin,
                CharacterRange.Latin1Supplement,
                CharacterRange.LatinExtendedA,
                CharacterRange.Cyrillic
            }
        // Se crea una fuente de sprite utilizando el GraphicsDevice.
        ).CreateSpriteFont(graphicsDevice);
    }
}