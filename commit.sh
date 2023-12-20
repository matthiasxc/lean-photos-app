#!/bin/sh
cd C:/Users/matth/source/repos/lean-photos-app
# Define the file name
file="daily-tracker.txt"

touch "$file"

# Add a new line of text to the file
echo "tracking daily info" >> "$file"

# pull latest changes
git pull 

# Commit changes to Git
git add .
git commit -m "Daily tracking"
git push origin main