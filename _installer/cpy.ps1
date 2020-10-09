$from = "c:\windows\syswow64\CredNet*"
$to = "c:\windows\system32\"

Copy-Item -path $from -destination $to -force

$from = "c:\windows\syswow64\CredentialManagement.dll"
Copy-Item -path $from -destination $to -force

$from = "c:\windows\syswow64\WindowsInput.dll"
Copy-Item -path $from -destination $to -force

$from = "System.Runtime.CompilerServices.Unsafe.dll"
Copy-Item -path $from -destination $to -force


