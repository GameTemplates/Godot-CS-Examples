using Godot;
using System;
using System.Collections.Generic; //this is required to create a List

public class navigation : Navigation2D
{
    // Member variables
	private float movementSpeed;
	private bool smoothPath;
	private List<Vector2> path;
	private Vector2 playerPosition;
	private Vector2 mousePosition; 
	private Node2D player;

   public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//get the player node
		player = (Node2D)this.GetNode("Player");
		//set movement speed of player
		movementSpeed = 200.0f;
		//set if smooth the path
		smoothPath = false;
		//initialize the path with an empty list
		path = new List<Vector2>();
        
    }
	

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//if left mouse button is pressed
		if(Input.IsActionJustPressed("left_mouse"))
		{
			//get sprite position
			playerPosition = player.GetPosition();
			//get mouse position
			mousePosition = GetViewport().GetMousePosition();
			//generate a path from position of sprite to the position of the mouse
			generatePath(playerPosition, mousePosition, smoothPath);
		}
		
		//if the number of nodes in the path > 1 which means the player did not reach the destination
		if(path.Count > 1)
		{
			//calculate walk speed
			var walkSpeed = movementSpeed * delta;
			
			//set start and end position of movement
			//we are moving from the current position to the first node in the path
			var pfrom = player.GetPosition();
			var pto = path[1];
			
			//calculate distance between the start and end position
			var dis = pfrom.DistanceTo(pto);
			
			//move sprite toward the position of the first node in the path
			player.SetPosition(pfrom.LinearInterpolate(pto, walkSpeed/dis));
			
			//if distance to the first node in the path is less than walkSpeed
			if(dis < walkSpeed)
				//remove the first node from the path (so the next become the first and continue to move)
				path.RemoveAt(0);
			
			
		}
        
    }
	
	private void generatePath(Vector2 start, Vector2 end, bool smooth)
	{
		//get a simple path, it is going to create an array of nodes
		var p = GetSimplePath(start, end, smooth);
		//put the array in to a list, we get more options with a List like removing elements
		path = new List<Vector2>(p);	
	}
	
}
