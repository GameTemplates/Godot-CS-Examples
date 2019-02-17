using Godot;
using System;

public class Explosion : Node2D
{
    private float lifeTime;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
		//initialize values
		lifeTime = 2; //lifetime of explosion is 2 seconds
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//reduce time
		lifeTime -= delta;
		
		//if time <= 0 delete particle emitter
		if(lifeTime <= 0)
        	this.Free();
    }
}
