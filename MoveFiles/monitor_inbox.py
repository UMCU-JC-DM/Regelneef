import os
import time
from process_upload import process_upload

def scan_inbox_directory(directory_path):
    """
    Periodically scan the inbox directory for new files or directories
    and process them using the process_upload function.
    """
    print("Scanning inbox directory for new files...")
    processed = set()  # Tracks processed directories

    while True:
        current_files = set(os.listdir(directory_path))
        new_entries = current_files - processed  # Detect new directories or files

        for entry in new_entries:
            entry_path = os.path.join(directory_path, entry)
            
            # Process new directories (assumes uploads are directories)
            if os.path.isdir(entry_path):
                print(f"New upload detected: {entry_path}")
                
                try:
                    # Call the process_upload function to handle the new directory
                    process_upload(entry_path)
                except Exception as e:
                    print(f"Error processing upload {entry_path}: {e}")
                
                # Mark the directory as processed
                processed.add(entry)
            else:
                print(f"Ignored file (not a directory): {entry_path}")
        
        time.sleep(10)  # Adjust the interval as needed

if __name__ == "__main__":
    INBOX_DIR = '/mnt/data/inbox'

    if not os.path.exists(INBOX_DIR):
        print(f"Error: Inbox directory '{INBOX_DIR}' does not exist!")
        exit(1)

    # Start scanning the inbox directory
    scan_inbox_directory(INBOX_DIR)