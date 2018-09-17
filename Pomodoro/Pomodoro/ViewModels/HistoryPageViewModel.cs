using Pomodoro.Classes;
using Pomodoro.Events;
using Pomodoro.Models;
using Pomodoro.Repositories;
using Pomodoro.Utils;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pomodoro.ViewModels
{
    public class HistoryPageViewModel : BindableBase
    {
        #region Attributes
        private ObservableRangeCollection<GroupValue> breaks;
        private ObservableRangeCollection<GroupValue> workings;

        #endregion

        #region Properties
        public ObservableRangeCollection<GroupValue> Workings
        {
            get { return workings; }
            set { SetProperty(ref workings, value); }
        }

        public ObservableRangeCollection<GroupValue> Breaks
        {
            get { return breaks; }
            set { SetProperty(ref breaks, value); }
        }
        #endregion

        #region Construct
        public HistoryPageViewModel(IEventAggregator eventAggregator)
        {
            Workings = new ObservableRangeCollection<GroupValue>();
            Breaks = new ObservableRangeCollection<GroupValue>();
            eventAggregator.GetEvent<UpdateNavBarEvent>().Publish(false);
            LoadStories();
        }
        #endregion

        #region Methodos
        private void LoadStories()
        {
            var repo = new HistoryRepository();
            List<History> data = repo.AllAsync();

            if (data != null && data.Count() > 0)
            {
                AddFirstValue(ref data);

                var summaryWorking = from h in data
                                     where h.IsWorking == true
                                     group h by h.End.Date into g
                                     select new GroupValue { Category = g.Key.Date, Value = g.Sum(x=> x.Durations) };

                var summaryBreaks = from h in data
                                    where h.IsWorking == false
                                    group h by h.End.Date into g
                                    select new GroupValue { Category = g.Key.Date, Value = g.Sum(x => x.Durations) };


                Workings.AddRange(summaryWorking);
                Breaks.AddRange(summaryBreaks);
            }
            else
            {
                var today = new List<GroupValue>() { new GroupValue() { Category = DateTime.Now.Date, Value = 0 } };
                Workings.AddRange(today);
                Breaks.AddRange(today);
            }
        }
        private void AddFirstValue(ref List<History> data)
        {
            var min = data.Min(x => x.End);
            if (min == null)
            {
                min = DateTime.Now.AddDays(-1);
            }
            else
            {
                min = min.AddDays(-1);
            }

            /*Para que la grafica siempre muestra algun valor*/
            data.Insert(0, new History() { End = min, Durations = 0, IsWorking = true });
            data.Insert(0, new History() { End = min, Durations = 0, IsWorking = false });
        }
        #endregion
    }
}
