import os
import json

print("Script started.")  # Debug print

# Define the JSON file that specifies the folder structure
json_file = 'folder_structure.json'

print(f"Loading JSON file: {json_file}")  # Debug print

# Function to create folders based on the JSON structure
def create_folders(base_path, structure):
    for folder in structure.get('directories', []):
        path = os.path.join(base_path, folder['name'])
        if not os.path.exists(path):
            os.makedirs(path)
            print(f"Created directory: {path}")
        create_folders(path, folder)

if __name__ == "__main__":
    # Load the folder structure from the JSON file
    with open(json_file) as f:
        folder_structure = json.load(f)

    print(f"Folder structure loaded: {folder_structure}")  # Debug print

    # Define the base path where the folder structure should be created
    base_path = os.getenv('BASE_PATH', '/path/to/create/folders')  # Get base path from environment variable

    print(f"Creating folders at base path: {base_path}")  # Debug print

    # Create the folders
    create_folders(base_path, folder_structure)

    print("Folders created successfully.")  # Debug print
