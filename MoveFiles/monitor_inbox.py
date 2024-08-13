import os
import time
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler
from process_upload import process_upload

class NewUploadHandler(FileSystemEventHandler):
    def on_created(self, event):
        # Check if the event is a directory (new upload folder)
        if event.is_directory:
            print(f"New upload directory detected: {event.src_path}")
            process_upload(event.src_path)

    def on_modified(self, event):
        # Sometimes files may be modified or completed after creation
        if event.is_directory:
            print(f"Directory modified: {event.src_path}")
            process_upload(event.src_path)

if __name__ == "__main__":
    INBOX_DIR = '/inbox'  # Replace with the actual inbox directory path

    # Validate the inbox directory
    if not os.path.exists(INBOX_DIR):
        print(f"Error: Inbox directory '{INBOX_DIR}' does not exist!")
        exit(1)

    # Set up event handler and observer
    event_handler = NewUploadHandler()
    observer = Observer()

    # Start observing the inbox directory
    observer.schedule(event_handler, path=INBOX_DIR, recursive=False)
    observer.start()
    print(f"Monitoring directory: {INBOX_DIR}")

    try:
        while True:
            # Sleep to keep the script running
            time.sleep(1)
    except KeyboardInterrupt:
        observer.stop()
        print("Observer stopped by user")
    observer.join()
