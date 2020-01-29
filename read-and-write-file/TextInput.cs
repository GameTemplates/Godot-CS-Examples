using Godot;
using System;

public class TextInput : TextEdit
{
    private string placeholder = "Enter some text here";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		this.Text = placeholder;
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        
		//if the text editor is in focus and it is displaying the placeholder, delete the text
		if(this.HasFocus() && this.Text == placeholder)
			this.Text = "";
		
    }
	
	private void _onSaveButtonPressed()
	{
        // sending a signal when save button is pressed

        //open file for write, if file does not exist, we are crerating a new file
        var file = new File();
        file.Open("res://text.txt", File.ModeFlags.Write);

        //get text from the text input and write it in to the file
        file.StoreString(this.Text);

        //close the file
        file.Close();

    }
	
	private void _onLoadButtonPressed()
	{
        // sending a signal when load button is pressed

        //open file for read
        var file = new File();
        file.Open("res://text.txt", File.ModeFlags.Read);

        //get text from the file and assign it to the text property
		//in case the file doesn't exist, it is return an empty string
        this.Text = file.GetAsText();

        //close the file
        file.Close();
    }

}

