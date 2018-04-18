@echo off

git add -u
git add .
git commit -m "Update for %DATE% from %COMPUTERNAME%"
git push

echo %date%
pause