using Microsoft.Xna.Framework;

// La camara se usa para controlar la ventana grafica  y la vista  del juego.
class Camera
{
    // Nivel de zoom de la camara. Un valor de 1.0f indica que no hay zoom, valores mayores a 1.0f aumentan el zoom y valores menores a 1.0f disminuyen el zoom.
    public float Zoom = 1.0f;

    // Posicion de la camara. Esta posici�n determina qu� parte de la escena se est� visualizando.
    public Vector2 Position = new Vector2();

    // Obtiene la matriz de transformacion de la camara. Este se utiliza para controlar c�mo se visualiza la escena en la ventana gr�fica, incluyendo su posici�n y escala.
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
