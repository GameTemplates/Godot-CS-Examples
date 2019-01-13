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
		
		//get current position of the player
		var x = this.GetPosition().x;
		var y = this.GetPosition().y;
		
		//MOVE LEFT
		if(Input.IsActionPressed("move_left"))
			x -= speed * delta;
		
		
		//MOVE RIGHT
		if(Input.IsActionPressed("move_right"))	
			x += speed * delta;
		
		
		//MOVE UP
		if(Input.IsActionPressed("move_up"))
			y -= speed * delta;
		
		
		//MOVE DOWN
		if(Input.IsActionPressed("move_down"))
			y += speed * delta;
		
		//update position
		this.SetPosition(new Vector2(x,y));
        
    }
}
