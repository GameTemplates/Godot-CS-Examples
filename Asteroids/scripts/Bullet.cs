using Godot;
using System;

public class Bullet : Node2D
{
    private int speed;
	private Sprite sprite;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//initialize values
		//speed
		speed = 50 + Player.movementSpeed + Player.accSpeed; //make sure the bullet speed is greater than the maximum speed of Player
        
		//get reference to the sprite
		sprite = (Sprite)this.GetNode("sprite");
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        
		/**********
		MOVE BULLET
		**********/
		
		//move bullet toward it angle at the given speed
		//calculate new vector using the movement direction
		var direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
		direction.Normalized();
		
		//move bullet toward new vector at the given speed
		this.SetPosition((this.GetPosition() + direction * (speed * delta)));
		
		
		/***********
		DELETE BULLET
		************/
		
		//if bullet is outside the screen, delete it
		//get bullet current position
		var posX = this.Position.x;
		var posY = this.Position.y;
		
		//get screen width and height
		var screenWidth = GetViewport().GetSize().x;
		var screenHeight = GetViewport().GetSize().y;
		
		//get sprite width and height
        var spriteWidth = sprite.GetTexture().GetWidth() / this.GetScale().x;
        var spriteHeight = sprite.GetTexture().GetHeight() / this.GetScale().y;
		
		//if bullet left the screen on the left, delete
		if(posX <= 0 - spriteWidth/2)
			this.Free();
		//if bullet left the screen on the right, delete
		else if(posX >= screenWidth + spriteWidth/2)
			this.Free();
		//if bullet left the screen on the top, delete
		else if(posY <= 0 - spriteHeight/2)
			this.Free();
		//if ship left the screen on the bottom, delete
		else if(posY >= screenHeight + spriteHeight/2)
			this.Free();
		
    }
}
