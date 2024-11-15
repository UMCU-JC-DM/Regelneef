import os
import time
import logging
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler
from process_upload import process_upload

# Setup logging
logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')
logger = logging.getLogger(__name__)

class NewUploadHandler(FileSystemEventHandler):
    def on_created(self, event):
        if event.is_directory:
            logger.info(f"New upload directory detected: {event.src_path}")
            # Wait for files to stabilize
            self.wait_for_stable_files(event.src_path)
            instructions_path = self.check_instructions_file(event.src_path)
            if instructions_path:
                process_upload(event.src_path)

    def wait_for_stable_files(self, directory_path):
        logger.info(f"Waiting for files to stabilize in: {directory_path}")
        stable = False
        while not stable:
            stable = True
            for root, _, files in os.walk(directory_path):
                for file in files:
                    file_path = os.path.join(root, file)
                    initial_size = os.path.getsize(file_path)
                    time.sleep(1)
                    new_size = os.path.getsize(file_path)
                    if initial_size != new_size:
                        logger.info(f"File {file_path} is still being copied.")
                        stable = False
                        break
                if not stable:
                    break
        logger.info(f"Files have stabilized in: {directory_path}")

    def check_instructions_file(self, directory_path):
        instructions_path = os.path.join(directory_path, 'instructions.json')
        time.sleep(10)
        if os.path.exists(instructions_path):
            logger.info(f"instructions.json found at: {instructions_path}")
            return instructions_path
        else:
            logger.error(f"instructions.json not found in {directory_path}")
            return None

if __name__ == "__main__":
    INBOX_DIR = os.getenv("INBOX_DIR", "/mnt/data/inbox")

    if not os.path.exists(INBOX_DIR):
        logger.error(f"Inbox directory '{INBOX_DIR}' does not exist!")
        exit(1)

    event_handler = NewUploadHandler()
    observer = Observer()
    observer.schedule(event_handler, path=INBOX_DIR, recursive=False)
    observer.start()
    logger.info(f"Monitoring directory: {INBOX_DIR}")

    try:
        while True:
            time.sleep(1)
    except KeyboardInterrupt:
        observer.stop()
        logger.info("Observer stopped by user")
    finally:
        observer.join()