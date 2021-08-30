# FingerprintAuth
 A cloud based 2FA, Webpassword and Windows Logon solution using Android and Windows.<br>
 Select the account in the overview of the android app, authenticate with your fingerprint, and the password will be entered on your PC (which will have to have the client app    running).<br><br>
 Currently supportet login types:<br>
-2FA (OTP)<br>
-Windows Logon using password<br>
-Typing in the password (Weblogin etc.)<br>
 <br>
 Please Note:
 You will need the app from github under "Releases" (install.bat will install all files from \redist, which is the credential provider for windows logons as well as the client app and a reistry key) and the Android app from: https://play.google.com/store/apps/details?id=com.heine.dennis.fingerprintauthentication <br><br>
 ToDo:<br>
-fix mockups like "AUTH"<br>
-split the servercode into classes<br>
-implement sha256 for password hashes<br>
<br>
Error fixes:<br>
-run the following command from an admin command prompt:<br> c:\windows\syswow64\regasmcpy.bat c:\windows\syswow64\CredNet.dll<br>
-run \redist\install.reg manually
