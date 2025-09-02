using demos.blazer.webapp.Features.PizzaShop.Client.Models;

namespace demos.blazer.webapp.Features.PizzaShop.Client.Services
{
    public class OrderState
    {
        public bool ShowingConfigureDialog { get; private set; }
        public Pizza? ConfiguringPizza { get; private set; }
        public Order Order { get; private set; } = new Order();

        public void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new Pizza()
            {
                Special = special,
                Size = Pizza.DefaultSize,
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

        public void RemoveConfiguredPizza(Pizza pizza)
        {
            Order.Pizzas.Remove(pizza);
        }

        public void ResetOrder()
        {
            Order = new Order();
        }
    }
}
