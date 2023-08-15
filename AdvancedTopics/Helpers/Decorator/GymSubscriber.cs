namespace AdvancedTopics.Helpers.Decorator
{
    public class BasicGymSubscriber : IGymSubscriber
    {
        public string Name { get; set; }
        public double SubscriptionFee { get; set; }
        public BasicGymSubscriber(string subscriberName)
        {
            this.Name = subscriberName;
            this.SubscriptionFee = 175.00;
        }
        public string Subscribe()
        {
            string subscriptionDetail = $"{this.Name} enrolled in ANT Gymnasium with monthly basic subscription fee of {this.SubscriptionFee} BDT.";
            subscriptionDetail += $"\r\n ---------------------------------------------";
            return subscriptionDetail;
        }
    }

    public class SubscriberDecorator : IGymSubscriber
    {
        public IGymSubscriber _gymSubscriber;
        public double ExtraCharge;
        public double TotalCharge;
        public SubscriberDecorator(IGymSubscriber gymSubscriber)
        {
            _gymSubscriber = gymSubscriber;
        }
        public virtual string Subscribe()
        {
            return _gymSubscriber.Subscribe();
        }
    }

    public class LockerSubscriberDecorator : SubscriberDecorator
    {
        public int LockerNo = 100;
        public LockerSubscriberDecorator(IGymSubscriber gymSubscriber): base(gymSubscriber)
        {
            this.LockerNo++;
            this.ExtraCharge = 100;
        }
        public override string Subscribe()
        {
            string subscriptionDetail = _gymSubscriber.Subscribe();
            subscriptionDetail += $"\r\n * Extra {this.ExtraCharge} BDT will be charged for availing Locker facility.";
            subscriptionDetail += $" (Your Locker No is {this.LockerNo}.)";
            return subscriptionDetail;
        }
    }

    public class SwimmingSubscriberDecorator : SubscriberDecorator
    {
        public double SwimmingHour = 1;
        public SwimmingSubscriberDecorator(IGymSubscriber gymSubscriber, double swimmingHour) : base(gymSubscriber)
        {
            this.SwimmingHour = swimmingHour;
            this.ExtraCharge = this.SwimmingHour * 300.00;
        }
        public override string Subscribe()
        {
            string subscriptionDetail = _gymSubscriber.Subscribe();
            subscriptionDetail += $"\r\n * Extra {this.ExtraCharge} BDT will be charged for Swimming Pool subscription.";
            subscriptionDetail += $" (You have a {this.SwimmingHour} hour subscription for Swimming Pool.)";
            return subscriptionDetail;
        }
    }

    public class TableTennisSubscriberDecorator : SubscriberDecorator
    {
        public double PlayingHour = 1;
        public TableTennisSubscriberDecorator(IGymSubscriber gymSubscriber, int playingHour) : base(gymSubscriber)
        {
            this.PlayingHour = playingHour;
            this.ExtraCharge = this.PlayingHour * 200.00;
        }
        public override string Subscribe()
        {
            string subscriptionDetail = _gymSubscriber.Subscribe();
            subscriptionDetail += $"\r\n * Extra {this.ExtraCharge} BDT will be charged for Table Tennis subscription.";
            subscriptionDetail += $" (You have a {this.PlayingHour} hour subscription for Table Tennis.)";
            return subscriptionDetail;
        }
    }
}
