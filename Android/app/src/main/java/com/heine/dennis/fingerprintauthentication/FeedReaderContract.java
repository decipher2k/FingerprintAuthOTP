package com.heine.dennis.fingerprintauthentication;

import android.provider.BaseColumns;

public class FeedReaderContract {
    private FeedReaderContract() {}

    /* Inner class that defines the table contents */
    public static class FeedEntryAccounts implements BaseColumns {
        public static final String TABLE_NAME = "accounts";
        public static final String COLUMN_SEED_TITLE = "Seed";
        public static final String COLUMN_CAPTION_TITLE = "Caption";
    }

    public static class FeedEntryUser implements BaseColumns {
        public static final String TABLE_NAME = "user";
        public static final String COLUMN_SEED_TITLE = "username";
        public static final String COLUMN_CAPTION_TITLE = "password";
    }
}
