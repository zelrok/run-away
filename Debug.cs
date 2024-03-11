using Godot;
using System;
using System.Reflection;

public partial class Debug : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private bool toggleDebug = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public override void _Input(InputEvent @event)
    {
        if (toggleDebug)
        {
            GD.Print(@event.AsText());
        }
    }
}


//InputEventScreenTouch: index = 0, pressed = true, position = (260, 330)
//InputEventScreenTouch: index = 0, pressed = false, position = (260, 332)

//InputEventMouseButton: button_index = BUTTON_LEFT, pressed = true, position = (260, 330), button_mask = 1, doubleclick = false
//InputEventMouseButton: button_index = BUTTON_LEFT, pressed = false, position = (260, 332), button_mask = 0, doubleclick = false

//InputEventScreenDrag: index = 0, position = (260, 331), relative = (0, 1), speed = (-0.025905, 0.040765)
//InputEventScreenDrag: index = 0, position = (260, 332), relative = (0, 1), speed = (-0.025905, 0.040765)

//InputEventMouseMotion: button_mask = BUTTON_MASK_LEFT, position = (260, 331), relative = (0, 1), speed = (-5.777166, -64.833839), pressure = (1), tilt = (0, 0), pen_inverted = (0)
//InputEventMouseMotion: button_mask = BUTTON_MASK_LEFT, position = (260, 332), relative = (0, 1), speed = (-0.025905, 0.040765), pressure = (1), tilt = (0, 0), pen_inverted = (0)
//InputEventMouseMotion: button_mask = 0, position = (260, 332), relative = (0, 0), speed = (-0.025905, 0.040765), pressure = (0), tilt = (0, 0), pen_inverted = (0)