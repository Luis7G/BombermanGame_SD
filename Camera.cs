using Microsoft.Xna.Framework;


// La camara se usa para controlar la ventana grafica del juego
class Camera
{
    // Nivel de zoom de la camara.
    public float Zoom = 1.0f;

    // Posicion de la camara.
    public Vector2 Position = new Vector2();

    // Obtiene la matriz de transformacion de la camara.
    public Matrix transformationMatrix()
    {
        Matrix matrix = new Matrix();

        // Aplica el zoom a la camara
        matrix.M11 = Zoom;
        matrix.M22 = Zoom;
        matrix.M33 = Zoom;

        // Aplica la posicion a la camara
        matrix.M13 = Position.X;
        matrix.M23 = Position.Y;

        return matrix;
    }
}
