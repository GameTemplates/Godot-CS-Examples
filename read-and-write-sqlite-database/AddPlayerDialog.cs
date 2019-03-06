using Godot;
using System;
using Mono.Data.Sqlite; //added to references, required to work with SQLite database
using System.Data; //added to references, required to use data adapters and tables

public class AddPlayerDialog : WindowDialog
{
    
	private LineEdit newName;
	private LineEdit newScore;
	
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//get reference to input fields
		newName = (LineEdit)this.GetNode("NewName");
		newScore = (LineEdit)this.GetNode("NewScore");
        
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }

	private void _onAddButtonPressed()
	{
    	// sending a signal if add button is pressed
		
		//check if the score edit box contain number only
		
		try
		{
			//try to get score value from input field as an integer
			int score = int.Parse(newScore.Text);
			
			//get name value from input field
			var name = newName.Text; 
			
			//if the value was a number, make sure the name is at least 3 characters long
			if(name.Length < 3)
			{
				//if the length less than 3 display alert it must be 3 character
				Game.DisplayAlert("The name must be at least 3 characters long");
			}
			else //if everything looks ok
			{
				//add the name to the database
				Game.connection.Open();
				Game.queryString = $"INSERT INTO Players (Name, Score) VALUES ('{name}', {score})";
				Game.queryCommand = new SqliteCommand(Game.queryString, Game.connection);
				Game.queryCommand.ExecuteNonQuery();
				Game.connection.Close();
				
				//update the item list to display the new database
				Game.UpdateItemList();
				
				//close tis dialog
				this.Hide();
			}
			
		}
		catch(FormatException) //if we get this error, that means the value was not a number
		{
			
			//display the alert box we accept numbers only
			Game.DisplayAlert("The score can be numbers only");
		}
		
	}
	
	private void _onAddPlayerDialogAboutToShow()
	{
    	// sending a signal if the dialog is about to popup
		
		//make sure the input fields are empty
		newName.Text = "";
		newScore.Text = "";
	}

}
