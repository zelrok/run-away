using Godot;
using System;

public class ControlDot : Label
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}
	public override void _Input(InputEvent @event)
	{

		if (@event is InputEventScreenTouch touchEvent)
		{
			if (touchEvent.Pressed)
			{
				this.Show();
				this.SetPosition(touchEvent.Position);
			}
			else
			{
				this.Hide();
			}
		} else if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.Pressed) 
			{
				this.Show();
				this.SetPosition(mouseEvent.Position);
			} else
			{
				this.Hide();
			}
		}

	}
	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
