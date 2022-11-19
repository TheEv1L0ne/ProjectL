using System;

public interface IObserver
{
    void UpdateState(string data,
        params object[] receivers); //Just gave it different name from Update so it doesnt have same as Unity's update
}