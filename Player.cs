using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

class Player : GameObject
{
    // Controles del jugador 
    public PlayerControls Controls = null;

    // Velocidad a la que se mueve el jugador
    public float Speed = 400.0f;

    // Poder de las bombas del jugador
    public int Power = 1;

    // Tiempo entre las bombas para el jugtador
    public float BombTime = 3.0f;

    // Tiempo hasta que la siguiente bomba pueda colocarse
    private float NextBombTime = 0.0f;

    // Texturas 
    public static Texture2D[] Textures = new Texture2D[12];

    public static void LoadTextures(GraphicsDevice graphicsDevice)
    {
        string[] textures = {
            // Arriba
            "player_01.png", "player_02.png", "player_03.png",
            // Izquieda
            "player_11.png", "player_12.png", "player_13.png",
            // Abajo
            "player_21.png", "player_22.png", "player_23.png",
            // Derecha
            "player_31.png", "player_32.png", "player_33.png",
        };

        for (int i = 0; i < textures.Length; i++)
        {
            Player.Textures[i] = ContentUtils.Loadtexture(graphicsDevice, "./assets/textures/player/" + textures[i]);
        }
    }

    public Player(PlayerControls controls)
    {
        this.Controls = controls;
    }

    public override void Initialize(GraphicsDevice graphicsDevice)
    {
        this.Texture = Player.Textures[6];

        this.Scale = 1.0f;

        this.Body = this.Scene.World.CreateCircle(11.0f, 0.0f, this.Position, BodyType.Dynamic);
        this.Body.Tag = this;
        this.Body.FixedRotation = true;
        this.Body.LinearDamping = 4.0f;
        this.Body.OnCollision += this.OnCollision;
    }

    // Proceso de colisiones con otros objetos de la escena
    public bool OnCollision(Fixture self, Fixture other, Contact contact)
    {
        Body body = other.Body;


        if (body.Tag is Fire)
        {
            this.Destroy();
        }


        return true;
    }


    public override void Update(GameTime time)
    {
        base.Update(time);

        float delta = (float)time.ElapsedGameTime.TotalSeconds;

        // Mover al jugador
        if (this.Controls.left())
        {
            this.Body.ApplyLinearImpulse(new Vector2(-this.Speed * delta, 0));
            this.Texture = Player.Textures[3];
        }
        if (this.Controls.right())
        {
            this.Body.ApplyLinearImpulse(new Vector2(this.Speed * delta, 0));
            this.Texture = Player.Textures[9];
        }
        if (this.Controls.up())
        {
            this.Body.ApplyLinearImpulse(new Vector2(0, -this.Speed * delta));
            this.Texture = Player.Textures[0];
        }
        if (this.Controls.down())
        {
            this.Body.ApplyLinearImpulse(new Vector2(0, this.Speed * delta));
            this.Texture = Player.Textures[6];
        }

        // Decrementa el tiempo de la siguiente bomba
        if (this.NextBombTime > 0)
        {
            this.NextBombTime -= delta;
        }

        if (this.NextBombTime <= 0 && this.Controls.bomb())
        {
            Bomb bomb = new Bomb();
            bomb.Power = this.Power;
            bomb.Position = new Vector2(this.Position.X, this.Position.Y);
            bomb.Position.X = (float)Math.Round(bomb.Position.X / 30.0f) * 30.0f;
            bomb.Position.Y = (float)Math.Round(bomb.Position.Y / 30.0f) * 30.0f;
            this.Scene.Add(bomb);
            this.NextBombTime = this.BombTime;
        }
    }



}