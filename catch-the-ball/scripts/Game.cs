using Godot;
using System;
using System.Collections.Generic; //required to create a List

public class Game : Node
{
    //member varibales
    public static int playerLife; //this is a public static value so we can access it with Game.playerLife from anywhere in the game
    private List<Node> lifeHud;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//set player life to 3
		playerLife = 3;
		
		//display life on the screen
		//get the life scene containing our node
		var life = (PackedScene)ResourceLoader.Load("res://Life.tscn");
		
		//set initial position for the life nodes
		var position = new Vector2(40,40);
		
		//create a local array to store the instances of the life nodes temporary
		Node[] lifeNode = new Node[playerLife + 1];
		
		//create instances of Life node, same number as the value of playerLife
		for(var i = 1; i <= playerLife; i++)
		{
            lifeNode[i] =  life.Instance(); //create instance
            lifeNode[i].Set("position", position); //set position
			AddChild(lifeNode[i]); //add instance to scene
			position.x += 40; //increase position on the x for the next node
		}
		
		//add the array of life nodes to a List because List is more flexible to work with
		lifeHud = new List<Node>(lifeNode);
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//we don't need to process anything
		
		//if player life < 0 game over
		//NOTE: we end the game if player life < 0, because I wanted to end the game when we lose all the life (3) and our very last ball so we have 4 balls and end the game when life < 0
        if (playerLife < 0)
			
			//go to game over scene if the crash sound is no longer playing
			if(!Ball.crashSound.IsPlaying())
				GetTree().ChangeScene("res://scenes/GameOver.tscn");
        
    }
	
	private void _onBallOutOfBounds()
	{
    	//sending a signal if the ball is out of bounds
		
		//if the ball out of bounds and player life is > 0
		if(playerLife > 0)
		{
            lifeHud[playerLife].Free(); //delete last instance of Life from game
            lifeHud.RemoveAt(playerLife); //delete last instance from the list too
		}
		
		//reduce player life by 1
		playerLife -= 1;
		
	}	
}

