using System;
using Newtonsoft.Json.Linq;

public interface IObserver
{
    void UpdateState(JObject data, params object[] receivers); 
    //Just gave it different name from Update so it doesnt have same as Unity's update
}