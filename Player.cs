using Godot;
using System;

public class Player : Area2D
{
	// Using the export keyword on the first variable speed allows us to set its value in the Inspector. 
	[Export]
	public int Speed = 400; // movement in pixels/sec
	public Vector2 ScreenSize; //size of game window

	[Signal]
	public delegate void Hit();

	public override void _Ready()
	{
		// ready is called when a node enters the scene tree
		ScreenSize = GetViewportRect().Size;

		Hide(); // player hidden when the game starts
	}

	public override void _Process(float delta)
	{
		var velocity = Vector2.Zero;

		// check for input
		// move in the given direction
		if (Input.IsActionPressed("move_left")) { velocity.x -= 1; }
		if (Input.IsActionPressed("move_right")) { velocity.x += 1; }
		if (Input.IsActionPressed("move_up")) { velocity.y -= 1; }
		if (Input.IsActionPressed("move_down")) { velocity.y += 1; }

		// play the right animation
		var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite.Play();
		}
		else
		{
			animatedSprite.Stop();
		}

		// clamping logic
		Position += velocity * delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.x, 0, ScreenSize.x),
			y: Mathf.Clamp(Position.y, 0, ScreenSize.y)
			);

		if (velocity.x != 0)
		{
			animatedSprite.Animation = "walk";
			animatedSprite.FlipV = false;
			animatedSprite.FlipH = velocity.x < 0;
		}
		else if (velocity.y < 0) { animatedSprite.Animation = "up"; }
		else if (velocity.y > 0) { animatedSprite.Animation = "down"; }
		animatedSprite.Animation = "pc";
	}

	private void _on_Player_body_entered(object body)
	{
		Hide(); // player hides when hit
		EmitSignal(nameof(Hit));
		// Must be deferred as we can't change physics properties on a physics callback.
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
	}

	public void Start(Vector2 pos)
	{
		Position = pos;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
}
