import os
import time
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler
from process_upload import process_upload

class NewUploadHandler(FileSystemEventHandler):
    def on_created(self, event):
        if event.is_directory:
            print(f"Directory created: {event.src_path}")
        else:
            print(f"File created: {event.src_path}")

def scan_inbox_directory(directory_path):
    """
    Periodically scan the inbox directory for new files or directories
    """
    print("Scanning inbox directory for new files...")
    processed = set()
    while True:
        current_files = set(os.listdir(directory_path))
        new_files = current_files - processed

        for file_name in new_files:
            file_path = os.path.join(directory_path, file_name)
            if os.path.isdir(file_path):
                print(f"New upload detected: {file_path}")
                handler = NewUploadHandler()
                handler.on_created(type("Event", (object,), {"src_path": file_path, "is_directory": True}))
                processed.add(file_name)
            elif os.path.isfile(file_path):
                print(f"New file detected: {file_path}")
                processed.add(file_name)
        time.sleep(10)  # Adjust interval as needed

if __name__ == "__main__":
    INBOX_DIR = '/mnt/data/inbox'

    if not os.path.exists(INBOX_DIR):
        print(f"Error: Inbox directory '{INBOX_DIR}' does not exist!")
        exit(1)

    # Start scanning the inbox directory
    scan_inbox_directory(INBOX_DIR)