using Godot;
using System;

public class Player : Node2D
{

	private AnimatedSprite animation;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//get sprite node
		animation = (AnimatedSprite)this.GetNode("sprite");
        
    }
	

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//if space key is pressed, change animation of player
		if(Input.IsActionJustPressed("space_key"))
		{
			if(animation.GetAnimation() == "duck")
				animation.SetAnimation("cheer");
			else
				animation.SetAnimation("duck");
			
		}
        
    }
}
