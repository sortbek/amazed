using System.Threading;
using System.Timers;

namespace Assets.Scripts.Items.Potions
{
    public class PotionRunnable
    {
        private Potion _potion;
        private readonly Character.Character _character;
        private readonly Thread _thread;
        private System.Timers.Timer _timer;

        public PotionRunnable(Character.Character character, Potion potion)
        {
            _potion = potion;
            _character = character;
            _thread = new Thread(Run);

            _timer = new System.Timers.Timer {Interval = 1000};
            _timer.Elapsed += DisplayTimeEvent;
        }

        public void Start()
        {
            _thread.Start();
        }

        private void Run()
        {
            _timer.Start();
            while (_potion.TimeLeft > 0) { }
            _potion.RemoveEffect();
            _timer.Stop();
        }

        public void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            _potion.TimeLeft -= 1;
            if (_potion.GetType() == typeof(HealthRegenerationPotion)) _character.Health += _potion.Boost;
        }
    }
}
