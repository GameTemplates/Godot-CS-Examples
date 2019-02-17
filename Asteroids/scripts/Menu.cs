using Godot;
using System;

public class Menu : Node
{

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

	private void _onStartGameButtonPressed()
	{
    	// sending a signal if start game button pressed
		
		//if button pressed, go to game scene
		GetTree().ChangeScene("Game.scn");
	}
}

