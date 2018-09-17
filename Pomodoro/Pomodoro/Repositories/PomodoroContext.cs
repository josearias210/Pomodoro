using Microsoft.EntityFrameworkCore;
using Pomodoro.Models;
using System;
using System.IO;
using Xamarin.Forms;

namespace Pomodoro.Repositories
{
    public class PomodoroContext: DbContext
    {
        #region Constants
        private const string databaseName = "pomodoro.db";
        #endregion

        #region Properties
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<History> Stories { get; set; }
        #endregion

        #region Constructs
        public PomodoroContext()
        {
            this.Database.EnsureCreated();
        }
        #endregion

        #region Overrrite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath = "";
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
                    break;
                default:
                    throw new NotImplementedException("Platform not supported");
            }
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
        #endregion
    }
}
