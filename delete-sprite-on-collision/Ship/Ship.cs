using Godot;
using System;

public class Ship : Node2D
{
    private int rotationSpeed = 100;
	private int movementSpeed = 100;

   /* public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }
	*/

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//rotate ship
		if(Input.IsActionPressed("rotate_left")) //left
			this.SetRotationDegrees( this.GetRotationDegrees() - (rotationSpeed * delta));
		if(Input.IsActionPressed("rotate_right")) //right
			this.SetRotationDegrees( this.GetRotationDegrees() + (rotationSpeed * delta));
			
		//move forward
		if(Input.IsActionPressed("move_forward"))
		{
			//get rotation in radiant
			var angle = this.Rotation;
			
			//create new vector based on angle
			var direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
			direction.Normalized();
			
			//move ship toward vector at given speed
			this.SetPosition(this.GetPosition() + direction * (movementSpeed * delta));
			
			
		}
        
    }
}
