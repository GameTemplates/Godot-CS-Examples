using Godot;
using System;

public class Ship : Node2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
	
	private float rotationSpeed = 10f;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        
	
        //get position of mouse
		var mousePosition = GetViewport().GetMousePosition();
        var mouseX = mousePosition.x;
        var mouseY = mousePosition.y;
		
		//get position of sprite
		var objectPos = this.GetPosition();
		
		//get angle of object and target in radiant
        var objAngle = this.Rotation;
        var targetAngle = (float)System.Math.Atan2(mouseY - objectPos.y, mouseX - objectPos.x);

		//keep target angle value in range 0-6
        if (targetAngle < 0)
        {
            targetAngle += (float)(2 * Math.PI);
        }
		else if(targetAngle > 6)
		{
			targetAngle -= (float)(2 * Math.PI);
		}
		
		//keep object rotation value stay in range 0-6
		if (this.Rotation < 0)
        {
            this.Rotation += (float)(2 * Math.PI);
        }
		else if(this.Rotation > 6)
		{
			this.Rotation -= (float)(2 * Math.PI);
		}
		
		//calculating the differenve between the two angle
        var angleDiff = Math.Abs(targetAngle - objAngle);
		//calculate minimum angle difference, this is to decide if it worth rotating object at given speed or rotate immediately instead
		var minAngleDiff = 0.5f;
	
		//if rotation speed is <= 0 set angle immediately
        if (rotationSpeed <= 0)
        {
            this.LookAt(mousePosition);
 
        }
        else //otherwise rotate object toward position at the given speed
        {
			
			//if angle difference is > than minAngleDiff
			if(angleDiff > minAngleDiff)
			{
	            //if object angle is greater than target angle, rotate object to the left
	            if (objAngle > targetAngle)
	            {
	               	if (objAngle > 5 && targetAngle < 1) //if the target angle pass 0 and the angle of object is almost there, keep rotating the object to the right
	                {
	                    this.Rotation += rotationSpeed * delta;
	                }
	                else //otherwise just rotate the object to the left as normal
	                {
	                    this.Rotation -= rotationSpeed * delta;
	                }
	            }
	
	            //if object angle is less than the target angle, rotate object to the right
	            if(objAngle < targetAngle)
	            {
	                if (objAngle < 2 && targetAngle > 5) //if the target angle is pass 0 and the angle of the object is almost there, keep rotating the object to the left
	                {
	                    this.Rotation -= rotationSpeed * delta;
						
	                }
	                else //otherwise just rotate the object to the right as normal
	                {
	                    this.Rotation += rotationSpeed * delta;
	                }
	            }
				
			}
			else if(angleDiff <= minAngleDiff)// if angle diff is < than the minimum angle diff, rotate object immediately
			{
				this.LookAt(mousePosition);
			}
			
			GD.Print(targetAngle);

        }
			
		
    }
}
