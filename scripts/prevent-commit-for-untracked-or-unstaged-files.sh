#!/bin/bash

#if there are any unstaged or untracked .cs files
#   then prevent commit 

num_unstaged_cs_files=$(git diff --name-only | grep -c .cs)
if [ $num_unstaged_cs_files -gt 0 ];
then 
    echo "Unstaged files found - stage or stash them."
    exit 2
fi 

num_untracked_cs_files=$(git ls-files --others --exclude-standard | grep -c .cs)
if [ $num_untracked_cs_files -gt 0 ];
then 
    echo "Untracked .cs files found - stage or stash them."
    exit 2
fi
