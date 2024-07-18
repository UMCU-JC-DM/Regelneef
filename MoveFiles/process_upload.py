import json
import shutil
import os

def process_upload(upload_dir):
    instruction_file = os.path.join(upload_dir, 'instructions.json')
    if not os.path.exists(instruction_file):
        print(f"No instructions.json found in {upload_dir}")
        return
    
    with open(instruction_file, 'r') as file:
        instructions = json.load(file)
    
    for instruction in instructions.get('moves', []):
        src = os.path.join(upload_dir, instruction['source'])
        dest = instruction['destination']
        
        # Ensure destination directory exists
        os.makedirs(os.path.dirname(dest), exist_ok=True)
        
        # Move the file
        shutil.move(src, dest)
        print(f"Moved: {src} -> {dest}")

if __name__ == "__main__":
    INBOX_DIR = '/path/to/inbox'  # Update with the actual inbox directory
    
    for upload_dir in os.listdir(INBOX_DIR):
        full_path = os.path.join(INBOX_DIR, upload_dir)
        if os.path.isdir(full_path):
            process_upload(full_path)
