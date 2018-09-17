using Microsoft.AppCenter.Crashes;
using Pomodoro.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pomodoro.Repositories
{
    #region Methods
    public class HistoryRepository
    {
        public List<History> AllAsync()
        {
            var stories = new List<History>();
            try
            {
                using (var data = new PomodoroContext())
                {
                    stories = data.Stories.ToList();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return stories;
        }

        public async void SaveAsync(History entity)
        {
            using (var data = new PomodoroContext())
            {
                data.Stories.Add(entity);
                await data.SaveChangesAsync();
            }
        }

        public async void DeleteStores()
        {
            using (var data = new PomodoroContext())
            {
                var entities = data.Stories.ToList();
                data.Stories.RemoveRange(entities);
                await data.SaveChangesAsync();
            }
        }
        #endregion
    }
}
