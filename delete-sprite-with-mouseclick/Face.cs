using Godot;
using System;

public class Face : Node2D
{
    private Sprite sprite;
	private int spriteWidth;
	private int spriteHeight;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//if left mouse button is down (means we just created the node)
		if(Input.IsActionPressed("left_mouse"))
		{	
			//get mouse position
			var mX = GetViewport().GetMousePosition().x;
			var mY = GetViewport().GetMousePosition().y;
			
			//set sprite position to mouse
			this.SetPosition(new Vector2(mX,mY));
		}
		
        //get sprite node
        sprite = (Sprite)this.FindNode("sprite");

        //get the size of the sprite
        var size = sprite.Texture.GetSize();

        //get width and height component of the size
        spriteWidth = (int)size.x;
        spriteHeight = (int)size.y;

    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//if right mouse button was clicked
		if(Input.IsActionJustPressed("right_mouse"))
		{
			
			//get mouse position
			var mX = GetViewport().GetMousePosition().x;
			var mY = GetViewport().GetMousePosition().y;
			
			//get sprite top left corner (origin)
			var sX = this.GetPosition().x - (spriteWidth / 2);
			var sY = this.GetPosition().y - (spriteWidth / 2);
			
			//check if the mouse is over the sprite
			if(mX >= sX)
			{
				if(mX < sX + spriteWidth)
                {
                    if(mY >= sY)
                    {
                        if(mY < sY + spriteHeight)
                        {
							//delete sprite
							this.Free();
                        }
                    }
                }
			}
		}
        
    }
	
	
}

