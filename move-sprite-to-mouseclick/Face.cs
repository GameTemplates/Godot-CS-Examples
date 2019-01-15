using Godot;
using System;

public class Face : Node2D
{
	private float clickX;
	private float clickY;
	private Vector2 direction;
	
	private float speed = 3f;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.

		//if left mouse button is clicked
		if(Input.IsActionJustPressed("left_mouse"))
		{
			//get mouse current location
			clickX = GetViewport().GetMousePosition().x;
			clickY = GetViewport().GetMousePosition().y;
			
			//calculate direction using the position of the sprite and the mouse click
			direction = new Vector2(clickX, clickY) - new Vector2(this.GetPosition().x, this.GetPosition().y);
            direction.Normalized();
			
			
			
		}
        
		//if direction is not 0, move sprite to position
		if(direction.x != 0 || direction.y != 0)
		{
			//update direction using the current position of the sprite
			direction = new Vector2(clickX, clickY) - new Vector2(this.GetPosition().x, this.GetPosition().y);
            direction.Normalized();
			
			//move sprite to direction at the given speed, remove multiplication for instant position change
			this.GlobalTranslate(direction * (speed * delta));
			 
		}
    }
}
