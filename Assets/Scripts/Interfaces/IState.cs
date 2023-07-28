namespace Interfeces
{
    public interface IState
    {
        public enum State
        {
            Stand,
            Move,
            Damage,
            Die,
            Attack,
            Salvation
        }

        public void CheckState();
        public void SetState();
    }
}