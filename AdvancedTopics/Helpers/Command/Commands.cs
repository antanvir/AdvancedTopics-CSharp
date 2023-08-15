namespace AdvancedTopics.Helpers.Command
{
    public class Command : ICommand
    {
        public Book _book;
        public ShopCart _shopCart;
        public string _commandType;
        public Command(Book book, ShopCart shopCart)
        {
            _book = book;
            _shopCart = shopCart;
            _commandType = "";
        }
        public virtual bool Add()
        {
            return true;
        }

        public virtual bool Remove()
        {
            return true;
        }
    }

    public class CartCommand : Command
    {
        public CartCommand(Book book, ShopCart shopCart) : base(book, shopCart) { }
        public override bool Add()
        {
            if (_book.InStockAmount > 0)
            {
                _book.DecreaseStock();
                _shopCart.AddToCartItems();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Remove()
        {
            _book.IncreaseStock();
            _shopCart.RemoveFromCartItems();
            return true;
        }
    }

    public class WishListCommand : Command
    {
        public WishListCommand(Book book, ShopCart shopCart) : base(book, shopCart) { }

        public override bool Add()
        {
            if (_book.InStockAmount > 0)
            {
                _book.DecreaseStock();
                _shopCart.AddToWishListItems();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Remove()
        {
            _book.IncreaseStock();
            _shopCart.RemoveFromWishListItems();
            return true;
        }
    }
}
