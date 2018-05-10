@echo off

git add -u
git add .
git commit -m "Update %DATE% from %COMPUTERNAME%"
git push

pause