using AdvancedTopics.Helpers.Command;
using AdvancedTopics.Helpers.Decorator;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace AdvancedTopics.Controllers
{
    public class DesignPatternController : Controller
    {
        public Customer _customer;
        private readonly ISession _session;

        public DesignPatternController(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _customer = new Customer();
        }

        public IActionResult GetDesignPatternContents()
        {
            //TestSessionData();
            _session.SetString("CustomerObject", JsonConvert.SerializeObject(_customer));
            return PartialView("../DesignPatterns/_DesignPatternView", _customer);
        }

        [HttpPost]
        public IActionResult UpdateCustomerCart(string ActionName)
        {
            _customer = JsonConvert.DeserializeObject<Customer>(_session.GetString("CustomerObject")) ?? new Customer();
            _customer.CommandStack = prepareCommandStack(_customer.CommandStack);
            _customer.Action = ActionName;

            if (_customer.Action != "undo")
            {
                _customer.ExecuteCommand();
            }
            else
            {
                _customer.UndoCammand();
            }
            _session.SetString("CustomerObject", JsonConvert.SerializeObject(_customer));
            return PartialView("../DesignPatterns/_DesignPatternView", _customer);
        }

        [HttpPost]
        public JsonResult EnrolSubscriber(string Name, bool IsLockerSubscribed, bool IsSwimmingSubscribed, bool IsTennisSubscribed)
        {
            string subscriptionDetails = "";
            
            IGymSubscriber subscriber = new BasicGymSubscriber(Name);
            if (IsLockerSubscribed)
            {
                subscriber = new LockerSubscriberDecorator(subscriber);
            }
            if (IsSwimmingSubscribed)
            {
                subscriber = new SwimmingSubscriberDecorator(subscriber, 2);
            }
            if (IsTennisSubscribed)
            {
                subscriber = new TableTennisSubscriberDecorator(subscriber, 1);
            }
            subscriptionDetails = subscriber.Subscribe();

            return Json(subscriptionDetails);
        }


        private Stack<Command> prepareCommandStack(Stack<Command> commandsStack)
        {
            List<Command> commandList = commandsStack.ToList();
            for (int i = 0; i < commandList.Count; i++)
            {
                if (commandList[i]._commandType == "add-to-cart")
                {
                    commandList[i] = JsonConvert.DeserializeObject<CartCommand>(JsonConvert.SerializeObject(commandList[i]));
                }
                else if (commandList[i]._commandType == "add-to-wish-list")
                {
                    commandList[i] = JsonConvert.DeserializeObject<WishListCommand>(JsonConvert.SerializeObject(commandList[i]));
                }
            }

            commandsStack = new Stack<Command>(commandList);
            return commandsStack;
        }

        private void TestSessionData()
        {
            Customer customer = new Customer();
            customer.ShopCart.CartItems = 2;
            customer.ShopCart.WishListItems = 1;
            customer.CommandStack.Push(new CartCommand(customer.Book, customer.ShopCart));
            customer.CommandStack.Push(new CartCommand(new Book(), new ShopCart()));

            List<Command> custArr = customer.CommandStack.ToList();
            for (int i = 0; i < custArr.Count; i++)
            {
                if (custArr[i]._commandType == "add-to-cart")
                {
                    custArr[i] = (CartCommand)custArr[i];
                }
            }

            Stack<Command> custStack = new Stack<Command>(custArr);

            _session.SetString("CustomerObject", JsonConvert.SerializeObject(customer));
            _session.SetString("ShopCartObject", JsonConvert.SerializeObject(customer.ShopCart));

            Customer viewDataCustomer = JsonConvert.DeserializeObject<Customer>(_session.GetString("CustomerObject")) ?? new Customer();
            ShopCart viewDataCart = JsonConvert.DeserializeObject<ShopCart>(_session.GetString("ShopCartObject")) ?? new ShopCart();

        }
    }
}
