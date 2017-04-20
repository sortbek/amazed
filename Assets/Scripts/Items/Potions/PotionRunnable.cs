using System.Threading;
using System.Timers;

namespace Assets.Scripts.Items.Potions
{
    public class PotionRunnable
    {
        private Potion _potion;
        private readonly Character.Character _player;
        private readonly Thread _thread;
        private System.Timers.Timer _timer;

        public PotionRunnable(Character.Character player, Potion potion)
        {
            _potion = potion;
            _player = player;
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
            if (_potion.GetType() != typeof(HealthRegenerationPotion)) return;
            if(_player.Health < 100) _player.Health += _potion.Boost;
            //TODO fix niet hoger dan 100. Om de een of andere reden werkt de if hierboven niet
        }
    }
}
