#!/bin/bash

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

# Push changes to the master branch
git push origin main