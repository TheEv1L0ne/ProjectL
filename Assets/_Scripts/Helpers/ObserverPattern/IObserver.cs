using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public interface IObserver
{
    void UpdateState(Dictionary<string, object> data, params object[] receivers); 
    //Just gave it different name from Update so it doesnt have same as Unity's update
}