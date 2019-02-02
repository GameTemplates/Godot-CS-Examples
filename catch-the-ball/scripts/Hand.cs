using Godot;
using System;

public class Hand : Node2D
{
	//member variables
    private int movementSpeed;
	
	private Vector2 position;
    private Sprite sprite;
    private float spriteWidth;
    private float spriteHeight;
	private float screenWidth;
	private float screenHeight;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
		//set initial movement speed to 150
		movementSpeed = 150;
		
		//get screen width and height
		screenWidth = GetViewport().GetSize().x;
		screenHeight = GetViewport().GetSize().y;

        //get the sprite node
        sprite = (Sprite)this.GetNode("sprite");

        //get width and height of sprite
        spriteWidth = sprite.GetTexture().GetWidth() / this.GetScale().x;
        spriteHeight = sprite.GetTexture().GetHeight() / this.GetScale().y;
		
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        
		//get current position of hand
		position = this.GetPosition();
		
		//move the hand left
		if(Input.IsActionPressed("move_left"))
			if(this.Position.x > spriteWidth/2) //make sure the hand don't leave the screen on the left
				position.x -= movementSpeed * delta;
				
		//move the hand right		
		if(Input.IsActionPressed("move_right"))
			if(this.Position.x < screenWidth - spriteWidth/2) //make sure the hand don't leave the screen on the right
				position.x += movementSpeed * delta;
		
        //update position of the hand
		this.SetPosition(position);
    }
}
