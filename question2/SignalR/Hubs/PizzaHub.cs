using Microsoft.AspNetCore.SignalR;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class PizzaHub : Hub
    {
        private readonly PizzaManager _pizzaManager;

        public PizzaHub(PizzaManager pizzaManager) {
            _pizzaManager = pizzaManager;
        }

        public override async Task OnConnectedAsync()
        {
            _pizzaManager.AddUser();
            await Clients.All.SendAsync("UpdateNbUsers", _pizzaManager.NbConnectedUsers);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _pizzaManager.RemoveUser();
            await Clients.All.SendAsync("UpdateNbUsers", _pizzaManager.NbConnectedUsers);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SelectChoice(PizzaChoice choice)
        {
            var group = _pizzaManager.GetGroupName(choice);
            await Groups.AddToGroupAsync(Context.ConnectionId, group);

            await Clients.Caller.SendAsync("UpdatePizzaPrice", _pizzaManager.GetPizzaPrice(choice));
            await Clients.Caller.SendAsync("UpdateNbPizzasAndMoney", _pizzaManager.GetNbPizzas(choice), _pizzaManager.GetMoney(choice));
        }

        public async Task UnselectChoice(PizzaChoice choice)
        {
            var group = _pizzaManager.GetGroupName(choice);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }

        public async Task AddMoney(PizzaChoice choice)
        {
            _pizzaManager.IncreaseMoney(choice);
            var group = _pizzaManager.GetGroupName(choice);

            await Clients.Caller.SendAsync("UpdateMoney", _pizzaManager.GetMoney(choice));
        }

        public async Task BuyPizza(PizzaChoice choice)
        {
            _pizzaManager.BuyPizza(choice);
            var group = _pizzaManager.GetGroupName(choice);

            await Clients.Caller.SendAsync("UpdateNbPizzasAndMoney", _pizzaManager.GetNbPizzas(choice), _pizzaManager.GetMoney(choice));
        }
    }
}
