using Godot;
using System;

public class Face : Node2D
{
    
	private float scaleFactor = 1;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//get current scale of node
		var scaleX = this.GetScale().x;
		var scaleY = this.GetScale().y;
		
		//if right key is pressed, increase scale on x
		if(Input.IsActionPressed("right_key"))
			scaleX += scaleFactor * delta;
		
		//if left key is pressed, decrease scale on x
		if(Input.IsActionPressed("left_key"))
			scaleX -= scaleFactor * delta;
			
		//if up key is pressed, increase scale on y
		if(Input.IsActionPressed("up_key"))
			scaleY += scaleFactor * delta;
		
		//id down key is pressed, decrease scale on y
		if(Input.IsActionPressed("down_key"))
			scaleY -= scaleFactor * delta;
		
		//apply new scale
		this.SetScale(new Vector2(scaleX, scaleY));
    }
}
