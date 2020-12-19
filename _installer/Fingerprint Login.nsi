############################################################################################
#      NSIS Installation Script created by NSIS Quick Setup Script Generator v1.09.18
#               Entirely Edited with NullSoft Scriptable Installation System                
#              by Vlasis K. Barkas aka Red Wine red_wine@freemail.gr Sep 2006               
############################################################################################
RequestExecutionLevel Admin
!include x64.nsh
!addplugindir "C:\Program Files (x86)\NSIS\Plugins\"
!define APP_NAME "Fingerprint Login"
!define COMP_NAME "Dennis M. Heine"
!define WEB_SITE "https://h2x.us"
!define VERSION "00.01.00.00"
!define COPYRIGHT "Dennis M. Heine  © 2020"
!define DESCRIPTION "Application"
!define LICENSE_TXT "C:\Users\Someone\Downloads\gpl-3.0.txt"
!define INSTALLER_NAME "C:\Users\Someone\Documents\GitHub\fpa_dist_out\setup.exe"
!define MAIN_APP_EXE "FPAuth Client.exe"
!define INSTALL_TYPE "SetShellVarContext all"
!define REG_ROOT "HKLM"
!define REG_APP_PATH "Software\Microsoft\Windows\CurrentVersion\App Paths\${MAIN_APP_EXE}"
!define UNINSTALL_PATH "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}"

######################################################################

VIProductVersion  "${VERSION}"
VIAddVersionKey "ProductName"  "${APP_NAME}"
VIAddVersionKey "CompanyName"  "${COMP_NAME}"
VIAddVersionKey "LegalCopyright"  "${COPYRIGHT}"
VIAddVersionKey "FileDescription"  "${DESCRIPTION}"
VIAddVersionKey "FileVersion"  "${VERSION}"

######################################################################

SetCompressor ZLIB
Name "${APP_NAME}"
Caption "${APP_NAME}"
OutFile "${INSTALLER_NAME}"
BrandingText "${APP_NAME}"
XPStyle on
InstallDirRegKey "${REG_ROOT}" "${REG_APP_PATH}" ""
InstallDir "$PROGRAMFILES\Fingerprint Login"

######################################################################

!include "MUI.nsh"

!define MUI_ABORTWARNING
!define MUI_UNABORTWARNING

!insertmacro MUI_PAGE_WELCOME

!ifdef LICENSE_TXT
!insertmacro MUI_PAGE_LICENSE "${LICENSE_TXT}"
!endif

!ifdef REG_START_MENU
!define MUI_STARTMENUPAGE_NODISABLE
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "Fingerprint Login"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${REG_ROOT}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${UNINSTALL_PATH}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${REG_START_MENU}"
!insertmacro MUI_PAGE_STARTMENU Application $SM_Folder
!endif

!insertmacro MUI_PAGE_INSTFILES

!define MUI_FINISHPAGE_RUN "$INSTDIR\${MAIN_APP_EXE}"
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM

!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_UNPAGE_FINISH

!insertmacro MUI_LANGUAGE "English"

######################################################################

Section -MainProgram
${INSTALL_TYPE}
SetOverwrite ifnewer
SetOutPath "$INSTDIR"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\-fingerprint_90738.ico"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\cpy.bat"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\CredentialManagement.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\FPAuth Client.exe"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\install.reg"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\Microsoft.Win32.TaskScheduler.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\Microsoft.Win32.TaskScheduler.xml"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\PeanutButter.TrayIcon.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\PeanutButter.TrayIcon.xml"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\WindowsInput.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\WindowsInput.xml"
SetOutPath "$INSTDIR\zh-CN"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\zh-CN\Microsoft.Win32.TaskScheduler.resources.dll"
SetOutPath "$INSTDIR\ru"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\ru\Microsoft.Win32.TaskScheduler.resources.dll"
SetOutPath "$INSTDIR\pl"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\pl\Microsoft.Win32.TaskScheduler.resources.dll"
SetOutPath "$INSTDIR\it"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\it\Microsoft.Win32.TaskScheduler.resources.dll"
SetOutPath "$INSTDIR\fr"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\fr\Microsoft.Win32.TaskScheduler.resources.dll"
SetOutPath "$INSTDIR\es"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\es\Microsoft.Win32.TaskScheduler.resources.dll"
SetOutPath "$INSTDIR\de"
File "C:\Users\Someone\Documents\GitHub\fpa_dist\de\Microsoft.Win32.TaskScheduler.resources.dll"
SectionEnd

######################################################################

Section -Additional
SetOutPath "$WINDIR"
SetOutPath "$WINDIR\syswow64"
${DisableX64FSRedirection}
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\syswow64\CredentialManagement.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\syswow64\CredNet.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\syswow64\CredNet.pdb"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\syswow64\CredNet.Sample.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\syswow64\CredNet.Sample.pdb"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\syswow64\System.Runtime.CompilerServices.Unsafe.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\syswow64\WindowsInput.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\syswow64\WindowsInput.xml"
SetOutPath "$WINDIR\system32"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\system32\CredentialManagement.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\system32\CredNet.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\system32\CredNet.pdb"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\system32\CredNet.Sample.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\system32\CredNet.Sample.pdb"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\system32\System.Runtime.CompilerServices.Unsafe.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\system32\WindowsInput.dll"
File "C:\Users\Someone\Documents\GitHub\fpa_dist_win\system32\WindowsInput.xml"
${EnableX64FSRedirection}
SetRegView 64
WriteRegStr ${REG_ROOT} "Software\Microsoft\Windows\CurrentVersion\Authentication\Credential Providers\{f264df76-2c20-4884-8f05-7b75bb455b35}" "" "Credential Provider.NET"
SectionEnd

######################################################################

Section -Icons_Reg
Exec "$INSTDIR\cpy.bat"
Exec "$INSTDIR\install.reg"

SetOutPath "$INSTDIR"
WriteUninstaller "$INSTDIR\uninstall.exe"

!ifdef REG_START_MENU
!insertmacro MUI_STARTMENU_WRITE_BEGIN Application
CreateDirectory "$SMPROGRAMS\$SM_Folder"
CreateShortCut "$SMPROGRAMS\$SM_Folder\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
CreateShortCut "$DESKTOP\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
CreateShortCut "$SMPROGRAMS\$SM_Folder\Uninstall ${APP_NAME}.lnk" "$INSTDIR\uninstall.exe"

!ifdef WEB_SITE
WriteIniStr "$INSTDIR\${APP_NAME} website.url" "InternetShortcut" "URL" "${WEB_SITE}"
CreateShortCut "$SMPROGRAMS\$SM_Folder\${APP_NAME} Website.lnk" "$INSTDIR\${APP_NAME} website.url"
!endif
!insertmacro MUI_STARTMENU_WRITE_END
!endif

!ifndef REG_START_MENU
CreateDirectory "$SMPROGRAMS\Fingerprint Login"
CreateShortCut "$SMPROGRAMS\Fingerprint Login\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
CreateShortCut "$DESKTOP\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
CreateShortCut "$SMPROGRAMS\Fingerprint Login\Uninstall ${APP_NAME}.lnk" "$INSTDIR\uninstall.exe"

!ifdef WEB_SITE
WriteIniStr "$INSTDIR\${APP_NAME} website.url" "InternetShortcut" "URL" "${WEB_SITE}"
CreateShortCut "$SMPROGRAMS\Fingerprint Login\${APP_NAME} Website.lnk" "$INSTDIR\${APP_NAME} website.url"
!endif
!endif

WriteRegStr ${REG_ROOT} "${REG_APP_PATH}" "" "$INSTDIR\${MAIN_APP_EXE}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayName" "${APP_NAME}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "UninstallString" "$INSTDIR\uninstall.exe"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayIcon" "$INSTDIR\${MAIN_APP_EXE}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayVersion" "${VERSION}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "Publisher" "${COMP_NAME}"

!ifdef WEB_SITE
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "URLInfoAbout" "${WEB_SITE}"
!endif
SectionEnd

######################################################################

Section Uninstall
${INSTALL_TYPE}
Delete "$INSTDIR\-fingerprint_90738.ico"
Delete "$INSTDIR\cpy.bat"
Delete "$INSTDIR\CredentialManagement.dll"
Delete "$INSTDIR\FPAuth Client.exe"
Delete "$INSTDIR\install.reg"
Delete "$INSTDIR\Microsoft.Win32.TaskScheduler.dll"
Delete "$INSTDIR\Microsoft.Win32.TaskScheduler.xml"
Delete "$INSTDIR\PeanutButter.TrayIcon.dll"
Delete "$INSTDIR\PeanutButter.TrayIcon.xml"
Delete "$INSTDIR\WindowsInput.dll"
Delete "$INSTDIR\WindowsInput.xml"
Delete "$INSTDIR\zh-CN\Microsoft.Win32.TaskScheduler.resources.dll"
Delete "$INSTDIR\ru\Microsoft.Win32.TaskScheduler.resources.dll"
Delete "$INSTDIR\pl\Microsoft.Win32.TaskScheduler.resources.dll"
Delete "$INSTDIR\it\Microsoft.Win32.TaskScheduler.resources.dll"
Delete "$INSTDIR\fr\Microsoft.Win32.TaskScheduler.resources.dll"
Delete "$INSTDIR\es\Microsoft.Win32.TaskScheduler.resources.dll"
Delete "$INSTDIR\de\Microsoft.Win32.TaskScheduler.resources.dll"
 
RmDir "$INSTDIR\de"
RmDir "$INSTDIR\es"
RmDir "$INSTDIR\fr"
RmDir "$INSTDIR\it"
RmDir "$INSTDIR\pl"
RmDir "$INSTDIR\ru"
RmDir "$INSTDIR\zh-CN"
 
Delete "$INSTDIR\uninstall.exe"
!ifdef WEB_SITE
Delete "$INSTDIR\${APP_NAME} website.url"
!endif

RmDir "$INSTDIR"

!ifndef NEVER_UNINSTALL
Delete "$WINDIR\syswow64\CredentialManagement.dll"
Delete "$WINDIR\syswow64\CredNet.dll"
Delete "$WINDIR\syswow64\CredNet.pdb"
Delete "$WINDIR\syswow64\CredNet.Sample.dll"
Delete "$WINDIR\syswow64\CredNet.Sample.pdb"
Delete "$WINDIR\syswow64\System.Runtime.CompilerServices.Unsafe.dll"
Delete "$WINDIR\syswow64\WindowsInput.dll"
Delete "$WINDIR\syswow64\WindowsInput.xml"
Delete "$WINDIR\system32\CredentialManagement.dll"
Delete "$WINDIR\system32\CredNet.dll"
Delete "$WINDIR\system32\CredNet.pdb"
Delete "$WINDIR\system32\CredNet.Sample.dll"
Delete "$WINDIR\system32\CredNet.Sample.pdb"
Delete "$WINDIR\system32\System.Runtime.CompilerServices.Unsafe.dll"
Delete "$WINDIR\system32\WindowsInput.dll"
Delete "$WINDIR\system32\WindowsInput.xml"
 
!endif

!ifdef REG_START_MENU
!insertmacro MUI_STARTMENU_GETFOLDER "Application" $SM_Folder
Delete "$SMPROGRAMS\$SM_Folder\${APP_NAME}.lnk"
Delete "$SMPROGRAMS\$SM_Folder\Uninstall ${APP_NAME}.lnk"
!ifdef WEB_SITE
Delete "$SMPROGRAMS\$SM_Folder\${APP_NAME} Website.lnk"
!endif
Delete "$DESKTOP\${APP_NAME}.lnk"

RmDir "$SMPROGRAMS\$SM_Folder"
!endif

!ifndef REG_START_MENU
Delete "$SMPROGRAMS\Fingerprint Login\${APP_NAME}.lnk"
Delete "$SMPROGRAMS\Fingerprint Login\Uninstall ${APP_NAME}.lnk"
!ifdef WEB_SITE
Delete "$SMPROGRAMS\Fingerprint Login\${APP_NAME} Website.lnk"
!endif
Delete "$DESKTOP\${APP_NAME}.lnk"

RmDir "$SMPROGRAMS\Fingerprint Login"
!endif

DeleteRegKey ${REG_ROOT} "${REG_APP_PATH}"
DeleteRegKey ${REG_ROOT} "${UNINSTALL_PATH}"
SectionEnd

######################################################################

