namespace Custom.Menu
{
    internal class Menu(string title, ConsoleColor titlecolor)
    {
        List<MenuItem> MenuItems { get; set; } = [];

        ConsoleColor Titlecolor { get; } = titlecolor;

        ConsoleColor DeSelectColor { get; set; }
        ConsoleColor DeDisplayColor { get; set; }

        int SelectedItem { get; set; }
        int RealSelectedItem { get; set; }
        int NextItemNumber { get; set; }
        int HiddenItems { get; set; } = 0;

        public string Title { get; } = title;

        private bool isWrappingAllowed { get; set; } = false;
        private SelectionType selectionType { get; set; } = SelectionType.defalut;
        
        //debug options here disable for final build
        public bool DbgItemNumbersShown { get; } = false;
        public bool DbgItemIdsShown { get; } = false;
        public bool DbgSelectedItemShown { get; } = false;


        public void LineUp()
        {
            bool isFirst = SelectedItem == 0;
            
            if (isWrappingAllowed)
            {
                if(isFirst) {SelectedItem = MenuItems.Count - 1 - HiddenItems;}
                else { SelectedItem--; }
            }
            else if (!isFirst) { SelectedItem--; }
            
            DetermineRealSelectedItem();
        }

        public void LineDown()
        {
            bool isLast = SelectedItem == MenuItems.Count - 1 - HiddenItems;
            
            if (isWrappingAllowed)
            { 
                if(isLast) {SelectedItem = 0;}
                else {SelectedItem++;}
            }
            else if (!isLast) { SelectedItem ++; }
            
            DetermineRealSelectedItem();
        }

        public void AddItem(string label, ConsoleColor displayColor, ConsoleColor selectColor)
        {
            MenuItems.Add(new MenuItem(label, displayColor, selectColor, NextItemNumber, NextItemNumber));
            NextItemNumber++;
        }

        public void AddItem(string label)
        {
            //add item with defalut colors. if not set defaluts to black
            MenuItems.Add(new MenuItem(label, DeDisplayColor, DeSelectColor, NextItemNumber, NextItemNumber));
            NextItemNumber++;
        }

        public void SetDefalutColors(ConsoleColor display, ConsoleColor select)
        {
            DeSelectColor = select;
            DeDisplayColor = display;
        }

        public int GetSelectedItem()
        {
            return RealSelectedItem;
        }

        public void Render()
        {
            Console.Clear();
            Console.ForegroundColor = Titlecolor;
            Console.WriteLine(Title);
            if(DbgSelectedItemShown) Console.WriteLine(SelectedItem);
            foreach (MenuItem item in MenuItems)
            {
                bool IsSelected = item.ItemNumber == SelectedItem;
                if (IsSelected)
                {
                    item.IsSelected = true;
                }
                else
                {
                    item.IsSelected = false;
                }
                
                
                
                if (!item.IsHidden)
                {
                    if (DbgItemNumbersShown) Console.Write($"#{item.ItemNumber} ");
                    if (DbgItemIdsShown) Console.Write($"Id{item.ItemId} ");
                    
                    if(selectionType == SelectionType.defalut)
                    {
                        Console.ForegroundColor = (IsSelected)? item.SelectColor : item.DisplayColor;
                        Console.WriteLine(item.Label);
                    }
                    
                    if (selectionType == SelectionType.prefix && IsSelected)
                    {
                        Console.Write($"> ");
                        Console.ForegroundColor = item.SelectColor;
                        Console.WriteLine(item.Label);
                        continue;
                    }
                    else if (selectionType == SelectionType.prefix)
                    {
                        Console.ForegroundColor = item.DisplayColor;
                        Console.WriteLine(item.Label);
                    }
                    
                    if (selectionType == SelectionType.suffix && IsSelected) 
                    {
                        Console.ForegroundColor = item.SelectColor;
                        Console.Write(item.Label);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(" <"); 
                    }
                    else if (selectionType == SelectionType.suffix)
                    {
                        Console.ForegroundColor = item.DisplayColor;
                        Console.WriteLine(item.Label);
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            DetermineRealSelectedItem();
        }

        public void HideItem(int itemNumber)
        {
            if ((itemNumber > MenuItems.Count-1) || (itemNumber * -1 > 0)) throw new ("Tried to hide an item with a non valid index");
            else MenuItems.ElementAt(itemNumber).IsHidden = true;
            HiddenItems++;
            Renumber();
        }

        public void ShowItem(int itemNumber)
        {
            if ((itemNumber > MenuItems.Count-1) || (itemNumber * -1 > 0)) throw new ("Tried to show an item with a non valid index");
            else MenuItems.ElementAt(itemNumber).IsHidden = false;
            HiddenItems--;
            Renumber();
        }

        public void ChangeDisplayColor(int itemNumber, ConsoleColor newColor)
        {
            MenuItems.ElementAt(itemNumber).DisplayColor = newColor;
        }

        public void ChangeSelectColor(int itemNumber, ConsoleColor newColor)
        {
            MenuItems.ElementAt(itemNumber).DisplayColor = newColor;
        }

        public void ChangeLabel(int itemNumber, string newLabel)
        {
            MenuItems.ElementAt(itemNumber).Label = newLabel;
        }

        public void SetWrappingFlag(bool setTo)
        {
            isWrappingAllowed = setTo;
        }

        public void SetSelectionType(SelectionType setTo)
        {
            selectionType = setTo;
        }
        
        public enum SelectionType
        {
            prefix,
            suffix,
            defalut,
        }

#region Private class and methods
        private void Renumber()
        {
            NextItemNumber = 0;

            foreach (MenuItem item in MenuItems)
            {
                if (item.IsHidden) {item.ItemNumber = -1;}
                else 
                {
                    item.ItemNumber = NextItemNumber;
                    NextItemNumber++;
                }
            }
        }

        private void DetermineRealSelectedItem()
        {
            foreach (MenuItem item in MenuItems)
            {
                if(item.IsSelected)
                {
                    RealSelectedItem = item.ItemId;
                    break;
                }
            }
        }

        private class MenuItem(string label, ConsoleColor displayColor, ConsoleColor selectColor, int itemNumber, int itemId)
        {
            public int ItemNumber { get; set; } = itemNumber;
            public int ItemId { get; } = itemId;
            public string Label { get; set; } = label;
            public ConsoleColor DisplayColor { get; set; } = displayColor;
            public ConsoleColor SelectColor { get; set; } = selectColor;
            public bool IsHidden { get; set; }
            public bool IsSelected { get; set; }
        }
    
        
#endregion
    }
}