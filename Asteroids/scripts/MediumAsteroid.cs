using Godot;
using System;

public class MediumAsteroid : Node2D
{
    private AnimatedSprite sprite;
    private PackedScene sAsteroid;
	
	private string rotationDirection;
	private int rotationSpeed;
	private float movementDirection;
	private int movementSpeed;
	private string[] direction = new string[3];
	private int animation;
	private float spriteWidth;
	private float spriteHeight;
	
	
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//get reference to sprite node
		sprite = (AnimatedSprite)this.GetNode("sprite");
		
		//get reference to the scene containing small asteroid
		sAsteroid = (PackedScene)ResourceLoader.Load("res://objects/SmallAsteroid.tscn");
		
		//initialize values
		rotationSpeed = Game.rnd.Next(1,3);
		movementSpeed = Game.rnd.Next(40,50);
		
		//pick random movement direction
		movementDirection = Game.rnd.Next(1,359);
		
		//set random animation
		animation = Game.rnd.Next(1,4);
		sprite.SetAnimation(animation.ToString());
		
		//enable the collision shape for the current animation
		//get reference to collision shape
		var shape = (CollisionPolygon2D)this.FindNode("collision_shape" + animation.ToString());
		//enable shape
		shape.SetDisabled(false);
		
		//pick random rotation direction
		direction[0] = "";
		direction[1] = "left";
		direction[2] = "right";
		
		rotationDirection = direction[Game.rnd.Next(1,3)];
		
		//get sprite width and height
        spriteWidth = sprite.GetSpriteFrames().GetFrame(animation.ToString(),0).GetWidth() / this.GetScale().x;
        spriteHeight = sprite.GetSpriteFrames().GetFrame(animation.ToString(),0).GetHeight() / this.GetScale().y;
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//rotate asteroid
		if(rotationDirection == "left")
			this.Rotation -= rotationSpeed * delta;
		else if(rotationDirection == "right")
			this.Rotation += rotationSpeed * delta;
			
		//move asteroid toward movement direction
		//calculate new vector using the movement direction
		var direction = new Vector2((float)Math.Cos(movementDirection), (float)Math.Sin(movementDirection));
		direction.Normalized();
		
		//move asteroid toward new vector at the given speed
		this.SetPosition((this.GetPosition() + direction * (movementSpeed * delta)));
		
		/**********************************
				WARP ATEROID
		***********************************/
		
		//get asteroid current position
		var posX = this.Position.x;
		var posY = this.Position.y;
		
		//if asteroid left the screen on the left, bring it back on the right
		if(posX <= 0 - spriteWidth/2)
			posX = Game.screenWidth + spriteWidth/2;
		//if asteroid left the screen on the right, bring it back on the left
		else if(posX >= Game.screenWidth + spriteWidth/2)
			posX = 0 - spriteWidth/2;
		//if asteroid left the screen on the top, bring it back on the bottom
		if(posY <= 0 - spriteHeight/2)
			posY = Game.screenHeight + spriteHeight/2;
		//if asteroid left the screen on the bottom, bring it back on the top
		else if(posY >= Game.screenHeight + spriteHeight/2)
			posY = 0 - spriteHeight/2;
			
		//set asteroid position
		this.SetPosition(new Vector2(posX, posY));
    }
	
	
	private void _onCollisionMasksAreaEntered(Node node)
	{
    	// sending a signal if something entered the collision mask area
		
		//if the node is the Bullet
		if(node.Name == "bullet_collision_mask")
		{
			
			//we have the collision mask only, get reference to parent node which is the main node of the bullet
			var parent = node.GetParent();
			
			//delete bullet
			parent.Free();
			
			//delete asteroid (it is locked in the current frame so put in a queue )
			this.QueueFree();
			
			//create an instance of the explosion sound and add to parent node
			var explosionSoundInstance = Game.explosionSound.Instance();
			GetParent().AddChild(explosionSoundInstance);
			
			//create 3 small asteroids in position of this one	
			for(var i = 1; i <= 3; i++)
			{
				var sAsteroidInstance = (Node2D)sAsteroid.Instance();
				sAsteroidInstance.SetPosition(new Vector2(this.Position.x, this.Position.y));
				//GetParent().AddChild(sAsteroidInstance); //old code from 3.0.6, drop error in 3.1 because trying to execute immediately
				GetParent().CallDeferred("add_child", sAsteroidInstance); //in 3.1 defer execution for LATER to add asteroid instance to parent
			}
			
			//create an instance of the explosion paritcle and add to the parent node
			var explosionParticleInstance = (Node2D)Game.explosionParticle.Instance();
			explosionParticleInstance.SetPosition(new Vector2(this.Position.x, this.Position.y));
			var particles = (Particles2D)explosionParticleInstance.GetNode("particles");
			particles.Amount = 5; //we emit 5 particles
			particles.Scale = new Vector2(0.9f, 0.9f); //set particle size a bit smaller
			GetParent().AddChild(explosionParticleInstance);
			
		}
	}
}
