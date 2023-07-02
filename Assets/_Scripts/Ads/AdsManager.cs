public class AdsManager
{
    private static AdsManager instance;
    public static AdsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AdsManager();
            }
            return instance;
        }
    }

    protected AdsManager()
    {
        Init();
    }

    protected virtual void Init(){}
    
}