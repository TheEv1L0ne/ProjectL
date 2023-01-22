
namespace _Scripts.Game.GameField
{
    public class PlusPattern : ClickPattern
    {
        public PlusPattern()
        {
            //Using matrix like this only so it can be easily read.
            //1 -> used for pattern shape
            Pattern = new[,]
            {
                {0, 1, 0}, 
                {1, 1, 1}, 
                {0, 1, 0}
            };
        }
    }
}
