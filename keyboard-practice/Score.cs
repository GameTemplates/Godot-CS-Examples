using Godot;
using System;

public class Score : Node
{

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//get reference to message node
		var message = (RichTextLabel)this.GetNode("Message");
		
		//update message to display number of keys pressed in the game scene
		message.Text = "You have pressed " + Game.hit.ToString() + " correct keys in 60 seconds.";
        
    }

/*    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        
    }
*/

	private void _onPlayAgainButtonPressed()
	{
    	// sending a signal if play again button is pressed
		
		//go back to the game scene if pressed
		GetTree().ChangeScene("Game.scn");
	}

}

