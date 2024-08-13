import json
import os
import shutil

def create_directories(base_path, directories):
    for directory in directories:
        dir_path = os.path.join(base_path, directory['name'])
        os.makedirs(dir_path, exist_ok=True)
        create_directories(dir_path, directory['subdirectories'])

def process_upload(upload_dir):
    # First, confirm the instructions.json file exists
    instruction_file = os.path.join(upload_dir, 'instructions.json')
    if not os.path.exists(instruction_file):
        print(f"Error: instructions.json not found in {upload_dir}")
        return

    print(f"Processing instructions from: {instruction_file}")
    
    # Open and process the instructions.json file
    try:
        with open(instruction_file, 'r') as file:
            instructions = json.load(file)
    except json.JSONDecodeError as e:
        print(f"Error reading instructions.json: {e}")
        return

    target_structure = instructions.get('target_structure', None)
    if target_structure:
        root = target_structure['root']
        directories = target_structure['directories']
        create_directories(root, directories)

    for instruction in instructions.get('moves', []):
        src = os.path.join(upload_dir, instruction['source'])
        dest = instruction['destination']

        # Ensure the destination directory exists
        os.makedirs(os.path.dirname(dest), exist_ok=True)

        # Move the file
        shutil.move(src, dest)
        print(f"Moved: {src} -> {dest}")

    print(f"Processing complete for: {upload_dir}")
