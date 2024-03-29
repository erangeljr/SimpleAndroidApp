﻿
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views.InputMethods;
using System.Collections.Generic;

namespace SimpleAndroidApp
{
	[Activity(Label = "SimpleAndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);	

			//Get controls for UI
			Button   translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
			EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
			Button   callButton      = FindViewById<Button>(Resource.Id.CallButton);
			Button   callHistoryButton      = FindViewById<Button>(Resource.Id.CallHistoryButton);

			List<string> _phoneNumbers = new List<string>();

			// Disable the "Call" button
			callButton.Enabled = false;

			// Add code to translate number
			string translatedNumber = string.Empty;

			translateButton.Click += delegate
				{
					var imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
					imm.HideSoftInputFromWindow(phoneNumberText.WindowToken, 0);

					translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);

					if (string.IsNullOrWhiteSpace(translatedNumber))
					{
						callButton.Text    = "Call";
						callButton.Enabled = false;
					}
					else
					{
						callButton.Text    = "Call " + translatedNumber;
						callButton.Enabled = true;
					}
				};

			// Add code to make the phone call
			callButton.Click += delegate
				{
					// On "Call" button click, try to dial phone number.
					var callDialog = new AlertDialog.Builder(this);

				

					callDialog.SetMessage("Call " + translatedNumber + "?");

					callDialog.SetNeutralButton("Call", 
						delegate
						{
							_phoneNumbers.Add(translatedNumber);
							callHistoryButton.Enabled = true;

							// Create intent to dial phone
							var callIntent = new Intent(Intent.ActionCall);
							callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
							StartActivity(callIntent);
						});

					callDialog.SetNegativeButton("Cancel", 
						delegate
						{
							// nothing to do
						});

					// Show the alert dialog to the user and wait for response.
					callDialog.Show();
				};

			callHistoryButton.Click += delegate
			{
					var intent = new Intent(this, typeof(CallHistoryActivity));
					intent.PutStringArrayListExtra("phone_numbers", _phoneNumbers);
					StartActivity(intent);
			};

		}
	}
}


