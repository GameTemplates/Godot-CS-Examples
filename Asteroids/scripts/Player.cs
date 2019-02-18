using Godot;
using System;

public class Player : Node2D
{
	
	//member vars
    public static int movementSpeed; //this is a public static value so we can access it from anywhere using Player.movementSpeed
	private int rotationSpeed;
	public static int accSpeed; //this is a public static value so we can access it from anywhere using Player.accSpeed
	private int speed;
	private float movementDirection;
	private bool canDamage;
	private float godTime;
	
	private Sprite sprite;
	private float spriteWidth;
	private float spriteHeight;
	
	private float shootTimer;
	private float shootSpeed;
	private PackedScene bullet;
	private AudioStreamPlayer shootSound;
	
	private AudioStreamPlayer thrusterSound;
	private AudioStreamPlayer thrusterDownSound;
	private Particles2D thruster; 
	
	private PackedScene playerDebris;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//initialize default values
		movementSpeed = 50;
		rotationSpeed = 100;
		accSpeed = 50;
		speed = movementSpeed;
		shootTimer = 0;
		shootSpeed = 0.3f; //shoot every 0.3 second if button is hold
		
		//player can not be damaged for 3 seconds at the beginning in case spawn on top of an asteroid
		canDamage = false;
		godTime = 3;
		
		//set random movement direction
		var rnd = new Random();
		movementDirection = rnd.Next(1,359);
		
		//get reference to sprite
		sprite = (Sprite)this.GetNode("sprite");
		
		//load the scene containing our bullet
		bullet = (PackedScene)ResourceLoader.Load("res://objects/Bullet.tscn");
		
		//get reference to the shoot sound player
		shootSound = (AudioStreamPlayer)this.GetNode("shoot_sound");
		
		//get reference to thruster sounds
		thrusterSound = (AudioStreamPlayer)this.GetNode("thruster_sound");
		thrusterDownSound = (AudioStreamPlayer)this.GetNode("thrusterdown_sound");
		
		//get reference to thruster particle emitter
		thruster = (Particles2D)this.GetNode("thruster_particle");
		
		//get reference to scene containing the player debris
		playerDebris = (PackedScene)ResourceLoader.Load("res://objects/PlayerDebris.tscn");
		
		//get sprite width and height
        spriteWidth = sprite.GetTexture().GetWidth() / this.GetScale().x;
        spriteHeight = sprite.GetTexture().GetHeight() / this.GetScale().y;
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        
		/***********
		DAMAGE CONTROL
		***********/
		
		//reduce god time
		godTime -= delta;
		
		//if god time <= 0, player can be damaged
		if(godTime <= 0)
			canDamage = true;
		
		/******************
			SHIP CONTROLS
		******************/
		
		
		//rotate ship
		if(Input.IsActionPressed("turn_left"))
			this.RotationDegrees -= rotationSpeed * delta;
		
		if(Input.IsActionPressed("turn_right"))
			this.RotationDegrees += rotationSpeed * delta;
	
		//if forward key is pressed, update movement direction to the current angle
		if(Input.IsActionPressed("forward"))
		{
			movementDirection = this.Rotation;
			speed = movementSpeed + accSpeed; //apply acceleration to speed also
			
			//play thruster sound
			if(!thrusterSound.IsPlaying())
				thrusterSound.Play();
				
			//emit thruster particles
			thruster.Emitting = true;
		}
			
		//if forward key is released
		if(Input.IsActionJustReleased("forward"))
		{
			//change speed back to normal
			speed = movementSpeed;
			
			//play thruster down sound
			thrusterSound.Stop();
			thrusterDownSound.Play();
			
			//stop emitting thruster particles
			thruster.Emitting = false;
		}
		
		//calculate new vector using the movement direction
		var direction = new Vector2((float)Math.Cos(movementDirection), (float)Math.Sin(movementDirection));
		direction.Normalized();
		
		//update ship position to move toward new vector at the given speed
		this.SetPosition((this.GetPosition() + direction * (speed * delta)));
		
		
		/**********************************
				WARP SHIP
		***********************************/
		
		//get ship current position
		var posX = this.Position.x;
		var posY = this.Position.y;
		
		//if ship left the screen on the left, bring it back on the right
		if(posX <= 0 - spriteWidth/2)
			posX = Game.screenWidth + spriteWidth/2;
		//if ship left the screen on the right, bring it back on the left
		else if(posX >= Game.screenWidth + spriteWidth/2)
			posX = 0 - spriteWidth/2;
		//if ship left the screen on the top, bring it back on the bottom
		if(posY <= 0 - spriteHeight/2)
			posY = Game.screenHeight + spriteHeight/2;
		//if ship left the screen on the bottom, bring it back on the top
		else if(posY >= Game.screenHeight + spriteHeight/2)
			posY = 0 - spriteHeight/2;
			
		//update ship position
		this.SetPosition(new Vector2(posX, posY));
		
		
		/*****************
		  SHOOT BULLETS
		*****************/
		
		//if shoot key is pressed, create an instance of the bullet
		//bullet behavior is handles in Bullet.cs 
		if(Input.IsActionJustPressed("shoot"))  
			shootTimer = shootSpeed; //if shoot is pressed for the first time, set shootTimer to shootSpeed so we shoot once
			
		if(Input.IsActionPressed("shoot"))
		{
			if(shootTimer >= shootSpeed)
			{
				//get bullet point
				var bulletPoint = (Node2D)this.GetNode("bullet_point");
				
				//create a bullet instance in position of the bullet point
				var b = (Node2D)bullet.Instance();
				b.SetPosition(new Vector2(bulletPoint.GlobalPosition.x, bulletPoint.GlobalPosition.y));
				b.RotationDegrees = this.GlobalRotationDegrees;
				
				//pick the parent node (it is the actual scene)
				var p = this.GetParent();
				
				//add bullet to parent node (we add bullet to scene basically)
				p.AddChild(b);
				
				//reset timer	
				shootTimer = 0;
				
				//play shoot sound
				shootSound.Play();
			}
			
			shootTimer += delta;
		}
		
    }
	
	private void _onCollisionMaskAreaEntered(Node node)
	{
    	// sendinf signal if something colliding with the player
		
		/*************
		DESTROY PLAYER
		*************/
		
		//if player can damage
		if(canDamage)
		{
		
			//if something colliding and it is an asteroid
			if(node.Name == "asteroid_collision_masks")
			{
				//create an instance of the explosion sound and add to the parent node
				var explosionSoundInstance = Game.explosionSound.Instance();
				GetParent().AddChild(explosionSoundInstance);
				
				//delete player
				this.QueueFree();
				
				//create player debris 3 instances for the 3 side
				for(var i = 1; i <= 3; i++)
				{
					var playerDebrisInstance = (Node2D)playerDebris.Instance();
					var playerDebrisSprite = (AnimatedSprite)playerDebrisInstance.GetNode("sprite");
					playerDebrisSprite.Animation = i.ToString();
					playerDebrisInstance.SetPosition(new Vector2(this.Position.x, this.Position.y));
					playerDebrisInstance.Name = "PlayerDebris" + i.ToString();
					GetParent().AddChild(playerDebrisInstance);
				}
				
				//reduce life by 1
				Game.life -= 1;
			}
		}
	}
}

