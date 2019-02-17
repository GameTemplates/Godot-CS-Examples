using Godot;
using System;

public class GameOver : Node
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }

	private void _onPlayAgainButtonPressed()
	{
    	// sending a signal if play again buttons pressed
		
		//if button pressed, play again
		GetTree().ChangeScene("Game.scn");
	}
}

