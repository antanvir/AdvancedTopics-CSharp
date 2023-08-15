namespace AdvancedTopics.Helpers.Command
{
    public class Book
    {
        public string BookTitle { get; set; }
        public int InStockAmount { get; set; }

        public Book()
        {
            this.BookTitle = "SpyxFamily Vol. 06";
            this.InStockAmount = 10;
        }

        public void IncreaseStock()
        {
            this.InStockAmount++;
        }

        public void DecreaseStock()
        {
            this.InStockAmount--;
        }
    }

    public class ShopCart
    {
        public string CustomerId { get; set; }
        public int CartItems { get; set; }
        public int WishListItems { get; set; }

        public ShopCart()
        {
            this.CustomerId = new Guid().ToString();
            this.CartItems = 0;
            this.WishListItems = 0;
        }

        public void AddToCartItems()
        {
            this.CartItems++;
        }

        public void RemoveFromCartItems()
        {
            this.CartItems--;
        }

        public void AddToWishListItems()
        {
            this.WishListItems++;
        }

        public void RemoveFromWishListItems()
        {
            this.WishListItems--;
        }
    }

    public class Customer
    {
        public string Action { get; set; }
        public Book Book { get; set; }
        public ShopCart ShopCart { get; set; }
        public Stack<Command> CommandStack { get; set; }

        public Customer() {
            Action = "";
            ShopCart = new ShopCart();
            Book = new Book();
            CommandStack = new Stack<Command>();
        }

        public void ExecuteCommand()
        {
            Command command = null;
            switch (Action)
            {
                case "add-to-cart":
                    command = new CartCommand(Book, ShopCart);
                    command._commandType = Action;
                    break;
                case "add-to-wish-list":
                    command = new WishListCommand(Book, ShopCart);
                    command._commandType = Action;
                    break;
                default:
                    break;
            }
            if (command != null)
            {
                bool isSuccess = command.Add();
                if (isSuccess)
                {
                    CommandStack.Push(command);
                }
            }
        }

        public void UndoCammand()
        {
            if (CommandStack.Count != 0)
            {
                Command commandToUndo = CommandStack.Pop();
                commandToUndo._book = Book;
                commandToUndo._shopCart = ShopCart;
                commandToUndo.Remove();
            }
        }

        public ShopCart GetCustomersCart()
        {
            return ShopCart;
        }
    }
}
