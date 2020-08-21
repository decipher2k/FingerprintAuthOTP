# FingerprintAuth
 A cloud based 2FA solution using Android and Windows.<br>
 Select the 2FA account in the overview of the android app, authenticate with your fingerprint, and the OTP will be entered on your PC (which will have to have the client app running).<br>
 <br>
 ToDo:<br>
-fix mockups like "AUTH"<br>
-add possibility of static password<br>
-encode seeds serverside with gpg (means the user wont be able to reset his masterpassword without loss of his seeds)<br>
-encrypt password clientside in db with key in android keystore<br>
-split the servercode into classes
