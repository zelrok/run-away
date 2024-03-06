using Godot;
using System;

public class Main : Node
{
#pragma warning disable 649
	[Export]
	public PackedScene MobScene;
#pragma warning restore 649

	public int Score;

	public override void _Ready() { GD.Randomize(); }

	public void GameOver()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		GetNode<HUD>("HUD").ShowGameOver();
		GetNode<AudioStreamPlayer>("Music").Stop();
		GetNode<AudioStreamPlayer>("DeathSound").Play();
	}

	public void NewGame()
	{
		Score = 0;

		GetTree().CallGroup("mobs", "queue_free");
		GetNode<AudioStreamPlayer>("Music").Play();

		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Position2D>("StartPosition");
		player.Start(startPosition.Position);

		var hud = GetNode<HUD>("HUD");
		hud.UpdateScore(Score);
		hud.ShowMessage("Get Ready!");

		GetNode<Timer>("StartTimer").Start();
	}
	private void _on_MobTimer_timeout()
	{
		var mob = (Mob)MobScene.Instance(); //new instance of Mob
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.Offset = GD.Randi(); // rando location on Path2D
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2; // set dir perpendicular
		mob.Position = mobSpawnLocation.Position; // set mob position to the rando
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4); // some rando to direction
		mob.Rotation = direction; // set mob rotation to the rando
		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0); // pick a velocity
		mob.LinearVelocity = velocity.Rotated(direction); // set mob velo to rando
		AddChild(mob); // actually spawn mob by adding to Main
	}

	private void _on_ScoreTimer_timeout()
	{
		Score++;
		GetNode<HUD>("HUD").UpdateScore(Score);
	}

	private void _on_StartTimer_timeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}

}
