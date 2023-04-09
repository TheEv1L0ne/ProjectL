namespace _Scripts.Game.GameField
{
    public class OPattern : ClickPattern
    {
        public OPattern()
        {
            //Using matrix like this only so it can be easily read.
            //1 -> used for pattern shape
            Pattern = new[,]
            {
                {1, 1, 1}, 
                {1, 0, 1}, 
                {1, 1, 1}
            };
        }
    }
}
