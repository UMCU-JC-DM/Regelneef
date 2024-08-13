import json
import os
import shutil

def create_directories(base_path, directories):
    for directory in directories:
        dir_path = os.path.join(base_path, directory['name'])
        os.makedirs(dir_path, exist_ok=True)
        create_directories(dir_path, directory['subdirectories'])

def process_upload(upload_dir):
    instruction_file = os.path.join(upload_dir, 'instructions.json')
    if not os.path.exists(instruction_file):
        print(f"No instructions.json found in {upload_dir}")
        return

    with open(instruction_file, 'r') as file:
        instructions = json.load(file)

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
