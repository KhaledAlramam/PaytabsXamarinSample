using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;

using Com.Payment.Paymentsdk.Integrationmodels;
using Com.Payment.Paymentsdk;
using Com.Payment.Paymentsdk.Sharedclasses.Interfaces;

using System.Collections.Generic;
using Android.Widget;
using System;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ICallbackPaymentInterface
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            PaymentSdkActivity.StartCardPayment(this, GetConfigurationDetails(), this);


        }

        public PaymentSdkConfigurationDetails GetConfigurationDetails()
        {
            IList<PaymentSdkApms> apms = new List<PaymentSdkApms>();
            apms.Add(PaymentSdkApms.StcPay);
            return new PaymentSdkConfigBuilder("id", "key", "key", 44.0, "EGP")
                .SetCartId("123")
                .SetCartDescription("yyif")
                .SetTransactionType(PaymentSdkTransactionType.Sale)
                .SetTokenise(PaymentSdkTokenise.MerchantMandatory, new PaymentSdkTokenFormat.Hex32Format())
                .SetMerchantCountryCode("EG")
                .SetAlternativePaymentMethods(apms)
                .ShowBillingInfo(true)
                .SetBillingData(new PaymentSdkBillingDetails("Dubai", "AE", "email@domain.com", "John Smith", "+971111111111", "Dubai", "Address line", "12345"))
                .SetShippingData(new PaymentSdkShippingDetails("Dubai", "AE", "email@domain.com", "John Smith", "+971111111111", "Dubai", "Address line", "12345"))
                .Build();

        }

        public void OnError(PaymentSdkError error)
        {
            Toast.MakeText(Application.Context, error.Msg, ToastLength.Long).Show();
        }

        public void OnPaymentCancel()
        {
            Toast.MakeText(Application.Context, "OnPaymentCancel", ToastLength.Long).Show();
        }

        public void OnPaymentFinish(PaymentSdkTransactionDetails paymentSdkTransactionDetails)
        {
            Console.WriteLine(paymentSdkTransactionDetails);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}