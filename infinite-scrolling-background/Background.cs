using Godot;
using System;

public class Background : Node2D
{
    private float scrollSpeed;
	private Sprite sprite;
	private float spriteWidth;
	private float spriteHeight;
	private float screenWidth;
	private float screenHeight;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//initialize values
		scrollSpeed = 100f;
		
		//get reference to sprite
		sprite = (Sprite)this.GetNode("sprite");
		
		//get window width and height
		screenWidth = GetViewport().GetSize().x;
		screenHeight = GetViewport().GetSize().y;
		
		//get sprite width and height
		spriteWidth = sprite.GetTexture().GetWidth(); /// this.GetScale().x;
        spriteHeight = sprite.GetTexture().GetHeight(); /// this.GetScale().y;
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//get current position
		float posX = this.GetPosition().x;
		float posY = this.GetPosition().y;
		
		//move background to the left at the given speed
		posX -= scrollSpeed * delta;
		
		//update positon of the background
		this.SetPosition(new Vector2(posX, posY));
		
		//note: image is imported as Repeat and using the Region property to create a tiled background
		//if the origin point of the image (the middle of the Region) leave the screen on the left, reset the position to the right
		if(posX <= 0)
			posX = screenWidth;
		
       	//update positon of the background
		this.SetPosition(new Vector2(posX, posY));
    }
}
