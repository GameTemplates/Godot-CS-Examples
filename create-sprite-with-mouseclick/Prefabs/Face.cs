using Godot;
using System;

public class Face : Node2D
{
   

    public override void _Ready()
    {
        
		//when the node is created
		
		//get mouse current position
		var x = GetViewport().GetMousePosition().x;
		var y = GetViewport().GetMousePosition().y;
		
		//set position of the node to be at the mouse
		this.SetPosition(new Vector2(x,y));
        
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
