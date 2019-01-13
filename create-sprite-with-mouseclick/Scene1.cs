using Godot;
using System;

public class Scene1 : Node
{
   
	private PackedScene prefab;
	
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
		//load the scene containing the sprite we need, I call this prefab
		prefab = (PackedScene)ResourceLoader.Load("res://Prefabs/Face.tscn");
    }

    public override void _Process(float delta)
    {
        
		//if left mouse button is just pressed, create an instance from prefab
		if(Input.IsActionJustPressed("mouse_left"))
		{
		
			var face = prefab.Instance();
			AddChild(face);
			
		}
		
		
    }
}
