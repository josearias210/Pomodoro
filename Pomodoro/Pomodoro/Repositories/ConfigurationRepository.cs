using Pomodoro.Models;
using System.Linq;

namespace Pomodoro.Repositories
{
    public class ConfigurationRepository
    {
        #region Methods
        public Configuration LoadAsync()
        {
            Configuration config = null;
            using (var data = new PomodoroContext())
            {
                config = data.Configurations.FirstOrDefault();
                if (config == null)
                {
                    config = LoadConfigDefault();
                }
            }

            return config;
        }

        public async void SaveAsync(Configuration config)
        {
            DeleteAsync();
            using (var data = new PomodoroContext())
            {
                data.Configurations.Add(config);
                await data.SaveChangesAsync();
            }
        }

        private async void DeleteAsync()
        {
            using (var data = new PomodoroContext())
            {
                var configs = data.Configurations.ToList();
                data.Configurations.RemoveRange(configs);
                await data.SaveChangesAsync();
            }
        }

        private Configuration LoadConfigDefault()
        {
            var config = new Configuration()
            {
                LongBreak = 20,
                ShortBreak = 5,
                Pomorodos = 4,
                Working = 25
            };
            SaveAsync(config);
            return config;
        }
        #endregion
    }
}
