import os
import json
from smb.SMBConnection import SMBConnection
import getpass

def scan_directory_smb(conn, share_name, path):
    folder_structure = {"name": os.path.basename(path.rstrip('/')), "subdirectories": []}

    try:
        print(f"Scanning: {path}")  # Debug print
        file_list = conn.listPath(share_name, path)
        for file_info in file_list:
            if file_info.filename in ['.', '..']:
                continue
            
            full_path = os.path.join(path, file_info.filename).replace("\\", "/")
            if file_info.isDirectory:
                folder_structure["subdirectories"].append(scan_directory_smb(conn, share_name, full_path))
    except Exception as e:
        print(f"Error scanning {path}: {str(e)}")

    return folder_structure

if __name__ == "__main__":
    server_name = "ds.umcutrecht.nl"
    share_name = "Home"
    starting_directory = "JC/Datamanagement/Projecten/A new project"  # Starting directory within the share
    # Prompt for username and password
    username = input("Enter your username: ")
    password = getpass.getpass("Enter your password: ")

    try:
        conn = SMBConnection(username, password, "python-client", server_name, domain="DS", use_ntlm_v2=True, is_direct_tcp=True)
        assert conn.connect(server_name, 445)

        folder_structure = scan_directory_smb(conn, share_name, starting_directory)
        with open("scanned_folder_structure.json", "w") as f:
            json.dump({"root": os.path.basename(starting_directory.rstrip('/')), "directories": folder_structure["subdirectories"]}, f, indent=4)

        print(f"Folder structure saved to scanned_folder_structure.json")
    except ConnectionResetError as e:
        print(f"Connection error: {str(e)}")
    except Exception as e:
        print(f"An error occurred: {str(e)}")
    finally:
        if conn:
            conn.close()
