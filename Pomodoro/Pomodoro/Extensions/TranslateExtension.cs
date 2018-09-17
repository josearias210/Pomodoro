using Pomodoro.Interfaces;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pomodoro.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        #region Properties
        readonly CultureInfo ci;
        public string Text { get; set; }
        static readonly Lazy<ResourceManager> ResMgr =    new Lazy<ResourceManager>(() => new ResourceManager(ResourceId,typeof(TranslateExtension).GetTypeInfo().Assembly));
        #endregion

        #region Constants
        const string ResourceId = "Pomodoro.Resources.Resource";
        #endregion

        #region Constructs
        public TranslateExtension()
        {
            ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }
        #endregion

        #region Methods
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return "";
            }

            var translation = ResMgr.Value.GetString(Text, ci);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    String.Format(
                        "Key '{0}' was not found in resources '{1}' for culture '{2}'.",
                        Text, ResourceId, ci.Name), "Text");
#else
                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
#endif
            }

            return translation;
        }
        #endregion

    }
}