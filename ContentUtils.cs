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
        return TtfFontBaker.Bake(File.ReadAllBytes(fname), 20, 1024, 1024, new[]
            {
                CharacterRange.BasicLatin,
                CharacterRange.Latin1Supplement,
                CharacterRange.LatinExtendedA,
                CharacterRange.Cyrillic
            }
        ).CreateSpriteFont(graphicsDevice);
    }
}