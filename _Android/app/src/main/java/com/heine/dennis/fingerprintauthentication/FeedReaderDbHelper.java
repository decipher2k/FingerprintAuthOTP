package com.heine.dennis.fingerprintauthentication;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class FeedReaderDbHelper extends SQLiteOpenHelper {
    // If you change the database schema, you must increment the database version.
    public static final int DATABASE_VERSION = 1;
    public static final String DATABASE_NAME = "FPAuth.db";

    private static final String SQL_CREATE_ENTRIES1 =
            "CREATE TABLE " + "accounts" + " (" +
                   FeedReaderContract.FeedEntryAccounts._ID + " INTEGER PRIMARY KEY," +
                   FeedReaderContract.FeedEntryAccounts.COLUMN_CAPTION_TITLE + " TEXT," +
                    FeedReaderContract.FeedEntryAccounts.COLUMN_SEED_TITLE + " TEXT)";

    private static final String SQL_CREATE_ENTRIES2 =
            "CREATE TABLE " + "user" + " (" +
                    FeedReaderContract.FeedEntryUser._ID + " INTEGER PRIMARY KEY," +
                    FeedReaderContract.FeedEntryUser.COLUMN_SEED_TITLE + " TEXT," +
                    FeedReaderContract.FeedEntryUser.COLUMN_CAPTION_TITLE + " TEXT)";

    private static final String SQL_DELETE_ENTRIES1 =
            "DROP TABLE IF EXISTS " + FeedReaderContract.FeedEntryUser.TABLE_NAME;

    private static final String SQL_DELETE_ENTRIES2 =
            "DROP TABLE IF EXISTS " + FeedReaderContract.FeedEntryAccounts.TABLE_NAME;

    public FeedReaderDbHelper(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
    }
    public void onCreate(SQLiteDatabase db) {
        db.execSQL(SQL_CREATE_ENTRIES1);
        db.execSQL(SQL_CREATE_ENTRIES2);
    }
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        // This database is only a cache for online data, so its upgrade policy is
        // to simply to discard the data and start over
        db.execSQL(SQL_DELETE_ENTRIES1);
        db.execSQL(SQL_DELETE_ENTRIES2);
        onCreate(db);
    }
    public void onDowngrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        onUpgrade(db, oldVersion, newVersion);
    }
}
