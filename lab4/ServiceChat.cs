using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace lab4
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 0;
        public int Connect(string username)
        {
            ServerUser user = new ServerUser()
            {
                Id = nextId,
                Name = username,
                operationContext = OperationContext.Current
            };
            nextId++;
            SendMsg(user.Name + " connected!", 0);
            users.Add(user);
            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                users.Remove(user);
                SendMsg(user.Name + " disconnected(", 0);
            }
        }

        public void getUsers()
        {
            string[] answer = new string[users.Count];
            int i = 0;
            while (i < users.Count)
            {
                answer[i] = users[i].Name;
                i++;
            }
            foreach (var item in users)
            {
                item.operationContext.GetCallbackChannel<IServerChatCallBack>().UsrCallBack(answer);
            }
        }

        //public void getUsers2(int id)
        //{
        //    string answer = "";
        //    var user = users.FirstOrDefault(x => x.Id == id);
        //    if (user != null && user.Id == id)
        //    {
        //        answer += user.Name;
        //    }

        //    foreach (var item in users)
        //    {
        //        item.operationContext.GetCallbackChannel<IServerChatCallBack>().UsrCallBack2(answer);
        //    }
        //}

        public void SendMsgToParticular(string msg, int id_from, int id_to)
        {
            string answer = DateTime.Now.ToShortTimeString();

            var user_from = users.FirstOrDefault(x => x.Id == id_from);
            var user_to = users.FirstOrDefault(x => x.Id == id_to);
           
                if (user_from != null && user_to != null)
                {
                    answer += ": " + user_from.Name + " ";
                }
                answer += msg;

                user_to.operationContext.GetCallbackChannel<IServerChatCallBack>().MsgToParticularCallBack(answer);
            
        }

        public void SendMsg(string msg, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    answer += ": " + user.Name;
                }

                answer += " " + msg;

                item.operationContext.GetCallbackChannel<IServerChatCallBack>().MsgCallBack(answer);
            }
        }
    }
}
