@echo off
set ERRORLEVEL=	
dnx-watch web || (echo "install dnx-watch first with :>dnu commands install Microsoft.Dnx.Watcher" & pause)
