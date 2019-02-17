using Godot;
using System;

public class ExplosionSound : AudioStreamPlayer
{

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//randomize the pitch of the sound so it is a bit different each time
		var randomPitch = Game.rnd.NextDouble(); //return a number between 0.0 and 1.0
		
		if(randomPitch < 0.7) //make sure the value is not less than 0.7
			randomPitch = 0.7;
			
		this.PitchScale = (float)randomPitch; //set the pitch of the sound
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//when the sound finished playing, delete it self
		if(!this.IsPlaying())
			this.Free();
        
    }
}
