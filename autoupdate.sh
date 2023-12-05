#!/bin/bash

# Define the file name
file="daily-tracker.txt"

touch "$file"

# Add a new line of text to the file
echo "Your new line of text" >> "$file"

# Commit changes to Git
git add "$file"
git commit -m "Add new line to daily tracker"

# Push changes to the master branch
git push origin master