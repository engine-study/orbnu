# Open a new Windows Terminal window with Git Bash
wt -d . --tabColor "#0FB9B1" new-tab -p "Git Bash" -d . ;

# Wait for the new tab to open
Start-Sleep -Seconds 2

# Navigate to the directory and run 'pnpm run dev:node'
echo 'cd $PSScriptRoot' | clip
wt.exe -p "Git Bash" ; echo "Right-click to paste directory path and then press Enter"
echo 'pnpm run dev:node' | clip
wt.exe -p "Git Bash" ; echo "Right-click to paste command and then press Enter"

# Wait for a second
Start-Sleep -Seconds 1

# Open another tab with Git Bash, navigate to the directory and run 'pnpm run dev'
wt -d . --tabColor "#0FB9B1" new-tab -p "Git Bash" -d . ;
echo 'cd $PSScriptRoot | clip
wt.exe -p "Git Bash" ; echo "Right-click to paste directory path and then press Enter"
echo 'pnpm run dev' | clip
wt.exe -p "Git Bash" ; echo "Right-click to paste command and then press Enter"
