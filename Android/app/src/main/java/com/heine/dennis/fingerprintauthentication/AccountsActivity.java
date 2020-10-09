package com.heine.dennis.fingerprintauthentication;

import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;


public class AccountsActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_accounts);
       // Toolbar toolbar = findViewById(R.id.toolbar);
       // setSupportActionBar(toolbar);

        String ret=Utilities.getURL("https://fpauth.h2x.us/api/Session/GetSeeds?session="+Globals.password,null);
        try {
            JSONObject obj = new JSONObject(ret);


            JSONArray jsonArray = obj.getJSONArray("seeds");
            JSONObject[] mseeds = new JSONObject[jsonArray.length()];

            for(int i=0;i < jsonArray.length();i++) {
                mseeds[i] = jsonArray.getJSONObject(i);
                String name=mseeds[i].getString("name");
                String seed=mseeds[i].getString("seed");
                LinearLayout layout = (LinearLayout) findViewById(R.id.accounts);
                TextView tv = new TextView(this);
                tv.setHeight(150);
                tv.setLayoutParams(new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MATCH_PARENT, 150));
                tv.setBackgroundResource(R.drawable.textview_border);
                tv.setText("\n   "+name);
                tv.setTag(seed);
                tv.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        Globals.seed=view.getTag().toString();
                        Intent i = new Intent(AccountsActivity.this, MainActivity.class);
                        startActivity(i);
                        //) ret=Utilities.getURL("https://fpauth.h2x.us/api/Session/Login?username="+Globals.username+"&password="+Globals.password+"&masterpass="+view.getTag().toString(),null);
                    }
                });
                layout.addView(tv);
            }


        } catch (JSONException e) {
            e.printStackTrace();
        }


        FloatingActionButton fab = findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Snackbar.make(view, "Add Account", Snackbar.LENGTH_LONG)
                        .setAction("Add", monClick(view)).show();
            }
        });
    }

    private View.OnClickListener monClick(View view) {

        return null;
    }
}