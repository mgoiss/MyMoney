using Android.App;
using Android.Content;
using Android.Views.InputMethods;
using MyMoney.Droid.Helper;
using MyMoney.Servicos;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidKeyboardHelper))]
namespace MyMoney.Droid.Helper
{
    //Essa classe é responsável por fechar o teclaho do app
    class DroidKeyboardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            var context = Forms.Context;
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && context is Activity)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                activity.Window.DecorView.ClearFocus();
            }
        }
    }
}