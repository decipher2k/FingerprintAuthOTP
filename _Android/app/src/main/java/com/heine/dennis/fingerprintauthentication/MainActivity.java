package com.heine.dennis.fingerprintauthentication;

import android.Manifest;
import android.app.Activity;
import android.app.KeyguardManager;
import android.content.Context;
import android.content.pm.PackageManager;
import android.hardware.fingerprint.FingerprintManager;
import android.os.Build;
import android.os.Bundle;
import android.security.keystore.KeyGenParameterSpec;
import android.security.keystore.KeyPermanentlyInvalidatedException;
import android.security.keystore.KeyProperties;
import android.support.v4.app.ActivityCompat;
import android.widget.TextView;

import com.eatthepath.otp.TimeBasedOneTimePasswordGenerator;

import java.io.IOException;
import java.security.InvalidAlgorithmParameterException;
import java.security.InvalidKeyException;
import java.security.Key;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.NoSuchProviderException;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertificateException;
import java.time.Instant;
import java.util.Base64;

import javax.crypto.Cipher;
import javax.crypto.KeyGenerator;
import javax.crypto.NoSuchPaddingException;
import javax.crypto.SecretKey;

public class MainActivity extends Activity {

    private static final String KEY_NAME = "yourKey";
    private  static Cipher cipher;
    private static  KeyStore keyStore;
    private static  KeyGenerator keyGenerator;
    private  TextView textView;
    private static  FingerprintManager.CryptoObject cryptoObject;
    private static  FingerprintManager fingerprintManager;
    private static  KeyguardManager keyguardManager;
    private   Context m_context;

    @Override
    public void onWindowFocusChanged(boolean hasFocus) {
        super.onWindowFocusChanged(hasFocus);
        if(hasFocus)
            startAuth();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        m_context=this;

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.JELLY_BEAN) {
            try
            {
                keyguardManager =
                        (KeyguardManager) getSystemService(KEYGUARD_SERVICE);
                fingerprintManager =
                        (FingerprintManager) getSystemService(FINGERPRINT_SERVICE);



                if (!fingerprintManager.isHardwareDetected()) {



                }


                if (ActivityCompat.checkSelfPermission(this, Manifest.permission.USE_FINGERPRINT) != PackageManager.PERMISSION_GRANTED) {


                }

                if (!fingerprintManager.hasEnrolledFingerprints()) {


                }

                if (!keyguardManager.isKeyguardSecure()) {

                } else {


                }
            }catch(Exception ex){}
        }
        startAuth();
    }

   public  void startAuth()
   {

       generateKey();
       if (initCipher()) {
           cryptoObject = new FingerprintManager.CryptoObject(cipher);
           FingerprintHandler helper = new FingerprintHandler(m_context,this,Utilities.currentMasterpass);
           helper.startAuth(fingerprintManager, cryptoObject);
       }
   }

    private String genOTP() {
        try {
            final TimeBasedOneTimePasswordGenerator totp = new TimeBasedOneTimePasswordGenerator();
            final Key key;
            {
                final KeyGenerator keyGenerator = KeyGenerator.getInstance(totp.getAlgorithm());

                // SHA-1 and SHA-256 prefer 64-byte (512-bit) keys; SHA512 prefers 128-byte (1024-bit) keys
                keyGenerator.init(512);

                key = new SecretKey() {
                    @Override
                    public String getAlgorithm() {
                        return null;
                    }

                    @Override
                    public String getFormat() {
                        return null;
                    }

                    @Override
                    public byte[] getEncoded() {
                        return Base64.getDecoder().decode("JZDE4RKQJVJUSMSGK5MVSWSEIZJEQUKZKFLVOQSOLE2VQRCHK5DA");
                    }
                };
            }


            try {
                final Instant now = Instant.now();
                String s=String.valueOf(totp.generateOneTimePassword(key,now));
                System.out.println(s);
                return String.valueOf(totp.generateOneTimePassword(key,now));
            } catch (InvalidKeyException e) {
                return "";
            }


        } catch (NoSuchAlgorithmException e) {
            return "";
        }

    }

    private  void generateKey() {
        try {

            keyStore = KeyStore.getInstance("AndroidKeyStore");


            keyGenerator = KeyGenerator.getInstance(KeyProperties.KEY_ALGORITHM_AES, "AndroidKeyStore");

            keyStore.load(null);
            keyGenerator.init(new
                    KeyGenParameterSpec.Builder(KEY_NAME,
                    KeyProperties.PURPOSE_ENCRYPT |
                            KeyProperties.PURPOSE_DECRYPT)
                    .setBlockModes(KeyProperties.BLOCK_MODE_CBC)
                    .setUserAuthenticationRequired(true)
                    .setEncryptionPaddings(
                            KeyProperties.ENCRYPTION_PADDING_PKCS7)
                    .build());

            keyGenerator.generateKey();

        } catch (KeyStoreException
                | NoSuchAlgorithmException
                | NoSuchProviderException
                | InvalidAlgorithmParameterException
                | CertificateException
                | IOException exc) {
            exc.printStackTrace();

        }


    }



    public  boolean initCipher() {
        try {
            cipher = Cipher.getInstance(
                    KeyProperties.KEY_ALGORITHM_AES + "/"
                            + KeyProperties.BLOCK_MODE_CBC + "/"
                            + KeyProperties.ENCRYPTION_PADDING_PKCS7);
        } catch (NoSuchAlgorithmException |
                NoSuchPaddingException e) {
            throw new RuntimeException("Failed to get Cipher", e);
        }

        try {
            keyStore.load(null);
            SecretKey key1 = (SecretKey) keyStore.getKey(KEY_NAME,
                    null);
            cipher.init(Cipher.ENCRYPT_MODE, key1);
            return true;
        } catch (KeyPermanentlyInvalidatedException e) {
            return false;
        } catch (KeyStoreException | CertificateException
                | UnrecoverableKeyException | IOException
                | NoSuchAlgorithmException | InvalidKeyException e) {
            throw new RuntimeException("Failed to init Cipher", e);
        }
    }



    private class FingerprintException extends Exception {

        public FingerprintException(Exception e) {
            super(e);
        }
    }
}
