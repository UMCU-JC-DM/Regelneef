import os
import json

def get_subdirectories(node):
    """
    Retrieve subdirectories from a node.
    Prioritize 'subdirectories' key; fallback to 'directories'.
    """
    return node.get('subdirectories', []) or node.get('directories', [])

def create_folders(base_path, node):
    """
    Recursively create folders based on the provided node structure.
    """
    name = node.get('name', 'Unnamed')
    path = os.path.join(base_path, name)
    if not os.path.exists(path):
        os.makedirs(path)
        print(f"Created directory: {path}")
    else:
        print(f"Directory already exists: {path}")

    # Recursively process subdirectories
    subdirs = get_subdirectories(node)
    for subdir in subdirs:
        create_folders(path, subdir)

if __name__ == '__main__':
    json_file = 'folder_structure.json'

    # Load the folder structure from the JSON file
    try:
        with open(json_file, 'r') as f:
            folder_structure = json.load(f)
        print(f"Loaded folder structure from {json_file}")
    except FileNotFoundError:
        print(f"Error: {json_file} not found.")
        exit(1)
    except json.JSONDecodeError as e:
        print(f"Error parsing {json_file}: {e}")
        exit(1)

    # Define the base path where the folder structure should be created
    base_path = os.getenv('BASE_PATH', '/Users/mpadrosg/Documenten/')
    print(f"Base path for folder creation: {base_path}")
    root_folder = os.getenv('ROOT_FOLDER', folder_structure.get('root', 'default_root'))
    print(f"Root folder name: {root_folder}")

    # Ensure the base path exists
    if not os.path.exists(base_path):
        try:
            os.makedirs(base_path)
            print(f"Created base path: {base_path}")
        except Exception as e:
            print(f"Error creating base path {base_path}: {e}")
            exit(1)

    # Process the root directory
    root_name = root_folder
    root_path = os.path.join(base_path, root_name)
    if not os.path.exists(root_path):
        os.makedirs(root_path)
        print(f"Created root directory: {root_path}")
    else:
        print(f"Root directory already exists: {root_path}")

    # Process subdirectories under root
    subdirs = get_subdirectories(folder_structure)
    if not subdirs:
        print("No subdirectories found in the root. Nothing more to create.")
    else:
        for dir in subdirs:
            create_folders(root_path, dir)

    print("Folder creation process completed successfully.")
