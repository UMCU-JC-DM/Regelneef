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
            print(event.src_path)
            self.check_instructions_file(event.src_path)
            process_upload(event.src_path)

    def wait_for_stable_files(self, directory_path):
        """
        Wait until the files in the directory have finished copying by checking if
        their sizes stabilize over time.
        """
        print(f"Waiting for files to stabilize in: {directory_path}")
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
        print(f"Files have stabilized in: {directory_path}")

    def check_instructions_file(self, directory_path):
        """
        Ensure the instructions.json file exists and is readable.
        """
        instructions_path = os.path.join(directory_path, 'instructions.json')
        print(f"file list: {os.listdir(instructions_path)}")
        if os.path.exists(instructions_path):
            print(f"instructions.json found at: {instructions_path}")
            return instructions_path
        else:
            print(f"Error: instructions.json not found in {directory_path}")
            return None

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
