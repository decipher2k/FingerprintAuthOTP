package com.heine.dennis.fingerprintauthentication;

import android.content.ContentValues;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import java.io.UnsupportedEncodingException;

public class LoginActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Globals.dbHelper= new FeedReaderDbHelper(getApplicationContext());
        SQLiteDatabase db = Globals.dbHelper.getWritableDatabase();
        Cursor  cursor = db.rawQuery("select * from "+FeedReaderContract.FeedEntryUser.TABLE_NAME,null);
        if (cursor.moveToFirst()) {
            if (!cursor.isAfterLast()) {
                String pass = cursor.getString(cursor.getColumnIndex(FeedReaderContract.FeedEntryUser.COLUMN_SEED_TITLE));
                String name = cursor.getString(cursor.getColumnIndex(FeedReaderContract.FeedEntryUser.COLUMN_CAPTION_TITLE));
                Globals.username=name;
                try {
                    Globals.password=new String((new PasswordStorageHelper(this)).getData("FPAuth"), "UTF-8");
                } catch (UnsupportedEncodingException e) {
                    e.printStackTrace();
                }
                String ret=Utilities.getURL("https://fpauth.h2x.us/api/Session/DoLogin?username="+name+"&password="+Globals.password,null);
                if(ret.contains("AUTH")) {
                    Intent i = new Intent(LoginActivity.this, AccountsActivity.class);
                    startActivity(i);
                }
            }
        }

    /*     Globals.dbHelper= new FeedReaderDbHelper(getApplicationContext());

        else
        {
            RandomString rndUser=new RandomString();
            Globals.username=RandomString.alphanum;
            rndUser=new RandomString();
            Globals.password=RandomString.alphanum;
            String ret=Utilities.getURL("https://srv.h2x.us/api/Session/Register?username="+Globals.username+"&password="+Globals.password,null);
            if(ret.contains("AUTH")) {


                ContentValues values = new ContentValues();
                values.put(FeedReaderContract.FeedEntryUser.COLUMN_SEED_TITLE, Globals.password);
                values.put(FeedReaderContract.FeedEntryUser.COLUMN_SEED_TITLE, Globals.username);

// Insert the new row, returning the primary key value of the new row
                long newRowId = db.insert(FeedReaderContract.FeedEntryUser.TABLE_NAME, null, values);

                Intent i = new Intent(LoginActivity.this, LoginActivity.class);
            }
            else
            {
                Toast toast=Toast.makeText(getApplicationContext(),"Username allready taken. Please restart.",Toast.LENGTH_SHORT);
                toast.setMargin(50,50);
                toast.show();
            }
        }

        String ret=Utilities.getURL("https://srv.h2x.us/api/Session/DoLogin?username="+Globals.username+"&password="+Globals.password,null);
        if(ret.contains("AUTH")) {
            Intent i = new Intent(LoginActivity.this, AccountsActivity.class);
            startActivity(i);
        }
        else
        {
            Toast toast=Toast.makeText(getApplicationContext(),"Login Error.",Toast.LENGTH_SHORT);
            toast.setMargin(50,50);
            toast.show();
        }
*/
        setContentView(R.layout.activity_login);
    }

    public void clickLogin(View view) {
        EditText text = (EditText)findViewById(R.id.editTextEmail);
        String username = text.getText().toString();
        text = (EditText)findViewById(R.id.editTextPassword);
        String password = text.getText().toString();
        String ret=Utilities.getURL("https://fpauth.h2x.us/api/Session/DoLogin?username="+username+"&password="+password,null);
        if(ret.contains("AUTH")) {
            Globals.username=username;
            Globals.password=password;

            SQLiteDatabase db = Globals.dbHelper.getWritableDatabase();
            db.rawQuery("delete from "+FeedReaderContract.FeedEntryUser.TABLE_NAME,null);
            Cursor  cursor = db.rawQuery("select * from "+FeedReaderContract.FeedEntryUser.TABLE_NAME,null);

                ContentValues values = new ContentValues();
              //  values.put(FeedReaderContract.FeedEntryUser.COLUMN_SEED_TITLE, Globals.password);
            try {
                (new PasswordStorageHelper(this)).setData("FPAuth",password.getBytes("UTF-8"));
            } catch (UnsupportedEncodingException e) {
                e.printStackTrace();
            }
            values.put(FeedReaderContract.FeedEntryUser.COLUMN_CAPTION_TITLE, Globals.username);

// Insert the new row, returning the primary key value of the new row
                long newRowId = db.insert(FeedReaderContract.FeedEntryUser.TABLE_NAME, null, values);

            Intent i = new Intent(LoginActivity.this, AccountsActivity.class);
            startActivity(i);
        }
        else
        {
            Toast toast=Toast.makeText(getApplicationContext(),"Login Error.",Toast.LENGTH_SHORT);
            toast.setMargin(50,50);
            toast.show();
        }
    }

    public void clickRegister(View view) {
        EditText text = (EditText)findViewById(R.id.textRegisterEmail);
        String username = text.getText().toString();
        text = (EditText)findViewById(R.id.textRegisterPassword);
        String password = text.getText().toString();

        if(username=="" || password=="")
        {
            Toast toast=Toast.makeText(getApplicationContext(),"Please fill in all data.",Toast.LENGTH_SHORT);
            toast.setMargin(50,50);
            toast.show();
        }
        String ret=Utilities.getURL("https://fpauth.h2x.us/api/Session/Register?username="+username+"&password="+password,null);
        if(ret.contains("AUTH")) {
            SQLiteDatabase db = Globals.dbHelper.getWritableDatabase();
                Cursor  cursor = db.rawQuery("select * from "+FeedReaderContract.FeedEntryAccounts.TABLE_NAME,null);
                if (!cursor.moveToFirst()) {
                    ContentValues values = new ContentValues();
                    values.put(FeedReaderContract.FeedEntryUser.COLUMN_SEED_TITLE, Globals.password);
                    values.put(FeedReaderContract.FeedEntryUser.COLUMN_SEED_TITLE, Globals.username);

// Insert the new row, returning the primary key value of the new row
                    long newRowId = db.insert(FeedReaderContract.FeedEntryUser.TABLE_NAME, null, values);
                setContentView(R.layout.activity_login);
            }
            else
            {
                Toast toast=Toast.makeText(getApplicationContext(),"There is allready a user existing. Please restart.",Toast.LENGTH_SHORT);
                toast.setMargin(50,50);
                toast.show();
            }
        }
        else
        {
            Toast toast=Toast.makeText(getApplicationContext(),"Username allready taken.",Toast.LENGTH_SHORT);
            toast.setMargin(50,50);
            toast.show();
        }
    }

    public void viewRegisterClicked(View view) {
        setContentView(R.layout.activity_register);
    }

    public void viewForgotPAssword(View view) {
    }

    public void clickToLogin(View view) {
        setContentView(R.layout.activity_login);
    }
}
