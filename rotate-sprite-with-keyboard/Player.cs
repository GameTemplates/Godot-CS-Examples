using Godot;
using System;

public class Player : Node2D
{
    private int speed = 100;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//get current rotation
		var rotation = GetRotationDegrees();
		
		//ROTATE RIGHT
		if(Input.IsActionPressed("rotate_right"))
			this.SetRotationDegrees(rotation + speed * delta);
        
		//ROTATE LEFT
		if(Input.IsActionPressed("rotate_left"))
			this.SetRotationDegrees(rotation - speed * delta);
    }
}
