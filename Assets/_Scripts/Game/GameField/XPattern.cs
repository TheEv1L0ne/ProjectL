namespace _Scripts.Game.GameField
{
    public class XPattern : ClickPattern
    {
        public XPattern()
        {
            //Using matrix like this only so it can be easily read.
            //1 -> used for pattern shape
            Pattern = new[,]
            {
                {1, 0, 1}, 
                {0, 1, 0}, 
                {1, 0, 1}
            };
        }
    }
}
