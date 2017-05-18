using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp
{
    [Activity(Label = "ValidateActivity")]
    public class ValidateActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Validate);
            SetTitle(Resource.String.ValidateActivity);

            var ValidateButton = FindViewById<Button>(Resource.Id.ValidateButton);
            var EmailText = FindViewById<TextView>(Resource.Id.EmailText);
            var PasswordText = FindViewById<TextView>(Resource.Id.PasswordText);
            var ResultTextView = FindViewById<TextView>(Resource.Id.ResultTextView);


            ValidateButton.Click += async (object sender, System.EventArgs e) => {
                SALLab06.ServiceClient ServiceClient = new SALLab06.ServiceClient();
                string StudentEmail = EmailText.Text; //"marmosquera@gmail.com";
                string Pass = PasswordText.Text; //"brisco10";
                string myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

                SALLab06.ResultInfo result = await ServiceClient.ValidateAsync(StudentEmail, Pass, myDevice);

                ResultTextView.Text = $"{result.Status}\n{result.Fullname}\n{result.Token}";
            };

        }
    }
}