@echo off

REM Open Git Bash tab in Windows Terminal and go to script directory
start "" "wt.exe" -w 0 new-tab -p "Git Bash" -d "%~dp0" -e "pnpm run dev:node" && timeout /t 1

REM Open another Git Bash tab in Windows Terminal and go to script directory
start "" "wt.exe" -w 0 new-tab -p "Git Bash" -d "%~dp0" -e "pnpm run dev"

