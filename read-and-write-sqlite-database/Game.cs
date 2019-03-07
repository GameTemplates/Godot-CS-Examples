/*
NOTE: this example require to open the solution file in Visual Studio or Mono Develop to add reference to
the packages required to work with SQLite database and data.
Also require to install SQLite on our computer: https://www.sqlite.org/index.html

WHEN exporting the project you may need to copy the database file over to the export folder manually!
 */

using System;
using Godot;
using Mono.Data.Sqlite; //added to references, required to work with SQLite database
using System.Data; //added to references, required to use data adapters and tables

public class Game : Node
{
    private string database;
    public static SqliteConnection connection; //public access with Game.connection
    public static SqliteCommand queryCommand; //public access with Game.queryCommand
	public static string queryString; //public access with Game.queryString
	public static ItemList itemList; //public access with Game.itemList
	private Popup addPlayerDialog;
	private Popup editPlayerDialog;
	public static Popup alertDialog; //public access with Game.alertDialog
	public static string sortBy; //public access with Game.sortBy
	public static string sortOrder; //public access with Game.sortOrder
	
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		
		//initialize values
		sortBy = "Id"; //initially the list is sorted by Id
		sortOrder = "ASC"; //and ordered in ascending order
		
		//get reference to item list
		itemList = (ItemList)this.GetNode("ItemList");
		
		//get reference to popup window dialogs
		addPlayerDialog = (Popup)this.GetNode("AddPlayerDialog");
		editPlayerDialog = (Popup)this.GetNode("EditPlayerDialog");
		alertDialog = (Popup)this.GetNode("AlertDialog");
		
        //connection string to estabilish connection to a database file
        database = "URI=file:mydata.db"; //file is located in root directory and set to copy if newer when compile

        try
        {
            //establisih and open connection to the database
            connection = new SqliteConnection(database);
            connection.Open();

            //create a query string to get SQLite version from the database
            queryString = "SELECT SQLITE_VERSION()";
            //create a new command using the query string
            queryCommand = new SqliteCommand(queryString, connection);
            //execute the command which return the version number
            string version = Convert.ToString(queryCommand.ExecuteScalar());
            //display SQLite version number to validate the connection was successfull
            GD.Print($"SQLite version : {version}");
			
			//display all items from the database in the list
			UpdateItemListByOrder(sortBy, sortOrder);

        }

        //catch SQLite exceptions
        catch (SqliteException ex)
        {
            //if excepton happened, outut the error
            GD.Print("Error: {0}", ex.ToString());

        }
        finally //if no exception happened
        {
            if (queryCommand != null) //if the queryCommand is null, meaning contain no valid query string or connection
            {
                queryCommand.Dispose(); //dispose the command
            }

            if (connection != null) //if connection is null meanining, failed to connect
            {
                try
                {
                    connection.Close(); //close connection

                }
                catch (SqliteException ex) //display error if connection failed, it is also accour when we close the connection
                {
                    GD.Print("Closing connection failed.");
                    GD.Print("Error: {0}", ex.ToString());

                }
                finally
                {
                    connection.Dispose(); //dispose connection if connection failed (or closed)
                }
            }
        }

    }

    //    public override void _Process(float delta)
    //    {
    //        // Called every frame. Delta is time since last frame.
    //        // Update game logic here.
    //        
    //    }
	
	private void _onAddPlayerButtonPressed()
	{
    	// sending a signal if add player button pressed
		
		//show the dialog
		addPlayerDialog.PopupCentered();
	}
	
	private void _onEditPlayerButtonPressed()
	{
    	// sending a signal if edit player button pressed
		
		//get selected item
		var selectedItems = itemList.GetSelectedItems();
		
		//if item is selected, expected to be only 1 selected
		if(selectedItems.Length != 0)
		{
			//show the dialog
			editPlayerDialog.PopupCentered();
			
		}
		else //otherwise display a message to select an item ID first
		{
			DisplayAlert("Please select an ID from the list");
		}
	}
	
	private void _onDeletePlayerButtonPressed()
	{
    	// sending a signal if delete player button pressed
		
		//get selected item
		var selectedItems = itemList.GetSelectedItems();
		
		//if item is selected
		if(selectedItems.Length != 0)
		{
			//get id of selected item
			var id = itemList.GetItemText(selectedItems[0]);
			
			//delete selected item from database
			connection.Open();
			queryString = $"DELETE FROM Players WHERE Id = {id}";
			queryCommand = new SqliteCommand(queryString, connection);
            queryCommand.ExecuteNonQuery();
			connection.Close();
			
			//update item list
			UpdateItemListByOrder(Game.sortBy, Game.sortOrder);
		}
		else //otherwise display a dialog and ask the user to select an ID
		{
			
			DisplayAlert("Please select an ID from the list");
		}
		
	}
	
	private void _onIdButtonPressed()
	{
    	// sending a signal if ID button pressed
		
		//if the list is currently sorted by ID
		if(sortBy == "Id")
		{
			//if the list is ordered to ascending
			if(sortOrder == "ASC")
			{
				//set order to descending
				sortOrder = "DESC";
				
				//update list by order
				UpdateItemListByOrder(sortBy, sortOrder);
				
			}
			else //otherwise set order to ascending
			{
				sortOrder = "ASC";
				
				//update list by order
				UpdateItemListByOrder(sortBy, sortOrder);
			}
		}
		else //otherwise sort it by Id in descending order
		{
			sortBy = "Id";
			sortOrder = "DESC";
			
			//update list by order
			UpdateItemListByOrder(sortBy, sortOrder);
		}
	}
	
	private void _onNameButtonPressed()
	{
    	// sending signal if name button is pressed
		
		//if the list is currently sorted by Name
		if(sortBy == "Name")
		{
			//if the list is ordered to ascending
			if(sortOrder == "ASC")
			{
				//set order to descending
				sortOrder = "DESC";
				
				//update list by order
				UpdateItemListByOrder(sortBy, sortOrder);
				
			}
			else //otherwise set order to ascending
			{
				sortOrder = "ASC";
				
				//update list by order
				UpdateItemListByOrder(sortBy, sortOrder);
			}
		}
		else //otherwise sort it by Name in descending order
		{
			sortBy = "Name";
			sortOrder = "DESC";
			
			//update list by order
			UpdateItemListByOrder(sortBy, sortOrder);
		}
	}
	
	private void _onScoreButtonPressed()
	{
    	// sending a signal if score button is pressed
		
		//if the list is currently sorted by Score
		if(sortBy == "Score")
		{
			//if the list is ordered to ascending
			if(sortOrder == "ASC")
			{
				//set order to descending
				sortOrder = "DESC";
				
				//update list by order
				UpdateItemListByOrder(sortBy, sortOrder);
				
			}
			else //otherwise set order to ascending
			{
				sortOrder = "ASC";
				
				//update list by order
				UpdateItemListByOrder(sortBy, sortOrder);
			}
		}
		else //otherwise sort it by Score in descending order
		{
			sortBy = "Score";
			sortOrder = "DESC";
			
			//update list by order
			UpdateItemListByOrder(sortBy, sortOrder);
		}
		
	}
	
	
	//method to display alert box
	public static void DisplayAlert(string text)
	{
		var label = (RichTextLabel)Game.alertDialog.GetNode("AlertLabel");
		label.Text = text;
		Game.alertDialog.PopupCentered();
	}
	
	//method to display, update all items from the database in the list by order
	public static void UpdateItemListByOrder(string _sortBy, string _sortOrder)
	{
		
		//clear item list
		Game.itemList.Clear();
		
		//get all rows from the Players table
		if(_sortBy == "Id" || _sortBy == "Score")
			Game.queryString = $"SELECT * FROM Players ORDER BY {_sortBy} {_sortOrder}";
		if(_sortBy == "Name")
			Game.queryString = $"SELECT * FROM Players ORDER BY {_sortBy} COLLATE NOCASE {_sortOrder}";
        //create a command
        Game.queryCommand = new SqliteCommand(Game.queryString, Game.connection);
		//execute the command and store response inside an adapter
        SqliteDataAdapter da = new SqliteDataAdapter(Game.queryCommand);
		//create a new data table
        DataTable dt = new DataTable();
		//fill the data table in to the adapter
        da.Fill(dt);
		
		//add all rows to the item list
        foreach (DataRow dr in dt.Rows)
        {
			//3 in a row
			Game.itemList.AddItem(dr["Id"].ToString(),null,true);
			Game.itemList.AddItem(dr["Name"].ToString(),null,false);
			Game.itemList.AddItem(dr["Score"].ToString(),null,false);
        }
	}
	
}
