using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

class Enemy : GameObject
{
    // Velocidad a la que se mueve el enemigo
    public float Speed = 300.0f;

    // Poder de las bombas del enemigo
    public int Power = 1;

    // Tiempo que debe pasar antes de poder colocar otra bomba
    public float BombTime = 3.0f;

    // Almacena el tiempo restante antes de que el enemigo pueda colocar otra bomba.
    private float NextBombTime = 0.0f;

    // Referencia al jugador para seguirlo
    private Player player;

    //Constructor. Permite vincular el jugador al enemigo.
    public Enemy(Player player)
    {
        this.player = player;
    }

    public static Texture2D[] Textures = new Texture2D[12];

    public static void LoadTextures(GraphicsDevice graphicsDevice)
    {
        string[] textures = {
            // Arriba
            "enemy_01.png", "enemy_02.png", "enemy_03.png",
            // Izquieda
            "enemy_11.png", "enemy_12.png", "enemy_13.png",
            // Abajo
            "enemy_21.png", "enemy_22.png", "enemy_23.png",
            // Derecha
            "enemy_31.png", "enemy_32.png", "enemy_33.png",
        };

        for (int i = 0; i < textures.Length; i++)
        {
            Enemy.Textures[i] = ContentUtils.Loadtexture(graphicsDevice, "./assets/textures/enemy/" + textures[i]);
        }
    }

    public override void Initialize(GraphicsDevice graphicsDevice)
    {
        // Define la textura inicial del enemigo y crea un cuerpo físico circular en el mundo del juego.
        this.Texture = Player.Textures[6]; // Use the same texture for now

        this.Scale = 1.0f;

        this.Body = this.Scene.World.CreateCircle(11.0f, 0.0f, this.Position, BodyType.Dynamic);
        this.Body.Tag = this;
        this.Body.FixedRotation = true;
        this.Body.LinearDamping = 4.0f;
        this.Body.OnCollision += this.OnCollision;
    }

    public override void Update(GameTime time)
    {
        base.Update(time);

        float delta = (float)time.ElapsedGameTime.TotalSeconds;

        // Move towards the player
        Vector2 direction = player.Position - this.Position;
        direction.Normalize();
        this.Body.ApplyLinearImpulse(direction * Speed * delta);

        // Decrementa el tiempo de la siguiente bomba
        if (this.NextBombTime > 0)
        {
            this.NextBombTime -= delta;
        }

        if (this.NextBombTime <= 0)
        {
            // Simulate enemy's decision to place a bomb
            PlaceBomb();
            this.NextBombTime = this.BombTime;
        }
    }

    private void PlaceBomb()
    {
        Bomb bomb = new Bomb();
        bomb.Power = this.Power;
        bomb.Position = new Vector2(this.Position.X, this.Position.Y);
        bomb.Position.X = (float)Math.Round(bomb.Position.X / 30.0f) * 30.0f;
        bomb.Position.Y = (float)Math.Round(bomb.Position.Y / 30.0f) * 30.0f;
        this.Scene.Add(bomb);
    }

    public bool OnCollision(Fixture self, Fixture other, Contact contact)
    {
        Body body = other.Body;

        // Si la colisión fue con un objeto de tipo Fire (efecto de explosión), destruye al enemigo.
        if (body.Tag is Fire)
        {
            this.Destroy();
        }

        // Si la colisión fue con un objeto de tipo Block, detener el movimiento del enemigo.
        if (body.Tag is Block)
        {
            // Detener el movimiento del enemigo.
            this.Body.LinearVelocity = Vector2.Zero;
        }

        // Si la colisión fue con un objeto de tipo Player, implementa la lógica de cómo el enemigo interactúa con el jugador.
        if (body.Tag is Player)
        {
            // Por ejemplo, podrías restar vida al jugador, hacer que el enemigo cambie de dirección, etc.
        }

        return true;
    }

}