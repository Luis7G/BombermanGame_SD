using Microsoft.Xna.Framework.Input;

// Aqui se definen los controles del jugador
interface PlayerControls
{
    public bool up();
    public bool down();
    public bool left();
    public bool right();
    public bool bomb();
}


class PlayerKeyboardControls : PlayerControls
{
    public static Keys[] keysPlayer = { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.X };

    public Keys[] keys = PlayerKeyboardControls.keysPlayer;

    public PlayerKeyboardControls(Keys[] keys)
    {
        this.keys = keys;
    }

    public bool up() { return Keyboard.GetState().IsKeyDown(this.keys[0]); }
    public bool down() { return Keyboard.GetState().IsKeyDown(this.keys[1]); }
    public bool left() { return Keyboard.GetState().IsKeyDown(this.keys[2]); }
    public bool right() { return Keyboard.GetState().IsKeyDown(this.keys[3]); }
    public bool bomb() { return Keyboard.GetState().IsKeyDown(this.keys[4]); }
}