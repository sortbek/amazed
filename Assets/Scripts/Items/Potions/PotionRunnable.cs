using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Assets.Scripts.Items.Potions {
    // Created by:
    // Hugo Kamps
    // S1084074
    public class PotionRunnable {
        private readonly Character.Character _player;
        private readonly Thread _thread;
        private Potion _potion;
        private Timer _timer;

        public PotionRunnable(Character.Character player, Potion potion) {
            _potion = potion;
            _player = player;
            _thread = new Thread(Run);

            _timer = new Timer {Interval = 1000};
            _timer.Elapsed += DisplayTimeEvent;
        }

        public void Start() {
            _thread.Start();
        }

        private void Run() {
            _timer.Start();
            while (_potion.TimeLeft > 0) { }
            _potion.RemoveEffect();
            _timer.Stop();
        }

        public void DisplayTimeEvent(object source, ElapsedEventArgs e) {
            _potion.TimeLeft -= 1;
            if (_potion.GetType() != typeof(HealthRegenerationPotion)) return;
            if (_player.Health < Character.Character.MAX_HEALTH - _potion.Boost) _player.Health += _potion.Boost;
            else _player.Health = Character.Character.MAX_HEALTH;
        }
    }
}