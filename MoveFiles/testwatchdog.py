from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler

class TestHandler(FileSystemEventHandler):
    def on_created(self, event):
        print(f"File or directory created: {event.src_path}")

path = "/mnt/data/inbox"
event_handler = TestHandler()
observer = Observer()
observer.schedule(event_handler, path, recursive=False)
observer.start()

try:
    while True:
        pass
except KeyboardInterrupt:
    observer.stop()
observer.join()