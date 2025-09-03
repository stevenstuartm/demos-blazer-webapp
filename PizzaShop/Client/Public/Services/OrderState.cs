using demos.blazer.webapp.PizzaShop.Client.Public.Models;

namespace demos.blazer.webapp.PizzaShop.Client.Public.Services
{
    public class OrderState
    {
        public bool ShowingConfigureDialog { get; private set; }
        public OrderPizza? ConfiguringPizza { get; private set; }
        public Order Order { get; private set; } = new Order();

        public void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new OrderPizza()
            {
                Special = special,
                Size = OrderPizza.DefaultSize,
                ToppingIds = new List<int>()
            };

            ShowingConfigureDialog = true;
        }

        public void CancelConfigurePizzaDialog()
        {
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;
        }

        public void ConfirmConfigurePizzaDialog()
        {
            if (ConfiguringPizza == null) return;

            Order.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;

            ShowingConfigureDialog = false;
        }

        public void RemoveConfiguredPizza(OrderPizza pizza)
        {
            Order.Pizzas.Remove(pizza);
        }

        public void ResetOrder()
        {
            Order = new Order();
        }
    }
}
