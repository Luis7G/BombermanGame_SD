using System;


internal class FieldGenerator
{
    public static void Generate(Scene scene)
    {
        int size = 19;
        float spacing = 45.0f;

        // Piso
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

        // Paredes Laterales
        for (int i = 0; i < size; i++)
        {
            Block up = new Block(false);
            up.Position.Y = 0.0f;
            up.Position.X = i * spacing - 1;
            scene.Add(up);

            Block down = new Block(false);
            down.Position.Y = (size - 1) * spacing;
            down.Position.X = i * spacing;
            scene.Add(down);

            Block left = new Block(false);
            left.Position.X = 0.0f;
            left.Position.Y = i * spacing;
            scene.Add(left);

            Block right = new Block(false);
            right.Position.X = (size - 1) * spacing;
            right.Position.Y = i * spacing;
            scene.Add(right);
        }

        // Centrales
        int[] spaces = {2, 4, 6, 8, 10, 12, 14, 16};

        foreach (int a in spaces)
        {
            foreach (int b in spaces)
            {
                Block middle = new Block(false);
                middle.Position.X = a * spacing;
                middle.Position.Y = b * spacing;
                scene.Add(middle);
            }
        }

        // Bloques
        Random random = new Random();

        int blocks = 200;
        bool occupied = false;
        for (int i = 0; i < blocks; i++)
        {
            Block block = new Block(true);
            block.Position.X = (1 + random.Next(size - 2)) * spacing;
            block.Position.Y = (1 + random.Next(size - 2)) * spacing;
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
                if(occupied == true) break;
            }

            if (occupied == false) {
                if(block.Position.X != 1 * spacing && block.Position.Y != 1 * spacing) scene.Add(block);
            }

        }

        Player player = new Player(new PlayerKeyboardControls(PlayerKeyboardControls.keysPlayer)); 
        player.Position.X = 45.0f;
        player.Position.Y = 45.0f;
        scene.Add(player);
    }
}