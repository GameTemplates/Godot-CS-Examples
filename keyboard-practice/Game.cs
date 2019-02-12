using Godot;
using System;

public class Game : Node
{
	private string selectedKeyName;
	private int selectedKeyNumber;
	public static int hit; //this is a public static value so we can access it from anywhere in the game using Game.hit
	private float gameTime;
	private RichTextLabel gameTimeLabel;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//set hit to 0
		hit = 0;
		
		//set game time to 60
		gameTime = 60f;
		
		//get reference to game time text label
		gameTimeLabel = (RichTextLabel)this.GetNode("GameTime");
		
		//pick a random key
		var rnd = new Random();
		selectedKeyNumber = rnd.Next(0,26);

        //set key node animation to selected
        AnimatedSprite node = (AnimatedSprite)this.GetNode("Keys").GetChild(selectedKeyNumber);
        node.SetAnimation("selected");
		
		//get node name
		selectedKeyName = this.GetNode("Keys").GetChild(selectedKeyNumber).GetName();
		GD.Print(selectedKeyName);
		
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//decrease game time
		gameTime -= delta;
		
		//update GameTIme text to display time
		gameTimeLabel.Text = "Time left: " + Math.Floor(gameTime).ToString() + "s";
		
		//if game time <= 0, go to score scene
		if(gameTime <= 0)
			GetTree().ChangeScene("Score.scn"); 
        
    }

	public override void _UnhandledInput(InputEvent @event)
	{
		//if input has triggered
		if (@event is InputEventKey eventKey)
			//if any key was released
        	if (!eventKey.Pressed)
				//if the released key is the same as the selected key
				if(OS.GetScancodeString(eventKey.GetScancode()) == selectedKeyName)
				{
					//set animation of node back to default
					AnimatedSprite node = (AnimatedSprite)this.GetNode("Keys").GetChild(selectedKeyNumber);
        			node.SetAnimation("default");
					
					//pick a new random key
					var rnd = new Random();
					selectedKeyNumber = rnd.Next(0,26);
					
					//get node name
					selectedKeyName = this.GetNode("Keys").GetChild(selectedKeyNumber).GetName();
					GD.Print(selectedKeyName);
					
					//set node animation to selected
        			node = (AnimatedSprite)this.GetNode("Keys").GetChild(selectedKeyNumber);
        			node.SetAnimation("selected");
					
					//increase hit by 1
					hit += 1;
				}
	}
	
}

