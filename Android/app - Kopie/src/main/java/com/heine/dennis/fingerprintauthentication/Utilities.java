package com.heine.dennis.fingerprintauthentication;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.StrictMode;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;

public class Utilities {
    public static String currentMasterpass="";
    public static String getURL(String surl, MainActivity act)
    {
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);

        System.setProperty("java.net.preferIPv4Addresses", "true");
        System.setProperty("java.net.preferIPv6Addresses", "false");
        System.setProperty("validated.ipv6", "false");
        String fullString = "";
        try {

            URL url = new URL(surl);
            BufferedReader reader = new BufferedReader(new InputStreamReader(url.openStream()));
            String line;
            while ((line = reader.readLine()) != null) {
                fullString += line;
            }
            reader.close();
        }catch (Exception ex){

            showDialog("Verbindungsfehler.",act);
        }

        return fullString;
    }
    public static void showDialog(String data,MainActivity parent)
    {
        if(parent!=null) {
            AlertDialog.Builder builder1 = new AlertDialog.Builder(parent);
            builder1.setMessage(data);
            builder1.setCancelable(true);

            builder1.setPositiveButton(
                    "OK",
                    new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog, int id) {
                            dialog.cancel();
                        }
                    });

            AlertDialog alert11 = builder1.create();
            alert11.show();
        }

    }
}
