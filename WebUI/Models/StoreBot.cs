using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Telegram.Bot;

namespace WebUI.Models
{
    public class StoreBot
    {
        public delegate void MessageDelegate(string text);
        public delegate void RegisterDelegate(string key, long userId, long chatId);
        public event MessageDelegate OnMessage;
        public event RegisterDelegate RegisterAcc;
        private TelegramBotClient client = null;
        public StoreBot()
        {
            client = new TelegramBotClient(""); // enter your telegram bot key
            client.OnMessage += Client_OnMessage;
        }
        public void Start()
        {
            client.StartReceiving();
        }
        public void Stop()
        {
            client.StopReceiving();
        }
        private void Client_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message == null)
                return;
            OnMessage?.Invoke(e.Message.Text);
            Match match = Regex.Match(e.Message.Text, @"^/register (?<key>\w{32})$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string key = match.Groups["key"].Value;
                RegisterAcc?.Invoke(key, e.Message.From.Id, e.Message.Chat.Id);
            }
        }
        public bool SendMessage(string text, long chatId)
        {
            try
            {
                client.SendTextMessageAsync(chatId, text);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}