using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace lab4
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IServiceChat" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IServerChatCallBack))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string username);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id);

        [OperationContract(IsOneWay = true)]
        void SendMsgToParticular(string msg, int id_from, int id_to);


        [OperationContract(IsOneWay = true)]
        void getUsers();

        //[OperationContract(IsOneWay = true)]
        //void getUsers2(int id);
    }
    public interface IServerChatCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallBack(string msg);
        
        [OperationContract(IsOneWay = true)]
        void MsgToParticularCallBack(string msg);

        [OperationContract(IsOneWay = true)]
        void UsrCallBack(string[] user);

        
        //[OperationContract(IsOneWay = true)]
        //void UsrCallBack2(string user);

    }
}
