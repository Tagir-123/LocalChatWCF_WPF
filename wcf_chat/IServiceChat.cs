using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IServiceChat" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChat
    {
        [OperationContract]
        //метод для подключения к сервису
        int Connect(string name);

        [OperationContract]
        //метод для сообщения сервису, что больше не нужно присылать ему сообщения
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        //метод для отправки сообщений
        void SendMsg(string msg, int id);

    }

    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        //метод для обратного вызова, сообщения от сервера
        void MsgCallback(string msg);
    }
}
