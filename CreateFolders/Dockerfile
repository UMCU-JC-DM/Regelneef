# Use an official Python runtime as a parent image
FROM python:3.8-slim

# Set the working directory in the container
WORKDIR /app

# Copy the current directory contents into the container at /app
COPY . /app

# Make sure the create_folders.py script has the correct permissions
RUN chmod +x /app/create_folders.py

# Add an entrypoint script
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

# Run entrypoint.sh when the container launches
ENTRYPOINT ["/entrypoint.sh"]
