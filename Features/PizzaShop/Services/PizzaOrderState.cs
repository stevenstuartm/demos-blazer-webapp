using demos.blazer.webapp.Features.PizzaShop.DTOs;

namespace demos.blazer.webapp.Features.PizzaShop.Services
{
    public class PizzaOrderState
    {
        public bool ShowingConfigureDialog { get; private set; }
        public DTOs.Pizza? ConfiguringPizza { get; private set; }
        public Order Order { get; private set; } = new DTOs.Order();

        public void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new Pizza()
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>(),
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
    }
}
