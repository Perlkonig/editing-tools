; =====================================================================
; == make-installer.iss
; == Part of VstoAddinInstaller
; == (https://github.com/bovender/VstoAddinInstaller)
; == (c) 2016-2017 Daniel Kraus <bovender@bovender.de>
; == Published under the Apache License 2.0
; == See http://www.apache.org/licenses
; =====================================================================
;
; =====================================================================
; == DEBUG MODE

; Uncomment the line below to enable debug mode.
; #define DEBUG


; =====================================================================
; == VERSION INFORMATION

; The script will read the version information from a text file by
; default; the text file must contain two lines:
;   1. The semantic version (e.g. 2.1.8-beta.3)
;   2. The four-number Windows file version (e.g., 2.1.8.3)
; Uncomment the line below to define VERSIONFILE, which will make the
; script read the version information from a text file.
; Note: No sanity check is performed; you must make sure the file does
; contain the two lines with the appropriate information.
; #define VERSIONFILE "..\VERSION.TXT"

; Alternatively, you can indicate the version number directly in this
; script. The version numbers below are overwritten if VERSIONFILE is
; set.

; The version number. It is suggested to use the semantic versionin
; scheme (http://semver.org), but this is not a must. This version
; information may contain text.
#define SEMANTIC_VERSION "1.2.1"

; The version in four-number format
#define FOUR_NUMBER_VERSION "1.2.1.0"

; The year(s) of publication (e.g., "2014-2017")
#define PUB_YEARS "2017"


; =====================================================================
; == STATIC INFORMATION ABOUT THE ADD-IN

; Target application. This is used internally by the script
; in order to determine the appropriate registry keys etc.,
; and must be one of 'excel' or 'word'.
#define TARGET_HOST "word"

; Specific AppID
; Use InnoSetup's Generate UID command from the Tools menu
; to generate a unique ID. Make sure to have this ID start
; with TWO curly brackets.
; *Never* change this UID after the addin has been deployed,
; lest the users of your add-in will have multiple entries
; in their software list (Add/Remove Software).
#define APP_GUID "{{B9B3447A-E6F3-4CD1-9B0B-574FCD729800}"
#define ADDIN_NAME "Editing Tools"

; The ADDIN_SHORT_NAME is used to generate the installer file
; name and may also be used as a suggestion for installation
; directory during system-wide installations. If it is not defined,
; the value of ADDIN_NAME is used instead. Do not use characters
; that are illegal in file names.
;#define ADDIN_SHORT_NAME "Short name without special characters"

#define COMPANY "Aaron Dalton"
#define DESCRIPTION "A few utilities for professional editors."
#define HOMEPAGE "https://github.com/Perlkonig/editing-tools"
#define HOMEPAGE_SUPPORT "https://github.com/Perlkonig/editing-tools/issues"
#define HOMEPAGE_UPDATES "https://github.com/Perlkonig/editing-tools/releases"

; SOURCEDIR is the directory that contains the files that
; need to be installed; e.g. 'MyProject\bin\Release\'.
; Include a trailing slash!
#define SOURCEDIR "..\EditTools\bin\Release\"

; VSTOFILE is the file that needs to be written to the registry in
; order to activate the add-in.
; This is usually a file named after the Visual Studio project.
#define VSTOFILE "EditingTools.vsto"

; OUTPUTDIR is the directory where the installer will be saved.
#define OUTPUTDIR "releases\"

#define LOGFILE "INST-LOG.TXT"


; =====================================================================
; == ADDITIONAL FILES NEEDED DURING COMPILATION

; SETUPFILESDIR is the directory that contains additional
; files needed to create the installer.
; The files below are all expected to be in this directory.
; The SETUPFILESDIR *must* ende with a backslash, unless it is empty.
#define SETUPFILESDIR "setup-files\"

; License file
; #define LICENSE_FILE "license.rtf"

; Icon that is displayed as a file icon in Windows Explorer
; #define INSTALLER_ICO "icon.ico"

; Large installer banner; the size must be 166x314 px (WxH)
; #define INSTALLER_IMAGE_LARGE "logo-large.bmp"

; Small image to display in the setup wizard; 48x48 px
; #define INSTALLER_IMAGE_SMALL "logo-small.bmp"


; =====================================================================
; == BUILD

; You can optionally build your VSTO project.
; Adjust the paths below for your specific set-up and uncomment the lines.
; #define DEVENV "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.com"
; #define CSPROJ "C:\full\path\to\your\solution.sln"
; #ifdef DEBUG
;   #expr Exec(DEVENV, CSPROJ + " /Build Release")
; #else
;   #expr Exec(DEVENV, CSPROJ + " /Build Debug")
; #endif


; =====================================================================
; == INCLUDE VSTOADDININSTALLER SCRIPTS

; If the VstoAddinInstaller files are in a different subdirectory
; than 'VstoAddinInstaller', change the path below.
#include "VstoAddinInstaller\vsto-installer.iss"

; vim: ts=2 sts=2 sw=2 et
