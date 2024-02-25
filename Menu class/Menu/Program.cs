using Custom.Menu;

Menu menuDemo = new Menu("==--+--==\nMENU DEMO\n==--+--==", ConsoleColor.White);
//The title argument is gonna be displayed at the top of the menu in the specified color.
//============================================================
menuDemo.AddItem("item1", ConsoleColor.White, ConsoleColor.Green);
//You can add an item using the this method
//label is self explanatory (it specifies the item's label)
//displayColor specifies the item's color when it is not selected
//selectColor specifies the item's color when it not selected
//=============================================================
menuDemo.SetDefalutColors(ConsoleColor.White, ConsoleColor.Green);
//this method is used to make adding a lot of items in the same color easier,
//and works with an override of the AddItem method where the only argument is
//label, and the colors are set by this method. If not they defalut to black
//=============================================================
menuDemo.Render();
//this method is used to draw the menu to the screen
//=============================================================
menuDemo.LineUp(); /*and*/ menuDemo.LineDown();
//are used to move the current selected line up or down
//=============================================================
menuDemo.GetSelectedItem();

//returns the current selected line starting at 0
//=============================================================
menuDemo.HideItem(0); /*and*/ menuDemo.ShowItem(0);
//are used to show and hide the selected item statrting at 0
//=============================================================
menuDemo.ChangeDisplayColor(0, ConsoleColor.White); /*,*/ menuDemo.ChangeSelectColor(0, ConsoleColor.Green); 
/*and*/ menuDemo.ChangeLabel(0, "Item1");
//are used to change there separate parameters of the selected item starting at 0
//=============================================================
menuDemo.SetWrappingFlag(true);
//enables/disables the menu wrapping around once you scroll past the first or last item
//and is defaluted to false
//=============================================================
menuDemo.SetSelectionType(Menu.SelectionType.defalut);
//is used to change how the menu selection looks.
//prefix puts "> " before the selected item's name and highlights the item with its select color
//suffix puts " <" behind the selected item's name and highlights the item with its select color
//defalut only highlights the item with its select color
//=============================================================
//the menu class also contains 3 flags used for debugging that are only accessable manually:
//DbgItemNumbersShown: shows a line number infront of each item
//DbgItemIdsShown: shows the item id of each item
//DbgSelectedItemShown: shows the value of the selected item variable
//TO UNDERSTAND THESE YOU SHOULD LOOK INTO THE CODE
//=============================================================
//so a little menu program would look something like this:

Console.CursorVisible = false; 

Menu menu = new Menu("THIS IS A TITLE", ConsoleColor.White);
menu.SetDefalutColors(ConsoleColor.White, ConsoleColor.Green);
menu.AddItem("item1");
menu.AddItem("item2");
menu.AddItem("item3");
menu.AddItem("Quit", ConsoleColor.Red, ConsoleColor.Green);
menu.Render();
while (true)
{
    ConsoleKeyInfo key = Console.ReadKey(true);
    switch(key.Key)
    {
        case ConsoleKey.UpArrow:
        menu.LineUp();
        break;
        case ConsoleKey.DownArrow:
        menu.LineDown();
        break;
        case ConsoleKey.Enter:
        switch (menu.GetSelectedItem())
        {
            case 0:
            Console.Clear();
            Console.WriteLine("1");
            Console.ReadKey(true);
            break;
            case 1:
            Console.Clear();
            Console.WriteLine("2");
            Console.ReadKey(true);
            break;
            case 2:
            Console.Clear();
            Console.WriteLine("3");
            Console.ReadKey(true);
            break;
            case 3:
            Environment.Exit(0);
            break;
        }
        break;
    }
    menu.Render();
}