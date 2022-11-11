using System;

public interface IObserver 
{ 
    void UpdateState(ODType[] type, string data); //Just gave it different name from Update so it doesnt have same as Unity's update
}
