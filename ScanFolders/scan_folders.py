import os
import json

def scan_directory(path):
    folder_structure = {"name": os.path.basename(path), "subdirectories": []}
    
    try:
        for entry in os.listdir(path):
            full_path = os.path.join(path, entry)
            if os.path.isdir(full_path):
                folder_structure["subdirectories"].append(scan_directory(full_path))
    except PermissionError:
        # Skip directories that cannot be accessed
        pass

    return folder_structure

if __name__ == "__main__":
    start_path = "/Users/mpadrosg/Source/ANewProject"  # Replace with the starting directory path
    folder_structure = scan_directory(start_path)

    with open("scanned_folder_structure.json", "w") as f:
        json.dump({"root": os.path.basename(start_path), "directories": folder_structure["subdirectories"]}, f, indent=4)
    
    print(f"Folder structure saved to scanned_folder_structure.json")
