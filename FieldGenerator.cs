using System;
using Microsoft.Xna.Framework;

// Generar el terreno o escenario del juego con bloques, suelos y el jugador.
internal class FieldGenerator
{
    public static void Generate(Scene scene)
    {
        int size = 19; // Tamaño del campo de juego
        float spacing = 45.0f; // Espaciado entre elementos 

        // Se generan los suelos en una cuadrícula con texturas alternadas según la posición.
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Ground floor = new Ground();
                floor.Texture = Ground.Textures[(x + y) % 2];
                floor.Position.X = x * spacing;
                floor.Position.Y = y * spacing;
                scene.Add(floor);

            }
        }

        // Se generan los bordes de bloques alrededor del campo de juego.
        for (int i = 0; i < size; i++)
        {
            // Bordes superiores e inferiores
            Block up = new Block(false, false);
            up.Position.Y = 0.0f;
            up.Position.X = i * spacing - 1;
            scene.Add(up);

            Block down = new Block(false, false);
            down.Position.Y = (size - 1) * spacing;
            down.Position.X = i * spacing;
            scene.Add(down);

            // Bordes izquierdo y derecho
            Block left = new Block(false, false);
            left.Position.X = 0.0f;
            left.Position.Y = i * spacing;
            scene.Add(left);

            Block right = new Block(false, false);
            right.Position.X = (size - 1) * spacing;
            right.Position.Y = i * spacing;
            scene.Add(right);
        }

        // Se generan bloques en posiciones específicas dentro del campo.
        int[] spaces = { 2, 4, 6, 8, 10, 12, 14, 16 };

        foreach (int a in spaces)
        {
            foreach (int b in spaces)
            {
                Block middle = new Block(false, false);
                middle.Position.X = a * spacing;
                middle.Position.Y = b * spacing;
                scene.Add(middle);
            }
        }

        // Se generan bloques aleatorios, asegurando que no se superpongan con las posiciones especiales generadas anteriormente.
        Random random = new Random();

        int blocks = 200;
        bool occupied = false;
        for (int i = 0; i < blocks; i++)
        {
            Block block = new Block(true, random.NextDouble() > 0.8);
            block.Position.X = (1 + random.Next(size - 2)) * spacing;
            block.Position.Y = (1 + random.Next(size - 2)) * spacing;

            // Verificar que el espacio no esté ocupado
            foreach (int x in spaces)
            {
                foreach (int y in spaces)
                {
                    if (block.Position.X == x * spacing && block.Position.Y == y * spacing)
                    {
                        occupied = true;
                        break;
                    }
                    else
                    {
                        occupied = false;
                    }
                }
                if (occupied == true) break;
            }

            if (occupied == false)
            {
                // Agregar el bloque si el espacio no está ocupado
                if (block.Position.X != 1 * spacing && block.Position.Y != 1 * spacing) scene.Add(block);
            }

        }

        // Se agrega un jugador a la escena.
        Player player = new Player(new PlayerKeyboardControls(PlayerKeyboardControls.keysPlayer));
        player.Position.X = 45.0f;
        player.Position.Y = 45.0f;
        scene.Add(player);

        // Agregar un enemigo a la escena.
        AddEnemies(scene, player);

    }
    private static void AddEnemies(Scene scene, Player player)
    {
        int enemyCount = 1; // Number of enemies you want to add

        for (int i = 0; i < enemyCount; i++)
        {
            Enemy enemy = new Enemy(player);
            enemy.Position.X = 210.0f;
            enemy.Position.Y = 45.0f;

            scene.Add(enemy);
        }
    }
}