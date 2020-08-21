package com.heine.dennis.fingerprintauthentication;


import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.hardware.fingerprint.FingerprintManager;
import android.Manifest;
import android.os.CancellationSignal;
import android.support.v4.app.ActivityCompat;
import android.widget.Toast;


public class FingerprintHandler extends FingerprintManager.AuthenticationCallback {

    private CancellationSignal cancellationSignal;
    private Context context;
    private MainActivity mainActivity;
    private String masterpass;

    public FingerprintHandler(Context mContext, MainActivity act, String masterpass) {
        context = mContext;
        mainActivity=act;
        this.masterpass=masterpass;
    }

    public void startAuth(FingerprintManager manager, FingerprintManager.CryptoObject cryptoObject) {
        CancellationSignal cancellationSignal = new CancellationSignal();
       if (ActivityCompat.checkSelfPermission(context, Manifest.permission.USE_FINGERPRINT) != PackageManager.PERMISSION_GRANTED) {
            return;
        }

        manager.authenticate(cryptoObject,cancellationSignal,  0,this, null);
    }


    @Override
    public void onAuthenticationError(int errMsgId,
                                      CharSequence errString) {
        Toast.makeText(context,
                "Authentication error\n" + errString,
                Toast.LENGTH_LONG).show();
    }

    @Override
    public void onAuthenticationFailed() {
        Toast.makeText(context,
                "Authentication failed",
                Toast.LENGTH_LONG).show();
    }

    @Override
    public void onAuthenticationHelp(int helpMsgId,
                                     CharSequence helpString) {
    }

    @Override
    public void onAuthenticationSucceeded(FingerprintManager.AuthenticationResult result) {
        super.onAuthenticationSucceeded(result);
        Utilities.getURL("https://fpauth.h2x.us/api/Session/Login?username="+Globals.username+"&password="+Globals.password+"&masterpass="+Globals.seed,mainActivity);
        Intent i = new Intent(mainActivity, AccountsActivity.class);
        mainActivity.startActivity(i);
       // mainActivity.startAuth();
}
}
