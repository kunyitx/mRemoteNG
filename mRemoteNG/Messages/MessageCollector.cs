using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using mRemoteNG.Tools;

// ReSharper disable ArrangeAccessorOwnerBody

namespace mRemoteNG.Messages;

public class MessageCollector : INotifyCollectionChanged
{
    private readonly IList<IMessage> _messageList;

    public MessageCollector()
    {
        _messageList = new List<IMessage>();
    }

    public IEnumerable<IMessage> Messages => _messageList;

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public void AddMessage(MessageClass messageClass, string messageText, bool onlyLog = false)
    {
        var message = new Message(messageClass, messageText, onlyLog);
        AddMessage(message);
    }

    public void AddMessage(IMessage message)
    {
        AddMessages(new[] { message });
    }

    public void AddMessages(IEnumerable<IMessage> messages)
    {
        var newMessages = new List<IMessage>();
        foreach (var message in messages)
        {
            if (_messageList.Contains(message)) continue;
            _messageList.Add(message);
            newMessages.Add(message);
        }

        if (newMessages.Any())
            RaiseCollectionChangedEvent(NotifyCollectionChangedAction.Add, newMessages);
    }

    public void AddExceptionMessage(string message,
        Exception ex,
        MessageClass msgClass = MessageClass.ErrorMsg,
        bool logOnly = true)
    {
        AddMessage(msgClass, message + Environment.NewLine + MiscTools.GetExceptionMessageRecursive(ex),
            logOnly);
    }

    public void AddExceptionStackTrace(string message,
        Exception ex,
        MessageClass msgClass = MessageClass.ErrorMsg,
        bool logOnly = true)
    {
        AddMessage(msgClass, message + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace,
            logOnly);
    }

    public void ClearMessages()
    {
        _messageList.Clear();
    }

    private void RaiseCollectionChangedEvent(NotifyCollectionChangedAction action, IList items)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, items));
    }
}