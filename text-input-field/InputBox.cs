using Godot;
using System;

public class InputBox : LineEdit
{
   
/*
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        
    }
*/

	private void _onButtonPressed()
	{
    	//we are sending a signal to the object this script is attached to if the button is pressed
		
		//if button is pressed, get the dialog box
		AcceptDialog dialog = (AcceptDialog)this.FindNode("Dialog");
		
		//set text of dialog box to display the text of the input box
		if(this.Text != "")
			dialog.SetText(this.Text);
			
		//or in case the input box was empty, leave a message you entered no text
		if(this.Text == "") 
			dialog.SetText("You entered no text");
		
		//show dialog
        dialog.Popup_();
		
	}
}
