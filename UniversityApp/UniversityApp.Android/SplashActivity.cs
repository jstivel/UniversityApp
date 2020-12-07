using Android.App;
using Android.OS;
using System.Threading.Tasks;

namespace UniversityApp.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Task.Delay(1800);
            StartActivity(typeof(MainActivity));
        }
    }
}