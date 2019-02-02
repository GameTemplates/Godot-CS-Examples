using Godot;
using System;

public class Ball : Node2D
{
	//member variables
    private int movementSpeed;
	private int movementDirectionX;
	private int movementDirectionY;
	
	private Vector2 position;
    private Sprite sprite;
	public static AudioStreamPlayer crashSound; //this is a public static value so we can access it with Ball.crashSound from anywhere in the game
	private AudioStreamPlayer swishSound;
    private float spriteWidth;
    private float spriteHeight;
	private float screenWidth;
	private float screenHeight;
	
	//create custom signal
	[Signal]
	public delegate void _BallOutOfBounds();

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//set initial movement speed to 150
		movementSpeed = 150;
		
		//set initial movement direction
		movementDirectionX = 1; //move down
		movementDirectionY = -1; //move right
		
		//get screen width and height
		screenWidth = GetViewport().GetSize().x;
		screenHeight = GetViewport().GetSize().y;

        //get the sprite node
        sprite = (Sprite)this.GetNode("sprite");

        //get width and height of sprite
        spriteWidth = sprite.GetTexture().GetWidth() / this.GetScale().x;
        spriteHeight = sprite.GetTexture().GetHeight() / this.GetScale().y;
		
		//get crash sound player
		crashSound = (AudioStreamPlayer)this.GetNode("crash_sound");
		//get swish sound player
		swishSound = (AudioStreamPlayer)this.GetNode("swish_sound");
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//if player life is >= 0
		//NOTE: we want it so the game last until we lose all the life (3) and our very last ball too so bascially we have 4 balls and end the game when life < 0
		if(Game.playerLife >= 0)
		{
			//update position of ball
			position.x += (movementSpeed * delta) * movementDirectionX;
			position.y += (movementSpeed * delta) * movementDirectionY;
			this.SetPosition(position);
		
			//change movement direction of ball if the ball hit the edge of the screen 
			if(this.GetPosition().x >= screenWidth - spriteWidth/2)
				movementDirectionX = -1;
			if(this.GetPosition().x <= 0 + spriteWidth/2)
				movementDirectionX = 1;
			if(this.GetPosition().y <= 0 + spriteHeight/2)
				movementDirectionY = 1;
				
			//if ball left the screen at the bottom
			if(this.GetPosition().y > screenHeight + spriteHeight/2)
			{
				//change ball position to the top
				position.y = -spriteHeight;
			
				//play crash sound
				crashSound.Play();
			
				//amit signal the ball is out of bounds
				EmitSignal("_BallOutOfBounds");
							
			}
		}
        
    }
	
	private void _onHitboxAreaEntered(Node node)
	{
    	// sending a signal from hitbox if something entered the hitbox
		
		//if the ball colliding with the hand
		if(node.GetParent().GetName() == "Hand")
		{
			//play swish sound
			swishSound.Play();
			
			//change Y direction
			movementDirectionY = -1;
		}
	}
	
	private void _onBallSpeedTimerTimeout()
	{
    	// sending a signal if the timer runs out
		
		//if timer runs out (every 3 seconds), increase the speed of the ball by 10
		movementSpeed += 10;
	}
}

