copy /y c:\windows\syswow64\CredNetd.dll c:\windows\system32\CredNetd.dll
copy /y c:\windows\syswow64\CredNetd.Sample.dll c:\windows\system32\CredNetd.Sample.dll
copy /y c:\windows\syswow64\CredentialManagement.dll c:\windows\system32\CredentialManagement.dll
copy /y c:\windows\syswow64\WindowsInput.dll c:\windows\system32\WindowsInput.dll
copy /y c:\windows\syswow64\System.Runtime.CompilerServices.Unsafe.dll c:\windows\system32\System.Runtime.CompilerServices.Unsafe.dll
c:\windows\Microsoft.NET\Framework64\v4.0.30319\regasm.exe /tlb c:\windows\system32\CredNet.dll
c:\windows\Microsoft.NET\Framework64\v4.0.30319\regasm.exe /tlb c:\windows\syswow64\CredNet.dll
c:\windows\Microsoft.NET\Framework64\v4.0.30319\regasm.exe /tlb c:\windows\system32\CredNet.Sample.dll
c:\windows\Microsoft.NET\Framework64\v4.0.30319\regasm.exe /tlb c:\windows\syswow64\CredNet.Sample.dll
reg import c:\Wndows\SysWOW64\install.reg
