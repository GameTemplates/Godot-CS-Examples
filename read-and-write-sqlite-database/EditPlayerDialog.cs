using Godot;
using System;
using Mono.Data.Sqlite; //added to references, required to work with SQLite database
using System.Data; //added to references, required to use data adapters and tables

public class EditPlayerDialog : WindowDialog
{

	private LineEdit editName;
	private LineEdit editScore;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//get reference to edit boxes
		editName = (LineEdit)this.GetNode("EditName");
		editScore = (LineEdit)this.GetNode("EditScore");
        
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }

	private void _onEditButtonPressed()
	{
    	// sending a signal if edit button is pressed
		
		//get id value of selected item, expected to be only 1 selected
		var selectedItems = Game.itemList.GetSelectedItems();
		var id = Game.itemList.GetItemText(selectedItems[0]);
		
		try
		{
			//try to get score value from input field as an integer
			int score = int.Parse(editScore.Text);
			
			//get name value from input field
			var name = editName.Text; 
			
			//if the value was a number, make sure the name is at least 3 characters long
			if(name.Length < 3)
			{
				//if the length less than 3 display alert it must be 3 character
				Game.DisplayAlert("The name must be at least 3 characters long");
			}
			else //if everything looks ok
			{
				//update name and score in database
				Game.connection.Open();
				Game.queryString = $"Update Players SET Name = '{name}', Score = {score} WHERE Id = {id} ";
				Game.queryCommand = new SqliteCommand(Game.queryString, Game.connection);
				Game.queryCommand.ExecuteNonQuery();
				Game.connection.Close();
				
				//update the item list to display the new database
				Game.UpdateItemListByOrder(Game.sortBy, Game.sortOrder);
				
				//close this dialog
				this.Hide();
			}
			
		}
		catch(FormatException) //if we get this error, that means the value was not a number
		{
			
			//display the alert box we accept numbers only
			Game.DisplayAlert("The score can be numbers only");
		}
		
	}
	
	private void _onEditPlayerDialogAboutToShow()
	{
    	// sending a signal if dialog is about to popup
		
		//get selected item, only 1 item expected to be selected
		var selectedItems = Game.itemList.GetSelectedItems();
		
		//get id of selected item
		var id = Game.itemList.GetItemText(selectedItems[0]);
		
		//get name and score of the item from the database
		Game.connection.Open(); //open connection to database
		
		Game.queryString = $"SELECT Name FROM Players WHERE Id = {id}";
		Game.queryCommand = new SqliteCommand(Game.queryString, Game.connection);
		var name = Convert.ToString(Game.queryCommand.ExecuteScalar());
		
		Game.queryString = $"SELECT Score FROM Players WHERE Id = {id}";
		Game.queryCommand = new SqliteCommand(Game.queryString, Game.connection);
		var score = Convert.ToString(Game.queryCommand.ExecuteScalar());
		
		//get dialog edit boxes and enter the name and score of the selected player
		editName.Text = name;
		editScore.Text = score;
		
		Game.connection.Close(); //close connection to database
	}

}

