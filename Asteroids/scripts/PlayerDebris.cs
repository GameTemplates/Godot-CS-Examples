using Godot;
using System;

public class PlayerDebris : Node2D
{
   	private float movementDirection;
	private int movementSpeed;
	private int rotationSpeed;
	private AnimatedSprite sprite;
	
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//initialize values
		movementSpeed = 150;
		rotationSpeed = 50;
		movementDirection = Game.rnd.Next(0,360);
		
		//get reference to the sprite
		sprite = (AnimatedSprite)this.GetNode("sprite");
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		/*********************
		MOVE AND ROTATE DEBRIS
		**********************/
		
		//calculate new vector using the movement direction
		var direction = new Vector2((float)Math.Cos(movementDirection), (float)Math.Sin(movementDirection));
		direction.Normalized();
		
		//move debris toward new vector at the given speed
		this.SetPosition((this.GetPosition() + direction * (movementSpeed * delta)));
		
		//rotate object
		this.RotationDegrees += rotationSpeed * delta;
		
		/***********
		DESTROY DEBRIS
		************/
		
		//get debris current position
		var posX = this.Position.x;
		var posY = this.Position.y;
		
		//get screen width and height
		var screenWidth = GetViewport().GetSize().x;
		var screenHeight = GetViewport().GetSize().y;
		
		//get sprite width and height
        var spriteWidth = sprite.GetSpriteFrames().GetFrame(sprite.GetAnimation(),0).GetWidth() / this.GetScale().x;
        var spriteHeight = sprite.GetSpriteFrames().GetFrame(sprite.GetAnimation(),0).GetHeight() / this.GetScale().y;
		
		//if debris left the screen on the left, delete
		if(posX <= 0 - spriteWidth/2)
			this.Free();
		//if debris left the screen on the right, delete
		else if(posX >= screenWidth + spriteWidth/2)
			this.Free();
		//if debris left the screen on the top, delete
		if(posY <= 0 - spriteHeight/2)
			this.Free();
		//if debris left the screen on the bottom, delete
		else if(posY >= screenHeight + spriteHeight/2)
			this.Free();
        
    }
}
