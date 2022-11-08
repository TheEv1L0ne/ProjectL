using System;

public interface IObserver 
{ 
    void UpdateState(ODType[] type, Object[] data); //Just gave it different name from Update so it doesnt have same as Unity's update
}
