using Godot;
using System;

public class Faces : Node2D
{
   
    private PackedScene prefab;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here

        //load scene containing the sprite, I call this prefab
        prefab = (PackedScene)ResourceLoader.Load("res://Face.tscn");

    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		
		//if left mouse button was pressed
		if(Input.IsActionJustPressed("left_mouse"))
		{

            //create an instance of the Face from the prefab
            var face = prefab.Instance();
            AddChild(face);
        }
        
    }
}
