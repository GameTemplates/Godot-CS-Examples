using Godot;
using System;

public class Asteroid : Node2D
{

    /*public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }
	
	public override void _Process()
	{
		// Called every frame. Delta is time since last frame.
        // Update game logic here.
	}
	*/

	private void _onHitboxAreaEntered(Node2D node)
	{
    	//we are sending a signal to the node the script is attached to if something enter the Area of the asteroid
		
		//if the ship enter the Area of the asteroid, delete the asteroid
		if(node.GetName() == "Ship");
			//the asteroid object is locked at the moment of collision so we queue the object to be freed the next time it is unlocked (next frame)			
			this.QueueFree();
	}

}

