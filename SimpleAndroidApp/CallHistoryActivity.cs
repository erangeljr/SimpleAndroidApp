﻿
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

namespace SimpleAndroidApp
{
	[Activity(Label = "@string/callHistory")]		
	public class CallHistoryActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Create your application here
			SetContentView(Resource.Layout.CallHistory);

			var phoneNumbers = Intent.GetStringArrayListExtra("phone_numbers") ?? new string[0];
			var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, phoneNumbers);
			var list = FindViewById<ListView>(Resource.Id.PhoneNumberList);
			list.Adapter = adapter;
		}
	}


}

