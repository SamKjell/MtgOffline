using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Mod_Framework
{
    public class EventBus
    {
        public delegate EventArgs EventListener(object sender, EventArgs args);

        public static Dictionary<string, List<EventListener>> subscribers = new Dictionary<string, List<EventListener>>();

        public static void Subscribe(string eventName, EventListener listener)
        {
            if (subscribers.ContainsKey(eventName)) { subscribers[eventName].Add(listener); }
            else { subscribers.Add(eventName, new List<EventListener>() { listener }); }
        }

        public static void PostEvent(string eventName, object sender, EventArgs args)
        {
            if (!subscribers.ContainsKey(eventName)) return;
            foreach (EventListener listener in subscribers[eventName])
                listener.Invoke(sender, args);
        }
    }
}
