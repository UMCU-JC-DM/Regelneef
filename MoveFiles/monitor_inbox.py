import os
import time
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler
from process_upload import process_upload

class NewUploadHandler(FileSystemEventHandler):
    def on_created(self, event):
        if event.is_directory:
            print(f"New upload directory detected: {event.src_path}")
            # Wait until all files in the directory are stable
            self.wait_for_stable_files(event.src_path)
            process_upload(event.src_path)

    def wait_for_stable_files(self, directory_path):
        """
        Wait until the files in the directory have finished copying by checking if
        their sizes stabilize over time.
        """
        stable = False
        while not stable:
            stable = True
            for root, _, files in os.walk(directory_path):
                for file in files:
                    file_path = os.path.join(root, file)
                    initial_size = os.path.getsize(file_path)
                    time.sleep(1)  # Check again after 1 second
                    new_size = os.path.getsize(file_path)
                    if initial_size != new_size:
                        print(f"File {file_path} is still being copied.")
                        stable = False
                        break
                if not stable:
                    break

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
