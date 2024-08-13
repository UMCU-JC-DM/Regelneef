#!/bin/bash

INBOX_DIR="/inbox"  # Replace with the actual directory to watch

inotifywait -m -e create "$INBOX_DIR" --format '%w%f' | while read NEW_UPLOAD_DIR
do
    echo "New upload directory detected: $NEW_UPLOAD_DIR"
    if [ -d "$NEW_UPLOAD_DIR" ]; then
        python /app/process_upload.py
    fi
done
