using Microsoft.AspNetCore.SignalR;
using Server.Models;

namespace Server.ServerHubs
{
    public class MainHub : Hub
    {

       

        //Подключение пользователя к хабу(получение от него данных)
        public async Task Send(ConnectionForm form)
        {
            //Добавляем юзера в список подключенных пользователей
            StaticData.UsersList.Add(new ActiveUser()
            {
                ConnectionId = Context.ConnectionId,
                Domain = form.Domain,
                LocalIP = form.LocalIP,
                Machine = form.Machine,
                RemoteIP = form.RemoteIP,
                LastActive = DateTime.UtcNow
            });
            
        }
        

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            //При отключении пользователя, удаляем его из списка активных
            var user = StaticData.UsersList.Where(x => x.ConnectionId == Context.ConnectionId).FirstOrDefault();
            if (user != null)
                StaticData.UsersList.Remove(user);
            return base.OnDisconnectedAsync(exception);
        }
    }

}
