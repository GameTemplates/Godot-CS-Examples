using Godot;
using System;

public class Ship : Node2D
{
    private Tween tween;
	private float tweenDuration = 0.5f; //lower the value the faster the rotation

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//get the tween node
		tween = (Tween)this.GetNode("tween");
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//get mouse position
		var mouseX = GetViewport().GetMousePosition().x;
		var mouseY = GetViewport().GetMousePosition().y;
		
		//get ship current position
		var objectPos = this.GetPosition();
		
		//get ship current rotation in radiant
		var currentRotation = this.Rotation;
		//calculate target rotation using the mouse and ship position
		var targetRotation = (float)Math.Atan2(mouseY - objectPos.y, mouseX - objectPos.x);
		
		//initialize tween to change the rotation property of ship and change it from current toward target in the given time with 0 delay
		tween.InterpolateProperty(this, "rotation", currentRotation, targetRotation, tweenDuration, Tween.TransitionType.Linear, Tween.EaseType.In, 0f );
		//start tween
        tween.Start();
        
    }
}
