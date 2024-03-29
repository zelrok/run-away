using Godot;
using System;

public class HUD : CanvasLayer
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	[Signal]
	public delegate void StartGame();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }

	public void ShowMessage(string text)
	{
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();

		GetNode<Timer>("MessageTimer").Start();
	}

	async public void ShowGameOver()
	{
		ShowMessage("Game Over");

		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, "timeout");

		await ToSignal(GetTree().CreateTimer(1), "timeout");
		GetNode<Button>("StartButton").Show();

		var message = GetNode<Label>("Message");
		message.Text = "Dodge the Roids!";
		message.Show();
	}

	public void UpdateScore(int score) { GetNode<Label>("ScoreLabel").Text = score.ToString()+" pts"; }
	private void OnMessageTimerTimeout() { GetNode<Label>("Message").Hide(); }


	private void OnStartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		EmitSignal("StartGame");
	}
}



