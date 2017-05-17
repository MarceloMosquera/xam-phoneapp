using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content;

namespace PhoneApp
{
    [Activity(Label = "Phone App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly List<string> phoneNumbers = new List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            var TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            var CallButton = FindViewById<Button>(Resource.Id.CallButton);
            var ResultTextView = FindViewById<TextView>(Resource.Id.ResultTextView);
            var CallHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);

            CallButton.Enabled = false;
            var TranslatedNumber = string.Empty;

            TranslateButton.Click += (object sender, System.EventArgs e) =>
            {
                var Translator = new PhoneTranslator();
                TranslatedNumber = Translator.ToNumber(PhoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(TranslatedNumber))
                {
                    CallButton.Text = $"Llamar";
                    CallButton.Enabled = false;
                }
                else
                {
                    CallButton.Text = $"Llamar al: {TranslatedNumber}";
                    CallButton.Enabled = true;
                }

            };

            CallButton.Click += async (object sender, System.EventArgs e) => {
                var CallDialog = new AlertDialog.Builder(this);
                CallDialog.SetMessage($"Llamar al: {TranslatedNumber}");
                CallDialog.Show();

                phoneNumbers.Add(TranslatedNumber);
                CallHistoryButton.Enabled = true;

                SALLab06.ServiceClient ServiceClient = new SALLab06.ServiceClient();
                string StudentEmail = "marmosquera@gmail.com";
                string Pass = "brisco10";
                string myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

                SALLab06.ResultInfo result = await ServiceClient.ValidateAsync(StudentEmail, Pass, myDevice);

                //AlertDialog.Builder builder = new AlertDialog.Builder(this);
                ResultTextView.Text = $"{result.Status}\n{result.Fullname}\n{result.Token}";

                //AlertDialog alert = builder.Create();
                //alert.SetTitle("Resultado de la verificacion");
                //alert.SetIcon(Resource.Drawable.Icon);
                //alert.SetMessage($"{result.Status}\n{result.Fullname}\n{result.Token}");
                //alert.SetButton("Ok", (s, ev) => { });
                //alert.Show();


            };

 
            CallHistoryButton.Click += (object sender, System.EventArgs e) =>
            {
                var intent = new Intent(this, typeof(CallHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
            };
        }
    }
}

