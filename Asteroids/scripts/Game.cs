using Godot;
using System;
using System.Collections.Generic; //required to be able to use Lists

public class Game : Node
{

	//we use the same random number generarot for everything so we initialize it here, only once
	public static Random rnd = new Random(); //it is a public static value so we can access it using Game.rnd.Nex()
	public static PackedScene explosionSound; //it is a public static value so we can access it using Game.explosionSound
	public static PackedScene explosionParticle; ////it is a public static value so we can access it using Game.explosionParticle
	public static int life; //it is a public static value so we can access it with Game.life
	public static float screenWidth; //it is a public value to access use Game.screenWidth
	public static float screenHeight; //it is a public value to access use Game.screenHeight
	
	private PackedScene player;
	private RichTextLabel lifeText;
	
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//initialize values
		life = 3;
		
		//get a reference to the scene containing the explosion sound
		explosionSound = (PackedScene)ResourceLoader.Load("res://objects/ExplosionSound.tscn");
		
		//get a reference to the scene containing the explosion particle emitter
		explosionParticle = (PackedScene)ResourceLoader.Load("res://objects/Explosion.tscn");
		
		//get reference to the scene containing the player
		player = (PackedScene)ResourceLoader.Load("res://objects/Player.tscn");
		
		//get reference to the life text
		lifeText = (RichTextLabel)this.GetNode("LifeText");
		
    }

  	public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here. 
		
		//get screen width and height
		screenWidth = GetViewport().GetSize().x;
		screenHeight = GetViewport().GetSize().y;
		
		/***********
		UPDATE LIFE TEXT
		***********/
		
		//update life text to display life
		lifeText.Text = "Life: " + life.ToString();
		
		/************
		RESPAWN THE PLAYER
		*************/
		
		//if player doesn't exist
		if(!this.HasNode("Player"))
		{
			//wait for player debris to disappear all 3 instance
			if(!this.HasNode("PlayerDebris1") && !this.HasNode("PlayerDebris2") && !this.HasNode("PlayerDebris3"))
			{
				//if life > 0 respawnt the player
				if(life > 0)
				{
					
					//create a new instance of the player in the middle of the screen
					var playerInstance = (Node2D)player.Instance();
					playerInstance.SetPosition(new Vector2(screenWidth/2, screenHeight/2));
					this.AddChild(playerInstance);
				}
				else //if player life <= 0 GAME OVER
				{
					GetTree().ChangeScene("GameOver.scn");
				}
			}
		}
		
		
		/***********
		 GAME OVER
		***********/
		
		//get the number of astroids in the scene (it is including the explosion sounds too)
		var asteroids = this.GetNode("Asteroids").GetChildren();
		
		//if the number of items in asteroids is 0 GAME OVER
		if(asteroids.Count == 0)
			GetTree().ChangeScene("GameOver.scn");
    }
	
}

