#!/usr/bin/env bash

find . -type l -print0 | while IFS= read -r -d $'\0' link_path; do
  target=$(readlink "$link_path")
  link_name=$(basename "$link_path")
  link_dir=$(dirname "$link_path")

  echo "  if [ ! -e \"$link_dir/$link_name\" ]; then"
  echo "    ln -s \"$target\" \"$link_dir/$link_name\""
  echo "    echo \"Created symbolic link: $link_dir/$link_name -> $target\""
  echo "  fi"
done